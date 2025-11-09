

namespace AsyncExample
{
    internal class Program
    {
        static string[] allTxtFilePaths;
        static List<string> allTexts;

        static void Main(string[] args)
        {
            Dictionary<string, string> pathTextDict = new Dictionary<string, string>();
            Dictionary<string, int> pathSpacesDict = new Dictionary<string, int>();
            string path = "";

            Console.Write("Enter files directory: ");
            path = Console.ReadLine();


            while (!Directory.Exists(path))
            {
                Console.Clear();
                Console.WriteLine($"Path \"{path}\" doesn't exist.");
                Console.Write("Enter correct files directory: ");
                path = Console.ReadLine();
            }
            Console.Clear();

            allTxtFilePaths = Directory.GetFiles(path, "*.txt");
            if (allTxtFilePaths.Length == 0)
            {
                Console.WriteLine($"\t0 .txt files found in {path}");
                return;
            }

            try
            {
                pathTextDict = GetPathTextDictAsync(path).GetAwaiter().GetResult();

                foreach (var key in pathTextDict.Keys)
                {
                    pathSpacesDict[key] = pathTextDict[key].Count(c => c == ' ');
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nException: {ex.ToString()}");
            }

            foreach (var key in pathSpacesDict.Keys)
            {
                Console.WriteLine($"File: \"{key}\"; spaces: {pathSpacesDict[key]}");
            }

        }


        static async Task<Dictionary<string, string>> GetPathTextDictAsync(string path)
        {
            Dictionary<string, string> _pathTextDict = new();

            foreach (string filePath in allTxtFilePaths)
            {
                string text = await File.ReadAllTextAsync(filePath);
                _pathTextDict[filePath] = text;
            }

            return _pathTextDict;
        }



    }
}
