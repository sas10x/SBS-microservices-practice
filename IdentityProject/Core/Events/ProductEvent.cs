namespace Core.Events
{
    public class ProductEvent
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}