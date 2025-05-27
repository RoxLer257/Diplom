using System.Data.Entity.Migrations;

namespace Diplom.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Diplom.Classes.VSK_DBEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Diplom.Classes.VSK_DBEntities context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
} 