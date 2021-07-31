using Shared.Enums;

namespace Application.Dtos.Response.Update
{
    public class UpdateMinisterResponseDto
    {
        public int MinisterId { get; set; }
        public int MemberId { get; set; }
        public MinisterTitleEnum MinisterTitle { get; set; }
    }
}