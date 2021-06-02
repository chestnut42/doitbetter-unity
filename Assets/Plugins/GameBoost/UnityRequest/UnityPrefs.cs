#if UNITY_EDITOR
using UnityEngine;

namespace Plugins.GameBoost.UnityRequest
{
    internal class UnityPrefs : IPrefs
    {
        private static readonly string KEY = "tech.doitbetter.key.userid";

        public string UserID
        {
            get
            {
                return PlayerPrefs.HasKey(KEY) ? PlayerPrefs.GetString(KEY) : null;
            }
            set
            {
                if (value != null)
                {
                    PlayerPrefs.SetString(KEY, value);
                }
                else
                {
                    PlayerPrefs.DeleteKey(KEY);
                }
            }
        }
    }
}
#endif
