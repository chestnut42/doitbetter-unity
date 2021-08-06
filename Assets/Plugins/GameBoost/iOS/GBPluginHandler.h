#import "GameBoosterSDK.h"

extern "C" {
     void _initializeWith(const char * apiKEY, MessageBusCallback busCallback);
     void _sendEvent(const char * eventName, const char * jsonString, const char * deduplicateId);
     void _enableLogging(bool loggingEnabled);
 }
