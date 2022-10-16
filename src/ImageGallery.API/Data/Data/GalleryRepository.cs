using ImageGallery.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.API.Data.Data
{
    public interface IGalleryRepository
    {
        Task<IEnumerable<Image>> GetImagesAsync(string ownerId);
        Task<bool> IsImageOwnerAsync(Guid id, string ownerId);
        Task<Image?> GetImageAsync(Guid id);
        Task<bool> ImageExistsAsync(Guid id);
        void AddImage(Image image);
        void UpdateImage(Image image);
        void DeleteImage(Image image);
        Task<bool> SaveChangesAsync();
    }

    public class GalleryRepository : IGalleryRepository
    {
        private readonly GalleryContext _context;

        public GalleryRepository(GalleryContext galleryContext)
        {
            _context = galleryContext ??
                throw new ArgumentNullException(nameof(galleryContext));
        }

        public async Task<bool> ImageExistsAsync(Guid id)
        {
            return await _context.Images.AnyAsync(lbda => lbda.Id == id);
        }

        public async Task<Image?> GetImageAsync(Guid id)
        {
            return await _context.Images.FirstOrDefaultAsync(lbda => lbda.Id == id);
        }

        public async Task<IEnumerable<Image>> GetImagesAsync(string ownerId)
        {
            return await _context.Images
                .Where(lbda => lbda.OwnerId == ownerId)
                .OrderBy(lbda => lbda.Title).ToListAsync();
        }

        public async Task<bool> IsImageOwnerAsync(Guid id, string ownerId)
        {
            return await _context.Images
                .AnyAsync(lbda => lbda.Id == id && lbda.OwnerId == ownerId);
        }

        public void AddImage(Image image)
        {
            _context.Images.Add(image);
        }

        public void UpdateImage(Image image) { }

        public void DeleteImage(Image image)
        {
            _context.Images.Remove(image);
            //No cenario real, a imagem deve ser deletada do disco/servidor
            //em que esta sendo armazenada
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}