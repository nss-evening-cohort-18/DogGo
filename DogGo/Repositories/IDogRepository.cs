using DogGo.Models;
using DogGo.Models.Filters;

namespace DogGo.Repositories;

public interface IDogRepository
{
    List<Dog> GetDogs(DogFilter? filter = null);
    //List<Dog> GetAllDogs();
    //List<Dog> GetDogsByOwnerId(int ownerId);
    //Dog? GetDogById(int id);
    void AddDog(Dog dog);
    void UpdateDog(Dog dog);
    void DeleteDog(int id);
}