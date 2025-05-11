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
        string authHeader = Request.Headers.Authorization.FirstOrDefault() ?? string.Empty;
        TokenInformation tokenInformation = tokenParser.ParseToken(authHeader);

        Result<List<string>> result = await userService.GetUserProviders(tokenInformation.UserEmail);

        if (result.IsFailed)
        {
            return BadRequest(new ResultFailDto(false, result.Errors));
        }

        return Ok(new ResultSuccessDto<List<string>>(result.IsSuccess, result.Value));
    }

    [HttpPut("information")]
    public async Task<IActionResult> Information([FromBody] UserUpdateDto userUpdate)
    {
        string authHeader = Request.Headers.Authorization.FirstOrDefault() ?? string.Empty;
        TokenInformation tokenInformation = tokenParser.ParseToken(authHeader);

        userUpdate.OldUserEmail = tokenInformation.UserEmail;

        Result<bool> result = await userService.UpdateUserInformation(userUpdate);

        if (result.IsFailed)
        {
            return BadRequest(new ResultFailDto(false, result.Errors));
        }

        return Ok(new ResultSuccessDto<bool>(result.IsSuccess, result.Value));
    }

    [HttpPut("password")]
    public async Task<IActionResult> Information([FromBody] PasswordUpdateDto passwordUpdate)
    {
        Result<bool> result = await userService.UpdateUserPassword(passwordUpdate);

        if (result.IsFailed)
        {
            return BadRequest(new ResultFailDto(false, result.Errors));
        }

        return Ok(new ResultSuccessDto<bool>(result.IsSuccess, result.Value));
    }
}