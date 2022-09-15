namespace DogGo.Models;

public class Walk
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public int WalkerId { get; set; }
    public Walker Walker { get; set; } = new Walker();
    public int DogId { get; set; }
    public Dog Dog { get; set; } = new Dog();
}
