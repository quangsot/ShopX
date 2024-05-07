﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application;
using Shop.Application.Interface;
using Shop.Domain.Model.Response.Base;
using Shop.Domain.Model.Response;
using Shop.Domain.Model.Output;

namespace Shop.Controllers
{
    [ApiController]
    public abstract class ReadController<TEntityDTO> : ControllerBase where TEntityDTO : class
    {
        protected readonly IReadService<TEntityDTO> _readService;

        public ReadController(IReadService<TEntityDTO> readService)
        {
            _readService = readService;
        }
        /// <summary>
        /// hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        /// author: Trương Mạnh Quang (15/11/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _readService.GetAllAsync();
            return Ok(new BaseResponse<IEnumerable<TEntityDTO>>() { Data = result });
        }

        /// <summary>
        /// hàm lấy 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// author: Trương Mạnh Quang (15/11/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var result = await _readService.GetByIdAsync(id);

            return Ok(new BaseResponse<TEntityDTO>() { Data = result });
        }

        /// <summary>
        /// lấy nhiều bản ghi theo ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// author: Trương Mạnh Quang (15/11/2023)
        [HttpPost("get-ids")]
        public async Task<IActionResult> GetByIdsAsync([FromBody] List<Guid> ids)
        {
            var result = await _readService.GetByIdsAsync(ids);

            return Ok(new BaseResponse<IEnumerable<TEntityDTO>>() { Data = result });
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterPagingAsync([FromQuery] int page, [FromQuery] int size, [FromQuery] string? search)
        {
            var result = await _readService.FillterPagingAsync(page: page, size: size, search: search);

            return Ok(new PageResponse<TEntityDTO>() { Data = result });
        }
    }
}
