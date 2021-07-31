using Shared.Enums;

namespace Application.Dtos.Response.Create
{
    public class CreateMinisterResponseDto
    {
        public int MinisterId { get; set; }
        public int MemberId { get; set; }
        public MinisterTitleEnum MinisterTitle { get; set; }
    }
}