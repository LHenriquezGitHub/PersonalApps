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
        private FileType FileType { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            SetFileTypeItemsSource();

            txtSrcPath.Text = txtSrcPathDesc;
            txtTargetPath.Text = txtTargetPathDesc;
            txtFileMoviesList.Text = Constant.FileMoviesList;

            txtSrcPath.Text = Constant.SrcPath;
            txtTargetPath.Text = Constant.TargetPath;
        }

        private void cbFileType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Enum.TryParse(cbFileType.SelectedValue.ToString(), out FileType ft);
            FileType = ft;

            switch (FileType)
            {
                case FileType.Videos:
                    txtSrcPath.Text = Constant.SrcPath;
                    txtTargetPath.Text = Constant.TargetPath;
                    break;
                case FileType.Photos:
                    break;
                case FileType.Music:
                    break;
                case FileType.Any:
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

            OpenFileDialog openFileDialog = new OpenFileDialog();
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

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() ?? false)
            {
                txtTargetPath.Text = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
            }
        }

        private void txtFileMoviesList_GotFocus(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() ?? false)
            {
                txtFileMoviesList.Text = openFileDialog.FileName;
            }
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            bool isValidPath = IsValidPath(new List<string>() { txtSrcPath.Text, txtTargetPath.Text });
            rtxtMessage.Document.Blocks.Clear();

            if (cbFileType.SelectedIndex == 0)
            {
                EditRichTextBox("File Type required.", Brushes.Red);
            }
            else if (isValidPath)
            {
                string strMessage = string.Empty;
                switch (FileType)
                {
                    case FileType.Photos:
                        strMessage = ProcessFiles.ExecuteProcessFiles(txtSrcPath.Text, txtTargetPath.Text, FileType, chbAllDirectories.IsChecked);
                        break;
                    case FileType.Videos:
                        strMessage = CopyMovies.Copy(txtSrcPath.Text, txtTargetPath.Text, txtFileMoviesList.Text);
                        System.Console.Read();
                        break;
                    case FileType.Music:
                        strMessage = ProcessFiles.ExecuteProcessFiles(txtSrcPath.Text, txtTargetPath.Text, FileType, chbAllDirectories.IsChecked);
                        break;
                    case FileType.Any:
                        strMessage = ProcessFiles.ExecuteProcessFiles(txtSrcPath.Text, txtTargetPath.Text, FileType, chbAllDirectories.IsChecked);
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
            bool isValidPath = IsValidPath(new List<string>() { txtFileMoviesList.Text });
            if (isValidPath) { Process.Start("notepad.exe", txtFileMoviesList.Text); }
        }

        private void SetFileTypeItemsSource()
        {
            List<string> itemsSource = new List<string>();
            itemsSource.Insert(0, "--Select File Type--");

            Array fileTypes = Enum.GetValues(typeof(FileType));
            foreach (object fileType in fileTypes)
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
                    foreach (string path in listPath)
                    {
                        if (string.IsNullOrEmpty(path))
                        {
                            return false;
                        }
                        else
                        {
                            string ext = System.IO.Path.GetExtension(path);
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

        public int HandshakeOnceRecursive(int peopleCount) => peopleCount == 0 || peopleCount == 1 ? 0 : peopleCount - 1 + HandshakeOnce(peopleCount - 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="peopleCount"></param>
        /// <returns># of handshake exactly once</returns>
        public int HandshakeOnce(int peopleCount) => peopleCount * (peopleCount - 1) / 2;
    }

    public class Constant
    {
        public static string SrcPath = @"F:\Movies And TV Shows\Movies\Movies Kids";
        public static string TargetPath = @"E:\Movies";
        public static string FileMoviesList = SrcPath + @"\MovieToCopyToUsbOrSdCard.txt";
    }
}