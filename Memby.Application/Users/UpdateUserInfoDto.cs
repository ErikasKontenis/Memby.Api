using Memby.Application.Resources;
using Memby.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Memby.Application.Users
{
    public class UpdateUserInfoDto
    {
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

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.SurnameRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [MinLength(2, ErrorMessageResourceName = nameof(DataAnnotationMessages.SurnameMinLengthIncorrect), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Surname { get; set; }
    }
}
