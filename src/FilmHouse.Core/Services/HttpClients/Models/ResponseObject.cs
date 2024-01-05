using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Web;
using Isid.Ilex.Core.Domain.DataAnnotations;

namespace Isid.Ilex.Core.Domain.Services.HttpClients.Models
{
    /// <summary>
    /// WebAPIから返すレスポンスの型を定義するクラスです。
    /// </summary>
    /// <typeparam name="TMetadata">メタデータの型</typeparam>
    /// <typeparam name="TResponse">レスポンスの型</typeparam>
    public class ResponseObject<TMetadata, TResponse>
        where TMetadata : IResponseMetadata
        where TResponse : class
    {
        /// <summary>
        /// メタデータを設定または取得します。
        /// </summary>
        [Required(NeedsFullMessage = true)]
        [JsonPropertyName("metadata")]
        public TMetadata? Metadata { get; set; }

        /// <summary>
        /// レスポンスを設定または取得します。
        /// </summary>
        [JsonPropertyName("response")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TResponse? Response { get; set; }
    }
}