   M S B A M L   `   `   `  ����H  DCourseProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CourseProject.PagesCourseProject.Pages  5      '    !CourseProject.Pages.LecturersPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:CourseProject.Pages     5        N� Title$   Преподаватели��5      .��5
      � ���#F2F2F2 � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5      $	��98* �6   �� 5      $	��98* �6   �� 5      $
��196* �6   �� 5      $	��37* �6   �� 5      $	��90* �6   �� 5      $
��128* �6   �� 5      $	��30* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $1��+Добавить преподавателя��5      $
��Left=�5      $��
47,35,0,15q�5       $	��159��5!      ���#497C8A���#FFFFFF$��0q�5$      $��30��5%       � ToolTip$\ VПерейти на страницу добавления преподавателей��5&      $��1��6I   $��3��6Y   &   System.Windows.Controls.DataGrid  5)   
   -   5        $��lecturersDataGrid��5)      $��1��6,      AutoGenerateColumns .  $��18,16,20,11q�6U   $��7��6j   $��1��6~   $
��1920��6�   $
��1080��6�      Columns 5*      4  .System.Windows.Controls.DataGridTemplateColumn  5+         CellTemplate 5,      �� 5-      .R�5.      �� -   5        $��
Курсы��5.   %   ���#497C8A���#FFFFFF$��0q�52   %   53      55        56       57      �� 58      .R�59      �� -   5        $��Удалить��59   %   ���#497C8A���#FFFFFF$��0q�5=   %   5>      5@      0  *System.Windows.Controls.DataGridTextColumn  5A      ,  &System.Windows.Controls.DataGridColumn   Header$  Идентификатор��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *
LecturerID+  5B      $ Фамилия��6%    66   �� *LecturerLastName+  5C      $ Имя��6%    62   �� *LecturerFirstName+  5D      $ Отчество��6%    67   �� *LecturerPatronymic+5F      �� 5G   
   -   5        $��LecturersSearch��5G      $��1��6)   $
��Left=�69   $��
227,35,0,0q�6T   	 �� TextWrapping$
	 Wrap=�6h   $	��Top=�6|   $��Center=�6�   $	��160��6�   $��30��6�   $��2��6�   $# Введите фамилию��6�   6�   5H      5I      