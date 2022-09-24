namespace DogGo.Models.ViewModels;

public class WalkCreateViewModel
{
    public Walk Walk { get; set; }
    public List<Walker> WalkerOptions { get; set; }
    public List<Dog> DogOptions { get; set; }
    public List<int> SelectedDogIds { get; set; } = new List<int>();
}
