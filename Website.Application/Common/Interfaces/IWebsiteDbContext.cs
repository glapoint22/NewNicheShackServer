﻿using Microsoft.EntityFrameworkCore;
using Website.Domain.Entities;

namespace Website.Application.Common.Interfaces
{
    public interface IWebsiteDbContext
    {
        DbSet<Collaborator> Collaborators { get; }
        DbSet<List> Lists { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<User> Users { get; }
        

        Task<int> SaveChangesAsync();
    }
}