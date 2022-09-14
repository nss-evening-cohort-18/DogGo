using Microsoft.Data.SqlClient;

namespace DogGo.Repositories;

public abstract class BaseRepository
{
    private readonly IConfiguration _config;
    protected BaseRepository(IConfiguration config)
    {
        _config = config;
    }

    public SqlConnection Connection => new SqlConnection(_config.GetConnectionString("DefaultConnection"));
}
