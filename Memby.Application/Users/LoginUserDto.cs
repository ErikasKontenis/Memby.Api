﻿using Memby.Application.Resources;
using System.ComponentModel.DataAnnotations;

namespace Memby.Application.Users
{
    public class LoginUserDto
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.EmailRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationMessages.EmailFormatIncorrect), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Password { get; set; }
    }
}
