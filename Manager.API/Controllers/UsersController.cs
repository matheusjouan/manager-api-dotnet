using AutoMapper;
using Manager.API.Utilities;
using Manager.Application.DTOs;
using Manager.Application.Services.Interfaces;
using Manager.Core.Excpetions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Manager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var user = await _userService.GetAllAsync();
            return Ok(new ResultViewModel
            {
                Success = true,
                Message = "Users listed",
                Data = user
            });


        }
        catch (DomainExcpetion ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        try
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
                return NotFound();

            return Ok(new ResultViewModel
            {
                Success = true,
                Message = "User listed",
                Data = user
            });
        }
        catch (DomainExcpetion ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserDTO userDto)
    {
        try
        {
            var user = await _userService.CreateAsync(userDto);
            return Ok(new ResultViewModel
            {
                Success = true,
                Message = "User created",
                Data = user
            });


        } catch(DomainExcpetion ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch(Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateUserDTO userDto)
    {
        try
        {
            var user = await _userService.UpdateAsync(userDto);
            return Ok(new ResultViewModel
            {
                Success = true,
                Message = "User updated",
                Data = user
            });


        }
        catch (DomainExcpetion ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

        [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
                return NotFound();

            await _userService.RemoveAsync(user.Id);
            return NoContent();
      
        }
        catch (DomainExcpetion ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }
}

