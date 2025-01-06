using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCosmeticInput : MonoBehaviour
{
    CosmeticController Head, Body;
    public GameObject[] head, body;
    private enum navigationPos
    {
        Head,
        Body,
        Colours,
    }
    navigationPos navigationPosition;
    void Start()
    {
        navigationPosition = navigationPos.Head;
        Head = new CosmeticController(head);
        Body = new CosmeticController(body);
        Head.Next();
        Body.Next();
    }
    private bool IsDPadInput(InputAction.CallbackContext? context)
    {
        // Check if the control triggered by the input has a path related to the D-pad
        var controlPath = context.Value.control.path;

        if (string.IsNullOrEmpty(controlPath)) return false;

        // Check if the path contains "dpad"
        return controlPath.Contains("dpad", System.StringComparison.OrdinalIgnoreCase);
    }

    public void OnNavigate(InputValue inputValue)
    {
        var contextField = typeof(InputValue).GetField("m_Context", BindingFlags.NonPublic | BindingFlags.Instance);
        var callbackContext = contextField?.GetValue(inputValue) as InputAction.CallbackContext?;
        if (this.enabled)
        {
            if (callbackContext.HasValue)
            {
                var action = callbackContext.Value.action; // Access the associated action
                if (this.enabled)
                {
                    // Replace IsDPadInput with your own validation logic
                    if (!IsDPadInput(callbackContext))
                    {
                        Debug.LogWarning("Input is not from the DPad.");
                        return;
                    }
                    Vector2 Direction = action.ReadValue<Vector2>().normalized;
                    Direction = Direction.normalized;
                    int horizontalDirection = Direction.x > 0 ? 1 : Direction.x < 0 ? -1 : 0; // weird if statment BS. thanks google
                    int verticleDirection = Direction.y > 0 ? 1 : Direction.y < 0 ? -1 : 0;
                    switch (horizontalDirection)
                    {
                        case 1:
                            switch (navigationPosition)
                            {
                                case navigationPos.Head:
                                    Head.Next();
                                    break;
                                case navigationPos.Body:
                                    Body.Next();
                                    break;
                                case navigationPos.Colours:
                                    //UpdateColour(true);
                                    break;
                            }
                            break;
                        case -1:
                            switch (navigationPosition)
                            {
                                case navigationPos.Head:
                                    Head.Previous();
                                    break;
                                case navigationPos.Body:
                                    Body.Previous();
                                    break;
                                case navigationPos.Colours:
                                    //UpdateColour(false);
                                    break;

                            }
                            break;
                    }
                    if (IsDPadInput(callbackContext))
                    {
                        switch (verticleDirection)
                        {
                            case 1:
                                switch (navigationPosition)
                                {
                                    case navigationPos.Head:
                                        navigationPosition = navigationPos.Colours;
                                        break;
                                    case navigationPos.Body:
                                        navigationPosition = navigationPos.Head;
                                        break;
                                    case navigationPos.Colours:
                                        navigationPosition = navigationPos.Body;
                                        break;
                                }
                                break;
                            case -1:
                                switch (navigationPosition)
                                {
                                    case navigationPos.Head:
                                        navigationPosition = navigationPos.Body;
                                        break;
                                    case navigationPos.Body:
                                        navigationPosition = navigationPos.Colours;
                                        break;
                                    case navigationPos.Colours:
                                        navigationPosition = navigationPos.Head;
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
