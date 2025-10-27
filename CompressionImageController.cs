using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System.Diagnostics;

public class CompressionImageController
{
    private const string PngQuantExecutablePath = "F:\\Programming\\DotNet\\CompressionImage\\Tools\\pngquant.exe";

    public static void Main() => new CompressionImageController().RunInteractiveSession();

    Image LoadImage(string path) => Image.Load(path);

    public void RunInteractiveSession()
    {
        Console.WriteLine("--- Image Compression Tool ---");

        // Get Format
        string formatChoice = "";
        while (formatChoice != "1" && formatChoice != "2")
        {
            Console.WriteLine("Choose format:");
            Console.WriteLine("  1: JPEG (for photos, .jpg, .jpeg)");
            Console.WriteLine("  2: PNG (for graphics, .png)");
            Console.Write("Your choice (1 or 2): ");
            formatChoice = Console.ReadLine();
        }

        // --- Get Source Path ---
        string sourcePath = "";
        while (true)
        {
            Console.Write("Enter the full path to your source image: ");
            sourcePath = Console.ReadLine() ?? string.Empty;

            if (File.Exists(sourcePath))
            {
                break;
            }
            else
            {
                Console.WriteLine("Error: File not found. Please check the path and try again.");
            }
        }

        // --- Get Destination Path ---
        string sourceDirectory = Path.GetDirectoryName(sourcePath);
        string sourceFileName = Path.GetFileNameWithoutExtension(sourcePath);
        string sourceExtension = Path.GetExtension(sourcePath);
        string destinationPath = Path.Combine(sourceDirectory, $"{sourceFileName}_compressed{sourceExtension}");

        try
        {
            if (formatChoice == "1") //jpg 
            {
                int quality = 0;
                while (quality < 1 || quality > 100)
                {
                    Console.WriteLine("Enter JPEG quality (1-100, where 100 is best): ");
                    int.TryParse(Console.ReadLine(), out quality);
                }

                Console.WriteLine($"Compressing JPEG to {quality}% quality...");
                CompressJpeg(sourcePath, destinationPath, quality);
            }
            else // PNG 
            {
                int quality = 0;
                while (quality < 10 || quality > 100)
                {
                    Console.Write("Enter PNG quality (10-100, e.g., 80): ");
                    int.TryParse(Console.ReadLine(), out quality);
                }

                int minQuality = Math.Max(10, quality - 20);
                string qualityRange = $"{minQuality}-{quality}";

                Console.WriteLine($"Compressing PNG with PngQuant (quality range {qualityRange})...");
                CompressPngWithQuant(sourcePath, destinationPath, qualityRange);
            }

            Console.WriteLine($"\n Success! Compressed file saved to: {destinationPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n An error occurred during compression: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    private void SaveJpeg(Image img, string dst, int qualityPercent) // quality = from 0% to 100%
    {
        JpegEncoder encoder = new JpegEncoder
        {
            Quality = qualityPercent,
        };
        img.Save(dst, encoder);
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

    private void CompressJpeg(string source, string destination, int qualityPercent)
    {
        using (Image img = LoadImage(source))
        {
            SaveJpeg(img, destination, qualityPercent);
        }
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