
using System.IO;

namespace RoutesFinder.helper
{

    public class RouteFile : IRouteFile
    {
        public string content { get; set; }
        private string _filePath { get; set; }


        public RouteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                this._filePath = filePath;
                readFile(filePath);
            }
            else
                throw new FileNotFoundException();
        }

        public void readFile(string filePath)
        {
            this.content = File.ReadAllText(filePath);
        }

        public void saveFile(string[] lines)
        {
            File.WriteAllLines(this._filePath, lines);
        }
    }
}
