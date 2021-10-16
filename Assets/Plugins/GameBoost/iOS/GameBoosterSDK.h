#ifndef GameBoosterSDK_Public_h
#define GameBoosterSDK_Public_h

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN


typedef void (^MessageBusBlock)(NSString * _Nonnull, NSString * _Nonnull);
typedef void (^ContentBlock)(NSString* _Nullable);

@interface GameBoosterSDK : NSObject
- (instancetype)init NS_UNAVAILABLE;
+ (instancetype)new NS_UNAVAILABLE;

+ (void)initializeWithApiKey:(NSString * _Nonnull)apiKEY
                  messageBus:(MessageBusBlock)messageBus;

+ (void)sendEventWithName:(NSString * _Nonnull)eventName
               jsonString:(NSString * _Nullable)jsonString
            deduplicateID:(NSString * _Nullable)deduplicateID;

+ (void) levelFor:(NSString * _Nonnull)room_number
         callback:(ContentBlock) callback;

+ (void)abilitiesFor:(NSString * _Nonnull) room_number
              reason:(NSString * _Nonnull) reason
            callback:(ContentBlock) callback;

+(BOOL) isNeedToProcess:(NSString*)hashValue;

+(void) addKey:(NSString*)key
          hash:(NSString*)hash;

+ (void)enableLogging:(BOOL) loggingEnabled;

+ (NSString *)version;

@end

NS_ASSUME_NONNULL_END

#endif /* GameBoosterSDK_Public_h */
