using FilmHouse.Core.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.Core.Services.HttpClients
{
    /// <summary>
    /// 是用于接收GET请求的QueryString的对象。
    /// </summary>
    public class GetRequestModel
    {
        [Required]
        [FromQuery(Name = "d")]
        public string Data { get; set; }
    }
}
