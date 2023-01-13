using AutoMapper;

namespace SupermarketApp.Data.Mapper
{
    public static class ImageConvertor
    {
        public static string ImageToString(IFormFile imageFile)
        {
            using var ms = new MemoryStream();
            imageFile.CopyTo(ms);

            return Convert.ToBase64String(ms.ToArray());
        }

        public static IFormFile Base64ToImage(string imageBase64, string name)
        {
            byte[] bytes = Convert.FromBase64String(imageBase64);
            using var stream = new MemoryStream(bytes);

            IFormFile file = new FormFile(stream, 0, bytes.Length, name, name);

            return file;
        }
    }
}
