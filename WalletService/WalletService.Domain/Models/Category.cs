using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletService.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }

    public interface ICategoryRepo
    {
        Task<Category> CreateCategory(Category request);
    }
}
