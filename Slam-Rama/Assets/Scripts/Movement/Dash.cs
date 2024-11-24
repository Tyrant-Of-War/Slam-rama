using System.Collections;
using UnityEngine;

public class Dash
{
    // The players rigid body for applying the dash force
    private Rigidbody playerRB;

    // The cool down timer to stop dash spaming
    private float dashCooldown;

    // The length of the dash
    private float dashDuration = 0.2f;

    // The speed / power of force that should be applied during the dash
    private float dashSpeed;

    // Required to start coroutines
    private MonoBehaviour owner;

    // Dash cooldown flag
    private bool canDash = true; 

    // Is a class with all dash attributes
    public Dash(Rigidbody rb, float dashCooldown, float dashSpeed, MonoBehaviour owner)
    {
        this.playerRB = rb;
        this.dashCooldown = dashCooldown;
        this.dashSpeed = dashSpeed;
        this.owner = owner;
    }

    // Is called by the player movement script when dash input is recorded
    public void ExecuteDash(Vector3 movementDirection, bool isGrounded, float stickInputX, float stickInputY)
    {
        // Checks if the player is not currently on cooldown, is on the ground, and is holding some kind of direction input
        if (canDash && isGrounded && (stickInputX != 0f || stickInputY != 0f))
        {
            // Normalize the movement direction
            Vector3 dashDirection = movementDirection.normalized;

            // Start the coroutine to dash and handle cooldown
            owner.StartCoroutine(DashCoroutine(dashDirection));
        }
    }

    private IEnumerator DashCoroutine(Vector3 dashDirection)
    {
        canDash = false;

        // Set the velocity for the dash
        playerRB.velocity = dashDirection * dashSpeed;

        // Wait for the duration of the dash
        yield return new WaitForSeconds(dashDuration);

        // Reset the velocity (allow normal movement after dash)
        playerRB.velocity = Vector3.zero;

        // Wait for cooldown to re-enable dashing
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}
