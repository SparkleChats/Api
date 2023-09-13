﻿using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.UploadMedia
{
    public class UploadMediaRequest : IRequest<Media>
    {
        [Required]
        public IFormFile File { get; set; }
    }
}