using Microsoft.AspNetCore.Mvc;
using NLayer.Data.Models;
using NLayer.Service;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //mvcdeki controllerlar arabuluculardır, request alır response dönerler. businessın işleneceği yer burası değil
    //try-catchlerin kullanılacağı yer service katmanı
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        ///<summary>
        /// Tüm Productları Listeler
        /// </summary>
        /// <returns>List of Product döndürür.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAll();
            return new ObjectResult(response)
            {
                StatusCode = response.Status
            };
        }

        [HttpGet("GetProductFull")]
        public async Task<IActionResult> GetProductFull()
        {
            var response = await _productService.GetProductFullModel();
            return new ObjectResult(response)
            {
                StatusCode = response.Status
            };
        }

        /// <summary>
        /// Product Ekler
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post(Product product)
        {
            if (ModelState.IsValid)
            {
                var model = _productService.CreateProduct(product);
                return CreatedAtAction("Get", new { id = model.Id }, model);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        /// <summary>
        /// Productı Günceller
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut()]
        public IActionResult Put(Product product)
        {
            if (_productService.GetProductById(product.Id) != null)
            {
                return Ok(_productService.UpdateProduct(product));
            }

            return NotFound();
        }

        /// <summary>
        /// Productı Siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_productService.GetProductById(id) != null)
            {
                _productService.DeleteProduct(id);
                return Ok();
            }

            return NotFound();

        }
    }
}
