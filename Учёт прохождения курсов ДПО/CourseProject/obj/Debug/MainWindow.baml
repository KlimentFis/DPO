   M S B A M L   `   `   `  ����H  DCourseProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CourseProjectCourseProject  5   	       CourseProject.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:CourseProject     5   	   -   5          � Title$  
MainWindow��5	   	   $
��1080��5
   	   $
��1920��5   	    � 	Resources 5         RoundedButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property	 P� Value��%�  &	        �� 5   
   " ��6&   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� ���	#FF495C8A$ 15��68   
�������$��1q�6^   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver	 . . �5   "   ��  � �� �#F2F2F26?   �� 5   "    � �� �#495C8A6?   5      5      5      �� 5       � ��
 �����6+   5      5       .��5"      � 
 � ColumnDefinitions
 5#   
   �� 5$      $��1303* �6   �� 5%      $
��617* �6    � RowDefinitions 5'   
   �� 5(      6   .+�5*   
    � -   5        $��	MainFrame��5*      $��0��6!     � Source$ Pages/MainPage.xaml?�6.     � NavigationUIVisibility$ Hidden=�6j   $��0,48,0,0q�6�   $��2��6�   �� 5+   
   -   5         ��Name$ Button��5+      $��
Курсы��6!   $
��Left=�61   $��0,0,0,0q�6L   $	��640��6]   $��50��6�   $	��Top=�6�   �� 5,   
   -   5        $ Button1��5,      $ ��Преподаватели��6"   $
��Left=�6;   $��	640,0,0,0q�6V   $	��Top=�6i   $	��640��6�   $��50��6�   6�   �� 5-   
   -   5        $ Button2��5-      $%��Общая отчетность��6"   $
��Left=�6=   $��
1280,0,0,0q�6X   $	��Top=�6l   $	��640��6�   $��50��6�   $��3��6�   5.      5/      