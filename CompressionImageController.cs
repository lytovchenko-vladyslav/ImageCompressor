using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System.Diagnostics;

public class CompressionImageController
{
    private const string PngQuantExecutablePath = "F:\\Programming\\DotNet\\NZWalks\\Test\\Tools\\pngquant.exe";

    public static void Main(string[] args)
    {
        CompressionImageController tester = new CompressionImageController();
        tester.TryTest();
    }

    Image LoadImage(string path) => Image.Load(path);

    public void TryTest()
    {
        string sourcePngPath = "C:/Users/go040/OneDrive/Desktop/Test_Images/test.png";
        string sourceJpegPath = "C:/Users/go040/OneDrive/Desktop/Test_Images/test2.jpg";

        //CompressPng(sourcePngPath, "C:/Users/go040/OneDrive/Desktop/Test_Images/PNG/test_L1.png", PngCompressionLevel.Level1);
        CompressPngWithQuant(sourcePngPath, "C:/Users/go040/OneDrive/Desktop/Test_Images/PNG/test_Quant.png");

        //CompressJpeg(sourceJpegPath, "C:/Users/go040/OneDrive/Desktop/Test_Images/JPEG/test_jpeg_q90.jpg", 90);
        //CompressJpeg(sourceJpegPath, "C:/Users/go040/OneDrive/Desktop/Test_Images/JPEG/test_jpeg_q50.jpg", 50);
        //CompressJpeg(sourceJpegPath, "C:/Users/go040/OneDrive/Desktop/Test_Images/JPEG/test_jpeg_q10.jpg", 10);
    }

    private void SaveJpeg(Image img, string dst, int qualityPercent) // quality = from 0% to 100%
    {
        JpegEncoder encoder = new JpegEncoder
        {
            Quality = qualityPercent,
        };
        img.Save(dst, encoder);
    }

    private void CompressJpeg(string source, string destination, int qualityPercent)
    {
        using (Image img = LoadImage(source))
        {
            SaveJpeg(img, destination, qualityPercent);
        }
    }

    private void CompressPng(string source, string destination, PngCompressionLevel level)
    {
        using (Image img = LoadImage(source))
        {
            SavePng(img, destination, level);
        }
    }

    private void SavePng(Image img, string dst, PngCompressionLevel level = PngCompressionLevel.Level9) //from 0 to 9 lvl
    {
        PngEncoder encoder = new PngEncoder
        {
            CompressionLevel = level,
            FilterMethod = PngFilterMethod.Adaptive
        };
        img.Save(dst, encoder);
    }

    private void CompressPngWithQuant(string source, string destination, string  qualityRange = "65-80")
    {
        string newFileName = $"{Guid.NewGuid()}.png";
        string tempPngPath = Path.Combine(Path.GetTempPath(), newFileName);
        string argumentPath = $"\"{tempPngPath}\" --quality {qualityRange} --output \"{destination}\" --force";

        try
        {
            using (Image img = LoadImage(source))
            {
                SavePng(img, tempPngPath, PngCompressionLevel.Level1);
            }

            ProcessStartInfo statInfo = new ProcessStartInfo
            {
                FileName = PngQuantExecutablePath,
                Arguments = argumentPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(statInfo))
            {
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    string stderr = process.StandardError.ReadToEnd();
                    Console.WriteLine($"PngQuant failed: {stderr}");
                }
                else
                {
                    Console.WriteLine($"PngQuant successfully compressed: {destination}");
                }
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error during PngQuant compression: {ex.Message}");
        }
        finally
        {
            if (File.Exists(tempPngPath))
            {
                File.Delete(tempPngPath );
            }
        }
    }
}