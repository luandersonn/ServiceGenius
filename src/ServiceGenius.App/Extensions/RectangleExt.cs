using System;
using Windows.Foundation;
using Windows.Graphics;

namespace ServiceGenius.App.Extensions;

public static class RectangleExt
{
    public static RectInt32 ToRectInt32(this Rect bounds, double scale = 1) => new(
            _X: (int)Math.Round(bounds.X * scale),
            _Y: (int)Math.Round(bounds.Y * scale),
            _Width: (int)Math.Round(bounds.Width * scale),
            _Height: (int)Math.Round(bounds.Height * scale)
        );
}