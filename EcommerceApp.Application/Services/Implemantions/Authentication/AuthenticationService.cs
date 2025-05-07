using AutoMapper;
using EcommerceApp.Application.DTOs.Common;
using EcommerceApp.Application.DTOs.Identity;
using EcommerceApp.Application.Services.Interfaces.Authentication;
using EcommerceApp.Application.Services.Interfaces.Logging;
using EcommerceApp.Application.Validations;
using EcommerceApp.Domain.Entities.Identity;
using EcommerceApp.Domain.Interfaces.Authentication;
using FluentValidation;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcommerceApp.Application.Services.Implemantions.Authentication;
public class AuthenticationService
    (ITokenManagement tokenManagement, IUserManagement userManagement
    , IRoleManagement roleManagement, IAppLogger<AuthenticationService> logger,
    IMapper mapper, IValidator<CreateUser> createUserValidator,
    IValidator<LoginUser> LoginUserValidator, IValidationService validationService) : IAuthenticationService
{
    public async Task<ServiceResponse> CreateUser(CreateUser user)
    {
        var _validationResult = await validationService.ValidateAsync(user, createUserValidator);
        if (!_validationResult.success) return _validationResult;

        var mappedModel = mapper.Map<AppUser>(user);
        mappedModel.UserName = user.Email;
        mappedModel.PasswordHash = user.Password;

        var result = await userManagement.CreateUser(mappedModel);
        if (!result)
            return new ServiceResponse { message = "Email Address might be already in user or unknwon error occurred " };

        var _user = await userManagement.GetUserByEmail(user.Email);
        var users = await userManagement.GetAllUsers();
        bool assignedResult = await roleManagement.AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");
        if (!assignedResult)
        {
            int removeUserResult=await userManagement.RemoveUserByEmail(_user!.Email!);
            if (removeUserResult <=0)
            {
                logger.LogError(
                    new Exception($"User with Email as{_user.Email} faild to be removed as a result of rolle assigning issue .")
                    ,"User could not be assigned Role");
                return new ServiceResponse { message = "Error occured in creating account" };

            }
        }
        return new ServiceResponse { success = true, message = "Account Created!" };
        // verfiy email 
    }

    public async Task<LoginResponse> LoginUser(LoginUser user)
    {
        var _valdiationResult = await validationService.ValidateAsync(user,LoginUserValidator);
        if (!_valdiationResult.success)
            return new LoginResponse(Message: _valdiationResult.message);

        var mappedModel = mapper.Map<AppUser>(user);
        mappedModel.PasswordHash = user.Password;

        bool loginResult = await userManagement.LoginUseer(mappedModel);
        if (!loginResult)
            return new LoginResponse(Message: "Email not founf or invalid credentials");

        var _user = await userManagement.GetUserByEmail(user.Email);
        var claims = await userManagement.GetUserClaims(_user!.Email!);

        string jwtToken = tokenManagement.GenerateToken(claims);
        string refreshToken = tokenManagement.GetRefreshToken();

        var saveTokenResult = 0;

        var userTokenCheck = await tokenManagement.ValidateRefreshToken(refreshToken);
        if (userTokenCheck)
            await tokenManagement.UpdateRefreshToken(_user.Id, refreshToken);
        else
            saveTokenResult = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);


        return saveTokenResult <= 0 ? new LoginResponse(Message: "Intetrnal error occured while authenticating")
            : new LoginResponse(Success: true, Token: jwtToken, RefreshToken: refreshToken);

    }

    public async Task<LoginResponse> ReviveToken(string refreshToken)
    {
        bool validateTokenResult = await tokenManagement.ValidateRefreshToken(refreshToken);

        if (!validateTokenResult)
            return new LoginResponse(Message: "Invalid token");

        string userId=await tokenManagement.GetUserIdByRefreshToken(refreshToken);
        AppUser? user= await userManagement.GetUserById(userId);

        var claims = await userManagement.GetUserClaims(user.Email!);
        string newJwtToken = tokenManagement.GenerateToken(claims);
        string newRefreshToken = tokenManagement.GetRefreshToken();

        await tokenManagement.UpdateRefreshToken(userId, newRefreshToken);
        return new LoginResponse(Success:true,Token:newJwtToken,RefreshToken:newRefreshToken);

    }
}
