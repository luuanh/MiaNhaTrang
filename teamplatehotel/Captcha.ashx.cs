using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace TeamplateHotel
{
    /// <summary>
    /// Summary description for Captcha
    /// </summary>
    public class Captcha : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            int iHeight = 40;
            int iWidth = 100;
            Random oRandom = new Random();

            int[] aBackgroundNoiseColor = new int[] {150, 150, 150};
            int[] aTextColor = new int[] {0, 0, 0};
            int[] aFontEmSizes = new int[] {15, 15, 15, 15, 15};

            string[] aFontNames = new string[]
                                      {
                                          "Comic Sans MS",
                                          "Arial",
                                          "Times New Roman",
                                          "Georgia",
                                          "Verdana",
                                          "Geneva"
                                      };
            FontStyle[] aFontStyles = new FontStyle[]
                                          {
                                              FontStyle.Bold,
                                              FontStyle.Italic,
                                              FontStyle.Regular,
                                              FontStyle.Strikeout,
                                              FontStyle.Underline
                                          };
            HatchStyle[] aHatchStyles = new HatchStyle[]
                                            {
                                                HatchStyle.ForwardDiagonal, HatchStyle.LightHorizontal,
        
                                            };

            //Get Captcha in Session
            string sCaptchaText = context.Session["Captcha"].ToString();

            //Creates an output Bitmap
            Bitmap oOutputBitmap = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
            Graphics oGraphics = Graphics.FromImage(oOutputBitmap);
            oGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            //Create a Drawing area
            RectangleF oRectangleF = new RectangleF(0, 0, iWidth, iHeight);
            Brush oBrush = default(Brush);

            //Draw background (Lighter colors RGB 100 to 255)
            oBrush = new HatchBrush(aHatchStyles[oRandom.Next
                                                     (aHatchStyles.Length - 1)],
                                    Color.FromArgb((oRandom.Next(100, 255)),
                                                   (oRandom.Next(100, 255)), (oRandom.Next(100, 255))), Color.White);
            oGraphics.FillRectangle(oBrush, oRectangleF);

            System.Drawing.Drawing2D.Matrix oMatrix = new System.Drawing.Drawing2D.Matrix();
            int i = 0;
            for (i = 0; i <= sCaptchaText.Length - 1; i++)
            {
                oMatrix.Reset();
                int iChars = sCaptchaText.Length;
                int x = iWidth/(iChars + 1)*i;
                int y = -10;

                //Rotate text Random
                oMatrix.RotateAt(oRandom.Next(5, 010), new PointF(x, y));
                oGraphics.Transform = oMatrix;

                //Draw the letters with Random Font Type, Size and Color
                oGraphics.DrawString
                    (
                        //Text
                    sCaptchaText.Substring(i, 1),
                    //Random Font Name and Style
                    new Font(aFontNames[oRandom.Next(aFontNames.Length - 1)],
                             aFontEmSizes[oRandom.Next(aFontEmSizes.Length - 1)],
                             aFontStyles[oRandom.Next(aFontStyles.Length - 1)]),
                    //Random Color (Darker colors RGB 0 to 100)
                    new SolidBrush(Color.FromArgb(oRandom.Next(0, 100),
                                                  oRandom.Next(0, 100), oRandom.Next(0, 100))),
                    x,
                    oRandom.Next(5, 10)
                    );
                oGraphics.ResetTransform();
            }

            MemoryStream oMemoryStream = new MemoryStream();
            oOutputBitmap.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] oBytes = oMemoryStream.GetBuffer();

            oOutputBitmap.Dispose();
            oMemoryStream.Close();

            context.Response.BinaryWrite(oBytes);
            context.Response.End();
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}