using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoConvertor
{
    public class ConvertorEngine
    {
        NReco.VideoConverter.FFMpegConverter ffMpeg = null;
        public ConvertorEngine(string sFilePath, string sOutPutPath, string sfileFormat)
        {
            FilePath = sFilePath;
            FileFormat = sfileFormat;
            OutPutFilePath = sOutPutPath;

        }
        public ConvertorEngine()
        {
            ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            ffMpeg.ConvertProgress += ffMpeg_ConvertProgress;
        }

        void ffMpeg_ConvertProgress(object sender, ConvertProgressEventArgs e)
        {
            
        }

        public string Convert()
        {

            string smessage = string.Empty;
           

            if (string.IsNullOrEmpty(FilePath) || string.IsNullOrEmpty(OutPutFilePath) || string.IsNullOrEmpty(FileFormat))
            {
                smessage = "Input path,Output path and file format are maindatory.";
            }
            else
            {
                try
                {

                 
                    switch (FileFormat)
                    {
                        case Format.webm:
                            //ffMpeg.ConvertMedia(FilePath, OutPutFilePath, Format.webm);
                            break;
                        case Format.mp4:
                            ffMpeg.ConvertMedia(FilePath, OutPutFilePath, Format.webm);
                            break;
                        case Format.mov:
                            ffMpeg.ConvertMedia(FilePath, OutPutFilePath, Format.mp4);
                            break;
                    }

                }
                catch (FFMpegException e)
                {
                    ffMpeg.Abort();
                }
                catch (Exception ex)
                {
                    ffMpeg.Abort();
                    throw new Exception("Unable to convert. " + ex.Message);
                }

            }
            return smessage;
        }

        public void ReleaseData()
        {
            ffMpeg.Abort();
        }

        #region Properties
        public string FilePath { get; set; }

        public string FileFormat { get; set; }

        public string OutPutFilePath { get; set; }


        #endregion

    }
}
