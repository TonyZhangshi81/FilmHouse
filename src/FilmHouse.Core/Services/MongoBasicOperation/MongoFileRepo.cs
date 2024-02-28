using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FilmHouse.Core.Services.MongoBasicOperation;

/// <summary>
/// MongoFile的基本操作
/// </summary>
public class MongoFileRepo : IMongoFileRepo
{
    private IMongoClient _client;
    private IMongoDatabase _database;
    private readonly IGridFSBucket _bucket;

    /// <summary>
    /// 初始化并实施配置信息依赖注入
    /// </summary>
    /// <param name="options">配置信息</param>
    public MongoFileRepo(IOptions<MongoDBContextOptions> options)
    {
        MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(options.Value.Connection));
        settings.Credential = MongoCredential.CreateCredential(options.Value.DatabaseName, options.Value.UserName, options.Value.Password);
        settings.ConnectTimeout = TimeSpan.FromSeconds(10);
        settings.MaxConnectionPoolSize = 100;

        this._client = new MongoClient(settings);
        this._database = this._client.GetDatabase(options.Value.DatabaseName);
        this._bucket = new GridFSBucket(this._database, new GridFSBucketOptions
        {
            BucketName = options.Value.BucketName,
            // 设置块的大小
            ChunkSizeBytes = options.Value.ChunkSizeBytes,
            // 写入确认级别为majority
            WriteConcern = WriteConcern.WMajority,
            // 优先从从节点读取
            ReadPreference = ReadPreference.Secondary
        });
    }

    /// <summary>
    /// BsonObjectId转换
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ObjectId GetInternalId(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId internalId))
        {
            internalId = ObjectId.Empty;
        }

        return internalId;
    }

    /// <summary>
    /// 指定ID获取的文件
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<GridFSFileInfo> GetFileByIdAsync(string id)
    {
        var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", GetInternalId(id));
        return await _bucket.Find(filter).FirstOrDefaultAsync();
    }

    /// <summary>
    /// 指定ID获取的文件
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<GridFSFileInfo> GetFileByIdAsync(ObjectId id)
    {
        var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", id);
        return await _bucket.Find(filter).FirstOrDefaultAsync();
    }

    /// <summary>
    /// 文件上传
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="source">文件IO</param>
    /// <returns></returns>
    public async Task<ObjectId> UploadFileAsync(string fileName, Stream source)
    {
        var id = await _bucket.UploadFromStreamAsync(fileName, source);
        return id;
    }

    /// <summary>
    /// 文件上传
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="source">字节信息</param>
    /// <returns></returns>
    public async Task<ObjectId> UploadFileAsync(string fileName, byte[] source)
    {
        var id = await _bucket.UploadFromBytesAsync(fileName, source);
        return id;
    }

    /// <summary>
    /// 文件下载（指定ID）
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <returns></returns>
    public async Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamSeekableAsync(string id)
    {
        var options = new GridFSDownloadOptions
        {
            Seekable = true
        };
        return await _bucket.OpenDownloadStreamAsync(GetInternalId(id), options);
    }

    /// <summary>
    /// 文件下载（指定ID）
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <returns></returns>
    public async Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamSeekableAsync(ObjectId id)
    {
        var options = new GridFSDownloadOptions
        {
            Seekable = true
        };
        return await _bucket.OpenDownloadStreamAsync(id, options);
    }


    /// <summary>
    /// 文件下载（指定文件名）
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public async Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamByNameSeekableAsync(string fileName)
    {
        var options = new GridFSDownloadByNameOptions
        {
            Seekable = true
        };
        return await _bucket.OpenDownloadStreamByNameAsync(fileName, options);
    }

    /// <summary>
    /// 文件下载（指定ID）
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <returns></returns>
    public async Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(string id)
    {
        return await _bucket.OpenDownloadStreamAsync(GetInternalId(id));
    }

    /// <summary>
    /// 文件下载（指定ID）
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <returns></returns>
    public async Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(ObjectId id)
    {
        return await _bucket.OpenDownloadStreamAsync(id);
    }

    /// <summary>
    /// 文件下载（指定文件名）
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public async Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamByNameAsync(string fileName)
    {
        return await _bucket.OpenDownloadStreamByNameAsync(fileName);
    }

    /// <summary>
    /// 文件删除（指定ID）
    /// </summary>
    /// <param name="id">ObjectId</param>
    public async Task DeleteFileAsync(string id)
    {
        await _bucket.DeleteAsync(GetInternalId(id));
    }

    /// <summary>
    /// 文件删除（指定ID）
    /// </summary>
    /// <param name="id">ObjectId</param>
    public async Task DeleteFileAsync(ObjectId id)
    {
        await _bucket.DeleteAsync(id);
    }

    /// <summary>
    /// 文件文件改名
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <param name="fileName">新文件名</param>
    public async Task RenameFileAsync(string id, string fileName)
    {
        await _bucket.RenameAsync(GetInternalId(id), fileName);
    }

    /// <summary>
    /// 文件文件改名
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <param name="fileName">新文件名</param>
    public async Task RenameFileAsync(ObjectId id, string fileName)
    {
        await _bucket.RenameAsync(id, fileName);
    }
}