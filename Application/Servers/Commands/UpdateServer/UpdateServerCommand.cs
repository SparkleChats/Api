using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sparkle.Application.Servers.Commands.UpdateServer
{
    public record UpdateServerCommand : IRequest
    {
        /// <summary>
        /// Id of the server to update
        /// </summary>
        [DefaultValue("5f95a3c3d0ddad0017ea9291")]
        public string ServerId { get; init; }

        /// <summary>
        /// Server's name (Optional)
        /// </summary>
        [DefaultValue("Server 1")]
        public string? Title { get; init; }

        /// <summary>
        /// Server's image url (Optional)
        /// </summary>
        [DataType(DataType.ImageUrl)]
        [DefaultValue("https://localhost:7060/api/media/5f95a3c3d0ddad0017ea9291")]
        public string? Image { get; init; }
    }
}
