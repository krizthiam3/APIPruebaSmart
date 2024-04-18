using AutoMapper;
using Azure;
using Entities;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositorio;
using Serilog;
using System.Data;
using System.Diagnostics;

namespace PruebaAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;


        public OrderController(IOrderRepository repository, IMapper mapper, IEmailSender emailSender)
        {
            _repository = repository;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public  IActionResult GetOrder() 
        {
            var stopwatch = new Stopwatch();        

            try
            {
                stopwatch.Start();

                var list = _repository.GetAllOrders();
                var listDto = new List<OrderDto>();

                foreach (var order in list)
                {
                    listDto.Add(_mapper.Map<OrderDto>(order));
                }

                return Ok(listDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return Ok();
            }
            finally
            {
               
                Log.Information($"La consulta de todos los productos fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();
            }              

        }

        [AllowAnonymous]
        [HttpGet("{orderId:int}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrder(int orderId)
        {
            var stopwatch = new Stopwatch();



            try
            {
                stopwatch.Start();
                var order = _repository.GetOrderById(orderId);

                if (order == null)
                    return NotFound();

                var OrderDto = _mapper.Map<OrderDto>(order);

                return Ok(OrderDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return CreatedAtRoute("GetOrder", new { Id = orderId }, orderId);
            }
            finally
            {

                Log.Information($"La consutla del producto fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();
            }


        }

        [Authorize(Roles = "user")]
        [HttpPost]
        [ProducesResponseType(201, Type =  typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateOrder([FromBody] OrderCreateDto OrderDto)
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (OrderDto == null)
                    return BadRequest(ModelState);

                var order = _mapper.Map<Order>(OrderDto);

                if (!_repository.CrearOrder(order))
                {
                    ModelState.AddModelError("", $"Algo salio mal creando el orden, {order.OrderCode}");
                    return StatusCode(500, ModelState);
                }
                
                _emailSender.SendEmail("c.valenciaguilar@gmail.com", $"Nueva orden Nro:  {order.OrderCode}, Estado: {Enum.GetName(typeof(Status), order.StatusId)}");


                return CreatedAtRoute("GetOrder", new { orderId = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return CreatedAtRoute("GetOrder", new { orderCode = OrderDto.OrderCode }, OrderDto);
            }
            finally
            {
                Log.Information($"La Creacion del Orden fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();

            }
        }

        [Authorize(Roles = "user")]

        [HttpPut("{orderId:int}", Name = "UpdateOrder")]
        [ProducesResponseType(201, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateOrder(int orderId, [FromBody] OrderDto OrderDto)
        {
            var stopwatch = new Stopwatch();

            try
            { 
                stopwatch.Start();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (OrderDto == null || orderId != OrderDto.OrderId)
                    return BadRequest(ModelState);

                var order = _mapper.Map<Order>(OrderDto);

                if (!_repository.UpdateOrder(order))
                {
                    ModelState.AddModelError("", $"Algo salio mal actualizando el orden, {order.OrderCode}");
                    return StatusCode(500, ModelState);
                }

                _emailSender.SendEmail("c.valenciaguilar@gmail.com", $"Orden Nro:  {order.OrderCode}, Estado: {Enum.GetName(typeof(Status), order.StatusId)}");
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return NoContent();
            }
            finally { 
                Console.WriteLine($"La actualizacion de order fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();

            }
        }

        // Types of User Roles
        public enum Status
        {
            CREATED = 1,
            CONFIRMED = 2,
            COMPLETED = 3,
            CANCELLED = 4
        }

    }
}
