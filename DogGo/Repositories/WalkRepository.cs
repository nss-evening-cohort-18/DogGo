using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories;

public class WalkRepository : BaseRepository, IWalkRepository
{
    public WalkRepository(IConfiguration config) : base(config) { }

    public List<Walk> GetWalksByWalker(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Walks.Id,
                                    	   [Date],
                                    	   Duration,
                                    	   WalkerId,
                                    	   DogId,
                                    	   [Owner].[Name] AS OwnerName
                                    FROM Walks
                                    INNER JOIN Dog ON Dog.Id = Walks.DogId
                                    INNER JOIN [Owner] ON [Owner].Id = Dog.OwnerId
                                    WHERE Walks.WalkerId = @Id";

                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    List<Walk> results = new List<Walk>();
                    while (reader.Read())
                    {
                        Walk walk = LoadFromData(reader);
                        walk.Dog.Owner.Name = reader.GetString(reader.GetOrdinal("OwnerName"));
                        results.Add(walk);
                    }

                    return results;
                }
            }
        }
    }
    private Walk LoadFromData(SqlDataReader reader)
    {
        return new Walk
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
        };
    }
}
