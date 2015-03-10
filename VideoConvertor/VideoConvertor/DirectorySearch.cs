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
                            oEngine.FilePath = files.First().FullName;
                            oEngine.OutPutFilePath = files.First().FullName;
                            ConvertVideo(item);
                        }
                    }

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
                switch (file.Extension)
                {
                    case Format.mov:
                        oEngine.FileFormat = Format.mp4;
                        oEngine.Convert();
                        break;
                    case Format.mp4:
                         oEngine.FileFormat = Format.webm;
                        oEngine.Convert();
                        break;
                }
            }

        }

        public string InitialDirectory { get; set; }
    }
}
