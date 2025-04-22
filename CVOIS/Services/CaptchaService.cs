using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CVOIS.Services
{
    public class CaptchaService
    {
        private string GenerateRandomCode()
        {
            Random rand = new Random();
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            return new string(Enumerable.Range(0, 5).Select(_ => chars[rand.Next(chars.Length)]).ToArray());
        }

        public byte[] GenerateCaptcha(out string captchaCode)
        {
            int width = 120, height = 40;
            captchaCode = GenerateRandomCode();

            using (Bitmap bitmap = new Bitmap(width, height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (MemoryStream ms = new MemoryStream())
            {
                graphics.Clear(Color.White);

                // Font settings
                using (Font font = new Font("Arial", 18, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    graphics.DrawString(captchaCode, font, brush, new PointF(10, 5));
                }

                // Add some noise
                Random rand = new Random();
                for (int i = 0; i < 10; i++)
                {
                    int x1 = rand.Next(width);
                    int y1 = rand.Next(height);
                    int x2 = rand.Next(width);
                    int y2 = rand.Next(height);
                    graphics.DrawLine(Pens.Gray, x1, y1, x2, y2);
                }

                // Save image to MemoryStream
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
