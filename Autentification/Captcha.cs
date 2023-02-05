using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autentification
{
    class Captcha
    {
        private const string letters = "QWERTYUIOPASDFGHJKLZXCVBNM";
        private const string numbers = "123456789";
        
        public static string generateCaptcha()
        {
            Random rnd = new Random();
            int maxRand = letters.Length - 1;

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < 7; i++)
            {
                int index = rnd.Next(maxRand);
                stringBuilder.Append(letters[index]);
            }
            maxRand = numbers.Length - 1;
            for (int i = 0; i < 3; i++)
            {
                int index = rnd.Next(maxRand);
                stringBuilder.Append(numbers[index]);
            }

            Global.captchaText = stringBuilder.ToString();
            return stringBuilder.ToString();

        }

        public static CaptchaResult GetCaptchaImage(int width, int height, string capthcaCode)
        {
            Random rnd = new Random();
            using (Bitmap baseMap = new Bitmap(width, height))
            using (Graphics graphics = Graphics.FromImage(baseMap))
            {
                graphics.Clear(GetRandomLightColor());

                drawCaptchaCode();
                drawDisorderLine();
                AdjustRippleEffect();

                MemoryStream memoryStream = new MemoryStream();
                baseMap.Save(memoryStream, ImageFormat.Png);

                return new CaptchaResult
                {
                    captchaCode = capthcaCode,
                    captchaByteCode = memoryStream.ToArray(),
                    Timestamp = DateTime.Now
                };

          
                Color GetRandomDeepColor()
                {
                    int redlow = 160, greenlow = 100, bluelow = 160;
                    return Color.FromArgb(rnd.Next(redlow), rnd.Next(greenlow), rnd.Next(bluelow));
                }
                Color GetRandomLightColor()
                {
                    int low = 180, high = 255;

                    int nRed = rnd.Next(high) % (high - low) + low;
                    int nGreen = rnd.Next(high) % (high - low) + low;
                    int nBlue = rnd.Next(high) % (high - low) + low;

                    return Color.FromArgb(nRed, nGreen, nBlue);
                }

                int GetFontSize(int imageWidth, int captchaCodeCount)
                {
                    var averageSize = imageWidth / captchaCodeCount;
                    return Convert.ToInt32(averageSize);
                }

                void drawCaptchaCode()
                {

                    SolidBrush fontBrush = new SolidBrush(Color.Black);
                    int fontSize = GetFontSize(100, 5);
                    Font fontBold = new Font(FontFamily.GenericSansSerif, fontSize,
                    FontStyle.Bold, GraphicsUnit.Pixel);

                    Font font = new Font(FontFamily.GenericSansSerif, fontSize,
                    FontStyle.Regular, GraphicsUnit.Pixel);

                    
                    for (int i = 0; i < 10; i++)
                    {
                        fontBrush.Color = GetRandomDeepColor();
                        int shiftPx = fontSize / 6;

                        float x = i * fontSize + rnd.Next(-shiftPx, shiftPx) + rnd.Next(-shiftPx, shiftPx);
                        int maxY = height - fontSize;
                        if (maxY < 0) maxY = 0;
                        float y = rnd.Next(0, maxY);

                       // Graphics graphics = new Graphics();

                        if(rnd.Next(3) == 1)
                             graphics.DrawString(capthcaCode[i].ToString(), font, fontBrush, x, y);
                        else
                            graphics.DrawString(capthcaCode[i].ToString(), fontBold, fontBrush, x, y);
                    }
                }

                void drawDisorderLine()
                {
                    Pen linePen = new Pen(new SolidBrush(System.Drawing.Color.Black), 3);
                    for (int i = 0; i < 3; i++)
                    {
                        linePen.Color = GetRandomDeepColor();

                        Point startPoint = new Point(rnd.Next(0, width), rnd.Next(0, height));
                        Point endPoint = new Point(rnd.Next(0, width), rnd.Next(0, height));
                        graphics.DrawLine(linePen, startPoint, endPoint);

                    }
                }

                void AdjustRippleEffect()
                {
                    short nWave = 6;
                    int nWidht = baseMap.Width;
                    int nHeight = baseMap.Height;

                    Point[,] pt = new Point[nWidht, nHeight];

                    for (int x = 0; x < nWidht; x++)
                    {
                        for (int y = 0; y < nHeight; y++)
                        {
                            var xo = nWave * Math.Sin(2.0 * 3.1415 * y / 128.0);
                            var yo = nWave * Math.Cos(2.0 * 3.1415 * y / 128.0);

                            var newX = x + xo;
                            var newY = y + yo;

                            if (newX > 0 && newX < nWidht)
                                pt[x, y].X = (int)newX;
                            else
                                pt[x, y].X = 0;

                            if (newY > 0 && newY < nHeight)
                                pt[x, y].Y = (int)newY;
                            else
                                pt[x, y].Y = 0;
                        }
                    }
                }
            }
        }

    }

}
