using OledSharp.Png;
using System.Drawing.Drawing2D;

namespace OledSharp.VirtualScreenDemo
{
    public partial class Form1 : Form
    {
        private const string outputFileName = "display_output.png";

        private PngOutputDisplay _display;
        private TextRenderer _textRenderer;
        private int currentImage = 0;

        public Form1()
        {
            InitializeComponent();
            _display = new PngOutputDisplay(outputFileName);
            _textRenderer = new TextRenderer(_display);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;

            _display.ClearBuffer();
            _textRenderer.DrawWrappedString(2, 2, text, 124); // 128 - 4 for padding

            _display.PushBuffer();

            // Dispose the old image to release file locks
            Image? oldImage = pictureBox1.Image;
            pictureBox1.Image = null;
            oldImage?.Dispose();

            // Load image from memory to avoid file locks and create scaled version with nearest neighbor
            byte[] fileBytes = File.ReadAllBytes(outputFileName);
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                using (Bitmap originalImage = (Bitmap)Bitmap.FromStream(memoryStream))
                {
                    // Scale up the image (4x for better visibility)
                    int scale = 4;
                    int scaledWidth = originalImage.Width * scale;
                    int scaledHeight = originalImage.Height * scale;
                    
                    Bitmap scaledImage = new Bitmap(scaledWidth, scaledHeight);
                    using (Graphics graphics = Graphics.FromImage(scaledImage))
                    {
                        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                        graphics.PixelOffsetMode = PixelOffsetMode.Half;
                        graphics.DrawImage(originalImage, new Rectangle(Point.Empty, scaledImage.Size));
                    }
                    
                    pictureBox1.Image = scaledImage;
                }
            }
        }
    }
}
