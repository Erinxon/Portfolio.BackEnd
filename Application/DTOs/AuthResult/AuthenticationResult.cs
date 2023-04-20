﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.AuthResult
{
    public record AuthenticationResult(int UserId, string Name, string Email, string Token);
}
