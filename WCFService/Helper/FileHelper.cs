using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace WCFService
{
    public class FileHelper
    {
        public static byte[] GetBytesByFile(string path)
        {
            if (!File.Exists(path))
                return null;
            byte[] data = null;
            using (FileStream fs_read = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                data = new byte[fs_read.Length];
                fs_read.Read(data, 0, data.Length);
            }
            return data;

        }
        public static void SaveFileFromBytes(byte[] data, string path)
        {
            if (data == null || data.Length == 0)
                return;
            using (FileStream fs_write = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                fs_write.Write(data, 0, data.Length);
            }
        }
    }
}
