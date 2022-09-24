using System.ComponentModel.DataAnnotations;

namespace DogGo.Models;

public class Walk
{
    public int Id { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int Duration { get; set; }
    [Required]
    public int WalkerId { get; set; }
    public Walker Walker { get; set; } = new Walker();
    public int DogId { get; set; }
    public Dog Dog { get; set; } = new Dog();
}
