using SmartSolution.SharedKernel.Domain.Seedwork;

namespace SmartSolution.Domain.AggregatesModel.RoleAggregate
{
    public class RoleParametr : Enumeration
    {
        public static RoleParametr SuperAdmin = new RoleParametr(1, RoleName.SuperAdmin);
        public static RoleParametr Admin = new RoleParametr(2, RoleName.Admin);
        public static RoleParametr User = new RoleParametr(3, RoleName.User);

        public RoleParametr(int id, string name) : base(id, name)
        {

        }

        public RoleParametr()
        {

        }
    }
}
