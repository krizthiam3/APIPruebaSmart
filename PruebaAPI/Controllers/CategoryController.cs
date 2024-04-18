using AutoMapper;
using Azure;
using Entities;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Repository.IRepositorio;
using Serilog;
using System.Data;
using System.Diagnostics;
using System.IO.Pipelines;

namespace PruebaAPI.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;


        public CategoryController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategory() 
        {            
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                var list = _repository.GetAllCategory();
                var listDto = new List<CategoryDto>();

                foreach (var category in list)
                {
                    listDto.Add(_mapper.Map<CategoryDto>(category));
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
               
                Log.Information($"La consulta de todas las categorias fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();
            }              

        }

        [AllowAnonymous]
        [HttpGet("{categoryId:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int categoryId)
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                var category= _repository.GetCategoryById(categoryId);

                if (category == null)
                    return NotFound();

                var categoryDto = _mapper.Map<CategoryDto>(category);


                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return CreatedAtRoute("GetCategory", new { Id = categoryId }, categoryId);
            }
            finally
            {

                Log.Information($"La consutla de la categoria fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();
            }


        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201, Type =  typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryDto)
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (categoryDto == null)
                    return BadRequest(ModelState);

                var category = _mapper.Map<Category>(categoryDto);

                if (!_repository.CrearCategory(category))
                {
                    ModelState.AddModelError("", $"Algo salio mal creando la categoria, {category.Nombre}");
                    return StatusCode(500, ModelState);
                }

                return CreatedAtRoute("GetCategory", new { categoryId = category.CategoryId}, category);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al realizar la solicitud: {ex.Message}");
                return CreatedAtRoute("GetCategory", new { nombre = categoryDto.Nombre }, categoryDto);
            }
            finally
            {
                Log.Information($"La Creacion de la categria fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();

            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{categoryId:int}", Name = "UpdateCategory")]
        [ProducesResponseType(201, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto categoryDto)
        {
            var stopwatch = new Stopwatch();

            try
            { 
                stopwatch.Start();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (categoryDto == null || categoryId != categoryDto.CategoryId)
                    return BadRequest(ModelState);

                var category = _mapper.Map<Category>(categoryDto);

                if (!_repository.UpdateCategory(category))
                {
                    ModelState.AddModelError("", $"Algo salio mal actualizando la caegoria, {category.Nombre}");
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
                Console.WriteLine($"La actualizacion de la categoria fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();

            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{categoryId:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCategory(int categoryId)
        {
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                if (!_repository.CategoryExist(categoryId))                
                    return NotFound();

                var category = _repository.GetCategoryById(categoryId);

                if (!_repository.DeleteCategory(category))
                {
                    ModelState.AddModelError("", $"Algo salio mal actualizando la caegoria, {category.Nombre}");
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
                Console.WriteLine($"SE borró de la categoria fue exitosa. tiempo de respuesta de request: {stopwatch.Elapsed}");
                stopwatch.Stop();

            }
        }

    }
}
