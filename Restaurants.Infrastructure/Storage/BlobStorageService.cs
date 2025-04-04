using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configurations;

namespace Restaurants.Infrastructure.Storage
{
    internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
    {
        private readonly BlobStorageSettings _blobStorageSettingsOptions = blobStorageSettingsOptions.Value;

        public async Task<string> UploadToBlobAsync(Stream data, string fileNAme)
        {
            var blobServiceClient = new BlobServiceClient(_blobStorageSettingsOptions.ConnectionString);

            var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettingsOptions.LogosContainerName);

            var blobClient = containerClient.GetBlobClient(fileNAme);

            await blobClient.UploadAsync(data);

            var blobUrl = blobClient.Uri.ToString();

            return blobUrl;
        }

        public string? GetBlobSasUrl(string? blobUrl)
        {
            if (blobUrl is null)
            {
                return null;
            }

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _blobStorageSettingsOptions.LogosContainerName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
                BlobName = GetBlobNameFromUrl(blobUrl)
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            var blobServiceClient = new BlobServiceClient(_blobStorageSettingsOptions.ConnectionString);

            var sasToken = sasBuilder
                .ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettingsOptions.AccountKey))
                .ToString(); 

            return $"{blobUrl}?{sasToken}";
        }

        private string GetBlobNameFromUrl(string blobUrl)
        {
            var uri = new Uri(blobUrl);

            return uri.Segments.Last();
        }
    }
}
