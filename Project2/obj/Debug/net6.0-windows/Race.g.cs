﻿#pragma checksum "..\..\..\Race.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E5D541A0440DAD07F0A0E8A556AE030820663DD1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Project2;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Project2 {
    
    
    /// <summary>
    /// Race
    /// </summary>
    public partial class Race : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Race.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label RaceBacktoMainMenu;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Race.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListForRaces;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Race.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RacesImageComboBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Race.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nameBox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Race.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox playerReqBox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Race.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Race.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListStarterAbilities;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Race.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListStarterResources;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Project2;component/race.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Race.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.RaceBacktoMainMenu = ((System.Windows.Controls.Label)(target));
            
            #line 13 "..\..\..\Race.xaml"
            this.RaceBacktoMainMenu.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.RaceMainMenu_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ListForRaces = ((System.Windows.Controls.ListView)(target));
            return;
            case 3:
            
            #line 23 "..\..\..\Race.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClickAddRaceList);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 24 "..\..\..\Race.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClickDeleteRaceList);
            
            #line default
            #line hidden
            return;
            case 5:
            this.RacesImageComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.nameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.playerReqBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.descBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.ListStarterAbilities = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            
            #line 39 "..\..\..\Race.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 40 "..\..\..\Race.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 41 "..\..\..\Race.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 43 "..\..\..\Race.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClickAddStarterAbilities);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 44 "..\..\..\Race.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClickDeleteStarterAbilities);
            
            #line default
            #line hidden
            return;
            case 15:
            this.ListStarterResources = ((System.Windows.Controls.ListView)(target));
            return;
            case 16:
            
            #line 53 "..\..\..\Race.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 61 "..\..\..\Race.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 69 "..\..\..\Race.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 77 "..\..\..\Race.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClickAddStarterResources);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 78 "..\..\..\Race.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClickDeleteStarterResources);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 81 "..\..\..\Race.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClickSaveRace);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

