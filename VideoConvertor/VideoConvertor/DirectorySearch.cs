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
        System.IO.DirectoryInfo oDr;

        /// <summary>
        /// Process each directory and files inside the directory.
        /// </summary>
        public void ProcessDirectories()
        {

            try
            {
                if (string.IsNullOrEmpty(InitialDirectory))
                    throw new Exception("Initial directory field is not assigned.");

                oDr = new System.IO.DirectoryInfo(InitialDirectory);
                oEngine = new ConvertorEngine();
                if (oDr.Exists)
                {
                    DirectoryInfo[] directories = oDr.GetDirectories();
                    foreach (var item in directories)
                    {
                        FileInfo[] files = item.GetFiles();
                        if (files.Count() > 0)
                            ConvertVideo(item);

                    }

                    FileInfo[] filesInsideDirectory = oDr.GetFiles();
                    if (filesInsideDirectory.Count() > 0)
                    {
                        ConvertVideo(oDr);
                    }

                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally
            {
                if (oEngine != null)
                    oEngine.ReleaseData();
                oEngine = null;
                oDr = null;
            }
        }

        /// <summary>
        /// Convert the file to specified format.
        /// </summary>
        /// <param name="dr"></param>
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
                        oEngine.OutPutFilePath = Path.Combine(file.Directory.FullName, fileName) + "." + Format.mp4;
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

        /// <summary>
        /// Set the Initial directory.
        /// </summary>
        public string InitialDirectory { get; set; }
    }
}
