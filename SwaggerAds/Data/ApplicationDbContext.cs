using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwaggerAds.Models;

namespace SwaggerAds.Data
{
    /// <summary>
    /// Represents the application database context.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        ///// <summary>
        ///// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        ///// </summary>
        ///// <param name="options">The options for configuring the context.</param>
        public ApplicationDbContext(DbContextOptions options) : base(options){

        }
        /// <summary>
        /// Gets or sets the collection of ads in the context.
        /// </summary>
        public DbSet<Ads> AdContext {get; set;} 
    }
}