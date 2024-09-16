using Microsoft.AspNetCore.Mvc;
using WalletService.Application.DTOs;
using WalletService.Application.Interfaces;

namespace WalletService.API.Controllers
{
    [ApiController]
    [Route("record")]
    public class RecordController : ControllerBase
    {
        public IRecordService _recordService { get; set; }
        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecord(CreateRecordDto request)
        {
            var res = await _recordService.CreateRecord(request);
            return Ok(res);
        }
    }
}
