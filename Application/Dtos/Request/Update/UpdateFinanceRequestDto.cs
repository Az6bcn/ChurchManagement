using Application.Dtos.Request.Create;

namespace Application.Dtos.Request.Update
{
    public class UpdateFinanceRequestDto: CreateFinanceRequestDto
    {
        public int FinanceId { get; set; }
    }
}