﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.VisualBasic;

namespace Project2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        config currentConfig = new config();
        public MainWindow()
        {
         
            InitializeComponent();
            

        }


        private void RaceMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            getappPath();
            Race race = new Race(currentConfig); //we need to talk about naming stuff!!
            this.Content = race;
           
        }

        private void GalleryMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GalleryWindow gallery = new GalleryWindow(currentConfig);
            this.Content = gallery;
        }

        private void ReligionMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Religion religion = new Religion(currentConfig);
            this.Content = religion;
        }

        private void ResourcesMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Resources resources = new Resources(currentConfig);
            this.Content = resources;
        }

     

        /* private void AddMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
         {
             string input = "Empty";
             input = Interaction.InputBox("Name:", "Name: (REQUIRED?)", "Default", x_coordinate, y_coordinate);

             string NewLabel = LabelGenerator(input);
             GridTextBlock.Children.Add(NewLabel);

             TextBlock myTextBlock = new TextBlock();
             myTextBlock.FontSize = "14";
             myTextBlock.FontWeight = Fontweights.Bold;
             myTextBlock.Text = input;

             input.Children.Add(myTextBlock);
         }

         private void LabelGenerator(string input)
         {
             Label newlabel = new Label();
             newlabel.HorizontalAlignment = "Left";
             newlabel.VerticalAlignment = "Top";
             newlabel.FontSize = "14";
             newlabel.FontWeight = Fontweights.Bold;
             int i = 0;
             int f = 0 % 2;
             while(true);
                 if(!string.IsNullOrEmpty(GridTextBlock.Grid.Column.i))
                 {
                     i++;
                 }
                 if(!string.IsNullOrEmpty(GridTextBlock.Grid.Column.f){
                     f++;
                 }
             if (string.IsNullOrEmpty(GridTextBlock.Grid.Column.f && string.IsNullOrEmpty(GridTextBlock.Grid.Column.i)))
                 {
                     break;
                 }
             newlabel.Column = i;
             newlabel.Row = f;
             newlabel.x:name = input;
             return newlabel;

         }*/

    }
}
