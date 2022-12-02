using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;

using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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

using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;
using Image = System.Windows.Controls.Image;

namespace Project2
{
    public partial class GalleryWindow : Page
    {
        public GalleryWindow(config currentConfig)
        {
            CurrentConfig = currentConfig;
            InitializeComponent();
            galleryIconlst = new ObservableCollection<galleryIcon>();
            foreach (galleryIcon imgName in CurrentConfig.IconList) //adds all icons to ObservableCollection
            {
                galleryIconlst.Add(imgName);
            }

            lstGallery.ItemsSource = galleryIconlst;
            lstGallery.SelectedIndex = 0;

        }
        public config CurrentConfig { get; set; }

        public ObservableCollection<galleryIcon> galleryIconlst;

        private void btnGallery_ClickDelete(object sender, RoutedEventArgs e)
        {
            var index = lstGallery.SelectedIndex;s
            if (lstGallery.SelectedIndex >= 0)
            {
                galleryIconlst.RemoveAt(index);
                CurrentConfig.IconList.RemoveAt(index);
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
        public void ProcessFile(string fullFileName, string fileLocation)
        {
            double fileSize = new FileInfo(fullFileName).Length;
            string shortFileName = fullFileName.Replace(fileLocation, "");
            galleryIcon tempIcon = new galleryIcon { imgName = shortFileName, imgSize = fileSize, imgPath = fullFileName };
            galleryIconlst.Add(tempIcon);
            CurrentConfig.saveIcontoList(tempIcon);
            Console.WriteLine("Processed file '{0}'.", fullFileName);
            SaveIcon();
        }

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
            OpenFileDialog theFileDialog = new OpenFileDialog();
            theFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            theFileDialog.InitialDirectory = @"E:\onM_Doc\Programmering\P3 - CMSE\Project2\Images";

            if (theFileDialog.ShowDialog() == true)
            {
                string fullFileName = theFileDialog.FileName;
                /*  var b = FileInfo(openFileDialog.FileName);*/
                double l = new FileInfo(fullFileName).Length;
                bool t = Fileverify(fullFileName);
                bool k = fullFileName.EndsWith(".png");
                debugrutine(fullFileName, l, t);

                // cuts away the name of the file and leaves the path
                string targetfolder = reduceToPath(fullFileName);

                //adds the image object to the observable list "processedimg"
                if (t)
                {
                    ProcessFile(fullFileName, targetfolder);
                }

                else throw new Exception("file is not a .JPG or .PNG!!!!");
                /*ProcessDirectory(targetfolder);    */
                selecttheuploadedfile();
                
             
            }
        }
        void selecttheuploadedfile()
        {
            int lastentry = galleryIconlst.Count;
            System.Diagnostics.Debug.WriteLine("Last entry = " + lastentry);
            lstGallery.SelectedIndex = lastentry - 1; // without "-1", it works if you upload 2 pictures in a row

        }
        bool Fileverify(string filename)
        {
            if (filename.EndsWith(".jpeg") || filename.EndsWith(".png"))
                return true;
            else
                return false;
        }
        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.InitialDirectory = @"E:\onM_Doc\Programmering\P3 - CMSE\Project2\Images";
            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Folder Selection.";


            if (folderBrowser.ShowDialog() == true)
            {
                string folderPath = reduceToPath(folderBrowser.FileName);
                // ...
            }

        }
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
            System.Diagnostics.Debug.WriteLine(a);
            System.Diagnostics.Debug.WriteLine(l);
            System.Diagnostics.Debug.WriteLine(t);

        }



        ///////////////////////////////////////////////////////
        /// /* if have no IDEA what this is*/

        ///////////////////////////////////////////////////////
        private void saveConfig_Click(object sender, RoutedEventArgs e)
        {


            int index = lstGallery.SelectedIndex;
            /*            System.Diagnostics.Debug.WriteLine("tried to save index " + index);
                        System.Diagnostics.Debug.WriteLine("tried to save file " + galleryIconlst[index].imgPath);
                        foreach (galleryIcon imgName in galleryIconlst) 
                        {
                            CurrentConfig.IconList[item] = galleryIconlst[index];
                            System.Diagnostics.Debug.WriteLine("tried to save index " + index);
                            System.Diagnostics.Debug.WriteLine("tried to save file " + galleryIconlst[index].imgPath);
                        }
                        */


        }

        private void SaveIcon(int index = -1)
        {
            int SelIndex = lstGallery.SelectedIndex;  //saves selected index so it is not lost

            string IconName = "";
            if (index == -1)    //is true when funtion is called via a button
            {
                if (lstGallery.SelectedIndex >= 0)
                {
                    IconName = CurrentConfig.IconList[lstGallery.SelectedIndex].imgName;    //uses the selected index to find the wanted imgName
                    index = CurrentConfig.IconList.FindIndex(i => string.Equals(i.imgName, IconName));
                }
            }
            else
            {
                IconName = CurrentConfig.IconList[index].imgName; //uses the given index to find the wanted UID
            }
            if (IconName != "")
            {
                galleryIcon currentIcon = CurrentConfig.getIcon(IconName);

            /*    currentIcon.DeleteImage();*/
          
          
                currentIcon.imgPath = galleryIconlst[index].imgPath;
                currentIcon.imgName = galleryIconlst[index].imgName;
                currentIcon.imgSize = galleryIconlst[index].imgSize;
          

                CurrentConfig.IconList[index] = currentIcon;
                galleryIconlst.Clear();    // clears the list
                foreach (galleryIcon imgName in CurrentConfig.IconList)  //rewrites the list.
                {
                   galleryIconlst.Add(imgName);
                    System.Diagnostics.Debug.WriteLine("added an icon from config to galleryIconlist");
                }


                CurrentConfig.TestWriteToJson("testConfiggallery.json");
                lstGallery.SelectedIndex = SelIndex;  //applies saved race selection
            }
        }

        private void GalleryMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SaveIcon();
            MainWindow mainWindow = new MainWindow(CurrentConfig);
            Application.Current.MainWindow.Content = mainWindow;
        }
    }

}
