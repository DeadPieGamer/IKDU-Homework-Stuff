using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegAnimator : MonoBehaviour
{
    [SerializeField, Tooltip("The distance a foot can max be from the ray end point")] private float stepLength = 2.5f;
    [SerializeField, Tooltip("Time it takes to take a step")] private float stepWait = 0.15f;
    [SerializeField, Tooltip("How much a foot rises for a step")] private float stepRaise = 0.2f;
    [SerializeField, Tooltip("Wherefrom the rays should be cast")] private List<Transform> raysFromHere = new List<Transform>();
    [SerializeField, Tooltip("How far the ray travels")] private float rayLength = 3f;
    [SerializeField, Tooltip("The layers the ray hits")] private LayerMask layersToHit;

    [SerializeField] private bool isTakingAStep = false;
    private float stepElapsed = 0f;
    private Vector2 stepStartPos;
    private Vector2 stepEndPos;

    // Update is called once per frame
    void Update()
    {
        // If a step is being taken
        if (isTakingAStep)
        {
            // Further the procedural animation
            TakeStep();
        }
    }

    // FixedUpdate is called once per physics iteration
    private void FixedUpdate()
    {
        // Only called if a step isn't currently being taken
        if (!isTakingAStep)
        {
            // Do this for every place a ray check should happen
            foreach (Transform toRayFrom in raysFromHere)
            {
                // Draw the ray that'll be shown
                Debug.DrawRay(toRayFrom.position, -toRayFrom.transform.up * rayLength, Color.red);
                // Raycast down from the toRayFrom and act upon its results
                CheckGround(Physics2D.Raycast(toRayFrom.position, -toRayFrom.transform.up, rayLength, layersToHit));
            }
        }
    }

    /// <summary>
    /// Checks ground at the point hit. If the point is too far from the leg, move it there
    /// </summary>
    /// <param name="myRay"></param>
    private void CheckGround(RaycastHit2D myRay)
    {
        // If this ray doesn't have a collider, ignore it
        if (myRay.collider == false)
        {
            return;
        }
        // Only act if it hit ground
        if (myRay.collider.tag == "Ground")
        {
            // Finds distance to ray from leg
            Vector2 distanceToLeg = (Vector2)transform.position - myRay.point;

            // If the length of the distance to the point is more than the max distance, move the leg to the ray end position
            if (distanceToLeg.magnitude > stepLength)
            {
                // Set the start and end position
                stepStartPos = transform.position;
                stepEndPos = myRay.point;
                // Reset the elapse step time
                stepElapsed = 0f;
                // Begin taking a step
                isTakingAStep = true;
                // Start the step taking animation
                TakeStep();
            }
        }
    }

    /// <summary>
    /// Animates steps
    /// </summary>
    private void TakeStep()
    {
        // Update stepElapsed
        stepElapsed += Time.deltaTime;

        // Lerp between the step start and end position by a stepElapsed amount. Make it scale to stepWait
        Vector2 targetPos = Vector2.Lerp(stepStartPos, stepEndPos, stepElapsed / stepWait);

        // If the step is finished
        if (stepElapsed / stepWait >= 1f)
        {
            // Stop taking a step
            isTakingAStep = false;
            // Set the target position as the target end position, to avoid any "> 1f" lerp conflicts
            targetPos = stepEndPos;
        }
        else // If the step is still happening
        {
            // Make a parabola of the step. Make the 0-points be the step end positions, and the top the stepraise
            float verticalOffset = stepRaise * (1f - ((2 * (stepElapsed - 0.5f)) * (2 * (stepElapsed - 0.5f))));
            // Add this to the lerped position
            targetPos = new Vector2(targetPos.x, targetPos.y + verticalOffset);
        }

        // Set the new position
        transform.position = targetPos;
    }
}
