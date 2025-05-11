using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Profile.Api.Attributes;
using Profile.Application.DTOs;
using Profile.Application.Interfaces;
using Shared.DTOs;
using Shared.Interfaces;
using Shared.Models;

namespace Profile.Api.Controllers;

[ApiController]
[Route("api/user")]
[RequireBearerToken]
public class UserController(ITokenParser tokenParser, IUserService userService) : ControllerBase
{
    [HttpGet("providers")]
    public async Task<IActionResult> Providers()
    {
        Result<List<string>> result = await userService.GetUserProviders(GetUserEmail());

        if (result.IsFailed)
        {
            return BadRequest(new ResultFailDto(false, result.Errors));
        }

        return Ok(new ResultSuccessDto<List<string>>(false, result.Value));
    }

    [HttpPut("information")]
    public async Task<IActionResult> Information([FromBody] UserUpdateDto userUpdate)
    {
        userUpdate.OldUserEmail = GetUserEmail();

        Result<bool> result = await userService.UpdateUserInformation(userUpdate);

        if (result.IsFailed)
        {
            return BadRequest(new ResultFailDto(false, result.Errors));
        }

        return Ok(new ResultSuccessDto<bool>(true, true));
    }

    [HttpPut("password")]
    public async Task<IActionResult> Password([FromBody] PasswordUpdateDto passwordUpdate)
    {
        passwordUpdate.UserEmail = GetUserEmail();

        Result<bool> result = await userService.UpdateUserPassword(passwordUpdate);

        if (result.IsFailed)
        {
            return BadRequest(new ResultFailDto(false, result.Errors));
        }

        return Ok(new ResultSuccessDto<bool>(true, true));
    }

    private string GetUserEmail()
    {
        string authHeader = Request.Headers.Authorization.FirstOrDefault() ?? string.Empty;
        TokenInformation tokenInformation = tokenParser.ParseToken(authHeader);
        return tokenInformation.UserEmail;
    }
}