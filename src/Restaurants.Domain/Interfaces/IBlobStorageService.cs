namespace Restaurants.Domain.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadToBlobAsync(Stream data, string fileNAme);
        string? GetBlobSasUrl(string? blobUrl);
    }
}
