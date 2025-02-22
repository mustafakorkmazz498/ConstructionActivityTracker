using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Activities.Constants;

public static class ActivitiesOperationClaims
{
    private const string _section = "Activities";

    public const string Admin = "Admin";

    public const string Read = "Read";
    public const string Write = "Write";

    public const string Create = "Create";
    public const string Update = "Update";
    public const string Delete = "Delete";
}
