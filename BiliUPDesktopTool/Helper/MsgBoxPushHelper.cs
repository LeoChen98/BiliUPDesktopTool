namespace BiliUPDesktopTool
{
    /// <summary>
    /// 信息推送类
    /// </summary>
    internal static class MsgBoxPushHelper
    {
        #region Public Delegates

        /// <summary>
        /// 信息推送委托
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="type">信息类型</param>
        public delegate void PushMsgHandler(string msg, MsgType type = MsgType.Info);

        #endregion Public Delegates

        #region Public Events

        /// <summary>
        /// 信息推送事件
        /// </summary>
        public static event PushMsgHandler PushMsg;

        #endregion Public Events

        #region Public Enums

        /// <summary>
        /// 信息类型枚举
        /// </summary>
        public enum MsgType
        {
            /// <summary>
            /// 一般信息
            /// </summary>
            Info = 0,

            /// <summary>
            /// 警告
            /// </summary>
            Warning = 1,

            /// <summary>
            /// 错误
            /// </summary>
            Error = 2
        }

        #endregion Public Enums

        #region Public Methods

        /// <summary>
        /// 发起信息推送
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="type">信息类型</param>
        public static void RaisePushMsg(string msg, MsgType type = MsgType.Info)
        {
            PushMsg.Invoke(msg, type);
        }

        #endregion Public Methods
    }
}