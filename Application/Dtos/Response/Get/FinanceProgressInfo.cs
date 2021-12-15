namespace Application.Dtos.Response.Get;

public class FinanceProgressInfo
{
    public string Title { get; set; }
    public decimal Value { get; set; }
    public decimal ActiveProgress { get; set; }
    public string Description { get; set; }
}