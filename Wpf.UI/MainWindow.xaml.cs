using Console.UI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Wpf.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const string txtSrcPathDesc = "Select one file in the Folder with all the files to Copy";
        private const string txtTargetPathDesc = "Select one file in the Folder where all the files will be Copy";

        private FileType fileType { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            SetFileTypeItemsSource();

            txtSrcPath.Text = txtSrcPathDesc;
            txtTargetPath.Text = txtTargetPathDesc;
            txtFileMoviesList.Text = @"F:\Videos\Movies\Movies Kids\MovieToCopyToUsbOrSdCard.txt"; ;

            txtSrcPath.Text = @"F:\Videos\Movies\Movies Kids";
            txtTargetPath.Text = @"E:\Movies";
        }


        private void cbFileType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FileType ft;
            Enum.TryParse(cbFileType.SelectedValue.ToString(), out ft);
            fileType = ft;

            switch (fileType)
            {
                case FileType.Videos:
                    txtSrcPath.Text = @"F:\Videos\Movies\Movies Kids";
                    txtTargetPath.Text = @"E:\Movies";
                    break;
                default:
                    break;
            }
        }

        private void txtSrcPath_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSrcPath.Text == txtSrcPathDesc)
            {
                txtSrcPath.Text = string.Empty;
            }

            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() ?? false)
            {
                txtSrcPath.Text = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
            }
        }

        private void txtTargetPath_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTargetPath.Text == txtTargetPathDesc)
            {
                txtTargetPath.Text = string.Empty;
            }

            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() ?? false)
            {
                txtTargetPath.Text = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
            }
        }
        private void txtFileMoviesList_GotFocus(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() ?? false)
            {
                txtFileMoviesList.Text = openFileDialog.FileName;
            }
        }

        

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            var isValidPath = IsValidPath(new List<string>() { txtSrcPath.Text, txtTargetPath.Text });
            rtxtMessage.Document.Blocks.Clear();

            if (cbFileType.SelectedIndex == 0)
            {
                EditRichTextBox("File Type required.", Brushes.Red);
            }
            else if (isValidPath)
            {
                var strMessage = string.Empty;
                switch (fileType)
                {
                    case FileType.Photos:
                        strMessage = ProcessFiles.ExecuteProcessFiles(txtSrcPath.Text, txtTargetPath.Text, fileType, chbAllDirectories.IsChecked);
                        break;
                    case FileType.Videos:
                        strMessage = CopyMovies.Copy(txtSrcPath.Text, txtTargetPath.Text, txtFileMoviesList.Text);
                        System.Console.Read();
                        break;
                    case FileType.Music:
                        strMessage = ProcessFiles.ExecuteProcessFiles(txtSrcPath.Text, txtTargetPath.Text, fileType, chbAllDirectories.IsChecked);
                        break;
                    case FileType.Any:
                        strMessage = ProcessFiles.ExecuteProcessFiles(txtSrcPath.Text, txtTargetPath.Text, fileType, chbAllDirectories.IsChecked);
                        break;
                    default:
                        break;
                }

                EditRichTextBox(strMessage, Brushes.Black);
            }
            else
            {
                EditRichTextBox("Something went wrong.", Brushes.Red);
            }
        }


        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var isValidPath = IsValidPath(new List<string>() { txtFileMoviesList.Text });
            if (isValidPath) { Process.Start("notepad.exe", txtFileMoviesList.Text); }
        }

        private void SetFileTypeItemsSource()
        {
            var itemsSource = new List<string>();
            itemsSource.Insert(0, "--Select File Type--");

            var fileTypes = Enum.GetValues(typeof(FileType));
            foreach (var fileType in fileTypes)
            {
                itemsSource.Add(fileType.ToString());
            }

            cbFileType.ItemsSource = itemsSource;
            cbFileType.SelectedIndex = 0;
        }

        private bool IsValidPath(List<string> listPath)
        {
            try
            {
                if (listPath?.Count > 0)
                {
                    foreach (var path in listPath)
                    {
                        if (string.IsNullOrEmpty(path))
                        {
                            return false;
                        }
                        else
                        {
                            var ext = System.IO.Path.GetExtension(path);
                            if (string.IsNullOrEmpty(ext))
                            {
                                System.IO.Path.GetDirectoryName(path);
                            }
                            else
                            {
                                System.IO.Path.GetFileName(path);
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                // Path is not valid
                return false;
            }

        }

        private void EditRichTextBox(string strMessage, SolidColorBrush solidColorBrush)
        {
            rtxtMessage.Document.Blocks.Add(new Paragraph(new Run(strMessage)));
            rtxtMessage.SelectAll();
            rtxtMessage.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, solidColorBrush);
        }

    }
}