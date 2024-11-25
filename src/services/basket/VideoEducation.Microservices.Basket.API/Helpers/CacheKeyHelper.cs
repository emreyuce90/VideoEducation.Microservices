using VideoEducation.Microservices.Basket.API.Constants;

namespace VideoEducation.Microservices.Basket.API.Helpers {
    public static class CacheKeyHelper {
        public static string GetCacheKey(Guid userId) {
            var cacheKey = CacheKey.GetCacheKey;
            return String.Format(cacheKey, userId.ToString());
        }
    }
}
