using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MultiImage
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "MultiImage v1.2";
            Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }

                if (filePath.ToString() != "")
                {
                    textBox1.Text = filePath.ToString();
                    pictureBox1.Image = Image.FromFile(textBox1.Text);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please fill all fields!");
            }
            else
            {
                char[] delims = new[] { '\r', '\n', ' ', ',', ';' };
                string _finalPath;
                List<string> skus = new List<string>();
                skus = textBox3.Text.Split(delims, StringSplitOptions.RemoveEmptyEntries).ToList();
                //skus.RemoveRange(0, 2);
                foreach (string sku in skus)
                {
                    if (Directory.Exists(textBox2.Text.ToString()))
                    {
                        if (checkBox1.Checked)
                        {
                            _finalPath = System.IO.Path.Combine(textBox2.Text.ToString(), sku + textBox4.Text.ToString() + ".png");
                            System.IO.File.Copy(textBox1.Text, _finalPath, true);
                        }
                        else
                        {
                            _finalPath = System.IO.Path.Combine(textBox2.Text.ToString(), sku + ".png");
                            System.IO.File.Copy(textBox1.Text, _finalPath, true);
                        }
                    }
                }
                MessageBox.Show(skus.Count + " copies were done!");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
