using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgesRegistrationRazorDb.Models
{
    public static class SeedHelper
    {
        public static List<TEntity>? SeedData<TEntity>(string fileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = "SeedData";
            string fullPath = Path.Combine(currentDirectory, path, fileName);

            var result = new List<TEntity>();
            using (StreamReader reader = new StreamReader(fullPath))
            {
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<TEntity>>(json);
            }

            return result;
        }
    }
}
