using System.Net.NetworkInformation;

namespace Inventory.Helpers
{
    public class Logger
    {
        public static void LogError(Exception ex)
            {
            Console.WriteLine($"Error: {ex.Message}");
        }
}
}
