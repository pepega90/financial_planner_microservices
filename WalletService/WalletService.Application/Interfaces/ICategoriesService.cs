using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletService.Application.DTOs;
using WalletService.Domain.Models;

namespace WalletService.Application.Interfaces
{
    public interface ICategoriesService
    {
        Task<Category> CreateCategory(CreateCategoryDto createCategoryDto);
    }
}
