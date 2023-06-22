namespace WeatherWatcher.Domain.Entities.Base
{
    public abstract class DbModel
    {
        public long Id { get; set; }
        public DateTime AddedTime { get; set; }
    }
}
