using System;
using System.IO;
using ImageCompressor.Logic;
using System.Threading.Tasks;

namespace ImageCompressor.UI
{
    public partial class CompressorUI : Form
    {
        private readonly CompressorService _compressor;
        private string _selectedSourcePath = "";
        private bool _isJpeg = false;

        public CompressorUI()
        {
            InitializeComponent();

            string pngQuantPath = Path.Combine(Application.StartupPath, "F:\\Programming\\DotNet\\CompressionImage\\ImageCompressor.UI\\Tools\\pngquant.exe");

            try
            {
                _compressor = new CompressorService(pngQuantPath);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Critical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                compressButton.Enabled = false;
                selectFileButton.Enabled = false;
            }
        } 

        private async void compressButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedSourcePath))
            {
                MessageBox.Show("First, choose a file, please.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prepare paths and options
            string sourceDirectory = Path.GetDirectoryName(_selectedSourcePath);
            string sourceFileName = Path.GetFileNameWithoutExtension(_selectedSourcePath);
            string sourceExtension = Path.GetExtension(_selectedSourcePath);
            string destinationPath = Path.Combine(sourceDirectory, $"{sourceFileName}_compressed{sourceExtension}");
            int quality = (int)qualityNumeric.Value;

            // Disable UI to prevent double-clicks
            compressButton.Enabled = false;
            selectFileButton.Enabled = false;
            statusLabel.Text = "Compression...";

            try
            {
                await Task.Run(() =>
                {
                    if (_isJpeg)
                    {
                        _compressor.CompressJpeg(_selectedSourcePath, destinationPath, quality);
                    }
                    else
                    {
                        string qualityRange = $"{Math.Max(10, quality - 20)}-{quality}";
                        _compressor.CompressPngWithQuant(_selectedSourcePath, destinationPath, qualityRange);
                    }
                });

                statusLabel.Text = "Done!";
                MessageBox.Show($"File succesfully saved:\n{destinationPath}", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Error!";
                MessageBox.Show($"An error occurred:\n{ex.Message}", "Compression error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                compressButton.Enabled = true;
                selectFileButton.Enabled = true;
            }
        }

        private void selectFileButton_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|JPEG Files|*.jpg;*.jpeg|PNG Files|*.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedSourcePath = dialog.FileName;
                    sourcePathTextBox.Text = _selectedSourcePath;

                    string extension = Path.GetExtension(_selectedSourcePath).ToLowerInvariant();
                    _isJpeg = (extension == ".jpg" || extension == ".jpeg");

                    qualityNumeric.Enabled = true;
                    compressButton.Enabled = true;
                }
            }
        }
    }
}
