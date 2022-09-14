using DogGo.Models;

namespace DogGo.Repositories;

public interface IDogRepository
{
    List<Dog> GetDogsByOwnerId(int ownerId);
}