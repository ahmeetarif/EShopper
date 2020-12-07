using EShopper.Business.Services.Abstract;
using EShopper.Contracts.V1;
using EShopper.Contracts.V1.Requests.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShopper.Api.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Policy = "CreateCategoryPolicy", Roles = "Admin")]
        [Route(ApiRoutes.Category.Create)]
        [HttpPost]
        public IActionResult Create([FromBody] CreateCategoryRequestModel createCategoryRequestModel)
        {
            var response = _categoryService.CreateCategory(createCategoryRequestModel);
            return Ok(response);
        }
    }
}