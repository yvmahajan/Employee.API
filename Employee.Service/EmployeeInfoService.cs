using Elmah;
using Employee.Data.DBSession;
using Employee.Data.Infrastructure;
using Employee.Data.Repositories;
using Employee.Entities;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace Employee.Service
{
    // operations you want to expose
    public interface IEmployeeInfoService
    {
        IEnumerable<EmployeeInfo> GetEmployeeList();
        EmployeeInfo GetEmployee(int id);
        int CreateEmployee(EmployeeInfo EmployeeStatus);
        bool UpdateEmployee(EmployeeInfo EmployeeStatus);
        bool DeleteEmployee(int id);
    }
    public class EmployeeInfoService : IEmployeeInfoService
    {
        //private readonly IEmployeeInfoRepository EmployeeRepository;
        //private readonly IUnitOfWork UnitOfWork;
        //public EmployeeInfoService(IEmployeeInfoRepository _employeeRepository, IUnitOfWork unitOfWork)
        //{
        //    //this.EmployeeRepository = _employeeRepository;
        //    //this.UnitOfWork = unitOfWork;
        //}
        public EmployeeInfoService()
        {
            //this.EmployeeRepository = _employeeRepository;
            //this.UnitOfWork = unitOfWork;
        }
        public IEnumerable<EmployeeInfo> GetEmployeeList()
        {
            using (var repository = new RepositoryBase<EmployeeInfo>())
            {
                return repository.GetAll().ToList();
            }
        }
        public EmployeeInfo GetEmployee(int id)
        {
            using (var repository = new RepositoryBase<EmployeeInfo>())
            {
                return repository.GetById(id);
            }
        }
        public int CreateEmployee(EmployeeInfo Employee)
        {
            using (var repository = new RepositoryBase<EmployeeInfo>())
            {
                try
                {
                    repository.BeginTransaction();
                    repository.Add(Employee);
                }
                catch (System.Exception ex)
                {
                    repository.RollbackTransaction();
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    throw ex;
                }
            }
            return Employee.ID;
        }
        public bool UpdateEmployee(EmployeeInfo Employee)
        {
            using (var repository = new RepositoryBase<EmployeeInfo>())
            {
                try
                {
                    repository.BeginTransaction();
                    repository.Update(Employee);
                }
                catch (System.Exception ex)
                {
                    repository.RollbackTransaction();
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    throw ex;
                }
            }
            return true;
        }
        public bool DeleteEmployee(int id)
        {
            using (var repository = new RepositoryBase<EmployeeInfo>())
            {
                try
                {
                    repository.BeginTransaction();
                    repository.Delete(id);
                }
                catch (System.Exception ex)
                {
                    repository.RollbackTransaction();
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    throw ex;
                }
            }
            return true;
        }
    }
}
