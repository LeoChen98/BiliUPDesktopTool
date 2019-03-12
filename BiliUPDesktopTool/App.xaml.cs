using System.Windows;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        #region Private Methods

        /// <summary>
        /// 启动动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //初始化公共变量
            Bas.skin = new Skin();
            Bas.settings = new Settings();
            Bas.account = new Account();
            Bas.biliupdata = new BiliUPData();
        }

        #endregion Private Methods
    }
}