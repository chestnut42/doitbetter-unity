using System.Collections.Generic;
using UnityEngine;
using Plugins.GameBoost;

public class GBEventsScreenUI : MonoBehaviour
{
    public string APIKey;

    private void Awake()
    {
        GameBoostSDK.SetLoggingEnabled(true);
        GameBoostSDK.Initialize(APIKey);
    }

    public IArcheroRoom CreateRoom()
    {
        var roomDescription = new Dictionary<string, object>();
        roomDescription["Enemies"] = new List<string> {"enemy_042"};

        var balance = new Dictionary<string, object>();
        balance["enemy_042"] = new Dictionary<string, object> { {"health", 2000}, {"color", "green"} };

        return GameBoostSDK.CreateArcheroRoom(
            "location_1_room_2",
            "location_1_room_0023",
            roomDescription,
            balance
        );
    }

    public void RedButtonPressed()
    {
        Debug.Log("send RED event");
        CreateRoom().Started(
            new Dictionary<string, object>{{"health", 400}},
            new Dictionary<string, object>{{"crit_hit", 0.65}});
    }

    public void GreenButtonPressed()
    {
        Debug.Log("send GREEN event");
        CreateRoom().Finished(
            new Dictionary<string, object>{{"health", 120}},
            new Dictionary<string, object>{{"crit_hit", 0.65}},
            new Dictionary<string, object>{{"play_time", 25.3}});
    }

    public void OrangeButtonPressed()
    {
        Debug.Log("send ORANGE event");
        CreateRoom().PlayerDied(
            new Dictionary<string, object>{{"health", 0}},
            new Dictionary<string, object>{{"crit_hit", 0.65}},
            new Dictionary<string, object>{{"play_time", 12.1}});
    }
}
