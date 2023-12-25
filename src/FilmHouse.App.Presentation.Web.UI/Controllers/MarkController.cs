using System.Security.Claims;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FilmHouse.App.Presentation.Web.UI.Controllers
{
    public class MarkController : Controller
    {
        #region Initizalize

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="httpContextAccessor"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public MarkController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
            this._httpContextAccessor = Guard.GetNotNull(httpContextAccessor, nameof(IHttpContextAccessor));
        }

        #endregion Initizalize

        //
        // GET: /Mark/Create/
        [Authorize]
        public ActionResult Create(Guid targetId, MarkTypeVO markType, string returnurl)
        {
            if (markType.AsPrimitive() <= MarkTypeVO.Codes.MarkTypeCode0.AsPrimitive() || markType.AsPrimitive() > MarkTypeVO.Codes.MarkTypeCode7.AsPrimitive())
            {
                return RedirectToAction("NotFound", "Error");
            }
            else
            {
                switch (markType)
                {
                    case MarkTypeVO.Codes.MarkTypeCode1:
                    case MarkTypeVO.Codes.MarkTypeCode2:
                    case MarkTypeVO.Codes.MarkTypeCode3:
                        var command = new FilmHouse.Commands.Movie.IsExistWithMovieIdCommand(new MovieIdVO(targetId));
                        var isExist = await this._mediator.Send(command);
                        if (!isExist)
                        {
                            return RedirectToAction("NotFound", "Error");
                        }
                        break;

                    case MarkTypeVO.Codes.MarkTypeCode4:
                        var command = new FilmHouse.Commands.Celeb.IsExistWithCelebrityIdCommand(new CelebrityIdVO(targetId));
                        var isExist = await this._mediator.Send(command);
                        if (!isExist)
                        {
                            return RedirectToAction("NotFound", "Error");
                        }
                        break;

                    case MarkTypeVO.Codes.MarkTypeCode5:
                        var command = new FilmHouse.Commands.Resource.IsExistWithResourceIdCommand(new ResourceIdVO(targetId));
                        var isExist = await this._mediator.Send(command);
                        if (!isExist)
                        {
                            return RedirectToAction("NotFound", "Error");
                        }
                        break;

                    case MarkTypeVO.Codes.MarkTypeCode6:
                        var command = new FilmHouse.Commands.Ask.IsExistWithAskIdCommand(new AskIdVO(targetId));
                        var isExist = await this._mediator.Send(command);
                        if (!isExist)
                        {
                            return RedirectToAction("NotFound", "Error");
                        }
                        break;

                    case MarkTypeVO.Codes.MarkTypeCode7:
                    default:
                        var command = new FilmHouse.Commands.Album.IsExistWithAlbumIdCommand(new AlbumIdVO(targetId));
                        var isExist = await this._mediator.Send(command);
                        if (!isExist)
                        {
                            return RedirectToAction("NotFound", "Error");
                        }
                        break;
                }
            }

            // 用户认证情报取得
            var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
            // 登陆后的用户可以设置对影片的偏好
            if (userIdentity.IsAuthenticated)
            {
                // 取得登录用户的ID
                var claimsIdentity = userIdentity as ClaimsIdentity;
                var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));

                var command = new FilmHouse.Commands.Mark.CreateMarkCommand(new MarkTargetIdVO(targetId), userId, markType);
                await this._mediator.Send(command);
            }

            return Redirect(returnurl);
        }

        //
        // GET: /Mark/Cancel/
        [Authorize]
        public ActionResult Cancel(Guid targetId, MarkTypeVO markType, string returnurl)
        {
            if (markType <= MarkTypeVO.Codes.MarkTypeCode0 || markType > MarkTypeVO.Codes.MarkTypeCode7)
            {
                return RedirectToAction("NotFound", "Error");
            }
            switch (markType)
            {
                case MarkTypeVO.Codes.MarkTypeCode1:
                case MarkTypeVO.Codes.MarkTypeCode2:
                case MarkTypeVO.Codes.MarkTypeCode3:
                    var command = new FilmHouse.Commands.Movie.IsExistWithMovieIdCommand(new MovieIdVO(targetId));
                    var isExist = await this._mediator.Send(command);
                    if (!isExist)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    break;

                case MarkTypeVO.Codes.MarkTypeCode4:
                    var command = new FilmHouse.Commands.Celeb.IsExistWithCelebrityIdCommand(new CelebrityIdVO(targetId));
                    var isExist = await this._mediator.Send(command);
                    if (!isExist)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    break;

                case MarkTypeVO.Codes.MarkTypeCode5:
                    var command = new FilmHouse.Commands.Resource.IsExistWithResourceIdCommand(new ResourceIdVO(targetId));
                    var isExist = await this._mediator.Send(command);
                    if (!isExist)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    break;

                case MarkTypeVO.Codes.MarkTypeCode6:
                    var command = new FilmHouse.Commands.Ask.IsExistWithAskIdCommand(new AskIdVO(targetId));
                    var isExist = await this._mediator.Send(command);
                    if (!isExist)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    break;

                case MarkTypeVO.Codes.MarkTypeCode7:
                default:
                    var command = new FilmHouse.Commands.Album.IsExistWithAlbumIdCommand(new AlbumIdVO(targetId));
                    var isExist = await this._mediator.Send(command);
                    if (!isExist)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    break;
            }

            // 用户认证情报取得
            var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
            // 登陆后的用户可以设置对影片的偏好
            if (userIdentity.IsAuthenticated)
            {
                // 取得登录用户的ID
                var claimsIdentity = userIdentity as ClaimsIdentity;
                var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));

                var command = new FilmHouse.Commands.Mark.CancelCommand(new MarkTargetIdVO(targetId));
                await this._mediator.Send(command);
            }

            return Redirect(returnurl);
        }
    }
}
