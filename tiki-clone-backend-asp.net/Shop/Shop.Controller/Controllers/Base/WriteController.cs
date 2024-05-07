using Microsoft.AspNetCore.Mvc;
using Shop.Application;
using Shop.Application.Interface;
using Shop.Domain.Model.Response.Base;

namespace Shop.Controllers
{
    [ApiController]
    public class WriteController<TEntityDTO, TEntityCreateDTO, TEntityUpdateDTO> : ReadController<TEntityDTO>
        where TEntityDTO : class
        where TEntityCreateDTO : class
        where TEntityUpdateDTO : class
    {
        protected readonly IWriteService<TEntityDTO, TEntityCreateDTO, TEntityUpdateDTO> _writeService;
        public WriteController(IWriteService<TEntityDTO, TEntityCreateDTO, TEntityUpdateDTO> writeService) : base(writeService)
        {
            _writeService = writeService;
        }

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync([FromBody] TEntityCreateDTO entityCreateDTO)
        {
            var result = await _writeService.CreateAsync(entityCreateDTO);
            return Ok(new BaseResponse<TEntityDTO>() { Data = result });
        }

        [HttpPost("entities")]
        public async Task<IActionResult> CreateManyAsync([FromBody] List<TEntityCreateDTO> entityCreateDTOs)
        {
            var result = await _writeService.CreateManyAsync(entityCreateDTOs);
            return Ok(new BaseResponse<IEnumerable<TEntityDTO>>() { Data = result });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromQuery]Guid id,[FromBody]TEntityUpdateDTO entityUpdateDTO)
        {
            var result = await _writeService.UpdateAsync(id, entityUpdateDTO);
            return Ok(new BaseResponse<TEntityDTO>() { Data = result });
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="entityDTO"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result =  await _writeService.DeleteAsync(id);
            return Ok(new BaseResponse<TEntityDTO>() { Data = result });
        }
    }
}
