using Memby.Application.Resources;
using System.ComponentModel.DataAnnotations;

namespace Memby.Application.Users
{
    public class UpdateUserPasswordDto
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.OldPasswordRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [MinLength(6, ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordMinLengthIncorrect), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordConfirmationRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordConfirmationDoesNotMatch), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string PasswordConfirmation { get; set; }
    }
}
