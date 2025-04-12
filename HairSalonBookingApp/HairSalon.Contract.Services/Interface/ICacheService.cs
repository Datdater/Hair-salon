namespace HairSalon.Contract.Services.Interface
{
	public interface ICacheService
	{
		Task<T?> GetListAsync<T>(string key);
		Task SetAsync<T>(string key, T value, TimeSpan expiration, string prefix);
		Task RemoveByPrefixAsync(string prefix);

    }
}
