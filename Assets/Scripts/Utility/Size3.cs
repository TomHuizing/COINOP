
using UnityEngine;

public readonly struct Size3
{
    public readonly float Width;
    public readonly float Height;
    public readonly float Depth;

    public Size3(float width, float height, float depth)
    {
        Width = width;
        Height = height;
        Depth = depth;
    }

    public float Volume => Width * Height * Depth;
    public float SurfaceArea => 2 * (Width * Height + Width * Depth + Height * Depth);
    public float Perimeter => 4 * (Width + Height + Depth);

    public static Size3 operator +(Size3 a, Size3 b) => new(a.Width + b.Width, a.Height + b.Height, a.Depth + b.Depth);
    public static Size3 operator -(Size3 a, Size3 b) => new(a.Width - b.Width, a.Height - b.Height, a.Depth - b.Depth);
    public static Size3 operator *(Size3 a, Size3 b) => new(a.Width * b.Width, a.Height * b.Height, a.Depth * b.Depth);
    public static Size3 operator /(Size3 a, Size3 b) => new(a.Width / b.Width, a.Height / b.Height, a.Depth / b.Depth);
    public static Size3 operator *(Size3 a, float b) => new(a.Width * b, a.Height * b, a.Depth * b);
    public static Size3 operator /(Size3 a, float b) => new(a.Width / b, a.Height / b, a.Depth / b);
    public static Size3 operator *(float a, Size3 b) => new(a * b.Width, a * b.Height, a * b.Depth);
    public static Size3 operator /(float a, Size3 b) => new(a / b.Width, a / b.Height, a / b.Depth);
    public static Size3 operator *(Size3 a, Vector3 b) => new(a.Width * b.x, a.Height * b.y, a.Depth * b.z);
    public static Size3 operator /(Size3 a, Vector3 b) => new(a.Width / b.x, a.Height / b.y, a.Depth / b.z);
    public static Size3 operator *(Vector3 a, Size3 b) => new(a.x * b.Width, a.y * b.Height, a.z * b.Depth);
    public static Size3 operator /(Vector3 a, Size3 b) => new(a.x / b.Width, a.y / b.Height, a.z / b.Depth);
}
