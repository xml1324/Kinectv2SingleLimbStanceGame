   M S B A M L   `   `   `  ����J  FWpfApplication4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication4WpfApplication4  5   	       WpfApplication4.SubWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication4     5   	   -   5          � Title$  KINECT Balance Beam Game��5   	   $	��900��6*   $
��1550��67   .��5
      � .+�5   
   �� -   5         ��Name$ BackgroundImage��5      $��Resources/Interface.jpg��6)    J� Opacity$	 0.8Q�6J   (� 5   
   $��Center=�6   .��5      �� $��UniformToFill=�6;   ��6   �� *SkeletonImageSource+6;   5      �� 5   
   -   5         ��Name$ skeletonCanvas��5      �� 5   
   -   5         ��Name$ StandStatusTextBlock��5      $��Instructions��62   $��60(�6F   $��1053,165,89,404q�6T    �� TextAlignment$ Center=�6m   $	��250��6�   $	��400��6�   $��Agency FB Regular)�6�   �� 5   
   -   5        $ TestMode��5      $��Center=�6&   $
��Wrap=�6C   $��60(�6W   $��Agency FB Bold)�6e   $��	Test Mode��6�   $��50,25,0,727q�6�   �� 5   
   -   5        $ TimeText��5      $
��Left=�6&   $��50,76,0,727q�6A   $
��Wrap=�6V   $
��Time��6j   $��50(�6w   $��Agency FB Regular)�6�   � 5   
   -   5        $��TimeProgress��5      $	��Top=�6*   $	��200��6B   $��10��6N    � Minimum$ 0Q�6Z    � Maximum$ 45Q�6f    � Value$ 0Q�6s   $��258,231,1084,0q�6}   �� 5   
   -   5        $ TimeTextBlock��5      $
��00 s��6+   $��Right=�67   $	��Top=�6S   $
��Bold"�6k   $	��100(�6}   $��Agency FB Bold)�6�   $��0,144,1304,0q�6�   �� 5   
   -	   5        $ BalanceScoreText��5      $
��Left=�6.   $��50,286,0,520q�6I   $
��Wrap=�6_   $��Balance Score��6s   $��50(�6�   $��Agency FB Regular)�6�   �� 5   
   -
   5        $ BSTextBlock��5      $
��00 %��6)   $	��Top=�65   $
��Bold"�6M   $	��100(�6_   $��Agency FB Bold)�6n   $��
85,354,0,0q�6�   �� 5   
   -   5        $ FootLeftTextBlock��5      $��	Left Foot��6/   $
��Left=�6@   $��Bottom=�6[   $��50(�6v   $��	50,0,0,20q�6�   $��Agency FB Regular)�6�   �� 5   
   -   5        $ FootRightTextBlock��5      $��
Right Foot��60   $��Right=�6B   $��Bottom=�6^   $��50(�6y   $��	0,0,50,20q�6�   $��Agency FB Regular)�6�   �� 5   
   -   5        $ BalanceStatusText��5      $
��Left=�6/   $��50,568,0,238q�6J   $
��Wrap=�6`   $��Balance Status��6t   $��50(�6�   $��Agency FB Regular)�6�   � 5    
   -   5        	 �Name$	 StatusProgress��5       $	��Top=�6.   $	��300��6F   $��30��6R   $ 0Q�6^   $ 1Q�6j   $ 1Q�6v   $��90,655,1152,0q�6�   
�������
���   �6�   5#      5$      