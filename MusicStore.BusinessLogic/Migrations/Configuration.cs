
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MusicStore.BusinessLogic.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MusicStore.BusinessLogic.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(MusicStore.BusinessLogic.Data.AppDbContext context)
        {
        }
    } 
}