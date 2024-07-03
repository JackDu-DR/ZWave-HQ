using ZWave.Shared.Enums;

namespace ZWave.Shared.models
{
    public class PermissionDto
    {
        public UserRole UserRole { get; set; }
        public TabPage TabPage { get; set; }
        public bool AccessAllowed { get; set; }
    }
}
