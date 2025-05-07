using EcommerceApp.Application.DTOs.Common;
using FluentValidation;

namespace EcommerceApp.Application.Validations;

public class ValidationService : IValidationService
{
    public async Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator)
    {

        var _validation = await validator.ValidateAsync(model);
        if (!_validation.IsValid)
        {
            var errors = _validation.Errors.Select(ex => ex.ErrorMessage).ToList();
            string errorsTostring=string.Join("; ", errors);
            return new ServiceResponse { message = errorsTostring };
        }
        return new ServiceResponse { success = true };
    }
}
