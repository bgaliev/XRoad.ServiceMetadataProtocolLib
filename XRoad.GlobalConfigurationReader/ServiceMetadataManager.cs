using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XRoad.Configuration.Metadata;

namespace XRoad.Configuration
{
    public class ServiceMetadataManager : IServiceMetadataManager
    {
        public async Task<SharedParams> GetSharedParamsAsync(Uri securityServerUri)
        {
            var httpClient = new HttpClient();
            var verificationConfUri = new Uri(securityServerUri, "verificationconf");
            using (var httpStream = await httpClient.GetStreamAsync(verificationConfUri))
            {
                using (ZipArchive zipArchive = new ZipArchive(httpStream))
                {
                    string instanceIdentifier = await GetInstanceIdentifierAsync(zipArchive);
                    return ParseSharedParams(instanceIdentifier, zipArchive);
                }
            }
        }

        private async Task<string> GetInstanceIdentifierAsync(ZipArchive zipArchive)
        {
            ZipArchiveEntry zipEntry = zipArchive.GetEntry("verificationconf/instance-identifier");
            using (Stream instanceIdentiferStream = zipEntry.Open())
            {
                var reader = new StreamReader(instanceIdentiferStream);
                return await reader.ReadToEndAsync();
            }
        }

        private SharedParams ParseSharedParams(string instanceIdentifier, ZipArchive zipArchive)
        {
            ZipArchiveEntry zipEntry = zipArchive.GetEntry($"verificationconf/{instanceIdentifier}/shared-params.xml");
            using (Stream sharedParamsStream = zipEntry.Open())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SharedParams));
                return (SharedParams)xmlSerializer.Deserialize(sharedParamsStream);
            }
        }
    }
}
