using System.Security.Claims;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.Controllers
{
    public class MarkController : Controller
    {
        #region Initizalize

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentRequestId _currentRequestId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="currentRequestId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public MarkController(IMediator mediator, IHttpContextAccessor httpContextAccessor, ICurrentRequestId currentRequestId)
        {
            this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
            this._httpContextAccessor = Guard.GetNotNull(httpContextAccessor, nameof(IHttpContextAccessor));
            this._currentRequestId = Guard.GetNotNull(currentRequestId, nameof(ICurrentRequestId));
        }

        #endregion Initizalize

        //
        // GET: /Mark/Create/
        [Authorize]
        public async Task<IActionResult> Create(Guid target, MarkTypeVO type, string transfer)
        {
            if (!await this.CheckTargetIdIsExist(target, type))
            {
                return RedirectToAction("NotFound", "Error");
            }

            // 创建请求ID
            this._currentRequestId.Set(new RequestIdVO(Guid.NewGuid()));

            // 用户认证情报取得
            var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
            // 登陆后的用户可以设置对影片的偏好
            if (userIdentity.IsAuthenticated)
            {
                // 取得登录用户的ID
                var claimsIdentity = userIdentity as ClaimsIdentity;
                var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));

                var command = new FilmHouse.Commands.Mark.CreateMarkCommand(new MarkTargetIdVO(target), userId, type);
                await this._mediator.Send(command);
            }

            return Redirect(transfer);
        }

        private async Task<bool> CheckTargetIdIsExist(Guid targetId, MarkTypeVO markType)
        {
            if (markType <= MarkTypeVO.Codes.MarkTypeCode0 || markType > MarkTypeVO.Codes.MarkTypeCode7)
            {
                return false;
            }
            else if (markType == MarkTypeVO.Codes.MarkTypeCode1 || markType == MarkTypeVO.Codes.MarkTypeCode2 || markType == MarkTypeVO.Codes.MarkTypeCode3)
            {
                var movieCommand = new FilmHouse.Commands.Movie.IsExistWithMovieIdCommand(new MovieIdVO(targetId));
                return await this._mediator.Send(movieCommand);
            }
            else if (markType == MarkTypeVO.Codes.MarkTypeCode4)
            {
                var celebCommand = new FilmHouse.Commands.Celeb.IsExistWithCelebrityIdCommand(new CelebrityIdVO(targetId));
                return await this._mediator.Send(celebCommand);
            }
            else if (markType == MarkTypeVO.Codes.MarkTypeCode5)
            {
                var resourceCommand = new FilmHouse.Commands.Resource.IsExistWithResourceIdCommand(new ResourceIdVO(targetId));
                return await this._mediator.Send(resourceCommand);
            }
            else if (markType == MarkTypeVO.Codes.MarkTypeCode6)
            {
                var askCommand = new FilmHouse.Commands.Ask.IsExistWithAskIdCommand(new AskIdVO(targetId));
                return await this._mediator.Send(askCommand);
            }
            else
            {
                var albumCommand = new FilmHouse.Commands.Album.IsExistWithAlbumIdCommand(new AlbumIdVO(targetId));
                return await this._mediator.Send(albumCommand);
            }
        }

        //
        // GET: /Mark/Cancel/
        [Authorize]
        public async Task<IActionResult> Cancel(Guid target, MarkTypeVO type, string transfer)
        {
            if (!await this.CheckTargetIdIsExist(target, type))
            {
                return RedirectToAction("NotFound", "Error");
            }

            // 用户认证情报取得
            var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
            // 登陆后的用户可以设置对影片的偏好
            if (userIdentity.IsAuthenticated)
            {
                // 取得登录用户的ID
                var claimsIdentity = userIdentity as ClaimsIdentity;
                var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));

                var command = new FilmHouse.Commands.Mark.CancelCommand(new MarkTargetIdVO(target), userId, type);
                await this._mediator.Send(command);
            }

            return Redirect(transfer);
        }
    }
}
