using LinkShortener.Application;
using LinkShortener.Domain.Constants;
using LinkShortener.Domain.Model;
using LinkShortener.Domain.Request;
using LinkShortener.Persistence.Database.ShortenLink;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkShortenerController : ControllerBase
    {
        private readonly ILinkShortenerService _linkShortenerService;

        public LinkShortenerController(ILinkShortenerService linkShortenerService)
        {
            _linkShortenerService = linkShortenerService;
        }

        [HttpPost]
        public async Task<IActionResult> ShortenLink(ShortenLinkRequest request)
        {
            ShortenedLink link = _linkShortenerService.ShortenLink(request.url);
            return Ok(new ResponseMessageSchema(HttpContext)
            {
                Message = SysMsg.GetMsg(MsgCodes.LS_SUC_001_Success),
                Data = link
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetShortenedLinks()
        {
            List<ShortenedLink> links =  _linkShortenerService.GetShortenedLinks();
            return Ok(new ResponseMessageSchema(HttpContext)
            {
                Message = SysMsg.GetMsg(MsgCodes.LS_SUC_001_Success),
                Data = links
            });
        }
    }
}
