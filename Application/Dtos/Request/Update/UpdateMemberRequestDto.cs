using Application.Dtos.Request.Create;

namespace Application.Dtos.Request.Update
{
    public class UpdateMemberRequestDto: CreateMemberRequestDto
    {
        public int MemberId { get; set; }
    }
}