
using UnityEngine;

public readonly struct Size2
{
    public readonly float Width;
    public readonly float Height;

    public Size2(float width, float height)
    {
        Width = width;
        Height = height;
    }

    public float Area => Width * Height;
    public float Perimeter => 2 * (Width + Height);

    public static Size2 operator +(Size2 a, Size2 b) => new(a.Width + b.Width, a.Height + b.Height);
    public static Size2 operator -(Size2 a, Size2 b) => new(a.Width - b.Width, a.Height - b.Height);
    public static Size2 operator *(Size2 a, Size2 b) => new(a.Width * b.Width, a.Height * b.Height);
    public static Size2 operator /(Size2 a, Size2 b) => new(a.Width / b.Width, a.Height / b.Height);
    public static Size2 operator *(Size2 a, float b) => new(a.Width * b, a.Height * b);
    public static Size2 operator /(Size2 a, float b) => new(a.Width / b, a.Height / b);
    public static Size2 operator *(float a, Size2 b) => new(a * b.Width, a * b.Height);
    public static Size2 operator /(float a, Size2 b) => new(a / b.Width, a / b.Height);
    public static Size2 operator *(Size2 a, Vector2 b) => new(a.Width * b.x, a.Height * b.y);
    public static Size2 operator /(Size2 a, Vector2 b) => new(a.Width / b.x, a.Height / b.y);
    public static Size2 operator *(Vector2 a, Size2 b) => new(a.x * b.Width, a.y * b.Height);
    public static Size2 operator /(Vector2 a, Size2 b) => new(a.x / b.Width, a.y / b.Height);
}
