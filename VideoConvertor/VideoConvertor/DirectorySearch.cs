using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoConvertor
{
    public class DirectorySearch
    {
        ConvertorEngine oEngine;

        public DirectorySearch(string initialDirectory)
        {
            InitialDirectory = initialDirectory;
        }

        public void ProcessDirectories(string initialDirectory)
        {
            try
            {
                System.IO.DirectoryInfo dr = new System.IO.DirectoryInfo(initialDirectory);
                oEngine = new ConvertorEngine();
                if (dr.Exists)
                {
                    DirectoryInfo[] directories = dr.GetDirectories();
                    foreach (var item in directories)
                    {
                        FileInfo[] files = item.GetFiles();
                        if (files.Count() > 0)
                        {

                            ConvertVideo(item);
                        }
                    }
                    oEngine.ReleaseData();
                    oEngine = null;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConvertVideo(System.IO.DirectoryInfo dr)
        {
            foreach (var file in dr.GetFiles())
            {
                string fileName = string.Empty;
                switch (file.Extension)
                {
                    case Format.mov:
                        oEngine.FileFormat = Format.mp4;
                        oEngine.FilePath = file.FullName;

                        fileName = Path.GetFileNameWithoutExtension(file.FullName);
                        oEngine.OutPutFilePath = file.Directory.FullName + "\\sub.webm";
                        oEngine.Convert();
                        break;
                    case Format.mp4:
                        oEngine.FileFormat = Format.webm;
                        oEngine.FilePath = file.FullName;
                        fileName = Path.GetFileNameWithoutExtension(file.FullName);
                        oEngine.OutPutFilePath = Path.Combine(file.Directory.FullName, fileName) + "." + Format.webm;
                        oEngine.Convert();
                        break;
                }
            }

        }

        public string InitialDirectory { get; set; }
    }
}
