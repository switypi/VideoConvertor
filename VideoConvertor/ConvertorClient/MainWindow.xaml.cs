using Microsoft.Win32;
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

namespace ConvertorClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog openFile;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
                txtBox.Text = openFile.FileName;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            VideoConvertor.DirectorySearch osearch = new VideoConvertor.DirectorySearch("");
            osearch.InitialDirectory = txtBox.Text;
            osearch.ProcessDirectories(txtBox.Text);
        }
    }
}
