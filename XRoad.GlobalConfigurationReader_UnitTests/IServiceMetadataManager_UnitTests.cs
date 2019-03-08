using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XRoad.Configuration;

namespace XRoad.GlobalConfigurationReader_UnitTests
{
    [TestClass]
    public class IServiceMetadataManager_UnitTests
    {
        [TestMethod]
        public async System.Threading.Tasks.Task GetSharedParamsAsync_When__Async()
        {
            var manager =  new ServiceMetadataManager();
            var result = await manager.GetSharedParamsAsync(new Uri("http://10.55.0.4"));

            Assert.IsTrue(result.InstanceIdentifier.Equals("central-server"));
        }
    }
}
