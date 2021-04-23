#import "GameBoosterSDK.h"

void _initializeWith(char * apiKEY) {
    [GameBoosterSDK initializeWith:[NSString stringWithUTF8String:apiKEY] isProduction: YES];
}

void _sendEvent(char * eventName, char * jsonString) {
    [GameBoosterSDK send: [NSString stringWithUTF8String:eventName]
          withJsonString: [NSString stringWithUTF8String:jsonString]];
}

void _enableLogging(bool loggingEnabled) {
    [GameBoosterSDK enableLogging: loggingEnabled ? YES : NO];
}
