using System.ComponentModel.DataAnnotations;

namespace ImageGallery.Shared.Models
{
    public class ImageForUpdate
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        public ImageForUpdate(string title)
        {
            Title = title;
        }
    }
}