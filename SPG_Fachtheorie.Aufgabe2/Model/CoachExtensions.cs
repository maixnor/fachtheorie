namespace SPG_Fachtheorie.Aufgabe2.Model;

public static class CoachExtensions
{
    public static bool IsCoach(this Student student)
    {
        return student is Coach;
    }
}