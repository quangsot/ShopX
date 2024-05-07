using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common;
using Shop.Application.Interface;
using Shop.Application.Interface.ProductsService;
using Shop.Application.UnitOfWork;
using Shop.Domain.Entity;
using Shop.Domain.Exceptions;
using Shop.Domain.Interface.Repository;
using Shop.Domain.Interface.Repository.ProductsRepository;
using Shop.Domain.Interface.UseCases;
using Shop.Domain.Model.DTO;
using Shop.Domain.Model.Output;
using Shop.Domain.Model.Request;
using Shop.Domain.Model.Response;
using Shop.Domain.Model.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.UseCases
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelper _helper;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IProductDetailService _productDetailService;
        private readonly IProductImageService _productImageService;
        private readonly IProductConfigurationService _productConfigurationService;
        private readonly IVariationService _variationService;
        private readonly IVariationOptionService _variationOptionService;
        private readonly IVariationOptionGroupService _variationOptionGroupService;
        private readonly IBrandService _brandService;
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductConfigurationRepository _productConfigurationRepository;
        public ProductUseCase(IMapper mapper, IUnitOfWork unitOfWork, IHelper helper, IProductConfigurationRepository productConfigurationRepository, IProductImageRepository productImageRepository, IProductRepository productRepository, IProductService productService, IProductDetailService productDetailService, IProductImageService productImageService, IProductConfigurationService productConfigurationService, IVariationService variationService, IVariationOptionService variationOptionService, IVariationOptionGroupService variationOptionGroupService, IBrandService brandService)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
            _mapper = mapper;
            _productService = productService;
            _productDetailService = productDetailService;
            _productImageService = productImageService;
            _productConfigurationService = productConfigurationService;
            _variationService = variationService;
            _variationOptionService = variationOptionService;
            _variationOptionGroupService = variationOptionGroupService;
            _brandService = brandService;
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _productConfigurationRepository = productConfigurationRepository;
        }

        public async Task<Guid> AddNewProduct(ProductForm productForm)
        {
            // mở transaction
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // thêm product
                // thêm avatar
                var images = productForm.Images?.ToList();
                string avatarPath = string.Empty;
                if (images != null && images.Count > 0 && images[0] != null && images[0].Length > 0)
                {
                    avatarPath = await _helper.SaveImageAsync(images[0]);

                }

                // khởi tạo product
                var productCreated = new ProductCreateDTO()
                {
                    Code = productForm.Code,
                    Name = productForm.Name,
                    CategoryId = productForm.CategoryId,
                    Avatar = avatarPath,
                    BrandId = productForm.BrandId,
                    SupplierId = productForm.SupplierID
                };
                var product = await _productService.CreateAsync(productCreated, transaction);
                //var product = await _productService.CreateAsync(productCreated);
                //var product = _unitOfWork.DbContext.Add(productCreated);

                //thêm product detail
                var brand = await _brandService.GetByIdAsync(productForm.BrandId, transaction);
                var productDetailCreate = new ProductDetailCreateDTO()
                {
                    ProductId = product.Id,
                    Name = productForm.Name,
                    Brand = brand.Name,
                    InfoDetail = productForm.InfoDetail,
                    Discription = productForm.Discription
                };
                var productDetailCreated = await _productDetailService.CreateAsync(productDetailCreate, transaction);

                //thêm danh sách ảnh
                var imageList = new List<ProductImageDTO>();
                if (images != null && images.Count > 0)
                {
                    foreach (var image in images)
                    {
                        if (image != null && image.Length > 0)
                        {
                            string path = await _helper.SaveImageAsync(image);
                            imageList.Add(new ProductImageDTO() { ProductDetailId = productDetailCreated.Id, Name = $"{image.FileName}-{product.Name}", Path = $"/Images/{image.FileName}" });
                        }
                    }
                    // thêm ảnh vào DB
                    List<Productimage> productimages = new();
                    foreach (var item in imageList)
                    {
                        productimages.Add(_mapper.Map<Productimage>(item));
                    }
                    _unitOfWork.DbContext.AddRange(productimages);
                    //_ = await _productImageService.CreateManyAsync(imageList, transaction);
                }


                // thêm thông tin biến thể
                var variationList = productForm.Variations?.ToList();
                if (variationList != null && variationList.Count > 0)
                {
                    // thêm variation
                    var variation0 = variationList.First().VariationOptionGroup;
                    var attributeCreatedList = new List<VariationDTO>();
                    if (variation0 != null && variation0.Keys != null)
                    {
                        // kiểm tra sự tồn tại của thuộc tính
                        var keys = variation0.Keys;
                        foreach (var key in keys)
                        {
                            VariationDTO? variationExisted = await _variationService.GetByNameAsync(key, transaction);
                            // nếu không tồn tại
                            if (variationExisted == null)
                            {
                                // thêm variation vào DB
                                var attributeCreated = await _variationService.CreateAsync(new VariationDTO() { Name = key, Discription = null }, transaction);
                                attributeCreatedList.Add(attributeCreated);
                            }
                            else
                            {
                                attributeCreatedList.Add(variationExisted);
                            }
                        }

                    }
                    // thêm variation option group
                    var variationResponse = new List<VariationDTOResponse>();
                    foreach (var variation in variationList)
                    {
                        // đa biến thể
                        if (variation.VariationOptionGroup != null)
                        {
                            // tạo group mới
                            var group = await _variationOptionGroupService.CreateAsync(new VariationOptionGroupDTO(), transaction);

                            // thêm product config
                            var productConfigImage = new ProductImageDTO();
                            if (variation.Image != null)
                            {
                                string productConfigImagePath = await _helper.SaveImageAsync(variation.Image);
                                productConfigImage = await _productImageService.CreateAsync(new ProductImageDTO() { ProductDetailId = productDetailCreated.Id, Name = $"{variation.Image.FileName}-config-{product.Name}", Path = productConfigImagePath }, transaction);

                            }
                            var isDuplicateSKU = _productConfigurationRepository.IsDuplicateSKU(variation.SKU);
                            if (isDuplicateSKU) throw new BadRequestException(Domain.Enum.ErrorCode.InvalidConstrant, $"SKU {variation.SKU} đã tồn tại.");
                            var productConfig = new ProductConfigCreateDTO()
                            {
                                ProductId = product.Id,
                                VariationOptionGroupId = group.Id,
                                ProductImageId = productConfigImage.Id,
                                Price = variation.Price,
                                Inventory = variation.Inventory,
                                SKU = variation.SKU
                            };
                            _ = await _productConfigurationService.CreateAsync(productConfig, transaction);


                            // thêm variation option cùng group mới
                            var variationOptionGroup = variation.VariationOptionGroup;
                            if (variationOptionGroup != null)
                            {
                                foreach (var item in attributeCreatedList)
                                {
                                    var variationOptionValue = variationOptionGroup[item.Name];
                                    var variationOptionCreate = new VariationOptionDTO()
                                    {
                                        VariationId = item.Id,
                                        VariationOptionGroupId = group.Id,
                                        Value = variationOptionValue
                                    };
                                    _ = await _variationOptionService.CreateAsync(variationOptionCreate, transaction);
                                }
                            }
                            variationResponse.Add(new VariationDTOResponse()
                            {
                                DiscriptionVariationOptionGroup = group.Discription,
                                Image = productConfigImage.Path,
                                Price = variation.Price,
                                Inventory = variation.Inventory,
                                SKU = variation.SKU,
                            });
                        }
                        // đơn biến thể
                        else
                        {
                            // thêm product config
                            var productConfigImage = new ProductImageDTO();
                            if (variation.Image != null)
                            {
                                string productConfigImagePath = await _helper.SaveImageAsync(variation.Image);
                                productConfigImage = await _productImageService.CreateAsync(new ProductImageDTO() { ProductDetailId = productDetailCreated.Id, Name = $"{variation.Image.FileName}-config-{product.Name}", Path = productConfigImagePath }, transaction);

                            }
                            var productConfig = new ProductConfigCreateDTO()
                            {
                                ProductId = product.Id,
                                VariationOptionGroupId = Guid.Empty,
                                ProductImageId = productConfigImage.Id,
                                Price = variation.Price,
                                Inventory = variation.Inventory,
                                SKU = variation.SKU
                            };
                            _ = await _productConfigurationService.CreateAsync(productConfig, transaction);
                        }
                    }
                    _unitOfWork.DbContext.SaveChanges();
                    await transaction.CommitAsync();

                    return product.Id;
                }
                else
                {
                    throw new BadRequestException(Domain.Enum.ErrorCode.InvalidInput, "Thông tin biến thể không hợp lệ.");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }
        public async Task<BaseResponse<ProductDetailDTO>> GetProductDetailAsync(Guid ProductId)
        {
            var productDetailCommon = await _productDetailService.GetProductDetailByProductIdAsync(ProductId);
            if (productDetailCommon == null)
            {
                return new BaseResponse<ProductDetailDTO>()
                {
                    Message = "Success",
                    TraceId = "",
                    Data = null
                };
            }
            else
            {
                var productImages = await _productImageService.GetImagesByProductDetailIdAsync(productDetailCommon.Id);

                var productConfig = await _productConfigurationService.GetProductConfigByProductIdAsync(productDetailCommon.ProductId);

                foreach (var pc in productConfig)
                {
                    var variation = await _productConfigurationService.GetVariationProductAsync(pc.VariationOptionGroupId);
                    pc.variation = variation;
                }
                productDetailCommon.productConfigs = productConfig;
                productDetailCommon.Images = productImages.Select(x => x.Path).ToList();
                return new BaseResponse<ProductDetailDTO>()
                {
                    Message = "Success",
                    TraceId = "",
                    Data = productDetailCommon
                };
            }
        }
        public async Task<PageResponse<ProductResponse>> PagingFilterProductByCategoryAsync(Dictionary<string, string> conditionFilter)
        {
            /**
             * lấy mã : product
             * lấy tên : product
             * lấy hình ảnh : productImage > productId
             * lấy tổng số bán : product
             * lấy trung bình số sao : product
             * lấy độ hot : product
             * lấy tổng phần trăm giảm giá : discount
             */

            int page = 1;
            int size = 10;
            if (conditionFilter.ContainsKey("page"))
            {
                var pageString = conditionFilter["page"];
                _ = int.TryParse(pageString, out page);
                conditionFilter.Remove("page");
            }
            if (conditionFilter.ContainsKey("size"))
            {
                var sizeString = conditionFilter["size"];
                _ = int.TryParse(sizeString, out size);
                conditionFilter.Remove("size");

            }

            string sortBy = string.Empty;
            if (conditionFilter.ContainsKey("sortBy"))
            {
                sortBy = conditionFilter["sortBy"];
                conditionFilter.Remove("sortBy");

            }

            var filterPaging = await _productRepository.FilterPagingProductAsync(page, size, sortBy, conditionFilter);

            var result = new PageResponse<ProductResponse>
            {
                Message = "Success",
                TraceId = "",
                Data = filterPaging,
            };

            return result;
        }

        //public async Task<ProductCreateResponse> UpdateProductAsync(ProductForm productForm)
        //{
        //    // kiểm tra thông tin của product
        //    var productExist = await _productRepository.GetByCodeAsync(productForm.Code)
        //        ?? throw new BadRequestException(Domain.Enum.ErrorCode.InvalidInput, "Sản phẩm không tồn tại.");

        //    // kiểm tra SKU không được trùng
        //    var SKUDuplicates = new List<string>();
        //    foreach (var item in productForm.Variations)
        //    {
        //        var isDuplicateSKU = _productConfigurationRepository.IsDuplicateSKU(item.SKU);
        //        if (isDuplicateSKU) SKUDuplicates.Add(item.SKU);
        //    }
        //    if (SKUDuplicates != null && SKUDuplicates.Count > 0)
        //    {
        //        var err = new StringBuilder();
        //        SKUDuplicates.ForEach(s =>
        //        {
        //            err.Append($"{s}, ");
        //        });
        //        throw new BadRequestException(Domain.Enum.ErrorCode.InvalidConstrant, $"SKU: {err} đã tồn tại.");
        //    }

        //    using var transaction = await _unitOfWork.BeginTransactionAsync();
        //    try
        //    {
        //        // xóa productConfig cũ
        //        // lấy product config cũ
        //        var productConfigDelete = await _productConfigurationRepository.GetProductConfigurationByProductIdAsync(productExist.Id);
        //        var p = _productConfigurationRepository.DeleteManyAsync()

        //        // cập nhật product
        //        // thay đổi avatar
        //        if (productExist.Avatar != null)
        //        {
        //            _ = _helper.DeleteImage(productExist.Avatar);
        //        }
        //        var images = productForm.Images?.ToList();
        //        string avatarPath = string.Empty;
        //        if (images != null && images[0] != null && images[0].Length > 0)
        //        {
        //            avatarPath = await _helper.SaveImageAsync(images[0]);

        //        }

        //        // khởi tạo product
        //        var productUpdate = new ProductUpdateDTO()
        //        {
        //            Code = productForm.Code,
        //            Name = productForm.Name,
        //            CategoryId = productForm.CategoryId,
        //            Avatar = avatarPath,
        //            BrandId = productForm.BrandId,
        //            SupplierId = productForm.SupplierID
        //        };
        //        var productUpdated = await _productService.UpdateAsync(productExist.Id, productUpdate, transaction);
        //        //var product = await _productService.CreateAsync(productCreated);
        //        //var product = _unitOfWork.DbContext.Add(productCreated);

        //        //cập nhật product detail
        //        var brand = await _brandService.GetByIdAsync(productForm.BrandId, transaction);
        //        var productDetailNeedUpdate = await _productDetailService.GetProductDetailByProductIdAsync(productUpdated.Id)
        //            ?? throw new BadRequestException(Domain.Enum.ErrorCode.Error, "Thông tin chi tiết sản phẩm không tồn tại.");
        //        var productDetailUpdate = new ProductDetailUpdateDTO()
        //        {
        //            Name = productForm.Name,
        //            Brand = brand.Name,
        //            InfoDetail = productForm.InfoDetail,
        //            Discription = productForm.Discription
        //        };
        //        var productDetailUpdated = await _productDetailService.UpdateAsync(productDetailNeedUpdate.Id, productDetailUpdate, transaction);

        //        //cập nhật danh sách ảnh
        //        // lấy các ảnh của sản phẩm
        //        var imagePathNeedUpdate = await _productImageService.GetImagesByProductDetailIdAsync(productDetailNeedUpdate.Id);
        //        foreach (var item in imagePathNeedUpdate)
        //        {
        //            if (item.Path != null)
        //                _helper.DeleteImage(item.Path);
        //        }
        //        // xóa danh sách ảnh khỏi DB
        //        await _productImageRepository.DeleteManyAsync(imagePathNeedUpdate);
        //        var imageList = new List<ProductImageDTO>();
        //        if (images != null && images.Count > 0)
        //        {
        //            foreach (var image in images)
        //            {
        //                if (image != null && image.Length > 0)
        //                {

        //                    string path = await _helper.SaveImageAsync(image);
        //                    imageList.Add(new ProductImageDTO() { ProductDetailId = productDetailUpdated.Id, Name = $"{image.FileName}-{productUpdated.Name}", Path = $"/Images/{image.FileName}" });
        //                }
        //            }
        //            // thêm ảnh vào DB
        //            List<Productimage> productimages = new();
        //            foreach (var item in imageList)
        //            {
        //                productimages.Add(_mapper.Map<Productimage>(item));
        //            }
        //            _unitOfWork.DbContext.AddRange(productimages);
        //            //_ = await _productImageService.CreateManyAsync(imageList, transaction);
        //        }


        //        // cập nhật thông tin biến thể
        //        var variationList = productForm.Variations?.ToList();
        //        if (variationList != null && variationList.Count > 0)
        //        {
        //            // thêm variation
        //            var variation0 = variationList.First().VariationOptionGroup;
        //            var attributeCreatedList = new List<VariationDTO>();
        //            if (variation0 != null && variation0.Keys != null)
        //            {
        //                // kiểm tra sự tồn tại của thuộc tính
        //                var keys = variation0.Keys;
        //                foreach (var key in keys)
        //                {
        //                    VariationDTO? variationExisted = await _variationService.GetByNameAsync(key, transaction);
        //                    // nếu không tồn tại
        //                    if (variationExisted == null)
        //                    {
        //                        // thêm variation vào DB
        //                        var attributeCreated = await _variationService.CreateAsync(new VariationDTO() { Name = key, Discription = null }, transaction);
        //                        attributeCreatedList.Add(attributeCreated);
        //                    }
        //                    else
        //                    {
        //                        attributeCreatedList.Add(variationExisted);
        //                    }
        //                }

        //            }
        //            // thêm variation option group
        //            var variationResponse = new List<VariationDTOResponse>();
        //            foreach (var variation in variationList)
        //            {
        //                // đa biến thể
        //                if (variation.VariationOptionGroup != null)
        //                {
        //                    // tạo group mới
        //                    //var group = await _variationOptionGroupService.CreateAsync(new VariationOptionGroupDTO(), transaction);

        //                    // cập nhật product config

        //                    // lấy config cũ
        //                    var productConfigNeedUpdate = await _productConfigurationService.GetProductConfigByProductIdAsync(productUpdated.Id);

        //                    // xóa ảnh cũ


        //                    var productConfigImage = new ProductImageDTO();
        //                    if (variation.Image != null)
        //                    {
        //                        string productConfigImagePath = await _helper.SaveImageAsync(variation.Image);
        //                        productConfigImage = await _productImageService.CreateAsync(new ProductImageDTO() { ProductDetailId = productDetailCreated.Id, Name = $"{variation.Image.FileName}-config-{product.Name}", Path = productConfigImagePath }, transaction);

        //                    }
        //                    var productConfig = new ProductConfigCreateDTO()
        //                    {
        //                        ProductId = product.Id,
        //                        VariationOptionGroupId = group.Id,
        //                        ProductImageId = productConfigImage.Id,
        //                        Price = variation.Price,
        //                        Inventory = variation.Inventory,
        //                        SKU = variation.SKU
        //                    };
        //                    _ = await _productConfigurationService.CreateAsync(productConfig, transaction);


        //                    // thêm variation option cùng group mới
        //                    var variationOptionGroup = variation.VariationOptionGroup;
        //                    if (variationOptionGroup != null)
        //                    {
        //                        foreach (var item in attributeCreatedList)
        //                        {
        //                            var variationOptionValue = variationOptionGroup[item.Name];
        //                            var variationOptionCreate = new VariationOptionDTO()
        //                            {
        //                                VariationId = item.Id,
        //                                VariationOptionGroupId = group.Id,
        //                                Value = variationOptionValue
        //                            };
        //                            _ = await _variationOptionService.CreateAsync(variationOptionCreate, transaction);
        //                        }
        //                    }
        //                    variationResponse.Add(new VariationDTOResponse()
        //                    {
        //                        DiscriptionVariationOptionGroup = group.Discription,
        //                        Image = productConfigImage.Path,
        //                        Price = variation.Price,
        //                        Inventory = variation.Inventory,
        //                        SKU = variation.SKU,
        //                    });
        //                }
        //                // đơn biến thể
        //                else
        //                {
        //                    // thêm product config
        //                    var productConfigImage = new ProductImageDTO();
        //                    if (variation.Image != null)
        //                    {
        //                        string productConfigImagePath = await _helper.SaveImageAsync(variation.Image);
        //                        productConfigImage = await _productImageService.CreateAsync(new ProductImageDTO() { ProductDetailId = productDetailCreated.Id, Name = $"{variation.Image.FileName}-config-{product.Name}", Path = productConfigImagePath }, transaction);

        //                    }
        //                    var productConfig = new ProductConfigCreateDTO()
        //                    {
        //                        ProductId = product.Id,
        //                        VariationOptionGroupId = Guid.Empty,
        //                        ProductImageId = productConfigImage.Id,
        //                        Price = variation.Price,
        //                        Inventory = variation.Inventory,
        //                        SKU = variation.SKU
        //                    };
        //                    _ = await _productConfigurationService.CreateAsync(productConfig, transaction);
        //                }
        //            }
        //            _unitOfWork.DbContext.SaveChanges();
        //            await transaction.CommitAsync();

        //            return new ProductCreateResponse()
        //            {
        //                Code = productCreated.Code,
        //                Name = productCreated.Name,
        //                Avatar = productCreated.Avatar,
        //                CategoryId = productCreated.CategoryId,
        //                SupplierId = productCreated.SupplierId,
        //                BrandId = productCreated.BrandId,
        //                VariationDTOs = variationResponse
        //            };
        //        }
        //        else
        //        {
        //            throw new BadRequestException(Domain.Enum.ErrorCode.InvalidInput, "Thông tin biến thể không hợp lệ.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync();
        //        throw;
        //    }
        //    finally
        //    {
        //        await transaction.DisposeAsync();
        //    }
        //}
    }
}
