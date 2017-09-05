namespace AwtApplication.Services
{
    public interface IServiceStarter
    {
        void StartServices();
        void TriggerBackgroundRunManually( bool _AlsoOnAndroid = false );
    }

}
