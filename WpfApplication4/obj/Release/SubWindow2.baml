   M S B A M L   `   `   `  ���� J  FWpfApplication4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication4WpfApplication4       WpfApplication4.SubWindow2   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WpfApplication4     -     � Title$  KINECT Balance Beam Game��$	��900��$
��1550��.��� .+��� -    ��Name$ BackgroundImage��$��Resources/Interface.jpg�� J� Opacity$	 0.8Q�(� $��Center=�.���� $��UniformToFill=����� *SkeletonImageSource+�� -    ��Name$ StandStatusTextBlock��$��Instructions��$��60(�$��1053,165,89,404q� �� TextAlignment$ Center=�$	��250��$	��400��$��Agency FB Regular)��� -   $ ExerciseMode��$��Center=�$
��Wrap=�$��60(�$��Agency FB Bold)�$��Exercise Mode��$��50,25,0,727q��� -   $ Timer��$
��Left=�$��50,76,0,727q�$
��Wrap=�$��Timer��$��50(�$��Agency FB Regular)�� -   $��TimeProgress��$	��Top=�$	��100��$��10�� � Minimum$ 0Q� � Maximum$ 10Q� � Value$ 0Q�$��240,231,1202,0q��� -   $ TimerTextBlock��$
��10 s��$��Right=�$	��Top=�$
��Bold"�$	��100(�$��Agency FB Bold)�$��0,144,1322,0q��� -   $ Score��$
��Left=�$��50,286,0,520q�$
��Wrap=�$��Score��$��50(�$��Agency FB Regular)��� -	   $ ScoreTextBlock��$��00 pts��$	��Top=�$
��Bold"�$	��100(�$��Agency FB Bold)�$��
85,354,0,0q��� -
   $ FootLeftTextBlock��$��	Left Foot��$
��Left=�$��Bottom=�$��50(�$��	50,0,0,20q�$��Agency FB Regular)��� -   $ FootRightTextBlock��$��
Right Foot��$��Right=�$��Bottom=�$��50(�$��	0,0,50,20q�$��Agency FB Regular)�