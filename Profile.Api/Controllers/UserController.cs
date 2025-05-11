using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Profile.Application.Interfaces;
using Shared.DTOs;
using Shared.Interfaces;
using Shared.Models;

namespace Profile.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(ITokenParser tokenParser, IUserService userService) : ControllerBase
{
    [HttpGet("providers")]
    public async Task<IActionResult> Providers()
    {
        string? authHeader = Request.Headers.Authorization.FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return BadRequest("Invalid token");
        }

        TokenInformation tokenInformation = tokenParser.ParseToken(authHeader);

        Result<List<string>> result = await userService.GetUserProviders(tokenInformation.UserEmail);

        if (result.IsFailed)
        {
            return BadRequest(new ResultFailDto(false, result.Errors));
        }

        return Ok(new ResultSuccessDto<List<string>>(result.IsSuccess, result.Value));
    }
}