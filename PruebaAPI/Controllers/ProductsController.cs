using AutoMapper;
using Azure;
using Entities;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositorio;
using Serilog;
using System.Diagnostics;

namespace PruebaAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public  IActionResult GetProduct() 
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                var list = _repository.GetAllProducts();
                var listDto = new List<ProductDto>();

                foreach (var product in list)
                {
                    listDto.Add(_mapper.Map<ProductDto>(product));
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
        [HttpGet("{productId:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProduct(int productId)
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();
                var product = _repository.GetProductById(productId);

                if (product == null)
                    return NotFound();

                var productDto = _mapper.Map<ProductDto>(product);

                return Ok(productDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return CreatedAtRoute("GetProduct", new { Id = productId }, productId);
            }
            finally
            {

                Log.Information($"La consutla del producto fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();
            }


        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201, Type =  typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateProduct([FromBody] ProductCreateDto productDto)
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (productDto == null)
                    return BadRequest(ModelState);

                var product = _mapper.Map<Product>(productDto);

                if (!_repository.CrearProduct(product))
                {
                    ModelState.AddModelError("", $"Algo salio mal creando el producto, {product.Nombre}");
                    return StatusCode(500, ModelState);
                }

                return CreatedAtRoute("GetProduct", new { productId = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return CreatedAtRoute("GetProduct", new { nombre = productDto.Nombre }, productDto);
            }
            finally
            {
                Log.Information($"La Creacion del producto fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();

            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{productId:int}", Name = "UpdateProduct")]
        [ProducesResponseType(201, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct(int productId, [FromBody] ProductDto productDto)
        {
            var stopwatch = new Stopwatch();

            try
            { 
                stopwatch.Start();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (productDto == null || productId != productDto.ProductId)
                    return BadRequest(ModelState);

                var product = _mapper.Map<Product>(productDto);

                if (!_repository.UpdateProduct(product))
                {
                    ModelState.AddModelError("", $"Algo salio mal actualizando el producto, {product.Nombre}");
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
                Console.WriteLine($"La actualizacion de produto fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();

            }
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("{productId:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteProduct(int productId)
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                if (!_repository.ProductExist(productId))
                    return NotFound();

                var product = _repository.GetProductById(productId);

                if (!_repository.DeleteProduct(product))
                {
                    ModelState.AddModelError("", $"Algo salio mal eliminado el producto, {product.Nombre}");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return NoContent();
            }
            finally
            {
                Console.WriteLine($"Se borró el producto fue exitosament. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();

            }
        }

    }
}
