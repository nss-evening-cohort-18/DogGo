using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories
{
    public class OwnerRepository : BaseRepository, IOwnerRepository
    {
        private readonly string _baseSqlSelect = @"SELECT [Owner].Id,
                                                   	      Email,
                                                   	      [Owner].[Name],
                                                   	      [Address],
                                                   	      NeighborhoodId,
                                                          Neighborhood.[Name] AS NeighborhoodName,
                                                   	      Phone
                                                   FROM [Owner]
                                                   INNER JOIN Neighborhood ON Neighborhood.Id = NeighborhoodId";
        public OwnerRepository(IConfiguration config) : base(config) { }

        public List<Owner> GetAllOwners()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;
                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<Owner>();

                        while (reader.Read())
                        {
                            var owner = LoadFromData(reader);
                            results.Add(owner);
                        }
                        return results;
                    }
                }
            }
        }

        public Owner? GetOwnerById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE [Owner].Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Owner? result = null;

                        if (reader.Read())
                        {
                            result = LoadFromData(reader);
                        }

                        return result;
                    }
                }
            }
        }

        private Owner LoadFromData(SqlDataReader reader)
        {
            return new Owner
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Address = reader.GetString(reader.GetOrdinal("Address")),
                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                Neighborhood = new Neighborhood { Name = reader.GetString(reader.GetOrdinal("NeighborhoodName")) },
                Phone = reader.GetString(reader.GetOrdinal("Phone")),
            };
        }
    }
}
