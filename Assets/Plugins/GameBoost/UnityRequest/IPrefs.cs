#if UNITY_EDITOR
namespace Plugins.GameBoost.UnityRequest
{
    internal interface IPrefs
    {
        string UserID { get; set; }
    }
}
#endif
