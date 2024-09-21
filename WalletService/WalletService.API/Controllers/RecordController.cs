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
        [HttpGet("between-dates")]
        public async Task<IActionResult> GetRecordBetweenDates(Guid walletId, DateTime startTime, DateTime endTime)
        {
            var res = await _recordService.GetRecordsBetweenDates(walletId, startTime, endTime);
            return Ok(res);
        }

        [HttpGet("cash-flow")]
        public IActionResult GetCashFlow(DateTime startTime, DateTime endTime)
        {
            var cashFlow = _recordService.GetCashFlow(startTime, endTime);
            return Ok(new { TotalIncome = cashFlow.TotalIncome, TotalExpense = cashFlow.TotalExpense });
        }

        [HttpGet("expense-recap")]
        public IActionResult GetExpenseRecapByCategory(DateTime startTime, DateTime endTime)
        {
            var expenseRecap = _recordService.GetExpenseRecapByCategory(startTime, endTime);
            return Ok(expenseRecap);
        }

        [HttpGet("last-records")]
        public IActionResult GetLastRecords(int count)
        {
            var lastRecords = _recordService.GetLastRecords(count);
            return Ok(lastRecords);
        }
    }
}
