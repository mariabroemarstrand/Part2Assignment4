using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private IDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public ProductsController(IDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products =
                _dataService.GetProducts();
            //.Select(x => x);
            return Ok(products);
        }

        [HttpGet("{id}", Name = nameof(GetProduct))]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            //var model = CreateCategoryModel(product);

            return Ok(product);

        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetProductsByCAtegoryId(int categoryId)
        {
            var products = _dataService.GetProductByCategory(categoryId);

            if(!products.Any())
            {
                return NotFound(products);
            }

            return Ok(products);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetProductsByName(string name)
        {
            var products = _dataService.GetProductByName(name);

            if (!products.Any())
            {
                return NotFound(products);
            }

            return Ok(products);
        }
    }
}
