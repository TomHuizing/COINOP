using System.Collections;
using UnityEngine;

public class Keyboard
{
    public string KeyBuffer => keyBuffer; // Property to access the key buffer

    private string keyBuffer = string.Empty; // Buffer to store key presses
    private bool isBuffering = false; // Flag to indicate if buffering is active

    public Keyboard(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(KeyboardUpdate()); // Start the keyboard update coroutine
        // Constructor can be used to initialize any necessary components
    }

    private IEnumerator KeyboardUpdate()
    {
        while (true)
        {
            // Check for key presses
            for (int i = 0; i < 256; i++)
            {
                if (Input.GetKeyDown((KeyCode)i))
                {
                    keyBuffer += ((KeyCode)i).ToString(); // Append the pressed key to the buffer
                }
            }

            yield return null; // Wait for the next frame
        }
    }

    private void FillBuffer()
    {
        for (int i = 0; i < 256; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                char c = GetCharFromKeyCode((KeyCode)i);
                if (c != '\0')
                {
                    keyBuffer += c;
                }
            }
        }
        
    }

    char GetCharFromKeyCode(KeyCode keyCode)
    {
        return keyCode switch
        {
            KeyCode.Space => ' ',
            KeyCode.Return or KeyCode.KeypadEnter => '\n',
            KeyCode.Backspace => HandleBackspace(),
            >= KeyCode.A and <= KeyCode.Z => GetLetterChar(keyCode),
            >= KeyCode.Alpha0 and <= KeyCode.Alpha9 => (char)('0' + (keyCode - KeyCode.Alpha0)),
            _ => '\0'
        };
    }

    char HandleBackspace()
    {
        if (keyBuffer.Length > 0)
        {
            keyBuffer = keyBuffer.Substring(0, keyBuffer.Length - 1);
        }
        return '\0';
    }

    char GetLetterChar(KeyCode code)
    {
        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        char baseChar = (char)('a' + (code - KeyCode.A));
        return shift ? char.ToUpper(baseChar) : baseChar;
    }

    public void StartBuffer()
    {
        if (isBuffering)
            return; // If already buffering, do nothing
        keyBuffer = string.Empty; // Clear the key buffer
        isBuffering = true; // Set the buffering flag to true
    }

    public void FlushBuffer()
    {
        keyBuffer = string.Empty; // Clear the key buffer
    }
}
