using Application.Dtos.Request.Create;

namespace Application.Dtos.Request.Update
{
    public class UpdateNerwComerRequestDto: CreateNewComerRequestDto
    {
        public int NewComerId { get; set; }
    }
}