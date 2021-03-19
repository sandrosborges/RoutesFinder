namespace RoutesFinder.helper
{
    public interface IRouteFile
    {
        string content { get; set; }
        void readFile(string filePath);
        void saveFile(string[] lines);
    }
}