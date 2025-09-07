# OledSharp.Png

[![NuGet version](https://img.shields.io/nuget/v/OledSharp.Png.svg)](https://www.nuget.org/packages/OledSharp.Png/)

A .NET 8.0 implementation of `IOledDisplay` that outputs display content to PNG image files. This library is perfect for testing, debugging, and visualizing what would be displayed on an OLED screen without needing physical hardware.

> [INFO]
> This is a software implementation for testing and visualization. For the main library documentation and general usage, see [OledSharp](../OledSharp/README.md).

## Overview

This library provides the `PngOutputDisplay` class which implements the `IOledDisplay` interface from the base OledSharp library. Instead of controlling physical hardware, it renders the display buffer to a PNG image file, making it ideal for development, testing, and documentation purposes.

## Installation

To install the OledSharp.Png library via NuGet, run the following command in your project directory:

```bash
dotnet add package OledSharp.Png
```

## Features

- **PNG Output** - Renders display buffer to PNG image files
- **Configurable Resolution** - Default 128x64 pixels, customizable
- **Individual Pixel Control** - Set/get individual pixels in the display buffer
- **Grayscale Output** - White pixels for "on", black pixels for "off"
- **File-based Output** - Saves to specified file path

## Dependencies

- **OledSharp** - Base library for display abstraction (see [OledSharp](../OledSharp/README.md))
- **BigGustave** - PNG creation and manipulation library

## Usage Example

```csharp
using OledSharp.Png;

// Create PNG output display (128x64 pixels, saves to "output.png")
using (IOledDisplay display = new PngOutputDisplay("output.png", width: 128, height: 64))
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

    // Save to PNG file
    display.PushBuffer();
}
```

## Constructor Options

### With File Path Only (Default 128x64)
```csharp
PngOutputDisplay display = new PngOutputDisplay("output.png");
```

### With Custom Dimensions
```csharp
PngOutputDisplay display = new PngOutputDisplay("output.png", width: 256, height: 128);
```

## Use Cases

- **Development and Testing** - Visualize display output during development
- **Documentation** - Generate example images for documentation
- **Unit Testing** - Verify text rendering and graphics without hardware
- **Prototyping** - Test layouts and designs before implementing on hardware

## Output Format

- **File Format** - PNG image
- **Color Mode** - Grayscale (white = pixel on, black = pixel off)
- **Resolution** - Configurable, default 128x64 pixels
- **File Overwrite** - Each `PushBuffer()` call overwrites the existing file

## License

MIT License - see project file for details.

## Repository

https://github.com/AdamTovatt/oled-sharp
