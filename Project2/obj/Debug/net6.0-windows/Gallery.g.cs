﻿#pragma checksum "..\..\..\Gallery.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BEE6166F7620824243D73A3AF3E3548CB4FB527D"
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
    /// GalleryWindow
    /// </summary>
    public partial class GalleryWindow : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label GalleryBacktoMainMenu;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstGallery;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridView grdNames;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGalleryAdd;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGalleryDelete;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGalleryDelete_Copy;
        
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
            System.Uri resourceLocater = new System.Uri("/Project2;component/gallery.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Gallery.xaml"
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
            this.GalleryBacktoMainMenu = ((System.Windows.Controls.Label)(target));
            
            #line 13 "..\..\..\Gallery.xaml"
            this.GalleryBacktoMainMenu.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.GalleryMainMenu_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lstGallery = ((System.Windows.Controls.ListView)(target));
            return;
            case 3:
            this.grdNames = ((System.Windows.Controls.GridView)(target));
            return;
            case 4:
            this.btnGalleryAdd = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Gallery.xaml"
            this.btnGalleryAdd.Click += new System.Windows.RoutedEventHandler(this.btnOpenFile_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnGalleryDelete = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Gallery.xaml"
            this.btnGalleryDelete.Click += new System.Windows.RoutedEventHandler(this.btnGallery_ClickDeleteFile);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 29 "..\..\..\Gallery.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnOpenFolder_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnGalleryDelete_Copy = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\Gallery.xaml"
            this.btnGalleryDelete_Copy.Click += new System.Windows.RoutedEventHandler(this.btnGallery_ClickDeleteObject);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
