using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private ProductService _productService;
        private UserService _userService;
        private readonly ILoggerManager _logger;
        public ProductController(ProductService productService,
                                 UserService userService,
                                 ILoggerManager logger)
        {
            _productService = productService;
            _userService = userService;
            _logger = logger;
        }
        
        [HttpGet("GetProducts")]
        public async Task<IActionResult> Get()
        {           
            int userid = int.Parse(HttpContext.User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
            _logger.LogInfo("Geting Product for user" + userid);
            return Ok(await _productService.GetAllProduct(userid));
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Put(ProductModel product)
        {
            int userid = int.Parse(HttpContext.User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
            _logger.LogInfo("Update Product for user" + userid);
            ActionResult response = BadRequest();

            if (await _userService.IsExistByID(userid) && await _productService.IsExistByID(product.ID))
            {
                _logger.LogInfo("Before Product Update");
                await _productService.UpdateProduct(product, userid);
                _logger.LogInfo("Product updated !!");
                response = NoContent();
            }
            return response;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> Post(ProductModel product)
        {
            int userid = int.Parse(HttpContext.User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
            ActionResult response = BadRequest();

            if (await _userService.IsExistByID(userid))
            {
                _logger.LogInfo("Before Product Add");
                ProductModel newProduct =await _productService.AddProduct(product,userid);
                _logger.LogInfo("Product Added!!");
                response = Ok(newProduct);
            }
            return response;
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> Delete(int ID)
        {
            ActionResult response = NotFound();
            Product product =await _productService.GetProductById(ID);
            if (product!=null)
            {
                _logger.LogInfo("Before Product Delete");
                await _productService.DeleteProduct(product);
                _logger.LogInfo("Product Deleted");
                response = Ok();
            }
            return response;
        }

    }
}