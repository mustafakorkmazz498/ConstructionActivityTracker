﻿using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class RefreshToken : Entity<int>
{
    public int UserId { get; set; }

    public string Token { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string CreatedByIp { get; set; }

    public DateTime? RevokedDate { get; set; }

    public string? RevokedByIp { get; set; }

    public string? ReplacedByToken { get; set; }

    public string? ReasonRevoked { get; set; }

    public virtual User User { get; set; } = default!;

    public RefreshToken()
    {
        Token = string.Empty;
        CreatedByIp = string.Empty;
    }

    public RefreshToken(int userId, string token, DateTime expirationDate, string createdByIp)
    {
        UserId = userId;
        Token = token;
        ExpirationDate = expirationDate;
        CreatedByIp = createdByIp;
    }

    public RefreshToken(int id, int userId, string token, DateTime expirationDate, string createdByIp)
        : base(id)
    {
        UserId = userId;
        Token = token;
        ExpirationDate = expirationDate;
        CreatedByIp = createdByIp;
    }
}
