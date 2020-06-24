using AutoMapperEx.Mapper.Shared;

namespace AutoMapperEx.Mapper.Model
{
    public class EmployeeModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public TelephoneNumberModel Telephone { get; set; }
    }
}
