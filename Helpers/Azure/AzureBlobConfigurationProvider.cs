using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CS.Base.Helpers.Azure
{
    public class AzureBlobConfigurationProvider : ConfigurationProvider
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly string _blobName;

        public AzureBlobConfigurationProvider(
            string connectionString,
            string containerName,
            string blobName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
            _blobName = blobName;
        }

        public override void Load()
        {
            var container = new BlobContainerClient(
                _connectionString,
                _containerName);

            var blob = container.GetBlobClient(_blobName);

            using var stream = blob.OpenRead();
            using var doc = JsonDocument.Parse(stream);

            Data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

            Flatten(doc.RootElement, parentPath: null);
        }

        private void Flatten(JsonElement element, string? parentPath)
        {
            foreach (var prop in element.EnumerateObject())
            {
                var key = parentPath == null
                    ? prop.Name
                    : $"{parentPath}:{prop.Name}";

                if (prop.Value.ValueKind == JsonValueKind.Object)
                {
                    Flatten(prop.Value, key);
                }
                else
                {
                    Data[key] = prop.Value.ToString();
                }
            }
        }
    }
}
