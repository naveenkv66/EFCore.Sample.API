using System.ComponentModel.DataAnnotations;

namespace EFCore.Sample.API.DataModels
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        public string? EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }       
        public string? Password { get; set; }
       
            
    }
}
