   M S B A M L   `   `   `  ����E  ACalculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CalculatureCalculature  5   	   !    Calculator.CalculatorWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Calculature     5   	   -   5          � 
ResizeMode$  NoResize=�5   	    � Icon$  ./Resourses/Calculator.ico��5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � Title$ 
Calculator��5   	   $	��600��6   $	��800��6)   
�������
���   �.��5      � $	��584��6   $	��Top=�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $	��Top=�6K   $��1��6c   $��60��6p   .+�5      �� $	��Top=�6   $��Center=�61   $��5q�6N   .+�5       �� $��
Число��6   $��Center=�6)   �� 5!      -   5         ��Name$ InputNumberBox��5!      $��30��62   $	��150��6>   $��5q�6J   6U   5"      �� 5%      $	��Top=�6   $��Center=�61   $��5q�6N   .+�5&      �� $��	Из СИ��6   $��Center=�6)   �� 5'      -   5        $ InputBaseBox��5'      $��30��60   $��50��6<   $��5q�6G   6R   5(      �� 5+      $	��Top=�6   $��Center=�61   $��5q�6N   .+�5,      �� $��В СИ��6   $��Center=�6(   �� 5-      -   5        $ OutputBaseBox��5-      $��30��61   $��50��6=   $��5q�6H   6S   5.      �� 51      $	��Top=�6   $��Center=�61   $��5q�6N   .+�52      �� $��Результат��6   $��Center=�6-   �� 53      -   5        $ 	ResultBox��53      $��30��6-   $	��150��69   $��5q�6E    �� 
IsReadOnly$
 True��6P   6b   54      55      �� 58   
   $ 
Horizontal=�6   $��Center=�6.   $��Center=�6K   $��0��6f   $��10q�6s   .+�59      �� -   5        	 ��Name$	 ResetButton��59      $��25��6*   $��60��66   $��Сбросить��6A   $��5q�6T   6_   �� 5:      -   5        $	 CalculateButton��5:      $	��150��6.   $��50��6:   $��5q�6F   $��Вычислить��6Q   $��20(�6e   
�������
���  ��6�   �� 5;      -   5        $	 
QuitButton��5;      $	��100��6)   $��30��65   $��5q�6A   $��
Выйти��6L   
���  ��
���   �
�������$��20(�6�   6�   5<      �� 5?   
   -	   5        $ HistoryTextBox��5?      $
 True��6*   "
 �� VerticalScrollBarVisibility$

 Auto=�6<   $ �� HorizontalScrollBarVisibility$
 Auto=�6_    �� TextWrapping$
 Wrap=�6�   $��14(�6�   $��10,10,10,10q�6�   $	��234��6�   $��2��6�   5A      5B      