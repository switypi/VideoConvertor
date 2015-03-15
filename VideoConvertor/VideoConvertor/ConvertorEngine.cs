using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoConvertor
{
    /// <summary>
    /// Convertor engine.
    /// </summary>
    internal class ConvertorEngine
    {
        NReco.VideoConverter.FFMpegConverter ffMpeg = null;
        

        /// <summary>
        /// Constructor
        /// </summary>
        internal ConvertorEngine()
        {
            ffMpeg = new NReco.VideoConverter.FFMpegConverter();
        }

        /// <summary>
        /// Converts video file to specified format
        /// </summary>
        internal void Convert()
        {
            try
            {
                if (string.IsNullOrEmpty(FilePath) || string.IsNullOrEmpty(OutPutFilePath) || string.IsNullOrEmpty(FileFormat))
                    throw new Exception("Input path,Output path and file format are maindatory.");

                switch (FileFormat)
                {
                    case Format.webm:
                        ffMpeg.ConvertMedia(FilePath, OutPutFilePath, Format.webm);
                        break;
                    case Format.mp4:
                        ffMpeg.ConvertMedia(FilePath, OutPutFilePath, Format.webm);
                        break;
                    case Format.mov:
                        ffMpeg.ConvertMedia(FilePath, OutPutFilePath, Format.mp4);
                        break;
                }

            }
            catch (FFMpegException exp)
            {
                throw exp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Release the ffmpeg process.
        /// </summary>
        internal void ReleaseData()
        {
            ffMpeg.Abort();
            ffMpeg = null;
        }

        #region Properties
        /// <summary>
        /// Input file path
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// File format 
        /// </summary>
        public string FileFormat { get; set; }
        /// <summary>
        /// Output file
        /// </summary>
        public string OutPutFilePath { get; set; }

        public FFMpegConverter FFmpegProcess
        {
            get
            {
                return ffMpeg;
            }
        }
        #endregion

    }
}
