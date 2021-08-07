#import "GameBoosterSDK.h"
#import "GBPluginHandler.h"

NSString * GBCreateNSStringFromUnity(const char * cStr) {
    if (cStr != NULL) {
        return [NSString stringWithUTF8String:cStr];
    } else {
        return nil;
    }
}

static MessageBusBlock messageBusBlockWrapper = ^void (NSString * _Nonnull type, NSString * _Nonnull content) {};

void _initializeWith(const char * apiKEY, MessageBusCallback busCallback) {
    messageBusBlockWrapper = ^void (NSString * _Nonnull type, NSString * _Nonnull content) {
        busCallback([type cStringUsingEncoding:NSUTF8StringEncoding], [content cStringUsingEncoding:NSUTF8StringEncoding]);
    };

    [GameBoosterSDK initializeWithApiKey:GBCreateNSStringFromUnity(apiKEY) messageBus: messageBusBlockWrapper];
}

void _sendEvent(const char * eventName, const char * jsonString, const char * deduplicateId) {
    [GameBoosterSDK sendEventWithName:GBCreateNSStringFromUnity(eventName)
                           jsonString:GBCreateNSStringFromUnity(jsonString)
                        deduplicateID:GBCreateNSStringFromUnity(deduplicateId)];
}

void _enableLogging(bool loggingEnabled) {
    [GameBoosterSDK enableLogging:loggingEnabled ? YES : NO];
}