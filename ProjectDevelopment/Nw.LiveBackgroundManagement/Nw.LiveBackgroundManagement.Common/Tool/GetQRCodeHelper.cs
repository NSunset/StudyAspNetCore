using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common.Tool
{
    public class GetQRCodeHelper
    {
        public static Bitmap GetQRCode(string url, int pixel)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData codeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);
            QRCode qrcode = new QRCode(codeData); 
            Bitmap qrImage = qrcode.GetGraphic(pixel, Color.Black, Color.White, true); 
            return qrImage;
        }
    }
}
