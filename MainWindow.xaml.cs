using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;

namespace YourNamespace
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик для кнопки создания новой папки
        private void CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            string folderName = Interaction.InputBox("Введите название папки:", "Новая папка", "Новая папка");
            if (!string.IsNullOrWhiteSpace(folderName))
            {
                var newFolder = new TreeViewItem { Header = folderName };
                FoldersTreeView.Items.Add(newFolder);
            }
        }

        // Обработчик для кнопки создания новой записи
        private void CreateNote_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбрана ли папка
            if (FoldersTreeView.SelectedItem is TreeViewItem selectedFolder)
            {
                string noteName = Interaction.InputBox("Введите название записи:", "Новая запись", "Новая запись");
                if (!string.IsNullOrWhiteSpace(noteName))
                {
                    var newNote = new TreeViewItem { Header = noteName, Tag = "" }; // Tag для хранения текста записи
                    selectedFolder.Items.Add(newNote);
                    selectedFolder.IsExpanded = true; // Автоматически раскрываем папку
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите папку для добавления записи.");
            }
        }

        // Обработчик для выбора элемента в TreeView
        private void FoldersTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (FoldersTreeView.SelectedItem is TreeViewItem selectedItem)
            {
                // Проверяем, является ли выбранный элемент записью (имеет ли текстовое содержимое)
                if (selectedItem.Tag is string noteContent)
                {
                    NoteContentTextBox.Text = noteContent;
                    NoteContentTextBox.IsEnabled = true;
                }
                else
                {
                    NoteContentTextBox.Text = "";
                    NoteContentTextBox.IsEnabled = false;
                }
            }
        }

        // Обработчик для сохранения изменений в тексте записи
        private void NoteContentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FoldersTreeView.SelectedItem is TreeViewItem selectedItem && selectedItem.Tag is string)
            {
                // Сохраняем содержимое TextBox в Tag выбранного элемента
                selectedItem.Tag = NoteContentTextBox.Text;
            }
        }
    }
}
