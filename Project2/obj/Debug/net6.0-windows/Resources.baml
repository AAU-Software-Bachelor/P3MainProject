   M S B A M L   `   `   `  ����C  ?Project2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Project2Project2  5          Project2.ResourcePage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:Project2     5        N� Title$  Resourcewindow��5	      $	��760��6   $
��1543��6(   .��5      � 
�������$��	0,0,10,10q�6   .+�5   
   �� -   5         ��Name$ ResourcesBacktoMainMenu��5      $��0��6   $
��Left=�6>   $��
565,27,0,0q�6Y   $	��Top=�6m   $
��Bold"�6�   $��20(�6�   $	��136��6�   $��46��6�   .��5      ��  �� Orientation$ Vertical=�6   .+�5      �� 
����ح� �� Padding$ 	10,5,10,5q�60   ._�5      �� $��Center=�6    �� 6>   $�	Main Menu��6B   6V   5      5      5      �� 5   
   -   5         ��Name$ lstResources��5      $��18,99,1319,286q�6)   $��0��6A    �� View 5      �� 5      -   5        grdNames��.P�5      �� $
��Name��6%    �� Width$	 100��63    �� DisplayMemberBinding 6?   �� *Name+�� 5      $
��Type��6%   $ 80��63    6>   �� *type+5      5      �� 5   
   -   5         ��Name$ btnResourceAdd��5   o   $
��Left=�6   $	��Top=�6,   $��35��6D   $��21��6O   $��
17,469,0,0q�6[   $	��ADD��6�   
����ح�6�   �� 5   
   -   5        $ btnResourceDelete��5   o   $
��Left=�6   $	��Top=�6,   $��52��6D   $��21��6O   $��
60,469,0,0q�6[   $��DELETE��6�   
��� E��6�   � 5   
   
�������$��224,99,32,26q�6&   .+�5      �� 
���   �$��1q�6)   $��20,10,1016,430q�6=   6W   �� 5!      -   5        	 ��Name$	 ChosenImage��5!      $��/Images/Gallery.png��6)   $
��Fill=�6F   
 J� RenderTransformOrigin$
 0.5,0.5&�6U   $��23,17,1020,457q�6u   �� 5"      -   5        $��Change Icon��5"      $��23,165,1020,440q�6E   �� 5%      -   5         ��Name$ nameBox��5%      $
��Left=�6'   $��
25,227,0,0q�6B    �� TextWrapping$
 Wrap=�6V   $��resource name��6j   $	��Top=�6   $	��120��6�   $��0,0,0,2q�6�   
���   �6�   �� 5&      -	   5        $ descBox��5&      $
��Left=�6'   $��
25,245,0,0q�6B   $
 Wrap=�6V   $��resource description��6j   $	��Top=�6�   $	��365��6�   $	��178��6�   �� 5'      -
   5        
���� �$
��Save��5'   .   $
��Left=�6=   $��
31,428,0,0q�6X   $	��Top=�6l   $��38��6�   $��88��6�   6�   � 5(      -   5         �Name$ GridRadioButtons��5(   =   $��434,62,518,417q�6   
������� � ColumnDefinitions 5)      �� 5*      $��85 �6'   �� 5+      $��85 �6'    � RowDefinitions 5-      �� 5.      $��25 �6$   �� 5/      $��25 �6$   �� 50      $��25 �6$   .+�52      �� $��0��6   $��0��6)   $��Resource Type��69   	� 53      -   5         	�Name$ HPSTATBARRadioButton��53   ;   $��1��6   $��0��6+   
 � Tag$ 0��6Y   $��HP/Stat Bar��6a    	� 	GroupName$! ResourceTypeGrpRadioButtons��6w   	� 54      -   5        $ XPVARIANTRadioButton��54   ;   $��1��6   $��1��6+   $ 1��6Y   $��
XP Variant��6a   $! ResourceTypeGrpRadioButtons��6v   	� 55      -   5        $ CURRENCYRadioButton��55   ;   $��2��6   $��0��6+   $ 2��6X   $��Currency��6`   $! ResourceTypeGrpRadioButtons��6s   	� 56      -   5        $ MISCRadioButton��56   ;   $��2��6   $��1��6+   $ 3��6T   $��Misc.��6\   $! ResourceTypeGrpRadioButtons��6l   57      58      �� 59   
   $��Resource Editor��6   $
��Left=�6*   $��	10,10,0,0q�6E   $	��Top=�6X   $��63��6p   $	��295��6|   $
��Bold"�6�   $��38(�6�   �� 5:   
   $��
CMS Editor��6   $
��Left=�6%   $��
296,37,0,0q�6@   $	��Top=�6T   $��46��6l   $	��206��6x   $
��Bold"�6�   $��15(�6�   5;      5<      