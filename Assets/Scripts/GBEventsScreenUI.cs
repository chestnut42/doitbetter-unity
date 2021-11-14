using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.GameBoost;

public class GBEventsScreenUI : MonoBehaviour
{
    public string APIKey;
    public UnityEngine.UI.Text OutputText;

    private void Awake()
    {
        GameBoostSDK.SetLoggingEnabled(true);
        GameBoostSDK.Events.sandboxStatus += status =>
        {
            OutputText.text = status.ToString();
            Debug.Log($"sandboxStatus == {status} ");
        };

        GameBoostSDK.Events.boostEnabledStatus += status =>
        {
            OutputText.text = status.ToString();
            Debug.Log($"boostEnabledStatus == {status} ");
        };

        GameBoostSDK.Initialize(APIKey);
    }


    public IGame CreateGame()
    {
        var balance = new Dictionary<string, object>();
        var health = 2000;
        balance["enemy_042"] = new Dictionary<string, object> { {"health", health}, {"color", "green"} };

        return GameBoostSDK.CreateGame(balance);
    }

    public IArcheroRoom CreateRoom()
    {
        var roomDescription = new Dictionary<string, object>();
        var index = 42;
        roomDescription["Enemies"] = new List<string> {$"enemy_0{index}"};

        return CreateGame()
            .CreateArcheroRoom(
                "location_1_room_2",
                "location_1_room_0023",
                roomDescription
            );
    }

    public void RedButtonPressed()
    {
        Debug.Log("send RED event");
        var game = CreateGame();
        StartCoroutine(runTests(game));
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
        GameBoostSDK.TrackRevenue(0.01, "BYN", source: "ad");
        GameBoostSDK.MarkAsDevelopment();
    }

    IEnumerator runTests(IGame room)
    {
        Debug.Log($"***************");
        Debug.Log($"testCoroutines");
        Debug.Log($"***************");
        yield return testCoroutines(room);

        Debug.Log($"***************");
        Debug.Log($"testCallbacks");
        Debug.Log($"***************");
        testCallbacks(room);
    }

    IEnumerator testCoroutines(IGame game)
    {
        var abRequest = game.AbilitiesRequest("location_1_room_2", "lvlup");

        Debug.Log($"RedButtonPressed() Coroutine 1 - start");
        yield return abRequest.Run();
        var abResult = (abRequest.CommandResult == null) ? "null" : $"{abRequest.CommandResult.abilities}";
        Debug.Log($"abRequest.IsDone == {abRequest.IsDone} with Abilities = {abResult}");
        Debug.Log($"RedButtonPressed() Coroutine 1 - end");


        var leveRequest = game.LevelRequest("location_1_room_2");

        Debug.Log($"RedButtonPressed() Coroutine 2 - start");
        yield return leveRequest.Run();

        if (leveRequest.CommandResult != null)
        {
            Debug.Log($"leveRequest.IsDone == {leveRequest.IsDone} with DynBalance = {leveRequest.CommandResult.DynBalance} RoomName = {leveRequest.CommandResult.RoomName}");
        }
        else
        {
            Debug.Log($"leveRequest.IsDone == {leveRequest.IsDone} with null responce");
        }
        Debug.Log($"RedButtonPressed() Coroutine 2 - end");
    }

    void testCallbacks(IGame game)
    {
        Debug.Log($"__room.Abilities - start");
        game.Abilities( "location_1_room_2","lvlup", data =>
        {
            if (data!=null)
            {
                Debug.Log($"__Abilities( {data.abilities} )");
            }
            else
            {
                Debug.Log($"__Abilities( null )");
            }
            Debug.Log($"__room.Abilities - end");
        });

        Debug.Log($"____room.Level - start");
        game.Level("location_1_room_2", data =>
        {
            if (data != null)
            {
                Debug.Log($"____Level( DynBalance = {data.DynBalance} RoomName = {data.RoomName} )");
            }
            else
            {
                Debug.Log($"____Level( null )");
            }

            Debug.Log($"____room.Level - end");
        });
    }
}
