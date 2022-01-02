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
    }
}