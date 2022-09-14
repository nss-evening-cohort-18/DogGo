using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories
{
    public class DogRepository : BaseRepository, IDogRepository
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                   	   [Name],
                                                   	   OwnerId,
                                                   	   Breed,
                                                   	   ISNULL(Notes, '') AS Notes,
                                                   	   ISNULL(ImageUrl, '') AS ImageUrl
                                                   FROM Dog ";
        public DogRepository(IConfiguration config) : base(config) { }

        public List<Dog> GetDogsByOwnerId(int ownerId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE OwnerId = @OwnerId ";
                    cmd.Parameters.AddWithValue("@OwnerId", ownerId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Dog> results = new();
                        
                        while (reader.Read())
                        {
                            results.Add(LoadFromData(reader));
                        }
                        
                        return results;
                    }
                }
            }
        }

        private Dog LoadFromData(SqlDataReader reader)
        {
            return new Dog
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
            };
        }
    }
}
