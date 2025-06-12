namespace ZipCodeApi.Models
{
    public class ZipCode
{
    // Primary key
    public int Id { get; set; }

    // Basic zip code info
    public string? Zip { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? County { get; set; }
    
    // Geolocation
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    
    // Source ID for origin tracking
    public int? SourceId { get; set; }
}

}