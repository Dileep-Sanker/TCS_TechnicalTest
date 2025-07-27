namespace TCSTest.Models
{
    public abstract class ScheduleBase
    {
        public Guid Id { get; set; }
        public Guid ChannelId { get; set; }
        public Guid ContentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
