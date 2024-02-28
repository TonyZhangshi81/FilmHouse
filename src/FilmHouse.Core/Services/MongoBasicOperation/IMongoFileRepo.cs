using FilmHouse.Core.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace FilmHouse.Core.Services.MongoBasicOperation;

/// <summary>
/// 
/// </summary>
[ServiceRegister(FilmHouseServiceLifetime.Scoped)]
public interface IMongoFileRepo
{
    /// <summary>
    /// BsonObjectId转换
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ObjectId GetInternalId(string id);

    /// <summary>
    /// 指定ID获取的文件
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<GridFSFileInfo> GetFileByIdAsync(string id);

    /// <summary>
    /// 指定ID获取的文件
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<GridFSFileInfo> GetFileByIdAsync(ObjectId id);

    /// <summary>
    /// 文件上传
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="source">文件IO</param>
    /// <returns></returns>
    Task<ObjectId> UploadFileAsync(string fileName, Stream source);

    /// <summary>
    /// 文件上传
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="source">字节信息</param>
    /// <returns></returns>
    Task<ObjectId> UploadFileAsync(string fileName, byte[] source);


    /// <summary>
    /// 文件下载
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <returns></returns>
    Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamSeekableAsync(string id);

    /// <summary>
    /// 文件下载
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <returns></returns>
    Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamSeekableAsync(ObjectId id);

    /// <summary>
    /// 文件下载（指定文件名）
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamByNameSeekableAsync(string fileName);

    /// <summary>
    /// 文件下载（指定ID）
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <returns></returns>
    Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(string id);

    /// <summary>
    /// 文件下载（指定ID）
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <returns></returns>
    Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(ObjectId id);

    /// <summary>
    /// 文件删除（指定ID）
    /// </summary>
    /// <param name="id">ObjectId</param>
    Task DeleteFileAsync(string id);

    /// <summary>
    /// 文件删除（指定ID）
    /// </summary>
    /// <param name="id">ObjectId</param>
    Task DeleteFileAsync(ObjectId id);

    /// <summary>
    /// 文件改名
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <param name="fileName">新文件名</param>
    Task RenameFileAsync(string id, string fileName);

    /// <summary>
    /// 文件改名
    /// </summary>
    /// <param name="id">ObjectId</param>
    /// <param name="fileName">新文件名</param>
    Task RenameFileAsync(ObjectId id, string fileName);

}
