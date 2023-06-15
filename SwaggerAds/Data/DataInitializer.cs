using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwaggerAds.Models;

namespace SwaggerAds.Data
{    
    /// <summary>
     /// Class responsible for initializing data in the application.
     /// </summary>

    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="DataInitializer"/> class.
        /// </summary>
        /// <param name="dbContext">The application database context.</param>
        public DataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Migrates the database and seeds initial data.
        /// </summary>
        public void MigrateData()
        {
            _dbContext.Database.Migrate();
            SeedData();

        }

        private void SeedData()
        {
            if(!_dbContext.AdContext.Any(x => x.Id == 1)){

                _dbContext.AdContext.Add(new Ads (){
                    Price = 150,
                    StartDate = DateTime.Now,
                });
            }
            if(!_dbContext.AdContext.Any(x => x.Id == 2)){

                _dbContext.AdContext.Add(new Ads (){
                    Price = 390,
                    StartDate = DateTime.Now.AddDays(3),
                    Description = "Good product"
                });
            }
            _dbContext.SaveChanges();
        }
    }
}