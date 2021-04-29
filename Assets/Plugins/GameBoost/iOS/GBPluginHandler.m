#import "GameBoosterSDK.h"

NSString * GBCreateNSStringFromUnity(const char * cStr) {
    if (cStr != NULL) {
        return [NSString stringWithUTF8String:cStr];
    } else {
        return nil;
    }
}

void _initializeWith(const char * apiKEY) {
    [GameBoosterSDK initializeWithApiKey:GBCreateNSStringFromUnity(apiKEY)];
}

void _sendEvent(const char * eventName, const char * jsonString, const char * deduplicateId) {
    [GameBoosterSDK sendEventWithName:GBCreateNSStringFromUnity(eventName)
                           jsonString:GBCreateNSStringFromUnity(jsonString)
                        deduplicateID:GBCreateNSStringFromUnity(deduplicateId)];
}

void _enableLogging(bool loggingEnabled) {
    [GameBoosterSDK enableLogging:loggingEnabled ? YES : NO];
}
