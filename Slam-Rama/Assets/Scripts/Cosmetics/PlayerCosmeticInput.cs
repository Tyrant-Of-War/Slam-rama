using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCosmeticInput : MonoBehaviour
{
    CosmeticController Head, Body;
    public GameObject[] head, body;
    //private Color[] colors;
    //private int currentColorIndex = 0;
    private enum navigationPos
    {
        Head,
        Body,
        Colours
    }
    navigationPos navigationPosition;
    void Start()
    {
        navigationPosition = navigationPos.Head;
        Head = new CosmeticController(head);
        Body = new CosmeticController(body);
        Head.Next();
        Body.Next();
        //colors = new Color[]
        //{
        //    Color.white,
        //    Color.red,
        //    Color.green,
        //    Color.blue,
        //    Color.yellow,
        //    Color.magenta,
        //    Color.cyan,
        //    new Color(1f, 0.5f, 0f), // Orange
        //    new Color(0.5f, 0f, 0.5f) // Purple
        //};
        //ApplyColor(colors[0]);
    }
    private bool IsDPadInput(InputValue inputValue)
    {
        // Access the control associated with the input value
        var control = inputValue.isPressed;

        // Check if the control is a D-pad
        return control;
    }
    public void OnNavigate(InputValue inputValue)
    {
        if (this.enabled)
        {
            if (!IsDPadInput(inputValue)) return;
            Vector2 Direction = inputValue.Get<Vector2>();
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
    //public void UpdateColour(bool direction)
    //{
    //    // Increment or decrement the color index based on the direction
    //    if (direction)
    //    {
    //        currentColorIndex = (currentColorIndex + 1) % colors.Length; // Move forward, loop back if at the end
    //    }
    //    else
    //    {
    //        currentColorIndex = (currentColorIndex - 1 + colors.Length) % colors.Length; // Move backward, loop back if at the start
    //    }

    //    // Apply the current color
    //    ApplyColor(colors[currentColorIndex]);
    //}

    //public void ApplyColor(Color color)
    //{
    //    this.GetComponent<PlayerMovement>().playerData.playerMaterial.color = color;
    //}
}
