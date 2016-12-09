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

namespace ImageViewer_Desktop_App
{
    public partial class ImageViewerApp : Form
    {
        public ImageViewerApp()
        {
            InitializeComponent();
        }

        ///Variables
        public string usrimgpath;    //store the user image path
        public string usrtextpath;   //store the user text path
        public string savtxtfile;    //store the location of saved text file
        public string savimgfile;    //store the location of saved image file
        public Bitmap imageco;   //non-important variable - used outside to be public 
        public Bitmap loadimg;   //non-important variable - used outside to be public

        /// <summary>
        /// covert from text to image
        /// </summary>
        /// <returns>Bitmap image</returns>
        public Bitmap converttoimage(Bitmap img,string urtxtpath)
        {
            using (StreamReader file = new StreamReader(urtxtpath))
            {
                int wid = int.Parse(file.ReadLine());
                int hei = int.Parse(file.ReadLine());
                img = new Bitmap(wid, hei);
                int r, g, b;
                for (int i = 0; i < hei; i++)
                {
                    for (int j = 0; j < wid; j++)
                    {
                        r = int.Parse(file.ReadLine());
                        g = int.Parse(file.ReadLine());
                        b = int.Parse(file.ReadLine());
                        img.SetPixel(j, i, Color.FromArgb(r, g, b));

                    }
                }

            }
            return img;
        }


        /// <summary>
        /// Open The image and display it on App
        /// </summary>
        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog usrimg = new OpenFileDialog();
            usrimg.Filter = "Image | *.jpg; *.jpeg; *.jpe; *.jfif; *.bmp; *.png";
            if (usrimg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                usrimgpath = usrimg.FileName.ToString();              //Open the user image
            }
            if (usrimgpath != null)            //check if the image opened or not to display it
            {
                pictureBox1.Image = Image.FromFile(usrimgpath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        
        /// <summary>
        /// Open The text, and open the image on App
        /// </summary>
        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog usrtext = new OpenFileDialog();
            usrtext.Filter = "Text | *.txt";
            if (usrtext.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                usrtextpath = usrtext.FileName.ToString();              //Load the user text file
            }
            if (usrtextpath != null)
            {
                Bitmap im;
                im = converttoimage(imageco, usrtextpath);
                pictureBox1.Image = im;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }


        /// <summary>
        /// determine saved text location
        /// </summary>
        private void asTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usrimgpath != null)
            {
            SaveFileDialog saveastext = new SaveFileDialog();
            saveastext.Filter = "Text|*.txt";
            saveastext.Title = "Save it as text";
            saveastext.ShowDialog();
            savtxtfile = saveastext.FileName.ToString();
            if (saveastext.FileName != null)
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveastext.OpenFile();
                fs.Close();   
            }
            if (savtxtfile != null)
            {
                using (StreamWriter file = new StreamWriter(savtxtfile, true))
                {
                    Bitmap loadimg;
                    loadimg = new Bitmap(usrimgpath, true);
                    file.WriteLine(loadimg.Width);
                    file.WriteLine(loadimg.Height);
                    for (int i = 0; i < loadimg.Height; i++)
                    {
                        for (int j = 0; j < loadimg.Width; j++)
                        {
                            Color pixelColor = loadimg.GetPixel(j, i);
                            Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                            file.WriteLine(pixelColor.R);
                            file.WriteLine(pixelColor.G);
                            file.WriteLine(pixelColor.B);
                        }
                    }
                    file.Close();
                }
            }
            
                
            }
            else
            {
                MessageBox.Show("Please load an image first.");    
            }

        }


        /// <summary>
        /// determine saved image location
        /// </summary>
        private void asImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usrtextpath != null)
            {
                SaveFileDialog saveasimg = new SaveFileDialog();
                saveasimg.Filter = "Image |*.PNG";
                saveasimg.Title = "Save it as image";
                saveasimg.ShowDialog();
                if (saveasimg.FileName != null)
                {
                    System.IO.FileStream fs = (System.IO.FileStream)saveasimg.OpenFile();
                    fs.Close();
                }
                Bitmap reloadimg;
                reloadimg = converttoimage(loadimg, usrtextpath);
                pictureBox1.Image = reloadimg;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                savimgfile = saveasimg.FileName.ToString();
                pictureBox1.Image.Save(savimgfile, System.Drawing.Imaging.ImageFormat.Png);

            }
            else
            {
                SaveFileDialog saveasimg = new SaveFileDialog();
                saveasimg.Filter = "Image |*.PNG";
                saveasimg.Title = "Save it as image";
                saveasimg.ShowDialog();
                if (saveasimg.FileName != null)
                {
                    File.Copy(usrimgpath, saveasimg.FileName.ToString());
                }
                
            }
            

        }

         


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
         
    }
}
