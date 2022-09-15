using DogGo.Models;

namespace DogGo.Repositories;

public interface IWalkRepository
{
    List<Walk> GetWalksByWalker(int id);
}