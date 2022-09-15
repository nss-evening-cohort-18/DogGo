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

        public List<Dog> GetAllDogs()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;
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

        public Dog? GetDogById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE Id = @Id ";
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                             return LoadFromData(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public void AddDog(Dog dog)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Dog (
                                          [Name],
                                          OwnerId,
                                          Breed,
                                          Notes,
                                          ImageUrl)
                                        OUTPUT INSERTED.ID
                                        VALUES (
                                          @Name, 
                                          @OwnerId, 
                                          @Breed, 
                                          @Notes, 
                                          @ImageUrl)";

                    cmd.Parameters.AddWithValue("@Name", dog.Name);
                    cmd.Parameters.AddWithValue("@OwnerId", dog.OwnerId);
                    cmd.Parameters.AddWithValue("@Breed", dog.Breed);
                    cmd.Parameters.AddWithValue("@Notes", dog.Notes);
                    cmd.Parameters.AddWithValue("@ImageUrl", dog.ImageUrl);

                    int id = (int)cmd.ExecuteScalar();
                    dog.Id = id;
                }
            }
        }

        public void UpdateDog(Dog dog)
        {
            using ( var conn = Connection)
            {
                conn.Open();
                using ( var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Dog
                                        SET
                                          [Name] = @Name,
                                          OwnerId = @OwnerId,
                                          Breed = @Breed,
                                          Notes = @Notes,
                                          ImageUrl = @ImageUrl
                                        WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Name", dog.Name);
                    cmd.Parameters.AddWithValue("@OwnerId", dog.OwnerId);
                    cmd.Parameters.AddWithValue("@Breed", dog.Breed);
                    cmd.Parameters.AddWithValue("@Notes", dog.Notes);
                    cmd.Parameters.AddWithValue("@ImageUrl", dog.ImageUrl);
                    cmd.Parameters.AddWithValue("@Id", dog.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDog(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Dog
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
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
