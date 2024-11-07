namespace YoutubeDownloader.Services.Database;

public interface IDbContextService<T> where T : class, IEntity, new()
{
	Task<bool> DeleteAsync(IEntity item);
	Task<T> GetAsync(string id);
	Task<List<T>> GetItemsAsync();
	Task<bool> SaveAsync(IEntity item);
	Task<bool> UpdateAsync(IEntity item);
	Task<bool> UpdateifExistsAsync(IEntity item);
}