   M S B A M L   `   `   `  ����H  DCourseProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CourseProject.PagesCourseProject.Pages  5      %    CourseProject.Pages.CoursesPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:CourseProject.Pages     5        N� Title$  
Курсы��5	      .��5      � ���#fff$��0,50,0,0q�6   .+�5   
   � ���#fff � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� -   5        $��Добавить курс��5      $��16(�6E   ���#497c8a���#fff���#497c8a$
��Left=�6�   $��
20,30,0,20q�6�   $	��130��6�   $��30��6�    � ToolTip$L FПерейти на страницу добавления курсов��6�   �� 5      -   5        $��CoursesSearch��5      $
��Left=�6+   $��
169,30,0,0q�6F    �� TextWrapping$
 Wrap=�6Z   $	��Top=�6n   $��Center=�6�   $	��160��6�   $��30��6�   $0 *Введите название курса��6�   6�   &   System.Windows.Controls.DataGrid  5      -   5        $��coursesDataGrid��5      $��1��6.      AutoGenerateColumns .  $��18,16,18,28q�6W   ���#f2f2f2   RowBackground �#fff   AlternatingRowBackground �#f2f2f2   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5         CellTemplate 5      �� 5      .R�5   "   �� -   5        $��Удалить��5   )   ���#497c8a���#fff���#497c8a5"   )   5#      5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn	   Header$ 	 Идентификатор��6)   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6@   �� *
CourseID+  5'      $	 Название��6)   
 6;   �� *
CourseName+  5(      $	 Организация��6)   
 6>   �� *CourseOrganization+  5)      $%	 Количество часов��6)   
 6C   �� *CourseHours+  5*      $	 Вид��6)   
 66   �� *
CourseKind+  5+      $	 Стажировка��6)   
 6=   �� *CourseInternship+  5,      $	 Приоритет��6)   
 6<   �� *CoursePriority+5.      �� 50      -   5        $ ��Отфильтровать��50      $��16(�6I   ���#497c8a���#fff���#497c8a$��Right=�6�   $��	0,0,25,20q�6�   $	��130��6�   $��30��6�   $��Bottom=�6�   �� 52      -   5        $��CourseTypeComboBox��52   9   $��Center=�6   $��0,27,160,18q�6S   ���#497c8a���#497c8a���#f2f2f2$��Right=�6�   $	��130��6�   $��30��6�   .@�53      �� $��По умолчанию��6   �� 54      $��	Топ-50��6   �� 55      $#��Не приоритетные��6   �� 56      $��По названию��6   �� 57      $!��По организации��6   �� 58      $!��Со стажировкой��6   �� 59      $!��Без стажировки��6   �� 5:      $��Технический��6   �� 5;      $"��Педагогический��6   5<      5=      5>      5?      