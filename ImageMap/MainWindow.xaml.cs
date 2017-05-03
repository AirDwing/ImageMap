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
using ExifLib;
using System.IO;

namespace ImageMap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            window.Drop += Grid1_Drop;
        }

        private void Grid1_Drop(object sender, DragEventArgs e)
        {
            string[] x= (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(var i in x)
            {
                JpegInfo info;
                using (FileStream fs = new FileStream(i, FileMode.Open))
                {
                    info = ExifReader.ReadJpeg(fs);
                }
            }
        }
    }

}
