using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace BiliUPDesktopTool
{
    internal class Update : INotifyPropertyChanged
    {
        #region Private Fields

        private static Update instance;
        private bool _IsFinished;
        private string _Status;
        private string _UpdateText;

        #endregion Private Fields

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        public static Update Instance
        {
            get
            {
                if (instance == null) instance = new Update();
                return instance;
            }
        }

        /// <summary>
        /// 是否已完成
        /// </summary>
        public bool IsFinished
        {
            get { return _IsFinished; }
            set
            {
                _IsFinished = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsFinished"));
            }
        }

        /// <summary>
        /// 新版本号
        /// </summary>
        public string NewVersionStr
        {
            get
            {
                return jobj["data"]["version"].ToString();
            }
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public string Status
        {
            get { return "状态：" + _Status; }
            set
            {
                _Status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
            }
        }

        /// <summary>
        /// 更新内容
        /// </summary>
        public string UpdateText
        {
            get { return _UpdateText; }
            private set
            {
                _UpdateText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UpdateText"));
            }
        }

        #endregion Public Properties

        #region Private Properties

        private JObject jobj { get; set; }

        #endregion Private Properties

        #region Public Methods

        public void CheckUpdate(bool IsGUI = true)
        {
            IsFinished = false;
            string str = Bas.GetHTTPBody("https://cloud.api.zhangbudademao.com/117/Update");
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    jobj = JObject.Parse(str);

                    if ((int)jobj["code"] == 0)
                    {
                        if ((int)jobj["data"]["build"] <= Bas.Build)
                        {
                            if (IsGUI) MessageBox.Show("当前版本已是最新");
                            return;
                        }
                        else
                        {
                            if((int)jobj["data"]["force"] == 1)
                            {
                                WindowsManager.Instance.GetWindow<UpdateWindow>().Show();
                                DoUpdate(); 
                            }
                            else
                            {
                                UpdateText = $"当前版本：{Bas.Version}\r\n最新版本：{jobj["data"]["version"].ToString()}({jobj["data"]["updatetime"].ToString()}更新)\r\n\r\n更新内容：\r\n{jobj["data"]["content"].ToString()}";
                                if (IsGUI)
                                {
                                    WindowsManager.Instance.GetWindow<UpdateWindow>().Show();
                                }
                                else
                                {
                                    if ((int)jobj["data"]["build"] != Settings.Instance.IgnoredVersion) ToastHelper.Instance.NotifyUpdate();
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }

        public void DoUpdate()
        {
            Settings.Instance.IgnoredVersion = -1;

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\");

            WebClient Downloader = new WebClient();
            Downloader.DownloadProgressChanged += Downloader_DownloadProgressChanged;
            Downloader.DownloadFileCompleted += Downloader_DownloadFileCompleted;
            Downloader.DownloadFileAsync(new Uri(jobj["data"]["url"].ToString()), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\update.zip");
        }

        /// <summary>
        /// 忽略当前版本
        /// </summary>
        public void IgnoreThisVersion()
        {
            Settings.Instance.IgnoredVersion = (int)jobj["data"]["build"];
        }

        #endregion Public Methods

        #region Private Methods

        private void Downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Status = "正在校验...";
            if (Bas.GetFileHash(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\update.zip") == jobj["data"]["hash"].ToString())
            {
                Status = "正在解压...";
                if (Bas.UnZip(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\update.zip", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\UpdateTemp\\", ""))
                {
                    Status = "正在安装...";
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\update.zip");
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\update.bat", "@echo off\r\n" +
                                    "choice /t 5 /d y /n >nul\r\n" +                                                                                   //等待5s开始
                                    "xcopy \"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\UpdateTemp" + "\" \"" + Application.StartupPath + "\" /s /e /y\r\n" +     //覆盖程序
                                    "rmdir /s /q \"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\UpdateTemp\\" + "\"\r\n" +                                                //删除更新缓存
                                    "start \"\" \"" + Application.ExecutablePath + "\" -s\r\n" +                                                                     //启动程序
                                    "del %0", Encoding.Default);
                    Process p = new Process();

                    p.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\update.bat";
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.Verb = "runas";//管理员启动
                    p.Start();

                    Environment.Exit(2);
                }
            }
            else
            {
                MsgBoxPushHelper.RaisePushMsg("校验错误，请稍后再试！");
                IsFinished = true;
            }
            (sender as WebClient).Dispose();
        }

        private void Downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Status = "正在下载（" + e.ProgressPercentage + "%)...";
        }

        #endregion Private Methods
    }
}