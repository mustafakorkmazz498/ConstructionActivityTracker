﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pipelines.Authorization;
public interface ISecuredRequest
{
    string[] Roles { get; }
}
