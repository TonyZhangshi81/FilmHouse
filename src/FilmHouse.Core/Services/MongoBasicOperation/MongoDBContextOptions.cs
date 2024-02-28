namespace FilmHouse.Core.Services.MongoBasicOperation;

/// <summary>
/// 
/// </summary>
public class MongoDBContextOptions
{
    /// <summary>
    /// 连接MongoDB字符串
    /// </summary>
    public string Connection { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// 连接对应的数据库名称--MongoDB需要指定连接的数据库
    /// </summary>
    public string DatabaseName { get; set; }
    /// <summary>
    /// 桶名
    /// </summary>
    public string BucketName { get; set; }

    /// <summary>
    /// 设置块的大小
    /// </summary>
    public int ChunkSizeBytes { get; set; }
}
