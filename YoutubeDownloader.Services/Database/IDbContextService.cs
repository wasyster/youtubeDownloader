namespace YoutubeDownloader.Services.Database;

public interface IDbContextService<T> where T : class, IEntity, new()
{
	Task<bool> DeleteAsync(ProjectEntity item);
	Task<ProjectEntity> GetAsync(string id);
	Task<List<ProjectEntity>> GetItemsAsync();
	Task<bool> SaveAsync(ProjectEntity item);
	Task<bool> UpdateAsync(ProjectEntity item);
	Task<bool> UpdateifExistsAsync(ProjectEntity item);
}