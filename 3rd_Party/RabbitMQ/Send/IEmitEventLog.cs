namespace EmitEvent
{
    public interface IEmitEventLog
    {
        void EventPublish(string eventName, string message);
    }
}
