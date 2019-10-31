using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BiliUPDesktopTool
{
    public class WindowsManager
    {
        #region Private Fields

        private static WindowsManager instance;

        private readonly List<Window> Windows;

        #endregion Private Fields

        #region Private Constructors

        private WindowsManager()
        {
            Windows = new List<Window>();
        }

        #endregion Private Constructors

        #region Public Properties

        public static WindowsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WindowsManager();
                }
                return instance;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void AddWindow<T>(T window) where T : Window
        {
            var win = Windows.FirstOrDefault(i => i.GetType() == typeof(T));
            if (win == null)
            {
                Windows.Add(window);
            }
            else
            {
                throw new Exception($"{typeof(T).Name} 已存在");
            }
        }

        public T GetWindow<T>() where T : Window, new()
        {
            var win = Windows.FirstOrDefault(i => i.GetType() == typeof(T));
            if (win == null)
            {
                win = new T();
                win.Closed += Win_Closed;
                Windows.Add(win);
            }
            return (T)win;
        }

        #endregion Public Methods

        #region Private Methods

        private void Win_Closed(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.Closed -= Win_Closed;
            Windows.Remove(window);
        }

        #endregion Private Methods
    }
}