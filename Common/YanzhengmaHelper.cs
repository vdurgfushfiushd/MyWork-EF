using CaptchaGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CommonHelper
{
    public class YanzhengmaHelper
    {
        public static void Make(string yanzhengma)
        {
            using (MemoryStream ms = ImageFactory.GenerateImage(yanzhengma, 50, 100, 20, 5))
            {
                using (FileStream fs = File.OpenWrite(@"D:\vs project\日志管理\asp.net mvc+Autofacmvc+EF+EmitMapper+Webapi+mysql\NoteManager\WebFront\image\yanzhengma.jpg"))
                {
                    ms.CopyTo(fs);
                }
            }
        }
    }
}
