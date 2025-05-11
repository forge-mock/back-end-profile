using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Profile.Application.Interfaces;
using Shared.Models;

namespace Profile.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("information")]
    public async Task<IActionResult> Information()
    {
        if (payloadResult.IsFailed)
        {
            return BadRequest(new ResultFailDto(payloadResult.IsSuccess, payloadResult.Errors));
        }

        return await BaseProvider(provider, Providers.Google);
    }
}