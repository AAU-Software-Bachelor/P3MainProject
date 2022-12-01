using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static Project2.RaceWindow;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;
using Image = System.Windows.Controls.Image;

namespace Project2
{
    /// <summary>
    /// Interaction logic for Race.xaml
    /// </summary>
    /// 

    public partial class GalleryWindow : Page
    {
        public GalleryWindow(config currentConfig)
        {
            CurrentConfig = currentConfig;
            InitializeComponent();
            galleryIconlst = new ObservableCollection<galleryIcon>()
            {
            new galleryIcon(){ imgName = "Newimg", imgPath= "dont know where" , imgSize=2}
            };
            lstGallery.ItemsSource = galleryIconlst;

        }
        public config CurrentConfig { get; set; }


        /* public ObservableCollection<galleryImage> ProcessedImg = new ObservableCollection<galleryImage>();*/
        public ObservableCollection<galleryIcon> galleryIconlst;

        private void btnGallery_ClickAdd(object sender, RoutedEventArgs e)
        {
            /*int i = galleryIconslst.Count + 1;*/
            galleryIconlst.Add(new galleryIcon() { imgName = "Newimg", imgPath= "dont know where" , imgSize=2});
        }

        private void btnGallery_ClickDelete(object sender, RoutedEventArgs e)
        {
            var index = lstGallery.SelectedIndex;
            if (lstGallery.SelectedIndex >= 1)
            {
                galleryIconlst.RemoveAt(index);
            }
        }



        //Adds all jpeg files in the target directory
        public void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            Console.WriteLine("We found these image files in the targeted directory: ");

            foreach (string fileName in fileEntries)
            {

                if (fileName.EndsWith(".png") || fileName.EndsWith(".jpeg"))
                    ProcessFile(fileName, targetDirectory);
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);

        }

        //here we want to create an object for each file
        public void ProcessFile(string fileName, string fileLocation)
        {
            double fileSize = new FileInfo(fileName).Length;
            fileName = fileName.Replace(fileLocation, "");
            galleryIconlst.Add(new galleryIcon { imgName = fileName, imgSize = fileSize, imgPath = fileLocation });
            Console.WriteLine("Processed file '{0}'.", fileName);
        }


        // the create on click version
        private void ShowList_Click(object sender, RoutedEventArgs e)
        {
            ShowGalleryList();


        }

        public void ShowGalleryList()
        {

        }

        // can be called from other commands than on click :P

        /*private void deleteimg_Click(object sender, RoutedEventArgs e)
            {

            }*/

        public void getappPath()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Debug.WriteLine("this is the main app path:  ");
            System.Diagnostics.Debug.WriteLine(appPath);

        }
        public string getimagePath(string mainpath)
        {
            string imgPath = mainpath + "Images\\";
            System.Diagnostics.Debug.WriteLine(imgPath);

            return imgPath;
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"E:\onM_Doc\Programmering";
            
            if (openFileDialog.ShowDialog() == true)
                {
                 string a = openFileDialog.FileName;
                 /*  var b = FileInfo(openFileDialog.FileName);*/
                 double l = new FileInfo(a).Length;
                 bool t = a.EndsWith(".jpeg");
                 debugrutine(a, l, t);

                 // cuts away the name of the file and leaves the path
                 string targetfolder = reduceToPath(a);

                 //adds the image object to the observable list "processedimg"
                 if (t)
                     ProcessFile(a, targetfolder);
                 else throw new Exception("file is not a jpg!!!!");
                 /*ProcessDirectory(targetfolder);    */

                 ShowGalleryList();

                }
            }



            /*    private void createGalleryWindow_Click()
                {
                    throw new NotImplementedException();
                }*/

            private string reduceToPath(string filename)
            {
                string targetfolder = filename;
                while (!targetfolder.EndsWith("\\"))
                {
                    targetfolder = targetfolder.Remove(targetfolder.Length - 1, 1);
                    Console.WriteLine(targetfolder);
                }
                return targetfolder;
            }
            void debugrutine(string a, double l, bool t)
            {
                /*      btnOpenFile.Background = new ImageBrush(new BitmapImage(new Uri(a)));*/
                Console.WriteLine(a);
                Console.WriteLine(l);
                Console.WriteLine(t);
                /*Console.WriteLine(c);*/
                /*txtEditor.Text = File.ReadAllText(openFileDialog.FileName);*/
                /*                var byt = File.ReadAllBytes(a);*/
                /*                var filetext = Encoding.UTF8.GetString(byt);*/
                /*      foreach (var item in byt)
                      {
                          Console.WriteLine(item);
                      }*/


                /*                Console.WriteLine(filetext);*/
            }


        ///////////////////////////////////////////////////////
        /// /* if have no IDEA what this is*/
        
        ///////////////////////////////////////////////////////
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GalleryMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            MainWindow mainWindow = new MainWindow(CurrentConfig);
            Application.Current.MainWindow.Content = mainWindow;
        }
    }

}
