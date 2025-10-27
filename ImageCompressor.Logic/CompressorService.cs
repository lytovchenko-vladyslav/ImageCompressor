using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System.Diagnostics;

namespace ImageCompressor.Logic
{
    public class CompressorService
    {
        private readonly string _pngQuantPath;
        public CompressorService(string pngQuantExecutablePath)
        {
            if (string.IsNullOrEmpty(pngQuantExecutablePath) || !File.Exists(pngQuantExecutablePath))
            {
                throw new FileNotFoundException(
                    "pngquant.exe not found at the specified path.",
                    pngQuantExecutablePath);
            }
            _pngQuantPath = pngQuantExecutablePath;
        }

        public void CompressJpeg(string source, string destination, int qualityPercent)
        {
            using (Image img = LoadImage(source))
            {
                SaveJpeg(img, destination, qualityPercent);
            }
        }

        public void CompressPngWithQuant(string source, string destination, string qualityRange = "65-80")
        {
            string tempPngPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            try
            {
                using (Image img = LoadImage(source))
                {
                    SavePng(img, tempPngPath, PngCompressionLevel.Level1);
                }

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = _pngQuantPath,
                    Arguments = $"\"{tempPngPath}\" --quality {qualityRange} --output \"{destination}\" --force",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        string stderr = process.StandardError.ReadToEnd();
                        throw new InvalidOperationException($"PngQuant failed: {stderr}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"PngQuant compression failed: {ex.Message}", ex);
            }
            finally
            {
                if (File.Exists(tempPngPath))
                {
                    File.Delete(tempPngPath);
                }
            }
        }

        private Image LoadImage(string path) => Image.Load(path);

        private void SaveJpeg(Image img, string dst, int qualityPercent)
        {
            JpegEncoder encoder = new JpegEncoder
            {
                Quality = qualityPercent,
            };
            img.Save(dst, encoder);
        }

        private void SavePng(Image img, string dst, PngCompressionLevel level)
        {
            PngEncoder encoder = new PngEncoder
            {
                CompressionLevel = level,
                FilterMethod = PngFilterMethod.Adaptive
            };
            img.Save(dst, encoder);
        }
    }
}
