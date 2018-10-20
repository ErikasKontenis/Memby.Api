using Memby.Application.Resources;
using Memby.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Memby.Application.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.EmailRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationMessages.EmailFormatIncorrect), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.DateOfBirthRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.GenderRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public Genders Gender { get; set; }

        public bool IsIndividualOffersEnabled { get; set; }

        public bool IsNewOffersEnabled { get; set; }

        public bool IsSystemNotificationsEnabled { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.NameRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [MinLength(2, ErrorMessageResourceName = nameof(DataAnnotationMessages.NameMinLengthIncorrect), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [MinLength(6, ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordMinLengthIncorrect), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordConfirmationRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordConfirmationDoesNotMatch), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.SurnameRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [MinLength(2, ErrorMessageResourceName = nameof(DataAnnotationMessages.SurnameMinLengthIncorrect), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Surname { get; set; }
    }
}
