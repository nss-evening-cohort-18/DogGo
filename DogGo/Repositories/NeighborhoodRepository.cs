using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories;

public class NeighborhoodRepository : BaseRepository, INeighborhoodRepository
{
    public NeighborhoodRepository(IConfiguration config) : base(config) { }

    public List<Neighborhood> GetAllNeighborhoods()
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id, Name FROM Neighborhood";
                
                using (var reader = cmd.ExecuteReader())
                {
                    var results = new List<Neighborhood>();

                    while (reader.Read())
                    {
                        results.Add(LoadFromData(reader));
                    }

                    return results;
                }
            }
        }
    }

    private Neighborhood LoadFromData(SqlDataReader reader)
    {
        return new Neighborhood
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
        };
    }
}
