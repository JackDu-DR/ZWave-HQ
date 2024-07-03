using System.ComponentModel.DataAnnotations;

namespace ZWave.Shared.Enums
{
    public enum UserRole
    {
        [Display(Name = "Admin")]
        Admin,

        [Display(Name = "Production Engineer")]
        ProductionEngineer,

        [Display(Name = "Service Engineer")]
        ServiceEngineer,

        [Display(Name = "Operator")]
        Operator
    }
}
