using System;
using System.Threading.Tasks;
using XRoad.Configuration.Metadata;

namespace XRoad.Configuration
{
    public interface IServiceMetadataManager
    {
        Task<SharedParams> GetSharedParamsAsync(Uri securityServerUri);
    }
}
