using System.Threading.Tasks;
using CMS.Common.Caching.Redis;
using CMS.Common.Helpers.HttpClient.RestSharp;

namespace CMS.Integration.CacheRepository
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IRestSharpClient _restSharpClient;
        public CacheRepository(IRestSharpClient restSharpClient)
        {
            _restSharpClient = restSharpClient;
        }

        public async Task<object> GetEmployeeAsync(string nationalId)
        {
            var employee = RedisCacheHelper.GetT<object>(nationalId);
            return employee;
        }


    }
}
