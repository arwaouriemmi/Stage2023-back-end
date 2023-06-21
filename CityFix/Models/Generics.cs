namespace CityFix.Models
{
    public class Generics
    {
        public static string GenerateUniqueFileName(string originalFileName)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string randomString = Path.GetRandomFileName().Replace(".", "");

          
            string uniqueFileName = $"{timestamp}_{randomString}{Path.GetExtension(originalFileName)}";

            return uniqueFileName;
        }
    }
}
