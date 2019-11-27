using System.Windows;

namespace BiliUPDesktopTool
{
    internal class WindowBaseCommand
    {
        #region Public Properties

        public RelayCommand<Window> Close
        {
            get
            {
                return new RelayCommand<Window>((sender) =>
                {
                    sender.Close();
                });
            }
        }

        public RelayCommand<Window> MinSize
        {
            get
            {
                return new RelayCommand<Window>((sender) =>
                {
                    sender.WindowState = WindowState.Minimized;
                });
            }
        }

        #endregion Public Properties
    }
}