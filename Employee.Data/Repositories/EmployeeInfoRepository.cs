using Employee.Entities;
using Employee.Data.Infrastructure;

namespace Employee.Data.Repositories
{
    public class EmployeeInfoRepository : RepositoryBase<EmployeeInfo>, IEmployeeInfoRepository
    {
        public EmployeeInfoRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
    public interface IEmployeeInfoRepository : IRepository<EmployeeInfo>
    {
    }
}
