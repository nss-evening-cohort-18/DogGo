using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories;

public class WalkerRepository : BaseRepository, IWalkerRepository
{
    // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
    public WalkerRepository(IConfiguration config) : base(config) { }

    private readonly string _baseSqlSelect = @"SELECT Id,
                                                      [Name], 
                                                      ImageUrl, 
                                                      NeighborhoodId
                                               FROM Walker ";

    public List<Walker> GetAllWalkers()
    {
        using (SqlConnection conn = Connection)
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = _baseSqlSelect;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<Walker> walkers = new List<Walker>();
                    while (reader.Read())
                    {
                        Walker walker = LoadFromData(reader);
                        walkers.Add(walker);
                    }

                    return walkers;
                }
            }
        }
    }

    public Walker? GetWalkerById(int id)
    {
        using (SqlConnection conn = Connection)
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"{_baseSqlSelect} WHERE Id = @id";

                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Walker? result = null;
                    if (reader.Read())
                    {
                        return LoadFromData(reader);
                    }

                    return result;
                }
            }
        }
    }

    private Walker LoadFromData(SqlDataReader reader)
    {
        return new Walker
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
        };
    }
}
