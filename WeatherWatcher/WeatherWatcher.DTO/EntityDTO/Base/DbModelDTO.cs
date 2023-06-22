namespace WeatherWatcher.DTO.EntityDTO.Base
{
    public abstract class DbModelDTO
    {
        public long Id { get; set; }
        public DateTime AddedTime { get; set; }
    }
}
