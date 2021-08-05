using System.Collections.Generic;
using UnityEngine;
using Plugins.GameBoost;

public class GBEventsScreenUI : MonoBehaviour
{
    public string APIKey;
    public UnityEngine.UI.Text OutputText;
    
    private void Awake()
    {
        GameBoostSDK.Events.sandboxStatus += (status) =>
        {
            OutputText.text = status.ToString();
            Debug.Log($"sandboxStatus == {status} ");
        };
        GameBoostSDK.Initialize(APIKey);
        GameBoostSDK.SetLoggingEnabled(true);
    }

    public IArcheroRoom CreateRoom()
    {
        var roomDescription = new Dictionary<string, object>();
        roomDescription["Enemies"] = new List<string> {"enemy_042"};

        var balance = new Dictionary<string, object>();
        balance["enemy_042"] = new Dictionary<string, object> { {"health", 2000}, {"color", "green"} };

        return GameBoostSDK
            .CreateGame(balance)
            .CreateArcheroRoom(
                "location_1_room_2",
                "location_1_room_0023",
                roomDescription
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
        GameBoostSDK.TrackPurchase(0.99, "USD");
        GameBoostSDK.TrackPurchase(1.99, "EUR", "my_transaction_id");
        GameBoostSDK.TrackRevenue(0.01, "BYN");
        GameBoostSDK.MarkAsDevelopment();
    }
}
