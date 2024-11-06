using System.Text;

namespace YoutubeDownloader.Services.Database;

public class DbContextService<T> : IDbContextService<T> where T : class, IEntity, new()
{
	private readonly DBSettings dBSettings;
	private SQLiteAsyncConnection context;

	public DbContextService(DBSettings dBSettings)
	{
		this.dBSettings = dBSettings;
	}

	private async Task InitDB()
	{
		if (context is not null)
			return;

		var flags = SQLiteOpenFlags.ReadWrite |  // open the database in read/write mode
					SQLiteOpenFlags.Create |     // create the database if it doesn't exist
					SQLiteOpenFlags.SharedCache; // enable multi-threaded database access

		var dpPath = Path.Combine(FileSystem.AppDataDirectory, dBSettings.DatabaseFilename);

		this.context = new SQLiteAsyncConnection(dpPath, flags);
		await context.CreateTableAsync<SqlRecord>();
	}

	public async Task<bool> SaveAsync(IEntity item)
	{
		if (item == null) return false;

		await InitDB();

		var result = await context.InsertAsync(new SqlRecord(item));

		return result > 0;
	}

	public async Task<bool> UpdateAsync(IEntity item)
	{
		if (item == null) return false;

		await InitDB();

		var result = await context.UpdateAsync(new SqlRecord(item));

		return result > 0;
	}

	public async Task<bool> UpdateifExistsAsync(IEntity item)
	{
		if (item == null || item.Data == null || string.IsNullOrEmpty(item.Data.Id)) return false;

		await InitDB();

		var project = await GetAsync(item.Data.Id);

		if (project != null)
		{
			return await UpdateAsync(item);
		}
		return false;
	}

	public async Task<bool> UpdateOrSaveAsync(IEntity item)
	{
		if (item == null || item.Data == null || string.IsNullOrEmpty(item.Data.Id)) return false;

		await InitDB();

		var project = await GetAsync(item.Data.Id);

		if (project == null)
		{
			return await SaveAsync(item);
		}
		else
		{
			return await UpdateAsync(item);
		}
	}

	public async Task<T> GetAsync(string id) where T : class
	{
		await InitDB();

		var record = await context.Table<SqlRecord>()
								  .FirstOrDefaultAsync(x => x.Id == id);

		if (record is null || string.IsNullOrEmpty(record.JsonContent))
			return null;

		using var stream = new MemoryStream(Encoding.UTF8.GetBytes(record.JsonContent));
		var project = await System.Text.Json.JsonSerializer.DeserializeAsync<ProjectEntity>(stream);

		return project;
	}

	public async Task<List<ProjectEntity>> GetItemsAsync()
	{
		var data = new List<ProjectEntity>();

		await InitDB();

		var records = await context.Table<SqlRecord>().ToListAsync();

		if (!records.Any())
			return data;

		var settings = new JsonSerializerSettings
		{
			MissingMemberHandling = MissingMemberHandling.Error
		};

		foreach (var record in records)
		{
			using var stream = new MemoryStream(Encoding.UTF8.GetBytes(record.JsonContent));
			try
			{
				var project = JsonConvert.DeserializeObject<ProjectEntity>(record.JsonContent, settings);
				data.Add(project);
			}
			catch (JsonSerializationException ex)
			{
				Console.WriteLine($"JSON serialization error: {ex.Message}");
			}
		}

		return data;
	}

	public async Task<bool> DeleteAsync(ProjectEntity item)
	{
		await InitDB();

		var deletedObjectsCount = await context.DeleteAsync<SqlRecord>(item.Data.Id);

		return deletedObjectsCount > 0;
	}
}
