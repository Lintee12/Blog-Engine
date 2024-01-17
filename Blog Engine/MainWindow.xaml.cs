using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using Markdig;

namespace Blog_Engine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public abstract class MarkdownProcessor
    {
        public static string ConvertToHtml(string markdownContent)
        {
            return Markdown.ToHtml(markdownContent);
        }
    }
    
    public partial class MainWindow
    {
        private string _postTitle = string.Empty;
        private string _postAuthor = string.Empty;
        private string _postDescription = string.Empty;
        private string _postFile = string.Empty;
        private string _postOutput = string.Empty;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Markdown files (*.md)|*.md|All files (*.*)|*.*",
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _postFile = openFileDialog.FileName;
            }
        }
        
        private void SelectOutput_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new VistaFolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == true)
            {
                _postOutput = folderBrowserDialog.SelectedPath;
            }
        }

        private void Post_Click(object sender, RoutedEventArgs e)
        {
            var hasError = false;
            
            _postTitle = PostTitle.Text;
            _postAuthor = PostAuthor.Text;
            _postDescription = PostDescription.Text;
            
            ErrorRenderer.Children.Clear();

            if (_postTitle == "")
            {
                hasError = true;
                var error = new Error
                {
                    ErrorMessage = "Post title is empty..."
                };
                ErrorRenderer.Children.Add((error));
            }
            if (_postAuthor == "")
            {
                hasError = true;
                var error = new Error
                {
                    ErrorMessage = "Post author is empty..."
                };
                ErrorRenderer.Children.Add((error));
            }
            if (_postDescription == "")
            {
                hasError = true;
                var error = new Error
                {
                    ErrorMessage = "Post description is empty..."
                };
                ErrorRenderer.Children.Add((error));
            }
            if (_postFile == "")
            {
                hasError = true;
                var error = new Error
                {
                    ErrorMessage = "Post file is empty..."
                };
                ErrorRenderer.Children.Add((error));
            }
            if (_postOutput == "")
            {
                hasError = true;
                var error = new Error
                {
                    ErrorMessage = "Post output is empty..."
                };
                ErrorRenderer.Children.Add((error));
            }

            if (hasError)
            {
                return;
            }
            
            var htmlContent = MarkdownProcessor.ConvertToHtml(File.ReadAllText(_postFile));
            
            Console.Out.WriteLine("Post title: " + _postTitle);
            Console.Out.WriteLine("Post author: " + _postAuthor);
            Console.Out.WriteLine("Post description: " + _postDescription);
            Console.Out.WriteLine("Post file: " + _postFile);
            Console.Out.WriteLine("Post Output: " + _postOutput);
            
            File.WriteAllText(_postOutput + "/" + _postTitle + ".html", htmlContent);
        }
    }
}