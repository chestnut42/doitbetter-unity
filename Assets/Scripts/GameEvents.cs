using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GameEvents;
using UnityEngine;

namespace GameEvents
{
    public class TestParams
    {
        public static Dictionary<string, object> ParametersGreen()
        {
            return new Dictionary<string, object>() { {"GreenTime", DateTime.Now.ToLongTimeString()}, {"Green1", "sdfsd"}, {"Green2", "sdfsd"}, {"Green3", "sdfsd"}, {"Green4", "sdfsd"}, };
        }
        public static Dictionary<string, object> ParametersOrange()
        {
            return new Dictionary<string, object>() { {"OrangeTime", DateTime.Now.ToLongTimeString()},{"Orange1", "sdfsd"}, {"Orange2", "sdfsd"}, {"Orange3", "sdfsd"}, {"Orange4", "sdfsd"}, };
        }
        public static Dictionary<string, object> ParametersRed()
        {
            return new Dictionary<string, object>() { {"RedTime", DateTime.Now.ToLongTimeString()}, {"Red1", "sdfsd"}, {"Red2", "sdfsd"}, {"Red3", "sdfsd"}, {"Red4", "sdfsd"}, };
        }
    }
}
