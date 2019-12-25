///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ToastCore.Notification
{
    public class NotificationUserInput : IReadOnlyDictionary<string, string>
    {
        #region Private Fields

        private NOTIFICATION_USER_INPUT_DATA[] _data;

        #endregion Private Fields

        #region Internal Constructors

        internal NotificationUserInput(NOTIFICATION_USER_INPUT_DATA[] data)
        {
            _data = data;
        }

        #endregion Internal Constructors

        #region Public Properties

        public int Count => _data is null ? 0 : _data.Length;
        public IEnumerable<string> Keys => _data.Select(i => i.Key);
        public IEnumerable<string> Values => _data.Select(i => i.Value);

        #endregion Public Properties

        #region Public Indexers

        public string this[string key] => _data.First(i => i.Key == key).Value;

        #endregion Public Indexers

        #region Public Methods

        public bool ContainsKey(string key)
        {
            return _data.Any(i => i.Key == key);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _data.Select(i => new KeyValuePair<string, string>(i.Key, i.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(string key, out string value)
        {
            foreach (var item in _data)
            {
                if (item.Key == key)
                {
                    value = item.Value;
                    return true;
                }
            }

            value = null;
            return false;
        }

        #endregion Public Methods
    }
}