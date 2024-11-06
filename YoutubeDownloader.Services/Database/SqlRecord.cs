namespace YoutubeDownloader.Services.Database;

public class SqlRecord
{
	[PrimaryKey]
	public string Id { get; set; }

	public string JsonContent { get; set; }

    public SqlRecord()
    {
        
    }
    public SqlRecord(IEntity entity)
	{
		this.Id = entity.Id;
		this.JsonContent = JsonSerializer.Serialize(entity);
	}
}
