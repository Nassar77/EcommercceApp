using EcommerceApp.Application.DTOs.Common;
using EcommerceApp.Application.Validations.Authontication;
using FluentValidation;

namespace EcommerceApp.Application.Validations;
public interface IValidationService
{
    Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator);
}
