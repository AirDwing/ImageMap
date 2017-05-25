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
using System.Threading;

namespace ImageMap
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class OprateBasic
    {
        public void palymusic(string msg)
        {
            
        }
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<JpegInfo> info = new List<JpegInfo>();
        public MainWindow()
        {
            InitializeComponent();
            web1.NavigateToString(Properties.Resources.amap);
            web1.ObjectForScripting = new OprateBasic();
            web1.Navigating += Web1_Navigating;
            window.Drop += Grid1_Drop;
        }
        /// <summary>
        /// 禁用web1的文件拖拽功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Web1_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            e.Cancel = true;
            return;
        }

        private void Grid1_Drop(object sender, DragEventArgs e)
        {
            string[] x= (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(var i in x)
            {
                
                using (FileStream fs = new FileStream(i, FileMode.Open))
                {
                    //读取图片的位置信息
                    var imginfo = ExifReader.ReadJpeg(fs);
                    info.Add(imginfo);

                    Transform tf = new Transform();
                    var g = tf.WGS2GCJ(gps(imginfo.GpsLatitude), gps(imginfo.GpsLongitude));

                    //转换坐标
                    var GpsLatitude = g.Lat;
                    var GpsLongitude = g.Lng;

                    //调用js
                    object[] arrojj = new object[2] { GpsLongitude, GpsLatitude };
                    web1.InvokeScript("addMarker", arrojj);

                }
            }
        }
        /// <summary>
        /// gps转换
        /// </summary>
        /// <param name="g">度分秒</param>
        /// <returns>6位double</returns>
        private double gps(double[] g)
        {
            return Math.Round(g[0] + (g[1] / 60) + (g[2] / 3600),6);
        }
    }

}
