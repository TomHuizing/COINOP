using System;

namespace InputSystem
{
    [Flags] public enum KeyModifiers
    {
        None = 0,
        Shift = 1,
        Control = 2,
        Alt = 4,
        Command = 8, // For macOS
        CapsLock = 16,
        NumLock = 32
    }
}