namespace YoutubeDownloader.Services.Database;

public interface IDbContextService<T> where T : class, IEntity, new()
{
	Task<bool> DeleteAsync(IEntity item);
	Task<T> GetAsync(string id);
	Task<List<T>> GetItemsAsync();
	Task<bool> CreateAsync(IEntity item);
	Task<bool> UpdateAsync(IEntity item);
	Task<bool> UpdateIfExistsAsync(IEntity item);
    Task<bool> CreateOrUpdateIfExistsAsync(IEntity item);
}