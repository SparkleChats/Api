using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sparkle.Application.Common.Interfaces;
using Sparkle.Application.HubClients.Users.RelationshipDeleted;
using Sparkle.Application.HubClients.Users.RelationshipUpdated;
using Sparkle.Application.HubClients.Users.UserUpdated;
using Sparkle.Domain;
using Sparkle.Application.Users.Commands;
using Sparkle.Application.Users.Queries;
using Sparkle.Application.Users.Relationships.Commands;
using Sparkle.Application.Users.Relationships.Queries;

namespace Sparkle.WebApi.Controllers
{
    [Route("api/users")]
    public class UsersController : ApiControllerBase
    {
        public UsersController(IMediator mediator, IAuthorizedUserProvider userProvider) : base(mediator, userProvider)
        {
        }

        /// <summary>
        /// Gets detailed information about the provided user, including it's ServerProfile if ServerId is provided
        /// </summary>
        /// <param name="userId">Id of requested user. If null will return current user</param>
        /// <param name="userName">UserName of requested user.</param>
        /// <param name="serverId">string ObjectId represents of server. Can be provided if ServerProfile is required. Null by default</param>
        /// <returns>UserDetails object</returns>
        /// <response code="200">Ok. User details object in JSON</response>
        /// <response code="400">Bad Request. The requested user is not found</response>
        /// <response code="401">Unauthorized. The client must be authorized to send this request</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GetUserDetailsDto>> GetUser(Guid? userId = null, string? userName = null, string? serverId = null)
        {
            GetUserDetailsDto user;
            if (userName == null)
            {
                GetUserDetailsQuery query = new()
                {
                    UserId = userId ?? UserId,
                    ServerId = serverId
                };
                user = await Mediator.Send(query);
            }
            else
            {
                GetUserByUserNameQuery query = new()
                {
                    UserName = userName,
                    ServerId = serverId
                };
                user = await Mediator.Send(query);
            }
            return Ok(user);
        }

        /// <summary>
        /// Changes the current user's display name
        /// </summary>
        /// <param name="displayName">New display name. Send null to remove display name</param>
        /// <respose code="204">No Content. The display name was changed successfully</respose>
        [HttpPatch("displayname")]
        public async Task<ActionResult> UpdateDisplayName(string? displayName)
        {
            ChangeDisplayNameCommand command = new() { DisplayName = displayName };
            User user = await Mediator.Send(command);

            await Mediator.Send(new NotifyUserUpdatedQuery() { UpdatedUser = user });

            return NoContent();
        }

        /// <summary>
        /// Changes the current user's username
        /// </summary>
        /// <param name="username">New username</param>
        /// <respose code="204">The username was changed successfully</respose>
        [HttpPatch("username")]
        public async Task<ActionResult> UpdateUserName(string username)
        {
            ChangeUserNameCommand command = new(username);
            User user = await Mediator.Send(command);

            await Mediator.Send(new NotifyUserUpdatedQuery() { UpdatedUser = user });

            return NoContent();
        }
        /// <summary>
        /// Changes the current user's status
        /// </summary>
        /// <param name="status">New user status </param>
        /// <respose code="204">The status was changed successfully</respose>
        [HttpPatch("status")]
        public async Task<ActionResult> UpdateUserStatus(string? status)
        {
            ChangeTextStatusCommand command = new(status);
            User user = await Mediator.Send(command);

            await Mediator.Send(new NotifyUserUpdatedQuery() { UpdatedUser = user });

            return NoContent();
        }

        /// <summary>
        /// Changes the current user's avatar
        /// </summary>
        /// <param name="avatar">New avatar. Send null to remove</param>
        /// <respose code="204">The avatar was changed successfully</respose>
        [HttpPatch("avatar")]
        public async Task<ActionResult> UpdateAvatarName(string? avatar)
        {
            ChangeAvatarCommand command = new(avatar);
            User user = await Mediator.Send(command);

            await Mediator.Send(new NotifyUserUpdatedQuery() { UpdatedUser = user });

            return NoContent();
        }

        /// <summary>
        /// Gets all relationships of the current user
        /// </summary>
        /// <response code="200">Ok. List of current user relationships in JSON</response>
        /// <response code="401">Unauthorized. The client must be authorized to send this request</response>
        /// <returns>List of current user relationships</returns>
        [HttpGet("relationships")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<RelationshipViewModel>>> GetRelationships(bool includeAcquaintance = false)
        {
            return Ok(await Mediator.Send(new GetRelationshipQuery(includeAcquaintance)));
        }

        /// <summary>
        /// Sends a friend request to the user with the provided id
        /// </summary>
        /// <param name="friendId">Id of the user to send a friend request to</param>
        /// <response code="204">No Content. The request was sent successfully</response>
        /// <response code="400">Bad Request. Invalid friend id</response>
        [HttpPost("friends")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> SendFriendRequest(Guid friendId)
        {
            CreateFriendRequestCommand request = new() { FriendId = friendId };
            Relationship relationship = await Mediator.Send(request);

            await Mediator.Send(new NotifyRelationshipUpdatedQuery() { Relationship = relationship });

            return NoContent();
        }

        /// <summary>
        /// Accepts a friend request from the user with the provided id
        /// </summary>
        /// <param name="friendId">Id of the user to accept a friend request from</param>
        /// <response code="204">No Content. The request was accepted successfully</response>
        /// <response code="400">Bad Request. The requested user is not found</response>
        /// <response code="401">Unauthorized. The client must be authorized to send this request</response>
        /// <response code="403">Forbidden. The client has no friend request from the requested user</response>
        [HttpPatch("friends")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> AcceptFriendRequest(Guid friendId)
        {
            AcceptFriendRequestCommand command = new() { FriendId = friendId };
            Relationship relationship = await Mediator.Send(command);

            await Mediator.Send(new NotifyRelationshipUpdatedQuery() { Relationship = relationship });

            return NoContent();
        }

        /// <summary>
        /// Cancels a friend request to the user with the provided id
        /// </summary>
        /// <remarks>Friend request con be canceled from both sides</remarks>
        /// <param name="friendId">Id of the user to cancel a friend request</param>
        ///  <response code="204">No Content. The request was canceled successfully</response>
        ///  <response code="400">Bad Request. The friend request between users does not exists</response>
        [HttpDelete("friends/cancel")]
        public async Task<ActionResult> CancelFriendRequest(Guid friendId)
        {
            CancelFriendRequestCommand command = new() { FriendId = friendId };
            Relationship relationship = await Mediator.Send(command);

            await Mediator.Send(new NotifyRelationshipDelatedQuery() { Relationship = relationship });

            return NoContent();
        }

        /// <summary>
        /// Deletes a friend from the user with the provided id
        /// </summary>
        /// <param name="friendId">Id of the user to delete a friend</param>
        /// <response code="204">No Content. The friend was deleted successfully</response>
        /// <response code="400">Bad Request. You are no friends with this user</response>
        [HttpDelete("friends")]
        public async Task<ActionResult> DeleteFriendRequest(Guid friendId)
        {
            DeleteFriendCommand command = new() { FriendId = friendId };
            Relationship relationship = await Mediator.Send(command);

            await Mediator.Send(new NotifyRelationshipDelatedQuery() { Relationship = relationship });
            return NoContent();
        }

        [HttpPost("block")]
        public async Task<ActionResult> BlockUser(Guid userId)
        {
            BlockUserCommand command = new(userId);
            Relationship relationship = await Mediator.Send(command);

            await Mediator.Send(new NotifyRelationshipUpdatedQuery() { Relationship = relationship });
            return NoContent();
        }

        [HttpDelete("block")]
        public async Task<ActionResult> UnblockUser(Guid userId)
        {
            UnblockUserCommand command = new(userId);
            Relationship relationship = await Mediator.Send(command);

            await Mediator.Send(new NotifyRelationshipDelatedQuery() { Relationship = relationship });
            return NoContent();
        }
    }
}