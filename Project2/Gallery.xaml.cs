using Microsoft.Win32;
/*using Project2.classes;*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
/*using WinForms = System.Windows.Forms;
using System.Windows.Forms;*/
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
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
        
        private void btnGallery_ClickDeleteObject(object sender, RoutedEventArgs e)
        {
            var index = lstGallery.SelectedIndex;
            if (lstGallery.SelectedIndex >= 0)
            {
                string temppath = (galleryIconlst[index].imgPath);
                System.Diagnostics.Debug.WriteLine("the object is located at: "+galleryIconlst[index].imgPath);
                File.SetAttributes(temppath, FileAttributes.Normal); //makes file not read-only permission.                
                galleryIconlst.RemoveAt(index);
                CurrentConfig.IconList.RemoveAt(index);
                selectPrevobject();
                System.Diagnostics.Debug.WriteLine(File.GetAttributes(temppath).ToString());
                CurrentConfig.temppath=temppath;

              /* File.Delete(temppath);*/

            }
        }
        private void btnGallery_ClickDeleteFile(object sender, RoutedEventArgs e)
        {
            var index = lstGallery.SelectedIndex;
            if (lstGallery.SelectedIndex >= 0)
            {
                string temppath = CurrentConfig.temppath;
                File.SetAttributes(temppath, FileAttributes.Normal); //makes file not read-only permission.                
                System.Diagnostics.Debug.WriteLine("will now delete "+ temppath);
                System.Diagnostics.Debug.WriteLine(temppath);


               /*  File.Delete(temppath);*/

            }
        }


        void selectPrevobject()
        {
            if (lstGallery.SelectedIndex > 0)
            {
                lstGallery.SelectedIndex -= 1;
            }
            else if(galleryIconlst.Count > 0)
            {
                lstGallery.SelectedIndex = 0;
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

                if (Fileverify(fileName, targetDirectory) == 1)
                {
                    ProcessFile(fileName, targetDirectory);
                }
                    
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);

        }

        //here we want to create an object for each file
        public int ProcessFile(string fullFileName, string fileLocation)
        {
            double fileSize = new FileInfo(fullFileName).Length;
       /*     string shortFileName = fullFileName.Replace(fileLocation, "");*/
            string shortFileName = System.IO.Path.GetFileName(fullFileName);
            if (fileIsDuplicate(shortFileName) ==false)
            {
                copyimage(fullFileName, shortFileName, CurrentConfig.saveDestination);
            }            
            galleryIcon tempIcon = new galleryIcon { imgName = shortFileName, imgSize = fileSize, imgPath = (CurrentConfig.saveDestination + shortFileName) };
            galleryIconlst.Add(tempIcon);
            CurrentConfig.saveIcontoList(tempIcon);
            //System.Diagnostics.Debug.WriteLine("Processed file:  " + shortFileName +" size: "+ fileSize);
            SaveIcon(); 
            return galleryIconlst.Count;
            
        }

        public void getappPath()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            //System.Diagnostics.Debug.WriteLine("this is the main app path:  ");
            //System.Diagnostics.Debug.WriteLine(appPath);

        }
        public string getimagePath(string mainpath)
        {
            string imgPath = mainpath + "Images\\";
            //System.Diagnostics.Debug.WriteLine(imgPath);
            return imgPath;
        }
        public void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            uploadFile(sender, e);
        }

        public string uploadFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog theFileDialog = new OpenFileDialog();
            theFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            theFileDialog.InitialDirectory = CurrentConfig.saveDestination;

            if (theFileDialog.ShowDialog() == true)
            {
                string fullFileName = theFileDialog.FileName;
                /*  var b = FileInfo(openFileDialog.FileName);*/



                string shortFileName = System.IO.Path.GetFileName(fullFileName);
                /*  debugrutine(fullFileName, l, t);*/

                // cuts away the name of the file and leaves the path
                string targetfolder = reduceToPath(fullFileName);

                /*  MessageBox.Show("I will now verify you file " + shortFileName.ToString());*/

                if (Fileverify(fullFileName, targetfolder) == 1)//file is an image and does not exist as object
                {
                    ProcessFile(fullFileName, targetfolder);//adds the image object to the observable list "processedimg"
                    return (targetfolder + shortFileName);
                }
                else if (Fileverify(fullFileName, targetfolder) == 2)//file exists as object
                {
                    MessageBox.Show("Your project already contains an icon object with the name" + shortFileName.ToString() +" i have used the existing icon:)");
                    return (targetfolder + shortFileName); //for when called by other pages to set icon
                }
                else if (Fileverify(fullFileName, targetfolder) == 0)
                {
                    System.Diagnostics.Debug.WriteLine(fullFileName + " is not an image file");
                    return "error gallery 180";
                }
                else
                {
                    return "error 181";
                }
                /*  else MessageBox.Show("something went wrong with the file " + fullFileName.ToString() +" and I dont know why.");*/
                /*ProcessDirectory(targetfolder);    */

            }            
            return "error gallery 207";

        }


        bool fileIsDuplicate(string shortfilename)
        {   
            
            if (File.Exists(CurrentConfig.saveDestination + shortfilename))
            {                
                return true;
            }
            else return false;

        }
        public void selecttheuploadedfile(int index)
        {            
            System.Diagnostics.Debug.WriteLine("file found at index = " + index);
            lstGallery.SelectedIndex = index - 1; // without "-1", it works if you upload 2 pictures in a row            
        }
        public void selectlastentry()
        {
          
            lstGallery.SelectedIndex = galleryIconlst.Count - 1; // without "-1", it works if you upload 2 pictures in a row            
        }


        int Fileverify(string fullFileName, string folder)
        {

            double fileSize = new FileInfo(fullFileName).Length;
            string shortFileName = fullFileName.Replace(folder, "");
            if (IsImageFile(fullFileName))
            {
                int i=0;
                foreach (galleryIcon Icon in galleryIconlst)
                {
                    i++;
                    if (Icon.imgName == shortFileName)
                    {
                        
                        selecttheuploadedfile(i);
                        return 2;// is image file, already exist as object
                    }                    
                }
                return 1; // is image file, does not exist as object
            }
            else
                return 0;// is NOT image file
        }
        bool IsImageFile(string filename)
        {
            if (filename.EndsWith(".jpeg") || filename.EndsWith(".png"))
                return true;
            else
                MessageBox.Show("the file " + filename.ToString() + " is not a .png or .jpg");
            return false;
        }
        private void btnOpenFolder_Click(object sender, System.EventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.InitialDirectory = @"E:\onM_Doc\Programmering\P3 - CMSE\Project2\Images"; // Use current value for initial dir
            dialog.Title = "Select a Directory"; // instead of default "Save As"
            dialog.Filter = "Directory|*.this.directory"; // Prevents displaying files
            dialog.FileName = "select"; // Filename will then be "select.this.directory"
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                // Remove fake filename from resulting path
                path = path.Replace("\\select.this.directory", "");
                path = path.Replace(".this.directory", "");
                // If user has changed the filename, create the new directory
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                // Our final value is in path
                ProcessDirectory(path);
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


        void copyimage(string imgFullPath, string imgName, string destination)
        {
     
            File.Copy(imgFullPath, (destination + imgName), true);// true = Will overwrite if the destination file already exists.
            File.SetAttributes((destination + imgName), FileAttributes.Normal);
            System.Diagnostics.Debug.WriteLine("Copy complete !");
        }

        private void saveConfig_Click(object sender, RoutedEventArgs e)
        {
                        int index = lstGallery.SelectedIndex;    
                    /*System.Diagnostics.Debug.WriteLine("tried to save index " + index);
                        System.Diagnostics.Debug.WriteLine("tried to save file " + galleryIconlst[index].imgPath);
                        foreach (galleryIcon imgName in galleryIconlst) 
                        {
                            CurrentConfig.IconList[item] = galleryIconlst[index];
                            System.Diagnostics.Debug.WriteLine("tried to save index " + index);
                            System.Diagnostics.Debug.WriteLine("tried to save file " + galleryIconlst[index].imgPath);
                        }*/

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
