using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couldren : MonoBehaviour
{
    [SerializeField] Animator platforms;

    float platformDelay;

    // Start is called before the first frame update
    void Start()
    {
        platformDelay = 15f;

        if (PlayerPrefs.GetInt("Hazards_On") == 0)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (platformDelay > 0)
        {
            platformDelay = platformDelay - Time.deltaTime;
        }
        else
        {
            platforms.SetTrigger("Drop" + Random.Range(1, 4).ToString());

            platformDelay = 15f;
        }
    }
}
