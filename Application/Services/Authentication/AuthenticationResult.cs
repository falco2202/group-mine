using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authentication
{
    public record AuthenticationResult (
        Guid Id,
        string FistName,
        string LastName, 
        string Email,
        string Token
    );
}
