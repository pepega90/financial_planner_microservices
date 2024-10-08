﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletService.Domain.Models
{
    public class Record
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Tanggal { get; set; }
        public TransactionType Type { get; set; }
        public Guid WalletId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public enum TransactionType
    {
        Income,
        Expense
    }

    public interface IRecordRepo
    {
        Task<Record> CreateRecord(Record request);
        Task<List<Record>> GetRecordsBetweenDates(Guid walletId, DateTime startTime, DateTime endTime);
        (decimal TotalIncome, decimal TotalExpense) GetCashFlow(DateTime startTime, DateTime endTime);
        Dictionary<string, decimal> GetExpenseRecapByCategory(DateTime startTime, DateTime endTime);
        List<Record> GetLastRecords(int count);
    }
}
