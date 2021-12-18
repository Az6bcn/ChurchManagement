using Application.Dtos.Request.Create;

namespace Application.Dtos.Request.Update;

public class UpdateNewComerRequestDto: CreateNewComerRequestDto
{
    public int NewComerId { get; set; }
}