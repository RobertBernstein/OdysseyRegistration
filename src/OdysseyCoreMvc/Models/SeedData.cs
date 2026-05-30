using Microsoft.EntityFrameworkCore;
using OdysseyCoreMvc.Data;

namespace OdysseyCoreMvc.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OdysseyContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<OdysseyContext>>()))
            {
                if (context == null || context.Config == null)
                {
                    throw new ArgumentNullException("Null OdysseyContext");
                }

                // Look for any movies. If there are any movies in the database, the seed initializer returns and no
                // movies are added.
                if (context.Config.Any())
                {
                    return;   // DB has been seeded
                }

                context.Config.AddRange(
                    new Config
                    {
                        Name = "Name",
                        Value = "Value"
                    }//,

                    //new Config
                    //{
                    //    Name = "Name",
                    //    Value = "Value"
                    //},

                    //new Config
                    //{
                    //    Name = "Name",
                    //    Value = "Value"
                    //}
                );

                context.SaveChanges();
            }
        }
    }
}
