using AutoMapper;
using Azure;
using Entities;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositorio;
using Serilog;
using System.Diagnostics;
using System.Security.Cryptography;

namespace PruebaAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();
                var response =  _repository.Login(userLogin);
                
                if (response.User == null ||  string.IsNullOrEmpty(response.Token))                
                    return BadRequest(response);                

                return Ok(response);              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return Ok();
            }
            finally
            {
               
                Log.Information($"Login exitoso. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();
            }   
        }

     
    }
}
