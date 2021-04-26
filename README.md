# Unity plugin for Do-It-Better SDK

## Installation

### Short
Download and open **.unitypackage** of the latest release.

### Long

If you feel a little adventurous today, do not hesitate to read through this repo. You then can just copy what you want in your project.

## Integration

As rule of thumb you can read through **.cs** files that located in the 
[root directory of the plugin](https://github.com/chestnut42/doitbetter-unity/tree/main/Assets/Plugins/GameBoost).
They are well documented (if not, we highly appreciate if you create an issue).

### Basic

After you get plugin files into your project you need to add initialization call.
```
GameBoostSDK.Initialize(APIKey);
```

It's strongly recommended to put this call as **early** as possible, i.e. right on your splash screen. Fancy that!
In fact, we make our best to make sure your game isn't affected by our library. We use background threads,
but keep their number short. We reuse TCP connections, relentlessly cache and do only what absolutely necessary.

Please do not hesitate to file an issue if your game performance is barbarously affected by this SDK.

### Logging

If you're about to ask some help, please run your game with this line on and attach the logs.
```
GameBoostSDK.SetLoggingEnabled(true);
```

### Game Types

We respect each game's personality. Carefully harvesting each event and putting it in the right bucket.
Different kind of Games need different treatment.

Here's the list of currently supported integrations:
* [Archero-style games](https://github.com/chestnut42/doitbetter-unity/blob/main/docs/archero-style.md)


### Data Object

Several APIs utilise concept of **Data Object**. **Data Object** is an object that contain any serializable data, including embedded **Data Objects**. In simple words it's the kind of data, that can be losslessly converted to JSON.

In **Unity** this object is a `Dictionary<string, object>`. Such as:
* **All** keys are `string`-s. That means **any** `Dictionary` on **any** level of data tree must have a `string` key type
* Supported number types are:
  * `int`, `uint`, `sbyte`, `byte`, `short`, `ushort`, `long`, `ulong`
  * `float`, `double`, `decimal`
* Any `object` that is not number and not a string will be called `ToString` and treated as a string
* Objects that implement `IList` and `IDictionary` will be treated as such collections
