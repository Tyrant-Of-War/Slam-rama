using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Called when the script is loaded
    void Awake()
    {
        // Sets the object this is attached to to not destroy on the loading of a new scene
        DontDestroyOnLoad(this);
    }

}
