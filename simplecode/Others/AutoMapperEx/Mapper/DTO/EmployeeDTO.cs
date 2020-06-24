namespace AutoMapperEx.Mapper.DTO
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public TelephoneNumberDTO Telephone { get; set; }
    }
}
