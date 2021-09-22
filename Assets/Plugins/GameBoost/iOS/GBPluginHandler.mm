#import "GameBoosterSDK.h"
#import "GBPluginHandler.h"

NSString * GBCreateNSStringFromUnity(const char * cStr) {
    if (cStr != NULL) {
        return [NSString stringWithUTF8String:cStr];
    } else {
        return nil;
    }
}

void _initializeWith(const char * apiKEY, MessageBusCallback busCallback) {
    MessageBusBlock messageBusBlockWrapper = ^void (NSString * _Nonnull type, NSString * _Nonnull content) {
        busCallback([type cStringUsingEncoding:NSUTF8StringEncoding], [content cStringUsingEncoding:NSUTF8StringEncoding]);
    };

    [GameBoosterSDK initializeWithApiKey:GBCreateNSStringFromUnity(apiKEY) messageBus: messageBusBlockWrapper];
}

void _sendEvent(const char * eventName, const char * jsonString, const char * deduplicateId) {
    [GameBoosterSDK sendEventWithName:GBCreateNSStringFromUnity(eventName)
                           jsonString:GBCreateNSStringFromUnity(jsonString)
                        deduplicateID:GBCreateNSStringFromUnity(deduplicateId)];
}

 void _level(const char * room_number, const char * callbackID, MethodCallback callback) {
    NSString* nscallbackID = GBCreateNSStringFromUnity(callbackID);
    ContentBlock contentBlockWrapper = ^void (NSString * _Nullable content) {
        callback([nscallbackID cStringUsingEncoding:NSUTF8StringEncoding],[content cStringUsingEncoding:NSUTF8StringEncoding]);
    };

    [GameBoosterSDK levelFor: GBCreateNSStringFromUnity(room_number)
                    callback: contentBlockWrapper];
 }
  
 void _abilities(const char * room_number, const char * reason, const char * callbackID, MethodCallback callback) {
     NSString* nscallbackID = GBCreateNSStringFromUnity(callbackID);
     ContentBlock contentBlockWrapper = ^void (NSString * _Nullable content) {
         callback([nscallbackID cStringUsingEncoding:NSUTF8StringEncoding],[content cStringUsingEncoding:NSUTF8StringEncoding]);
     };
 
    [GameBoosterSDK abilitiesFor: GBCreateNSStringFromUnity(room_number)
                          reason: GBCreateNSStringFromUnity(reason)
                        callback: contentBlockWrapper];
 }
 
bool _isNeedToProcess(const char * hashValue) {
    return [GameBoosterSDK isNeedToProcess: GBCreateNSStringFromUnity(hashValue)];
}

void _addKeyHash(const char * keyValue, const char * hashValue, const char * type) {
    [GameBoosterSDK addKey:GBCreateNSStringFromUnity(keyValue)
                      hash:GBCreateNSStringFromUnity(hashValue)
                      with:GBCreateNSStringFromUnity(type)]; 
} 

void _enableLogging(bool loggingEnabled) {
    [GameBoosterSDK enableLogging:loggingEnabled ? YES : NO];
}
