namespace QuoteAPI.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public required string firstName { get; set; }
        public required string? lastName { get; set; }
        public required string? quote { get; set; }
        public required string? image {  get; set; }
        public required string? anime { get; set; }
    }
}
