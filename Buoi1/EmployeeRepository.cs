interface IEmployeeRepository : Employee
{
    Employee GetEmployee(int id);
    void SaveEmployee(Employee employee);
}