using Domain.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System;
using System.IO;
using SkiaSharp;

namespace Application_Layer.Services
{
    public class CaptchaServices : ICaptchaServices
    {
        private const int CaptchaLength = 6;
        private const int ImageWidth = 200;
        private const int ImageHeight = 80;
        public (string captchaText, byte[] captchaImage) GenerateCaptcha()
        {
            // Generate a random alphanumeric CAPTCHA
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] captchaChars = new char[CaptchaLength];

            for (int i = 0; i < CaptchaLength; i++)
            {
                captchaChars[i] = chars[random.Next(chars.Length)];
            }

            string captchaText = new string(captchaChars);

            
            using (var bitmap = new SKBitmap(ImageWidth, ImageHeight))
            using (var canvas = new SKCanvas(bitmap))
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.Color = SKColors.Black;
                paint.TextSize = 36;

                canvas.Clear(SKColors.White); 
                canvas.DrawText(captchaText, 10, 50, paint); 

                // Convert the image to a byte array
                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = new MemoryStream())
                {
                    data.SaveTo(stream);
                    byte[] captchaImageData = stream.ToArray();
                    return (captchaText, captchaImageData);
                }
            }
        }
        public bool ValidateCaptchaSolution(string solution, string captchaText)
        {
            if (string.IsNullOrEmpty(solution) || string.IsNullOrEmpty(captchaText))
            {
                return false;
            }

            
            solution = solution.Trim().ToUpper();
            captchaText = captchaText.Trim().ToUpper();

            // Compare the solution with the original CAPTCHA text
            return solution.Equals(captchaText);
        }

    }

        
}