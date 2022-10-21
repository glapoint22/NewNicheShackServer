﻿using Microsoft.EntityFrameworkCore;
using Website.Domain.Entities;

namespace Website.Application.Common.Interfaces
{
    public interface IWebsiteDbContext
    {
        DbSet<CollaboratorProduct> CollaboratorProducts { get; }
        DbSet<Collaborator> Collaborators { get; }
        DbSet<List> Lists { get; }
        DbSet<Media> Media { get; }
        DbSet<Niche> Niches { get; }
        DbSet<OrderProduct> OrderProducts { get; }
        DbSet<ProductOrder> ProductOrders { get; }
        DbSet<ProductPrice> ProductPrices { get; }
        DbSet<Product> Products { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<Subniche> Subniches { get; }
        DbSet<User> Users { get; }
        


        Task<int> SaveChangesAsync();
    }
}