   M S B A M L   `   `   `  ����D  @SnakeGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SnakeGame	SnakeGame  5   	       SnakeGame.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:SnakeGame     5   	   -   5          � Title$  
MainWindow��5   	   $
��1000��6   $��1378.713��6*   .��5
      � 
����p�� � ColumnDefinitions 5   
   �� 5      $��0.666* �6   �� 5      $��0.233* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��1��6   
���   �$��3q�6B    �� CornerRadius$ 6��6V   
���� ��$��0,15,0,0q�6{   ._�5      ��  �� Orientation$ Vertical=�6   .+�5      �� $ Vertical=�6   $��5q�64   .+�5      �� $ 
Horizontal=�6!   .+�5      �� $��Score:��6    $��18(�61   
�������6?   �� 5      -   5         ��Name$ 
LabelScore��5       $��16(�64   $��5,0,0,0q�6B   $��0��6S   
�������6_   5      5      � 5      .+�5      �� $��25(�6    
�������
Top Score:6M   �� 5      -   5         ��Name$ 	TopScorer��5       $��25(�63   
�������$��	130 0 0 0q�6T   0 - 06n   5      5       5!      �� 5#   
   .+�5$      �� -   5         ��Name$ Board��5$      ��5%      �� 5&      .R�5'      � .+�5(      ��  ��Name$ Peace��6(   $��10��67   $��10��6B   
���   �
�������$	��0.5q�6j   �� 5)      $��10��6>   $��10��6I   $��Center=�6U   $��Center=�6r   ��6%   �� *Image+6r   5*      5+      ��5-      �� 5.      .R�5/      � 	 �Name$	 PlayingField��6$   $
��auto��6:   $	��550��6H   50      52      53      54      55      