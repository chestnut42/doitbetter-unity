extern "C" {
     void _initializeWith(char * apiKEY);
     void _sendEvent(char * eventName, char * jsonString);
     void _enableLogging(bool loggingEnabled);
 }
