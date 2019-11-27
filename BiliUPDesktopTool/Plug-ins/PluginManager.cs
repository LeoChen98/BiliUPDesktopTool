using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace BiliUPDesktopTool
{
    public class PluginInfo
    {
        #region Public Fields

        public string[] args;

        [JsonIgnore]
        public StreamWriter Input;

        public bool is_first_run = true;
        public string name, description, launch_file, version_str, update_url, agreement;

        [JsonIgnore]
        public bool NeedUpdate;

        [JsonIgnore]
        public StreamReader Output;

        [JsonIgnore]
        public Process process;

        [JsonIgnore]
        public UpdateInfo.Data updateinfo;

        public int? version;

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// 专栏数据订阅列表
        /// </summary>
        [JsonIgnore]
        private List<string> ArticleDataSubscribed;

        /// <summary>
        /// 视频数据订阅列表
        /// </summary>
        [JsonIgnore]
        private List<string> VideoDataSubscribed;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 初始化
        /// </summary>
        public PluginInfo()
        {
            ArticleDataSubscribed = new List<string>();
            VideoDataSubscribed = new List<string>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// 退出插件
        /// </summary>
        public void Exit()
        {
            SendMsg("Q");
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        public void Save()
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\{name}\\mainfirst.json", json);
        }

        /// <summary>
        /// 通讯
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(string msg)
        {
            if (process != null)
            {
                if (!process.HasExited)
                {
                    Input.WriteLine(msg);
                }
                else
                {
                    Input?.Close();
                    Output?.Close();
                    process = null;
                    BiliUPData.Intance.article.PropertyChanged -= Article_PropertyChanged;
                    BiliUPData.Intance.video.PropertyChanged -= Video_PropertyChanged;
                }
            }
            else
            {
                Input?.Close();
                Output?.Close();
                BiliUPData.Intance.article.PropertyChanged -= Article_PropertyChanged;
                BiliUPData.Intance.video.PropertyChanged -= Video_PropertyChanged;
            }
        }

        /// <summary>
        /// 启动插件
        /// </summary>
        public void Start()
        {
            process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = launch_file,
                    Arguments = GetArgsString(),
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };
            Input = process.StandardInput;
            Output = process.StandardOutput;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            WebClient client = new WebClient();
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileAsync(new Uri(updateinfo.url), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\{name}\\update.zip");
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// 专栏数据更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Article_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (ArticleDataSubscribed.Contains(e.PropertyName) && !process.HasExited)
            {
                SendMsg($"data-article-{e.PropertyName} {sender.GetType().GetProperty(e.PropertyName).GetValue(sender).ToString()}");
            }
        }

        /// <summary>
        /// 更新下载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (Bas.GetFileHash(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\{name}\\update.zip") == updateinfo.hash)
            {
                SendMsg("Q");

                if (Bas.UnZip(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\{name}\\update.zip", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\{name}\\", ""))
                {
                    version = updateinfo.version;
                    version_str = updateinfo.version_str;
                    Save();

                    NeedUpdate = false;
                    updateinfo = null;
                    MsgBoxPushHelper.RaisePushMsg($"插件{name}更新成功!");
                }
                else
                {
                    MsgBoxPushHelper.RaisePushMsg($"插件{name}更新失败，解压或文件错误!");
                }
            }
            else
            {
                MsgBoxPushHelper.RaisePushMsg($"插件{name}更新失败，文件校验失败!");
            }
        }

        /// <summary>
        /// 更新下载进度变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
        }

        /// <summary>
        /// 构造命令行
        /// </summary>
        /// <returns></returns>
        private string GetArgsString()
        {
            string args_string = "";
            Regex reg = new Regex("^%(?:.*)%$");

            if (args.Length > 0)
            {
                foreach (string i in args)
                {
                    if (reg.IsMatch(i))
                    {
                        switch (i)
                        {
                            case "%cookies%":
                                args_string += Account.Instance.Cookies + " ";
                                break;

                            default:
                                Regex reg_data = new Regex("^%data-(?<class>:\\w*?)-(?<name>:\\w*?)%$", RegexOptions.ExplicitCapture);
                                Match match = reg_data.Match(i);
                                if (match.Success)
                                {
                                    switch (match.Groups["class"].Value)
                                    {
                                        case "article":
                                            BiliUPData.Intance.article.PropertyChanged -= Article_PropertyChanged;
                                            BiliUPData.Intance.article.PropertyChanged += Article_PropertyChanged;
                                            ArticleDataSubscribed.Add(match.Groups["name"].Value);
                                            break;

                                        case "video":
                                            BiliUPData.Intance.video.PropertyChanged -= Video_PropertyChanged;
                                            BiliUPData.Intance.video.PropertyChanged += Video_PropertyChanged;
                                            VideoDataSubscribed.Add(match.Groups["name"].Value);
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        args_string += i + " ";
                    }
                }
                args_string = args_string.Substring(0, args_string.Length - 1);
            }
            return args_string;
        }

        /// <summary>
        /// 通讯回传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data.ToString() == "Q")
            {
                Input?.Close();
                Output?.Close();
                process = null;
                BiliUPData.Intance.article.PropertyChanged -= Article_PropertyChanged;
                BiliUPData.Intance.video.PropertyChanged -= Video_PropertyChanged;
            }
        }

        /// <summary>
        /// 视频数据更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Video_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (VideoDataSubscribed.Contains(e.PropertyName))
            {
                SendMsg($"data-video-{e.PropertyName} {sender.GetType().GetProperty(e.PropertyName).GetValue(sender).ToString()}");
            }
        }

        #endregion Private Methods

        #region Public Classes

        public class UpdateInfo
        {
            #region Public Fields

            public int code;
            public Data data;

            #endregion Public Fields

            #region Public Classes

            public class Data
            {
                #region Public Fields

                public string content;
                public string hash;
                public DateTime update_time;
                public string url;
                public int version;
                public string version_str;

                #endregion Public Fields
            }

            #endregion Public Classes
        }

        #endregion Public Classes
    }

    internal class PluginManager
    {
        #region Public Fields

        public List<PluginInfo> Plugins;

        #endregion Public Fields

        #region Private Fields

        private static PluginManager instance;

        #endregion Private Fields

        #region Private Constructors

        private PluginManager()
        {
            Plugins = new List<PluginInfo>();

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\");
                return;
            }

            DirectoryInfo[] directories = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\").GetDirectories();
            if (directories.Length > 0)
            {
                foreach (DirectoryInfo i in directories)
                {
                    try
                    {
                        string str = File.ReadAllText(i.FullName + "\\mainfirst.json");
                        PluginInfo plugin = JsonConvert.DeserializeObject<PluginInfo>(str);

                        if (string.IsNullOrEmpty(plugin.name) || string.IsNullOrEmpty(plugin.launch_file) || string.IsNullOrEmpty(plugin.version_str) || plugin.version == null)
                            throw new Exception("参数错误！");

                        Plugins.Add(plugin);
                    }
                    catch (Exception ex)
                    {
                        MsgBoxPushHelper.RaisePushMsg($"加载插件错误！{ex.Message}");
                    }
                }
            }
        }

        #endregion Private Constructors

        #region Public Properties

        public static PluginManager Instance
        {
            get
            {
                if (instance == null) instance = new PluginManager();
                return instance;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 检查更新
        /// </summary>
        public void CheckUpdate()
        {
            if (Plugins.Count > 0)
            {
                foreach (PluginInfo i in Plugins)
                {
                    string str = Bas.GetHTTPBody(i.update_url);

                    if (!string.IsNullOrEmpty(str))
                    {
                        PluginInfo.UpdateInfo info = JsonConvert.DeserializeObject<PluginInfo.UpdateInfo>(str);
                        if (info.code == 0)
                        {
                            if (info.data.version > i.version)
                            {
                                i.NeedUpdate = true;
                                i.updateinfo = info.data;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="packet">插件包位置</param>
        /// <returns>插件信息</returns>
        public void Install(string packet)
        {
            if (File.Exists(packet))
            {
                if (Bas.UnZip(packet, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\InstallTemp\\", ""))
                {
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\InstallTemp\\mainfirst.json"))
                    {
                        try
                        {
                            string str = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\InstallTemp\\mainfirst.json");
                            PluginInfo plugin = JsonConvert.DeserializeObject<PluginInfo>(str);

                            if (string.IsNullOrEmpty(plugin.name) || string.IsNullOrEmpty(plugin.launch_file) || string.IsNullOrEmpty(plugin.version_str) || plugin.version == null)
                                throw new Exception("参数错误！");

                            Directory.Move(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\InstallTemp\\", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\{plugin.name}\\");

                            MsgBoxPushHelper.RaisePushMsg("安装插件成功！");
                            Plugins.Add(plugin);
                        }
                        catch (Exception ex)
                        {
                            MsgBoxPushHelper.RaisePushMsg($"安装插件错误！{ex.Message}");
                            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\InstallTemp\\"))
                                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\InstallTemp\\");
                        }
                    }
                    else
                    {
                        MsgBoxPushHelper.RaisePushMsg("安装插件失败，插件包不完整！");
                        if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\InstallTemp\\"))
                            Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\InstallTemp\\");
                    }
                }
                else
                {
                    MsgBoxPushHelper.RaisePushMsg("安装插件失败，解压包失败！");
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\InstallTemp\\"))
                        Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\InstallTemp\\");
                }
            }
            else
            {
                MsgBoxPushHelper.RaisePushMsg("安装插件失败，插件包不存在！");
            }
        }

        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <param name="plugin">插件信息</param>
        public void Uninstall(PluginInfo plugin)
        {
            Plugins.Remove(plugin);

            try
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\zhangbudademao.com\\BiliUPDesktopTool\\Plug-ins\\{plugin.name}\\");
                MsgBoxPushHelper.RaisePushMsg("卸载插件成功！");
            }
            catch (Exception ex)
            {
                MsgBoxPushHelper.RaisePushMsg($"卸载插件失败，{ex.Message}！");
            }
        }

        #endregion Public Methods
    }
}