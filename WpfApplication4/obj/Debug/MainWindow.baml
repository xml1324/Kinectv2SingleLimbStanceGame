   M S B A M L   `   `   `  ����J  FWpfApplication4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication4WpfApplication4  5   	        WpfApplication4.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication4     5   	     � Title$  KINECT Balance Beam Game��5   	   $	��700��6*   $
��1010��67   .��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ 
StartTitle��5      $$��KINECT Single Limb Stance Game��6(   $��0��6N   $��2��6[   $��Center=�6o   $��Bottom=�6�   $��50(�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ BalanceBeam��5      $��Resources/balancebeam.jpg��6%   $��1��6H   $��0��6U   $��3��6e   �� 5   
   -   5        $ 
Modeselect��5      $��Mode Select��6(   $��1��6;   $��1��6H   $��Center=�6X   $��Bottom=�6u   $��30(�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ StartButton��5      $��2��6&   $��1��63   $	��250��6C   $��75��6O   $��Center=�6[   $��	Test Mode��6x   $��30(�6�   6�   �� 5   
   -   5        $ StartButton2��5      $��3��6'   $��1��64   $	��250��6D   $��75��6P   $��Center=�6\   $	��Top=�6y   $��Exercise Mode��6�   $��30(�6�   6�   �� 5   
   -   5        $
 Logo��5      $��Resources/HFE_logo.jpg��6   $��4��6>   $��2��6K   $��Center=�6_   $��Center=�6|   $	��300��6�   $��	0,0,12,22q�6�   �� 5   
   -   5        $ 	Developer��5      $+��%Developed by Woojoo Kim, Jihyeok Jang��6'   $��4��6T   $��2��6a   $��Center=�6u   $��Center=�6�   $��14(�6�   $��352,88,352,24q�6�   5      5      