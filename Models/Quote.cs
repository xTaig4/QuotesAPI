namespace QuoteAPI.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string? LastName { get; set; }
        public required string? _Quote { get; set; }
        public required string? Image {  get; set; }
    }
}
