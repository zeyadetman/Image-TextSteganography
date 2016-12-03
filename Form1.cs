using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Image_Viewer
{

    public partial class Form1 : Form
    {
        //public static string GetExtension(string path);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        string imagetype,imagepath;                //store the type of file to make the operation on it
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "BMP File|*.bmp|TEXT File|*.txt";
            
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) // determine the type of file
            {
                imagepath = ofd.FileName.ToString();              //show the path on textbox
                textBox1.Text = imagepath;           
                if (Path.GetExtension(ofd.FileName) == ".bmp")   // if file -> bmp ,then imagetype -> bmp ,else imagetype ->text 
                {
                    imagetype = "bmp";
                }
                else
                {
                    imagetype = "text";
                }
                
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        string file2path;               // file2 path based on type of file1
        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dest = new OpenFileDialog();
            if (imagetype == "bmp")
            {
                dest.Filter = "TEXT File|*.txt";
                if (dest.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {    
                }
            }
            else
            {
                dest.Filter = "BMP File|*.bmp";
                if (dest.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {   
                }
            }

            file2path = dest.FileName.ToString();              //show the path of destination file on textbox2
            textBox2.Text = file2path;      
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        Bitmap bmpimage;
        int iheight = 0, iwidth = 0;   //to store height and width of the image
        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(file2path);
            if (imagetype == "bmp")
            {
                bmpimage = new Bitmap(imagepath, true);
                iwidth = bmpimage.Width; iheight = bmpimage.Height;
                
                file.WriteLine(iwidth);
                file.WriteLine(iheight);
                for (int i = 0; i < iwidth; i++)
                {
                    for (int j = 0; j < iheight; j++)
                    {
                        Color pixelColor = bmpimage.GetPixel(i, j);
                        Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                        file.WriteLine(pixelColor.R);
                        file.WriteLine(pixelColor.G);
                        file.WriteLine(pixelColor.B);
                    }
                }
                
            }
            else
            {
            }
            file.Close();
        }
    }
}
