#ifndef GameBoosterSDK_Public_h
#define GameBoosterSDK_Public_h

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (^MessageBusBlock)(NSString * _Nonnull, NSString * _Nonnull);

@interface GameBoosterSDK : NSObject
- (instancetype)init NS_UNAVAILABLE;
+ (instancetype)new NS_UNAVAILABLE;

+ (void)initializeWithApiKey:(NSString * _Nonnull)apiKEY
                  messageBus:(MessageBusBlock)messageBus;

+ (void)sendEventWithName:(NSString * _Nonnull)eventName
               jsonString:(NSString * _Nullable)jsonString
            deduplicateID:(NSString * _Nullable)deduplicateID;

+ (void)enableLogging:(BOOL) loggingEnabled;

+ (NSString *)version;

@end

NS_ASSUME_NONNULL_END

#endif /* GameBoosterSDK_Public_h */
