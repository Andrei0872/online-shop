using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authorization;

using server.Helpers.Constants;

using server.DAL.UnitOfWork;
using server.Responses;
using server.DTOs;
using server.Models;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

    private IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [Authorize(Roles = UserRoleType.Admin + "," + UserRoleType.User)]
    public IActionResult getAllProducts () {
        var productResp = new ProductResponse();
        var products = _unitOfWork.Product.GetAll();

        productResp.Data = products;

        return Ok(productResp);
    }


    [HttpPost]
    [Authorize(Roles = UserRoleType.Admin)]
    public async Task<IActionResult> AddProduct ([FromBody] AddProductDto addProductDto)
    {
        var resp = new GenericResponse();
        
        var newProduct = new Product();
        newProduct.Name = addProductDto.Name;
        newProduct.Price = addProductDto.Price;
        newProduct.Category = addProductDto.Category;

        this._unitOfWork.Product.Insert(newProduct);
        await this._unitOfWork.SaveAsync();

        resp.Data = "The product has been successfully added.";

        return Ok(resp);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = UserRoleType.Admin)]
    public async Task<IActionResult> UpdatedProduct (int id, [FromBody] AddProductDto updatedProductDto) {
        var resp = new GenericResponse();

        var existingProduct = await this._unitOfWork.Product.Get(id);
        existingProduct.Name = updatedProductDto.Name;
        existingProduct.Price = updatedProductDto.Price;
        existingProduct.Category = updatedProductDto.Category;

        this._unitOfWork.Product.Update(existingProduct);
        await this._unitOfWork.SaveAsync();

        resp.Data = "The product has been successfully updated.";

        return Ok(resp);
    }
    }
}