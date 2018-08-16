# RESTful Web API for Employee
You can use **Fiddler or SoapUI** tool to test API.

1. GET http://BaseURL/api/employee
   Return all employee data.
   
2. GET http://BaseURL/api/employee/{id}
   Return employee data by employee id.
   
3. POST http://BaseURL/api/employee Create new employee.
   
   Request Body:
   {"ID":0,"FirstName":"Yogesh","LastName":"Mahajan","Designation":"TL","Age":35,"Salary":10000.0}
   
4. PUT http://BaseURL/api/employee Update employee.
   
   Request Body:
   {"ID":1,"FirstName":"Yogesh","LastName":"Mahajan","Designation":"TL","Age":35,"Salary":10000.0}
      
5. DELETE http://BaseURL/api/employee/{id}
   Delete employee data by employee id.


#### Used below : 
  - Dependency Injection : Used Autofac container
  - Open source ORM tool : Used nHibernate
  - Logging : Used Elmah logger tool.
  - Exception handling : Implemeted "ExceptionFilterAttribute" to handle exception. 
  - SQL server db : Create "EmployeeDB" database with "Employee" table. find script file under /DB/DBScript.sql


