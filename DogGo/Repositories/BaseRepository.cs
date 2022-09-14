using Microsoft.Data.SqlClient;

namespace DogGo.Repositories;

public abstract class BaseRepository
{
    private readonly string _connectionString;
    protected BaseRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public SqlConnection Connection => new SqlConnection(_connectionString);
}
