   M S B A M L   `   `   `  ����L  HWCF_双工_Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WCF_双工_ServerWCF_双工_Server  5   	   "    WCF_双工_Server.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   3loadingclr-namespace:WCF_双工_Server     5   	   -   5          � Title$  Server��5   	   $	��350��6   $	��720��6%    � Icon$. (Icon/chat_177px_1201536_easyicon.net.png��6H    � 	Resources 5       �� 
TargetType �� Property �� Value��%W   '��       �� 5   
   " ��6   .�5      ��  � ��$ 15(�6)   66   5	      5
      .��5      � .+�5   
    � -   5        $��loadingFrame��5      5      � 5   
   -   5        $��mainGrid��5      $y�	Collapsed=�6     � ColumnDefinitions 5   
   �� 5      $	��150 �6   6-   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      $��50 �6   6*   .+�5   
   � $��1��6   $��2��6    5      �� 5      5      �� 5      $��40 �6    5      �� 5      $��50 �6    5       .+�5"      �� -   5        $��richTextBox��5"       �� 
IsReadOnly . ��. 6?   �� 5#      $��2��6   	 �� Orientation$	 
Horizontal=�6&   $��5q�6?   .+�5$      �� -   5        $��
tb_preChat��5$      $	��400��6-   $��Center=�6:   $��Center=�6^   6�   �� 5%      -   5        $��btn_SendMsg��5%      $��70��6-   .��6R   发送文字6X   �� 5&      -   5        $��btn_SendImage��5&      $��70��63   .��6Z   传送文件6`   5'      �� 5(      $��1��6   $	 
Horizontal=�6&   $��Right=�6?   $	��542��6[   .+�5)      	� -   5        $��allRb��5)   +   $��20��6   $��Center=�68   $��Center=�6\   
 �� 	ClickMode$
 Press=�6~   $��对所有好友发送��6�   $��	150,0,0,0q�6�   	� 5*      -	   5        $��singleRb��5*   *   $��20��6   $��Center=�6:   $��Center=�6^   $
 Press=�6�   $��对单个好友发送��6�   $��50,0,0,0q�6�   5+      5,      � 5-   
   $��2��6    5.      �� 5/      $��50 �6    6.   �� 50      6"   .+�52      �� $��Center=�6   $��Center=�68   $��18(�6Z   
����  �.��6z   在线好友6�   �� 53      -
   5        $��listBox��53   #   $��1��6   $��16(�62   $��Black"�6@    �� SelectionMode$ Single=�6S   6l   54      56      57      58      