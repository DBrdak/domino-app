namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }

        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }
    }
}