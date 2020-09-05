using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using ZipOneCode.ZipProvider;
using System.Threading;

namespace LogFileCompress
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //从config文件获取配置项值
            string strLogFilePath = ConfigClass.LogFilePath;
            string strTargetPath = ConfigClass.TargetPath;
            int strDays = Convert.ToInt32(ConfigClass.Days);
            string strIsCompress = ConfigClass.IsCompress;
            string strIsDeleteDirectory = ConfigClass.IsDeleteDirectory;

            string strsubfolder = DateTime.Now.ToString("yyyyMMdd") + "-" + strDays;

            this.label2.Text = strLogFilePath;
            this.label4.Text = strTargetPath;
            this.label6.Text = strDays.ToString();
            this.label8.Text = strIsCompress;
            this.label10.Text = strIsDeleteDirectory;

            //如果不存在就创建file文件夹
            if (Directory.Exists(strLogFilePath) == false)
            {
                Directory.CreateDirectory(strLogFilePath);
            }
            if (Directory.Exists(strTargetPath + "\\" + strsubfolder) == false)
            {
                Directory.CreateDirectory(strTargetPath + "\\" + strsubfolder);
            }
            
            DirectoryInfo dyInfo = new DirectoryInfo(strLogFilePath);
            
            //获取文件夹下所有的文件
            foreach (FileInfo feInfo in dyInfo.GetFiles())
            {
                //判断文件创建日期是否小于配置的天数，是则转移
                if (feInfo.CreationTime < DateTime.Now.AddDays(-strDays))
                {
                    this.label12.Text = feInfo.ToString();
                    feInfo.MoveTo(strTargetPath + "\\" + strsubfolder + "\\" + feInfo);
                }
            }

            if(strIsCompress == "1")
            {
                if (File.Exists(strTargetPath + "\\" + strsubfolder + ".zip"))
                {
                    Random r = new Random(System.Environment.TickCount);
                    int i = r.Next(100000, 999999);
                    ZipHelper.CreateZip(strTargetPath + "\\" + strsubfolder, strTargetPath + "\\" + strsubfolder + "." + i +".zip");
                }
                else
                {
                    ZipHelper.CreateZip(strTargetPath + "\\" + strsubfolder, strTargetPath + "\\" + strsubfolder + ".zip");
                }
            }

            if (strIsDeleteDirectory == "1")
            {
                DirectoryInfo dir = new DirectoryInfo(strTargetPath + "\\" + strsubfolder);
                if (dir.Exists)
                {
                    DirectoryInfo[] childs = dir.GetDirectories();
                    foreach (DirectoryInfo child in childs)
                    {
                        child.Delete(true);
                    }
                    dir.Delete(true);
                }
            }
            
            System.Environment.Exit(0);
        }
        
    }
}
