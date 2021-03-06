using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

/// <summary>
/// class that is responsible for creating a captcha pattern
/// </summary>
public class Captcha
{
    /// <summary>
    /// gets a string and creates a Bitmap that represents a captcha
    /// </summary>
    /// <param name="context">string to insert to the captch</param>
    /// <returns>bitmap that contains represents</returns>
    public Bitmap ProcessRequest(string context)
    {
        int iHeight = 80;
        int iWidth = 180;
        Random oRandom = new Random();

        int[] aBackgroundNoiseColor = new int[] { 150, 150, 150 };
        int[] aTextColor = new int[] { 0, 0, 0 };
        int[] aFontEmSizes = new int[] { 15, 20, 25, 30, 35 };

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
         HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
         HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical, HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
         HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
         HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
         HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal, HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
         HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal, HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
         HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard, HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
         HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis, HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
         HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
        };

        //Get Captcha in Session
        string sCaptchaText = context;

        //Creates an output Bitmap
        Bitmap oOutputBitmap = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
        Graphics oGraphics = Graphics.FromImage(oOutputBitmap);
        oGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

        //Create a Drawing area
        RectangleF oRectangleF = new RectangleF(0, 0, iWidth, iHeight);
        Brush oBrush = default(Brush);

        //Draw background (Lighter colors RGB 100 to 255)
        oBrush = new HatchBrush(aHatchStyles[oRandom.Next(aHatchStyles.Length - 1)], Color.FromArgb((oRandom.Next(100, 255)), (oRandom.Next(100, 255)), (oRandom.Next(100, 255))), Color.White);
        oGraphics.FillRectangle(oBrush, oRectangleF);

        Matrix oMatrix = new Matrix();
        for (int i = 0; i <= sCaptchaText.Length - 1; i++)
        {
            oMatrix.Reset();
            int iChars = sCaptchaText.Length;
            int x = 10 + iWidth / (iChars + 1) * i;
            int y = 5 + iHeight / 2;

            //Rotate text Random
            oMatrix.RotateAt(oRandom.Next(-40, 40), new PointF(x, y));
            oGraphics.Transform = oMatrix;

            //Draw the letters with Randon Font Type, Size and Color
            oGraphics.DrawString
            (
            //Text
            sCaptchaText.Substring(i, 1),
            //Random Font Name and Style
            new Font(aFontNames[oRandom.Next(aFontNames.Length - 1)], aFontEmSizes[oRandom.Next(aFontEmSizes.Length - 1)],
            aFontStyles[oRandom.Next(aFontStyles.Length - 1)]),
            //Random Color (Darker colors RGB 0 to 100)
            new SolidBrush(Color.FromArgb(oRandom.Next(0, 100), oRandom.Next(0, 100), oRandom.Next(0, 100))),
            x,
            oRandom.Next(10, 40)
            );
            oGraphics.ResetTransform();
        }
        return oOutputBitmap;
    }

}