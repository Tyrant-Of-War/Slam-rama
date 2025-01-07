using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTutorial : MonoBehaviour
{
    public void ResetTutorialPref()
    {
        PlayerPrefs.SetInt("Tutorial_Completed", 0);
    }
}
