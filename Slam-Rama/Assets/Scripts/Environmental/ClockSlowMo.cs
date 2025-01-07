using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSlowMo : MonoBehaviour
{

    //slow motion delaby 10 seconds 
    //Time between slow motion should be random, but long
    //Should be 5 5 seconds grace, set random delay longer than 5 seconds. 

    float slowDelay;

    float slowDuration;

    bool isSlow;

    private void Start()
    {
        slowDelay = Random.Range(10, 20);

        if (PlayerPrefs.GetInt("Hazards_On") == 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (! isSlow)
        {
            if (slowDelay > 0)
            {
                slowDelay = slowDelay - Time.deltaTime;
            }
            else
            {
                Time.timeScale = 0.5f;
                isSlow = true;
                slowDuration = Random.Range(2, 5);    
            }
        }
        else
        {
            if (slowDuration > 0)
            {
                slowDuration = slowDuration - Time.deltaTime;
            }
            else
            {
                Time.timeScale = 1f;
                isSlow = false;
                slowDelay = Random.Range(10, 20);
            }
        }
    
    }

}
