using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// 设置基类
    /// </summary>
    /// <typeparam name="T">设置数据表类型</typeparam>
    public class SettingsBase<T> : INotifyPropertyChanged
    {
        #region Protected Fields

        protected T ST;

        #endregion Protected Fields

        #region Private Fields

        private string savepath;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SettingsBase()
        {
        }

        /// <summary>
        /// 初始化设置
        /// </summary>
        public SettingsBase(T _ST, string _savepath)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\");

            ST = _ST;
            savepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zhangbudademao.com\\BiliUPDesktopTool\\" + _savepath;

            if (File.Exists(savepath))
            {
                using (FileStream fs = File.OpenRead(savepath))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string str = reader.ReadToEnd();
                        OutputTable<T> OT = JsonConvert.DeserializeObject<OutputTable<T>>(str);
                        ST = OT.settings;
                    }
                }
            }
        }

        #endregion Public Constructors

        #region Public Events

        /// <summary>
        /// 属性更改事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// 属性改变通知函数
        /// </summary>
        /// <param name="sender">发送对象</param>
        /// <param name="e">属性</param>
        public void PropertyChangedA(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public virtual void Save()
        {
            OutputTable<T> OT = new OutputTable<T>(ST);
            string json = JsonConvert.SerializeObject(OT);
            using (FileStream fs = File.Open(savepath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }
            }
        }

        #endregion Public Methods

        #region Public Classes

        /// <summary>
        /// 导出数据表
        /// </summary>
        public class OutputTable<T>
        {
            #region Public Fields

            [JsonProperty("pid")]
            public const int pid = 117;

            [JsonProperty("version")]
            public const int version = 1;

            public T settings;

            #endregion Public Fields

            #region Public Constructors

            public OutputTable(T ST)
            {
                settings = ST;
            }

            #endregion Public Constructors
        }

        #endregion Public Classes
    }
}