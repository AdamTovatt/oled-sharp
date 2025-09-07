# OledSharp.SSD1306

[![NuGet version](https://img.shields.io/nuget/v/OledSharp.SSD1306.svg)](https://www.nuget.org/packages/OledSharp.SSD1306/)

A .NET 8.0 implementation of `IOledDisplay` for SSD1306 OLED displays. This library provides direct hardware control for 128x64 pixel SSD1306 OLED displays via I2C communication.

> [!IMPORTANT]
> This is a hardware-specific implementation. For the main library documentation and general usage, see [OledSharp](../OledSharp/README.md).

## Overview

This library provides the `SSD1306Display` class which implements the `IOledDisplay` interface from the base OledSharp library. It handles low-level communication with SSD1306 OLED displays and provides pixel-level control.

## Installation

To install the OledSharp.SSD1306 library via NuGet, run the following command in your project directory:

```bash
dotnet add package OledSharp.SSD1306
```

## Features

- **128x64 pixel resolution** - Standard SSD1306 display size
- **I2C communication** - Uses System.Device.I2c for hardware communication
- **Individual pixel control** - Set/get individual pixels in the display buffer
- **Hardware initialization** - Proper SSD1306 initialization sequence
- **Resource management** - Implements IDisposable for proper cleanup

## Dependencies

- **OledSharp** - Base library for display abstraction (see [OledSharp](../OledSharp/README.md))
- **System.Device.I2c** - I2C communication
- **Iot.Device.Bindings** - IoT device bindings

## Usage Example

```csharp
using OledSharp.SSD1306;

// Create SSD1306 display instance (I2C bus 1, device address 0x3C)
using (IOledDisplay display = new SSD1306Display(busId: 1, deviceAddress: 0x3C))
{
    display.Initialize();

    // Create text renderer
    TextRenderer renderer = new TextRenderer(display);

    // Draw with word wrapping
    renderer.DrawWrappedString(
        x: 2, // 2 px in from the left edge
        y: 2, // 2 px in from the left edge
        text: "This is a long text that will wrap",
        maxWidth: display.Width - 4); // 2 px from the right edge too

    // Draw individual pixels
    display.SetBufferPixel(10, 10, true);
    display.SetBufferPixel(11, 11, true);

    // Push changes to display
    display.PushBuffer();
}
```

## Constructor Options

### With I2C Bus ID and Device Address
```csharp
SSD1306Display display = new SSD1306Display(busId: 1, deviceAddress: 0x3C);
```

### With Existing I2C Device
```csharp
I2cDevice i2cDevice = I2cDevice.Create(settings);
SSD1306Display display = new SSD1306Display(i2cDevice);
```

## Hardware Requirements

- **SSD1306 OLED Display** - 128x64 pixel resolution
- **I2C Interface** - Standard I2C communication
- **Device Address** - Typically 0x3C (default) or 0x3D

## License

MIT License - see project file for details.

## Repository

https://github.com/AdamTovatt/oled-sharp
