using UnityEngine;

public class CosmeticController
{
    GameObject[] CosmeticObjects;
    byte IndexTracker;

    public CosmeticController(GameObject[] cosmeticObjects)
    {
        CosmeticObjects = cosmeticObjects;
        IndexTracker = 0;
        // Ensure the first cosmetic object is active
        if (CosmeticObjects.Length > 0)
        {
            CosmeticObjects[0].SetActive(true);
        }
    }

    public void Next()
    {
        // Deactivate the current object
        CosmeticObjects[IndexTracker].SetActive(false);

        // Increment the index and wrap around using modulo
        IndexTracker = (byte)((IndexTracker + 1) % CosmeticObjects.Length);

        // Activate the next object
        CosmeticObjects[IndexTracker].SetActive(true);
    }

    public void Previous()
    {
        // Deactivate the current object
        CosmeticObjects[IndexTracker].SetActive(false);

        // Decrement the index and wrap around (loop backward)
        IndexTracker = (byte)((IndexTracker - 1 + CosmeticObjects.Length) % CosmeticObjects.Length);

        // Activate the previous object
        CosmeticObjects[IndexTracker].SetActive(true);
    }
}
