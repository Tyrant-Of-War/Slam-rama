using System.Collections;
using UnityEngine;

public class Dash
{
    private Rigidbody playerRB;
    private float dashCooldown;
    private float dashDuration = 0.2f; // Dash duration in seconds
    private float dashSpeed;
    private MonoBehaviour owner; // Required to start coroutines

    private bool canDash = true; // Dash cooldown flag

    public Dash(Rigidbody rb, float dashCooldown, float dashSpeed, MonoBehaviour owner)
    {
        this.playerRB = rb;
        this.dashCooldown = dashCooldown;
        this.dashSpeed = dashSpeed;
        this.owner = owner;
    }

    public void ExecuteDash(Vector3 movementDirection, bool isGrounded, float stickInputX, float stickInputY)
    {
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
