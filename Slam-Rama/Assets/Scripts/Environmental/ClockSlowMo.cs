using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ClockSlowMo : MonoBehaviour
{

    //slow motion delaby 10 seconds 
    //Time between slow motion should be random, but long
    //Should be 5 5 seconds grace, set random delay longer than 5 seconds. 

    float slowCooldown;

    float slowDelay;

    float slowDuration;

    bool isSlow;

    bool inDelay;

    [SerializeField] GameObject defaultVolume;

    [SerializeField] GameObject slowVolume;

    // The animator for the clock hand
    [SerializeField] Animator animator;

    private void Start()
    {
        slowCooldown = Random.Range(10, 20);

        if (PlayerPrefs.GetInt("Hazards_On") == 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!isSlow)
        {
            if (slowCooldown > 0)
            {
                slowCooldown = slowCooldown - Time.deltaTime;
            }
            else if (slowCooldown > -1000)
            {
                animator.SetTrigger("Slow");

                slowDelay = 4.167f;

                slowCooldown = -5000;

                inDelay = true;
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
                UnSlowTime();
            }
        }

        if (inDelay)
        {
            if (slowDelay > 0)
            {
                slowDelay = slowDelay - Time.deltaTime;
            }
            else
            {

                inDelay = false;

                SlowTime();
            }
        }
    }


    public void SlowTime()
    {
        // Halfs the speed of time
        Time.timeScale = 0.5f;
        // Sets state to slowed
        isSlow = true;
        // Generates random duration for slow
        slowDuration = Random.Range(2, 5);
        // Switches currently shown volume
        defaultVolume.SetActive(false);
        slowVolume.SetActive(true);
    }

    public void UnSlowTime()
    {
        // Halfs the speed of time
        Time.timeScale = 1f;
        // Sets state to slowed
        isSlow = false;
        // Generates random duration for slow
        slowCooldown = Random.Range(10, 20);
        // Switches currently shown volume
        defaultVolume.SetActive(true);
        slowVolume.SetActive(false);

        animator.SetTrigger("UnSlow");
    }
}
