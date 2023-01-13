﻿using Microsoft.AspNetCore.Http;

namespace SupermarketApp.Data.Mapper
{
    public static class ImageConvertor
    {
        public const string NOIMAGE = "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";
        public static string ImageToString(IFormFile imageFile)
        {
            using var ms = new MemoryStream();
            imageFile.CopyTo(ms);

            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
