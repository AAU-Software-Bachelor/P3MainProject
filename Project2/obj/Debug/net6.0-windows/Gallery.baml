   M S B A M L   `   `   `  ����C  ?Project2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Project2Project2  5          Project2.GalleryWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:Project2     5        N� Title$  GalleryWindow��5      $	��760��6   $
��1543��6*   .��5
      � 
�������$��	0,0,10,10q�6   .+�5   
   �� $��Gallery��6   $
��Left=�6"   $��	10,10,0,0q�6=   $	��Top=�6P   $��63��6h   $	��165��6t   $
��Bold"�6�   $��38(�6�   �� 5   
   $��
CMS Editor��6   $
��Left=�6%   $��
186,37,0,0q�6@   $	��Top=�6T   $��46��6l   $	��160��6x   $
��Bold"�6�   $��15(�6�   �� 5   
   -   5         ��Name$ GalleryBacktoMainMenu��5      $��0��6   $
��Left=�6<   $��
565,27,0,0q�6W   $	��Top=�6k   $
��Bold"�6�   $��20(�6�   $	��136��6�   $��46��6�   .��5      ��  �� Orientation$ Vertical=�6   .+�5      �� 
����ح� �� Padding$ 	10,5,10,5q�60   ._�5      �� $��Center=�6    �� 6>   $�	Main Menu��6B   6V   5      5      5      �� 5   
   -   5         ��Name$ 
lstGallery��5      $��17,99,1228,286q�6'   $��0��6?    �� View 5      �� 5      -   5        grdNames��.P�5      �� $
��Name��6%    �� Width$	 150��63    �� DisplayMemberBinding 6?   �� *	imgName+5      5      �� 5   
   -   5         ��Name$ btnGalleryAdd��5   o   $
��Left=�6   $	��Top=�6,   $��93��6D   $��32��6O   $��
17,464,0,0q�6[   $��Upload Picture��6�   
����ح�6�   �� 5   
   -   5        $ btnGalleryDelete��5   p   $
��Left=�6   $	��Top=�6,   $��98��6D   $��32��6O   $��158,522,0,0q�6[   $��DELETE File��6�   
��� E��6�   �� 5   
   -   5        $��Upload Folder��5      $��17,514,1404,196q�6E   � 5   
   
�������$��334,99,32,26q�6%   .+�5      �� $��21,12,483,138q�6T   $	��350��6k   $	��350��6w   ��6   �� *SelectedItem.imgPath+	 �� ElementName$	 
lstGallery��6w   5       �� 5!   
   -   5        $ btnGalleryDelete_Copy��5!   q   $
��Left=�6   $	��Top=�6,   $	��104��6D   $��40��6P   $��158,464,0,0q�6\   $��Delete Object��6�   
��� E��6�   5#      5$      