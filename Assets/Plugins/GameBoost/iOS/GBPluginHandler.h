#ifndef GBPluginHandler_Public_h
#define GBPluginHandler_Public_h

#ifdef __cplusplus
    extern "C" {
#endif
         typedef void (*MessageBusCallback)(const char *, const char *);
         typedef void (*MethodCallback)(const char *, const char *);

         void _initializeWith(const char * apiKEY, MessageBusCallback busCallback);
         void _sendEvent(const char * eventName, const char * jsonString, const char * deduplicateId);
                 
         void _level(const char * room_name, const char * callbackID, MethodCallback callback);
         void _abilities(const char * room_name, const char * reason, const char * callbackID, MethodCallback callback);
                 
         bool _isNeedToProcess(const char * hashValue);
         void _addKeyHash(const char * keyValue, const char * hashValue, const char * type);
                 
         void _enableLogging(bool loggingEnabled);
#ifdef __cplusplus
    }
#endif

#endif /* GameBoosterSDK_Public_h */
