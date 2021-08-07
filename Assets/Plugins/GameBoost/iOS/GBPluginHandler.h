#ifndef GBPluginHandler_Public_h
#define GBPluginHandler_Public_h

#ifdef __cplusplus
    extern "C" {
#endif
         typedef void (*MessageBusCallback)(const char *, const char *);

         void _initializeWith(const char * apiKEY, MessageBusCallback busCallback);
         void _sendEvent(const char * eventName, const char * jsonString, const char * deduplicateId);
         void _enableLogging(bool loggingEnabled);
#ifdef __cplusplus
    }
#endif

#endif /* GameBoosterSDK_Public_h */
