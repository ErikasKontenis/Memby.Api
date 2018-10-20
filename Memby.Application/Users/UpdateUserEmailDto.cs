using Memby.Application.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Memby.Application.Users
{
    public class UpdateUserEmailDto
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.EmailRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationMessages.EmailFormatIncorrect), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Email { get; set; }
    }
}
