﻿using DogGo.Models;

namespace DogGo.Repositories;

public interface IWalkerRepository
{
    List<Walker> GetAllWalkers();
    Walker? GetWalkerById(int id);
    List<Walker> GetWalkersByNeighborhood(int neighborhoodId);
}