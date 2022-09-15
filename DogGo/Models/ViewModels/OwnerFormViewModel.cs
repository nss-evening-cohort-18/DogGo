﻿namespace DogGo.Models.ViewModels;

public class OwnerFormViewModel
{
    public Owner Owner { get; set; }
    public List<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();
}
