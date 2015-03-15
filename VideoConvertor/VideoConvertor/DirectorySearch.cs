using NReco.VideoConverter;
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
        public  event EventHandler<ProcessResultArgs> ConvertorProgressEvent;

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
                oEngine.FFmpegProcess.ConvertProgress += FFmpegProcess_ConvertProgress;

                if (oDr.Exists)
                {
                    ProcessSubDirectory(oDr);

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

        private void FFmpegProcess_ConvertProgress(object sender, ConvertProgressEventArgs e)
        {
            if (e.Processed == new TimeSpan(0, 0, 0, 0, 0))
            {
                if (ConvertorProgressEvent != null)
                    ConvertorProgressEvent(this, new ProcessResultArgs() { MediaState = ProcessResultState.Initialized, MediaName = FileName, TotalDuration = e.TotalDuration });
            }


            if (e.Processed.Equals(e.TotalDuration))
            {
                if (ConvertorProgressEvent != null)
                    ConvertorProgressEvent(this,new ProcessResultArgs() { MediaState = ProcessResultState.Finished, MediaName = FileName, TotalDuration = e.TotalDuration });
            }
        }

        private void ProcessSubDirectory(DirectoryInfo odr)
        {
            DirectoryInfo[] directories = odr.GetDirectories();
            foreach (var item in directories)
            {
                StartConversion(item);
                if (item.GetDirectories().Count() > 0)
                    ProcessSubDirectory(item);
            }
            FileInfo[] filesInsideDirectory = odr.GetFiles();
            if (filesInsideDirectory.Count() > 0)
            {
                ConvertVideo(odr);
            }

        }

        private void StartConversion(DirectoryInfo item)
        {
            FileInfo[] files = item.GetFiles();
            if (files.Count() > 0)
                ConvertVideo(item);
        }

        /// <summary>
        /// Convert the file to specified format.
        /// </summary>
        /// <param name="dr"></param>
        private void ConvertVideo(System.IO.DirectoryInfo dr)
        {
            foreach (var file in dr.GetFiles())
            {

                switch (file.Extension)
                {
                    case Format.mov:
                        oEngine.FileFormat = Format.mp4;
                        oEngine.FilePath = file.FullName;
                        FileName = Path.GetFileNameWithoutExtension(file.FullName);
                        oEngine.OutPutFilePath = Path.Combine(file.Directory.FullName, FileName) + "." + Format.mp4;
                        oEngine.Convert();
                        break;
                    case Format.mp4:
                        oEngine.FileFormat = Format.webm;
                        oEngine.FilePath = file.FullName;
                        FileName = Path.GetFileNameWithoutExtension(file.FullName);
                        oEngine.OutPutFilePath = Path.Combine(file.Directory.FullName, FileName) + "." + Format.webm;
                        oEngine.Convert();
                        break;
                }
            }
        }

        /// <summary>
        /// Set the Initial directory.
        /// </summary>
        public string InitialDirectory { get; set; }

        public string FileName { get; set; }


    }
}
