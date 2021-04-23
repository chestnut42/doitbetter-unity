#ifndef GameBoosterSDK_Public_h
#define GameBoosterSDK_Public_h

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface GameBoosterSDK : NSObject
- (instancetype)init NS_UNAVAILABLE;
+ (instancetype)new NS_UNAVAILABLE;

+(void) initializeWith:(NSString* _Nonnull)apiKEY isProduction: (BOOL) isProduction;

+(void) send:(NSString* _Nonnull)eventName withJsonString:(NSString* _Nullable) jsonString;

+(void) enableLogging:(BOOL) loggingEnabled;

+(NSString*) version;

@end

NS_ASSUME_NONNULL_END

#endif /* GameBoosterSDK_Public_h */
