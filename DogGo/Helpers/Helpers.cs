using System.Text;

namespace DogGo.Helpers;

public class Helpers
{
    public static string DurationToFullTime(int duration)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(duration);

        StringBuilder results = new();

        results.Append(timeSpan.Hours > 0 ? $"{timeSpan.Hours} hr " : "");
        results.Append(timeSpan.Minutes > 0 ? $"{timeSpan.Minutes} min " : "");
        results.Append(timeSpan.Seconds > 0 ? $"{timeSpan.Seconds} sec " : "");

        return results.ToString();
    }

    public static int DurationFromMinutesToSeconds(int durationInMinutes)
    {
        return durationInMinutes * 60;
    }
}
