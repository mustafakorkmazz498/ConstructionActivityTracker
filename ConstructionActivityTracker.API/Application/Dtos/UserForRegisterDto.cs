using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class UserForRegisterDto : IDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }

    public UserForRegisterDto()
    {
        Email = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        Password = string.Empty;
    }

    public UserForRegisterDto(string email, string firstName, string lastName, string password)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }
}
