using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing.Graphics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Image_Viewer
{

    public partial class ImageViewer : Form
    {
        //public static string GetExtension(string path);
        public ImageViewer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string imagetype,imagepath;                //store the type of file to make the operation on it
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
                file2path = dest.FileName.ToString();              //show the path of destination file on textbox2
                textBox2.Text = file2path;
                
            }
            else
            {
                MessageBox.Show("Your Image will be saved Automatically in name 'Image' ");

                //dest.Filter = "BMP File|*.bmp";
                //if (dest.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{   
                //}
                file2path = "";
            }

            }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        public Bitmap bmpimage,picturepath;
        int iheight = 0, iwidth = 0;   //to store height and width of the image
        public int lines;
        public string curline;
        public int curlin, wid, hei;

        private void button2_Click(object sender, EventArgs e)
        {
            if (imagetype == "bmp")             //covert from .bmp to .txt file
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(file2path);
                picturepath = bmpimage;
                bmpimage = new Bitmap(imagepath, true);
                iwidth = bmpimage.Width; iheight = bmpimage.Height;
                
                file.WriteLine(iwidth);
                file.WriteLine(iheight);
                for (int i = 0; i < bmpimage.Height; i++)
                {
                    for (int j = 0; j < bmpimage.Width; j++)
                    {
                        Color pixelColor = bmpimage.GetPixel(j, i);
                        Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                        file.WriteLine(pixelColor.R);
                        file.WriteLine(pixelColor.G);
                        file.WriteLine(pixelColor.B);
                    }
                }

                pictureBox1.Image = Image.FromFile(imagepath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                file.Close();   
            }
            else                   //convert from .txt to .bmp file
            {

                using (System.IO.StreamReader file1 = new System.IO.StreamReader(imagepath))
                {

                    wid = int.Parse(file1.ReadLine());
                    hei = int.Parse(file1.ReadLine());
                    Bitmap img = new Bitmap(wid, hei);
                    int r, g, b, k = 2;
                    for (int i = 0; i < hei; i++)
                    {
                        for (int j = 0; j < wid; j++)
                        {
                            r = int.Parse(file1.ReadLine());
                            g = int.Parse(file1.ReadLine());
                            b = int.Parse(file1.ReadLine());
                            img.SetPixel(j, i, Color.FromArgb(r, g, b));

                        }
                    }
                    pictureBox1.Image = img;
                    pictureBox1.Image.Save("Image.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
