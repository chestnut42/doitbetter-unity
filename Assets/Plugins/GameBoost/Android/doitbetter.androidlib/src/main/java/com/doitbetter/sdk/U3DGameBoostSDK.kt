package com.doitbetter.sdk.u3d

import com.doitbetter.sdk.GameBoostSDK
import com.unity3d.player.UnityPlayer

class U3DGameBoostSDK {
    companion object {

        @JvmOverloads
        @JvmStatic
        fun initialize(apiKEY: String) {
            UnityPlayer.currentActivity?.let {
                GameBoostSDK.initialize(it.application, apiKEY)
            }
        }

        @JvmOverloads
        @JvmStatic
        fun sendEvent(eventName: String, jsonString: String? = null, deduplicateID: String? = null) {
            GameBoostSDK.sendEvent(eventName, jsonString, deduplicateID)
        }

        @JvmOverloads
        @JvmStatic
        fun enableLogging(enabled: Boolean) {
            GameBoostSDK.enableLogging(enabled)
        }

        @JvmOverloads
        @JvmStatic
        fun version(): String {
            return GameBoostSDK.version()
        }
    }
}