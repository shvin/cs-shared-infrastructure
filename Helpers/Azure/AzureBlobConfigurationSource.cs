using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Base.Helpers.Azure
{
    public class AzureBlobConfigurationSource : IConfigurationSource
    {
        public required string ConnectionString { get; init; }
        public required string ContainerName { get; init; }
        public required string BlobName { get; init; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
            => new AzureBlobConfigurationProvider(
                ConnectionString,
                ContainerName,
                BlobName);
    }
}
