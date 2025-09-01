using System.Device.I2c;

namespace OledSharp.SSD1306
{
    /// <summary>
    /// Low-level SSD1306 OLED display driver with individual pixel control
    /// </summary>
    public sealed class SSD1306Display : IOledDisplay
    {
        // Physical display constants
        private const int _width = 128;
        private const int _height = 64;
        private const int _pageCount = 8; // 64 pixels / 8 pixels per page

        // Display buffer - mirrors what's on the OLED screen
        // 128 columns × 8 pages = 1024 bytes total
        private readonly byte[] _displayBuffer = new byte[_width * _pageCount];
        private readonly I2cDevice _i2cDevice;
        private bool _disposed = false;

        /// <summary>
        /// Display width (128 pixels)
        /// </summary>
        public int Width => _width;

        /// <summary>
        /// Display height (64 pixels)
        /// </summary>
        public int Height => _height;

        /// <summary>
        /// Creates a new SSD1306 display instance
        /// </summary>
        /// <param name="busId">I2C bus ID.</param>
        /// <param name="deviceAddress">I2C device address (typically 0x3C)</param>
        public SSD1306Display(int busId, int deviceAddress = 0x3C)
        {
            I2cConnectionSettings settings = new I2cConnectionSettings(busId, deviceAddress);
            _i2cDevice = I2cDevice.Create(settings);
        }

        /// <summary>
        /// Creates a new SSD1306 display instance with an existing I2C device
        /// </summary>
        /// <param name="i2cDevice">Existing I2C device</param>
        public SSD1306Display(I2cDevice i2cDevice)
        {
            this._i2cDevice = i2cDevice;
        }

        /// <summary>
        /// Sets or clears an individual pixel in the display buffer
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="isOn">True to turn pixel on, false to turn off</param>
        public void SetBufferPixel(int x, int y, bool isOn)
        {
            // Bounds checking
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;

            // Calculate which page (row of 8 pixels) this pixel belongs to
            int page = y / 8;
            // Calculate which bit within that page (0 = top pixel, 7 = bottom pixel)  
            int bitPosition = y % 8;
            // Calculate buffer index
            int bufferIndex = (page * _width) + x;

            // Set or clear the specific bit
            if (isOn)
            {
                _displayBuffer[bufferIndex] |= (byte)(1 << bitPosition);
            }
            else
            {
                _displayBuffer[bufferIndex] &= (byte)~(1 << bitPosition);
            }
        }

        /// <summary>
        /// Gets the state of an individual pixel from the display buffer
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>True if pixel is on, false if off or coordinates are out of bounds</returns>
        public bool GetBufferPixel(int x, int y)
        {
            // Bounds checking
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return false;

            int page = y / 8;
            int bitPosition = y % 8;
            int bufferIndex = (page * _width) + x;

            return (_displayBuffer[bufferIndex] & (1 << bitPosition)) != 0;
        }

        /// <summary>
        /// Clears the display buffer (turns all pixels off)
        /// Call PushBuffer() to apply changes to the physical display
        /// </summary>
        public void ClearBuffer()
        {
            Array.Clear(_displayBuffer, 0, _displayBuffer.Length);
        }

        /// <summary>
        /// Pushes the display buffer contents to the physical display
        /// </summary>
        public void PushBuffer()
        {
            // Set addressing window to entire display
            SendCommand(0x21, 0x00, 0x7F); // Set column address (0 to 127)
            SendCommand(0x22, 0x00, 0x07); // Set page address (0 to 7)

            // Send our display buffer to the screen
            byte[] displayData = new byte[_displayBuffer.Length + 1];
            displayData[0] = 0x40; // Data prefix
            Array.Copy(_displayBuffer, 0, displayData, 1, _displayBuffer.Length);

            _i2cDevice.Write(displayData);
        }

        /// <summary>
        /// Initializes the SSD1306 display with the required configuration
        /// </summary>
        public void Initialize()
        {
            // SSD1306 initialization sequence - send commands directly via I2C
            // Commands are sent with 0x00 prefix, followed by the command byte(s)

            SendCommand(0xAE); // Display OFF
            SendCommand(0xD5, 0x80); // Set display clock divide ratio/oscillator frequency
            SendCommand(0xA8, 0x3F); // Set multiplex ratio (64-1)
            SendCommand(0xD3, 0x00); // Set display offset
            SendCommand(0x40); // Set display start line
            SendCommand(0x8D, 0x14); // Charge pump setting (enable charge pump)
            SendCommand(0x20, 0x00); // Memory addressing mode (horizontal)
            SendCommand(0xA1); // Set segment re-map (column 127 mapped to SEG0)
            SendCommand(0xC8); // Set COM output scan direction (remapped)
            SendCommand(0xDA, 0x12); // Set COM pins hardware configuration
            SendCommand(0x81, 0x8F); // Set contrast control
            SendCommand(0xD9, 0xF1); // Set pre-charge period
            SendCommand(0xDB, 0x40); // Set VCOMH deselect level
            SendCommand(0xA4); // Entire display ON (use RAM content)
            SendCommand(0xA6); // Set normal display (not inverted)
            SendCommand(0xAF); // Display ON
        }

        private void SendCommand(params byte[] commands)
        {
            // SSD1306 command format: 0x00 followed by command byte(s)
            byte[] buffer = new byte[commands.Length + 1];
            buffer[0] = 0x00; // Command prefix
            Array.Copy(commands, 0, buffer, 1, commands.Length);
            _i2cDevice.Write(buffer);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _i2cDevice?.Dispose();
                _disposed = true;
            }
        }
    }
}
