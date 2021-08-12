# Archero style

## Create room

As a starting point for all events in archero style games, you need to create a room object:
```
IGame game = /* get shared singleton instance of IGame object */;
var room = game.CreateArcheroRoom(
  roomNumber,
  roomName,
  roomDescription,
  balance
);
```

### Room Number

This is the number of the room, that the player percieves. That is, the number of room, the number of level, the number of location etc. Whatever numbers/devisions you use to present content structure to the user. `roomNumber` is a string that should contain all that information.

For example, if you have several locations/settings in your game and each of such location consts of, say, 10 levels, where each of 10 levels constists of 20 rooms. Your room number can be `"2_5_15"` for 15th room in 5th level in 2nd location.

There's no any format restrictions to that string. There are some requirements for that string:
* `roomNumber` format must be consistent across game updates. e.g. if there was  a room with number "2_5_15", in all further updates this number will remain the same.
* Each `roomNumber` has only one version. There must not be any two room numbers corresponding to the same room.


### Room Name

This is the name of the actual room being used. That is, the name of actual configuration of enemies, obstacles, traps, etc. on the screen. This room name is usually the name of the file that contains such configuration.

`roomName` is tightly bound with `roomDescription`. That means it is used for caching of `roomDescription` during a signle game launch. For example:
1. SDK receives a `CreateArcheroRoom` call with `roomName` X and `roomDescription` N.
2. SDK receives a subsequent call to `CreateArcheroRoom` with `roomName` X.
3. SDK assumes, that this time `roomDescription` would be **exactly** the same as in the first call.

This correlation between `roomName` and `roomDescription` can, of cource, change between different app versions if you have modified your rooms.


### Room Description

`roomDescription` is a **Data Object** ([section Data Object](https://github.com/chestnut42/doitbetter-unity/blob/main/README.md)) that contains **all** information about given room.

Same recommendation as for `balance` data object are also valid for `roomDescription`. Those are: rarely modify its contents, avoid modifications of format and assume that some more data will be added in the future.


## Room object usage

After you successfully created a room object you can start sending events. It's recommended to utilise only one room object for a given room session to save some CPU. This object is used to create and cache a hash constructed of given data objects. However it's not strictly necessary. If you need so, you can create a room object for each event you're sending.

If the player died, and you are restarting the level instantly you're not required to create new room object.

In other words, room object serves caching purposes **only**. It does **not** contain any business logic and can be created/disposed as you find convenient to.


## Events

* **Started**
* **EnemiesKilled** - the enemies are killed in the room, but the player hasn't yet entered the gates
* **Finished** - the room finished, i.e. the player has won
* **PlayerDied**
* **PlayerResurrected**
* **PlayerChoseAbility**


## Event Parameters

All events have additional parameters to ones given at room creation.

### Player State


### Dynamic Balance


### Room PlayData


**TODO**:
- list and describe event parameters
