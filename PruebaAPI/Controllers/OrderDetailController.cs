using AutoMapper;
using Azure;
using Entities;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.IRepositorio;
using Serilog;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PruebaAPI.Controllers
{
    [ApiController]
    [Route("api/orderDetail")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailRepository repository, IMapper mapper, IProductRepository productRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOrderDetail()
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                var list = _repository.GetAllOrderDetail();
                var listDto = new List<OrderDetail>();

                foreach (var order in list)
                {
                    listDto.Add(_mapper.Map<OrderDetail>(order));
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
        [HttpGet("{orderId:int}", Name = "GetOrderDetailByOrderId")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrderDetailByOrderId(int orderId)
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                var list = new List<OrderDetail>();
                var listDto = _repository.GetAllOrderDetailByOrderId(orderId);

                foreach (var order in list)
                {
                    listDto.Add(_mapper.Map<OrderDetail>(order));
                }

                return Ok(listDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return CreatedAtRoute("GetOrderDetailByOrderId", new { Id = orderId }, orderId);
            }
            finally
            {

                Log.Information($"La consutla del detalle fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();
            }


        }


        [Authorize(Roles = "user")]
        [HttpPost]
        [ProducesResponseType(201, Type =  typeof(OrderDetailCreateDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateOrderDetail([FromBody] ICollection<OrderDetailCreateDto> OrderDetailDto)
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (OrderDetailDto == null)
                    return BadRequest(ModelState);

                var orderDetail = new OrderDetail();

                foreach (var item in OrderDetailDto)
                {
                    var product = _productRepository.GetProductById(item.ProductId);

                    if (product.Stock > 0 && item.Quantity < product.Stock)
                    {
                        orderDetail = _mapper.Map<OrderDetail>(item);

                        if (orderDetail != null && orderDetail.OrderId > 0)
                        {
                            if (!_repository.CrearOrderDetail(orderDetail))
                            {
                                ModelState.AddModelError("", $"Algo salio mal creando el orden, {orderDetail.OrderDetailId}");
                                return StatusCode(500, ModelState);
                            }
                        }

                    }                   
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return CreatedAtRoute("GetOrderDetail", new { OrderDetailId = 0 }, OrderDetailDto);

            }
            finally
            {
                Log.Information($"La Creacion del Orden fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();

            }
        }

        [Authorize(Roles = "user")]
        [HttpPut("{orderDetailId:int}", Name = "UpdateOrderDetail")]
        [ProducesResponseType(201, Type = typeof(OrderDetailDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateOrderDetail(int orderDetailId, [FromBody] OrderDetailDto OrderDetailDto)
        {
            var stopwatch = new Stopwatch();

            try
            { 
                stopwatch.Start();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (OrderDetailDto == null || orderDetailId != OrderDetailDto.OrderDetailId)
                    return BadRequest(ModelState);

                var orderDetail = _mapper.Map<OrderDetail>(OrderDetailDto);

                if (!_repository.UpdateOrderDetail(orderDetail))
                {
                    ModelState.AddModelError("", $"Algo salio mal actualizando el orden, {orderDetail.OrderDetailId}");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return NoContent();
            }
            finally { 
                Console.WriteLine($"La actualizacion de orderDetail fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();

            }
        }
       
    }
}
