using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Constants;

public static class AuthOperationClaims
{
    private const string _section = "Auth";

    public const string Admin = "Admin";

    public const string Write = "Write";
    public const string Read = "Read";

    public const string RevokeToken = "RevokeToken";
}

