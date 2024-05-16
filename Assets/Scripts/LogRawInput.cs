using UnityEngine;
using UnityRawInput;

public class LogRawInput : MonoBehaviour
{
    public bool WorkInBackground;
    public bool InterceptMessages;

    private bool isKeyPressed = false;

    private bool mouseKeyPressed = false;
    private bool isKBKeyPressed = false;

    // Public property to access the key pressed status
    public bool IsKeyPressed
    {
        get { return isKeyPressed; }
    }

    public bool isMouseKeyPressed
    {
        get { return mouseKeyPressed; }
    }

    public bool isKeyboardKeyPressed
    {
        get { return isKBKeyPressed; }
    }

    private void OnEnable()
    {
        RawInput.WorkInBackground = WorkInBackground;
        RawInput.InterceptMessages = InterceptMessages;

        RawInput.OnKeyUp += LogKeyUp;
        RawInput.OnKeyDown += LogKeyDown;
        RawInput.OnKeyDown += DisableIntercept;

        RawInput.Start();
    }

    private void OnDisable()
    {
        RawInput.Stop();

        RawInput.OnKeyUp -= LogKeyUp;
        RawInput.OnKeyDown -= LogKeyDown;
        RawInput.OnKeyDown -= DisableIntercept;
    }

    private void OnValidate()
    {
        // Apply options when toggles are clicked in editor.
        // OnValidate is invoked only in the editor (won't affect build).
        RawInput.InterceptMessages = InterceptMessages;
        RawInput.WorkInBackground = WorkInBackground;
    }

    private void LogKeyUp(RawKey key)
    {
        //Debug.Log("Key Up: " + key);
    }

    private void LogKeyDown(RawKey key)
    {
        //Debug.Log("Key Down: " + key);
        isKeyPressed = true; // Set the flag to true when a key is pressed
        switch (key)
        {
            case RawKey.LeftButton:
            case RawKey.RightButton:
            case RawKey.MiddleButton:
            case RawKey.WheelDown:
            case RawKey.WheelUp:
                mouseKeyPressed = true; break;

            default:
                isKBKeyPressed = true;
                break;

        }
    }

    private void DisableIntercept(RawKey key)
    {
        if (RawInput.InterceptMessages && key == RawKey.Escape)
            RawInput.InterceptMessages = InterceptMessages = false;
    }

    public void setKeyUnpressed()
    {
        isKeyPressed = false;
        mouseKeyPressed = false;
        isKBKeyPressed = false;
    }
}
