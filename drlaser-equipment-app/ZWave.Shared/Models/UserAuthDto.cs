using ZWave.Shared.Enums;

namespace ZWave.Shared.Models
{
    public class UserAuthDto
    {
        [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
        public UserAuthDto()
        {

        }

        public Guid Id { get; set; }

        public string? Fullname { get; set; }

        public required IEnumerable<TabPage> PermittedPages { get; set; }
    }
}