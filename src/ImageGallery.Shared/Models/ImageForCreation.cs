using System.ComponentModel.DataAnnotations;

namespace ImageGallery.Shared.Models
{
    public class ImageForCreation
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public byte[] Bytes { get; set; }

        public ImageForCreation(string title, byte[] bytes)
        {
            Title = title;
            Bytes = bytes;
        }
    }
}