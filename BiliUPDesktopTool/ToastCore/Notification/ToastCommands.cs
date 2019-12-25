///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------

namespace ToastCore.Notification
{
    /// <summary>
    /// Toast通知的按钮命令
    /// </summary>
    public struct ToastCommands
    {
        #region Public Fields

        public string ActivationType;
        public string AfterActivationBehavior;
        public string Argument;

        public string Content;

        #endregion Public Fields

        #region Public Constructors

        public ToastCommands(string arg, string content, string after, string type)
        {
            Argument = arg;
            Content = content;
            AfterActivationBehavior = after;
            ActivationType = type;
        }

        #endregion Public Constructors
    }
}