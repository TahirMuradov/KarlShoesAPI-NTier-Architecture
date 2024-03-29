using Microsoft.AspNetCore.Http;

namespace KarlShoes.Core.Helper
{
    public static class FileHelper
    {
        public static async Task<string> SaveFileAsync(this IFormFile file, string WebRootPath)
        {
            string filePath = Path.Combine(wwwrootGetPath.GetwwwrootPath, "uploads");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var path = "/uploads/" + Guid.NewGuid().ToString() + file.FileName;
            using FileStream fileStream = new(Path.Combine(wwwrootGetPath.GetwwwrootPath + path), FileMode.Create);
            await file.CopyToAsync(fileStream);
            return path;
        }
        public static async Task<List<string>> SaveFileRangeAsync(this List<IFormFile> file, string WebRootPath)
        {
            List<string> pictures = new();
            for (int i = 0; i < file.Count; i++)
            {
                pictures.Add(await file[i].SaveFileAsync(WebRootPath));
            }
            return pictures;
        }
        public static bool RemoveFileRange(this List<string> PhotoPaths)
        {
            foreach (var path in PhotoPaths)
            {
                string filePath = Path.Combine(wwwrootGetPath.GetwwwrootPath + path);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);

                }
                else
                {
                    continue;
                }

            }
            return true;
        }
        public static bool RemoveFile(this string PhotoPaths)
        {
            string filePath = Path.Combine(wwwrootGetPath.GetwwwrootPath + PhotoPaths);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);

            }
            else { return false; }
            return true;
        }
    }
}
