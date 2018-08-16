using Elmah;
using Employee.Data.Infrastructure;
using Employee.Data.Repositories;
using Employee.Entities;
using System.Collections.Generic;

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
        private readonly IEmployeeInfoRepository EmployeeRepository;
        private readonly IUnitOfWork UnitOfWork;
        public EmployeeInfoService(IEmployeeInfoRepository _employeeRepository, IUnitOfWork unitOfWork)
        {
            this.EmployeeRepository = _employeeRepository;
            this.UnitOfWork = unitOfWork;
        }
        public IEnumerable<EmployeeInfo> GetEmployeeList()
        {
            return EmployeeRepository.GetAll();
        }
        public EmployeeInfo GetEmployee(int id)
        {
            return EmployeeRepository.GetById(id);
        }
        public int CreateEmployee(EmployeeInfo Employee)
        {
            try
            {
                this.UnitOfWork.BeginTransaction();
                EmployeeRepository.Add(Employee);
                UnitOfWork.Commit();
            }
            catch (System.Exception ex)
            {
                UnitOfWork.Rollback();
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw ex;
            }
            return Employee.ID;
        }
        public bool UpdateEmployee(EmployeeInfo Employee)
        {
            try
            {
                this.UnitOfWork.BeginTransaction();
                EmployeeRepository.Update(Employee);
                UnitOfWork.Commit();
            }
            catch (System.Exception ex)
            {
                this.UnitOfWork.Rollback();
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw ex;
            }
            return true;
        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                this.UnitOfWork.BeginTransaction();
                EmployeeRepository.Delete(id);
                UnitOfWork.Commit();
            }
            catch (System.Exception ex)
            {
                this.UnitOfWork.Rollback();
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw ex;
            }
            return true;
        }
    }
}
