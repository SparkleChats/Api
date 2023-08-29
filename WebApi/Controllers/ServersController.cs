﻿namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ServersController : ApiControllerBase
    {
        public ServersController(IMediator mediator, IAuthorizedUserProvider userProvider) : base(mediator, userProvider)
        { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<GetServerLookupDto>>> GetServers()
        {
            GetServersRequest get = new();
            List<GetServerLookupDto> servers = await Mediator.Send(get);
            return Ok(servers);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ServerDetailsDto>> GetServerDetails(string serverId)
        {
            ServerDetailsDto server = await Mediator
                .Send(new GetServerDetailsRequest { ServerId = serverId });
            return Ok(server);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> CrateServer(CrateServerDto serverDto)
        {
            CreateServerRequest request = new()
            {
                Title = serverDto.Title,
                Image = serverDto.Image,
            };
            string id = await Mediator.Send(request);
            return Created(string.Empty, id);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateServer(UpdateServerRequest request)
        {
            try
            {
                await Mediator.Send(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteServer(DeleteServerRequest request)
        {
            try
            {
                await Mediator.Send(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
