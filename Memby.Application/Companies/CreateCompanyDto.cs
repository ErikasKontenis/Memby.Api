using Memby.Application.Resources;
using System.ComponentModel.DataAnnotations;

namespace Memby.Application.Companies
{
    public class CreateCompanyDto
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.AddressRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Address { get; set; }

        public string BrandName { get; set; }

        public string Logo { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.CompanyNameRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.RegistrationNumberRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.VatNumberRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string VatNumber { get; set; }
    }
}
