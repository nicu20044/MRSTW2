using System;
using System.Web;

public class ProductDTO
{
    public string Name { get; set; }
    public int Id { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public int Bpm { get; set; }
    public string Scale { get; set; }
    public string License { get; set; }
    public string ArtistName { get; set; }
    public int ArtistId { get; set; }

    public HttpPostedFileBase AudioFile { get; set; }    // aici e fișierul uploadat
    public string AudioFileUrl { get; set; }             // aici calea salvată în DB

    public HttpPostedFileBase ImageFile { get; set; }    // fișierul uploadat imagine
    public string ImageUrl { get; set; }                 // calea imaginii în DB

    public DateTime UploadDate { get; set; }
}