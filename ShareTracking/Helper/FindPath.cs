namespace ShareTracking.Helper;

public class FindPath
{
    public string GetFindPath()
    {
        PlatformID platform = Environment.OSVersion.Platform;

        if (!Directory.Exists("Hisse"))
        {
            Directory.CreateDirectory("Hisse");
        }

        return $"Hisse/{DateTime.Now.ToLocalTime()}.json";
    }
}