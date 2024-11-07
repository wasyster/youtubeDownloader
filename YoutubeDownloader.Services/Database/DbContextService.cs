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
		if (item == null || string.IsNullOrEmpty(item.Id)) return false;

		await InitDB();

		var project = await GetAsync(item.Id);

		if (project != null)
		{
			return await UpdateAsync(item);
		}
		return false;
	}

	public async Task<bool> UpdateOrSaveAsync(IEntity item)
	{
		if (item == null || string.IsNullOrEmpty(item.Id)) return false;

		await InitDB();

		var record = await GetAsync(item.Id);

		if (record == null)
		{
			return await SaveAsync(record);
		}
		else
		{
			return await UpdateAsync(record);
		}
	}

	public async Task<T> GetAsync(string id)
	{
		await InitDB();

		var record = await context.Table<SqlRecord>()
								  .FirstOrDefaultAsync(x => x.Id == id);

		if (record is null || string.IsNullOrEmpty(record.JsonContent))
			return null;

		using var stream = new MemoryStream(Encoding.UTF8.GetBytes(record.JsonContent));
		var item = await JsonSerializer.DeserializeAsync<T>(stream);

		return item;
	}

	public async Task<List<T>> GetItemsAsync()
	{
		var data = new List<T>();

		await InitDB();

		var records = await context.Table<SqlRecord>().ToListAsync();

		if (records.Count == 0)
		{
			return data;
		}

		foreach (var record in records)
		{
			using var stream = new MemoryStream(Encoding.UTF8.GetBytes(record.JsonContent));
            var item = await JsonSerializer.DeserializeAsync<T>(stream);
			data.Add(item);
		}

		return data;
	}

	public async Task<bool> DeleteAsync(IEntity item)
	{
		await InitDB();

		var deletedObjectsCount = await context.DeleteAsync<SqlRecord>(item.Id);

		return deletedObjectsCount > 0;
	}
}
