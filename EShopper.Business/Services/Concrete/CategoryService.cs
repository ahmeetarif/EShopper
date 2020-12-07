using System;
using System.Linq;
using AutoMapper;
using EShopper.Business.Services.Abstract;
using EShopper.Common.Exceptions;
using EShopper.Common.Middleware;
using EShopper.Contracts.V1.Requests.Category;
using EShopper.Contracts.V1.Responses.Category;
using EShopper.DataAccess.UnitOfWork;
using EShopper.Entities.Models;

namespace EShopper.Business.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork,
                IMapper mapper,
                CurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public CategoryResponseModel CreateCategory(CreateCategoryRequestModel createCategoryRequest)
        {
            if (createCategoryRequest == null) throw new EShopperException("Please provide required information!");

            var isCategoryExist = _unitOfWork.Category.Find(x => x.Title == createCategoryRequest.Title).FirstOrDefault();
            if (isCategoryExist != null) throw new EShopperException($"{createCategoryRequest.Title} - Already exist!");

            using (var transaction = _unitOfWork.EShopperDbContext.Database.BeginTransaction())
            {
                var mappedCategoryData = _mapper.Map<Category>(createCategoryRequest, options =>
                {
                    options.AfterMap((src, dest) => dest.UserId = _currentUser.Id);
                });

                try
                {
                    _unitOfWork.Category.Add(mappedCategoryData);

                    _unitOfWork.Complete();
                    transaction.Commit();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    throw new EShopperException(ex.Message);
                }

                transaction.Dispose();
            }

            return new CategoryResponseModel
            {
                Message = $"{createCategoryRequest.Title} - Added"
            };
        }

    }
}