using EShopper.Contracts.V1.Requests.Category;
using EShopper.Contracts.V1.Responses.Category;

namespace EShopper.Business.Services.Abstract
{
    public interface ICategoryService
    {
        CategoryResponseModel CreateCategory(CreateCategoryRequestModel createCategoryRequest);
    }
}