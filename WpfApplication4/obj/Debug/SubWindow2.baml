   M S B A M L   `   `   `  ����J  FWpfApplication4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication4WpfApplication4  5   	        WpfApplication4.SubWindow2   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication4     5   	   -   5          � Title$  KINECT Balance Beam Game��5   	   $	��900��6*   $
��1550��67   .��5
      � .+�5   
   �� -   5         ��Name$ BackgroundImage��5      $��Resources/Interface.jpg��6)    J� Opacity$	 0.8Q�6J   (� 5   
   $��Center=�6   .��5      �� $��UniformToFill=�6;   ��6   �� *SkeletonImageSource+6;   5      �� 5   
   -   5         ��Name$ StandStatusTextBlock��5      $��Instructions��62   $��60(�6F   $��1053,165,89,404q�6T    �� TextAlignment$ Center=�6m   $	��250��6�   $	��400��6�   $��Agency FB Regular)�6�   �� 5   
   -   5        $ ExerciseMode��5      $��Center=�6*   $
��Wrap=�6G   $��60(�6[   $��Agency FB Bold)�6i   $��Exercise Mode��6�   $��50,25,0,727q�6�   �� 5   
   -   5        $ Timer��5      $
��Left=�6#   $��50,76,0,727q�6>   $
��Wrap=�6S   $��Timer��6g   $��50(�6u   $��Agency FB Regular)�6�   � 5   
   -   5        $��TimeProgress��5      $	��Top=�6*   $	��100��6B   $��10��6N    � Minimum$ 0Q�6Z    � Maximum$ 10Q�6f    � Value$ 0Q�6s   $��240,231,1202,0q�6}   �� 5   
   -   5        $ TimerTextBlock��5      $
��10 s��6,   $��Right=�68   $	��Top=�6T   $
��Bold"�6l   $	��100(�6~   $��Agency FB Bold)�6�   $��0,144,1322,0q�6�   �� 5   
   -   5        $ Score��5      $
��Left=�6#   $��50,286,0,520q�6>   $
��Wrap=�6T   $��Score��6h   $��50(�6u   $��Agency FB Regular)�6�   �� 5   
   -	   5        $ ScoreTextBlock��5      $��00 pts��6,   $	��Top=�6:   $
��Bold"�6R   $	��100(�6d   $��Agency FB Bold)�6s   $��
85,354,0,0q�6�   �� 5   
   -
   5        $ FootLeftTextBlock��5      $��	Left Foot��6/   $
��Left=�6@   $��Bottom=�6[   $��50(�6v   $��	50,0,0,20q�6�   $��Agency FB Regular)�6�   �� 5   
   -   5        $ FootRightTextBlock��5      $��
Right Foot��60   $��Right=�6B   $��Bottom=�6^   $��50(�6y   $��	0,0,50,20q�6�   $��Agency FB Regular)�6�   5       5!      