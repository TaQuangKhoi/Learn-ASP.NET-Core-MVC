﻿
namespace CompanyManagement.Models.Employees
{
    public class Employee
    {
        // Khai báo các thuộc tính của Employee
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        
        public string PhotoPath { get; set; }
    }
}