namespace Plugins.GameBoost
{
    internal interface ISettings
    {
        void SetLoggingEnabled(bool isLoggingEnabled);
        void MarkAsDevelopment();
    }
}
