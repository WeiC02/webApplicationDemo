namespace webApplicationDemo.Models
{
    public class AddEmployeeToCompanyViewModel
    {
        public CompanyModel Company { get; set; }
        public List<EmployeeModel> Employees { get; set; }
        public List<int> SelectedEmployeeIDs { get; set; } // For storing selected employees
    }
}
