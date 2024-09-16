using Microsoft.AspNetCore.Mvc;
using WalletService.Application.DTOs;
using WalletService.Application.Interfaces;

namespace WalletService.API.Controllers
{
    [ApiController]
    [Route("wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet(CreateWalletDto request)
        {
            var result = await _walletService.CreateWallet(request);
            return Ok(result);
        }
    }
}
