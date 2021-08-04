using System;
using Shared.Enums;

namespace Application.Dtos.Request.Create
{
    public class CreateFinanceRequestDto
    {
        public int TenantId { get; set; }
        public FinanceEnum FinanceTypeEnum { get; set; }
        public ServiceEnum ServiceTypeEnum { get; set; }
        public CurrencyEnum CurrencyTypeEnum { get; set; }
        public decimal Amount { get; set; }
        public DateTime GivenDate { get; set; }
        public string? Description { get; set; }
    }
}