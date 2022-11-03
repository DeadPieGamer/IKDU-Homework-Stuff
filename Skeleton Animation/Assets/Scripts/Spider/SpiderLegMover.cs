using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegMover : MonoBehaviour
{
    [SerializeField, Tooltip("The distance a foot can max be from the ray end point")] private float stepLength = 2.5f;
    [SerializeField, Tooltip("Time it takes to take a step")] private float stepWait = 0.15f;
    [SerializeField, Tooltip("How much a foot rises for a step")] private float stepRaise = 0.2f;
    [SerializeField, Tooltip("Rays are cast from another object, which I should get the ray from")] private SpiderLegRayCheck[] rayChecker;

    [SerializeField] private bool isTakingAStep = false;
    private float stepElapsed = 0f;
    private Vector2 stepStartPos;
    private Vector2 stepEndPos;

    private List<GravityFieldSquare> gravityFields = new List<GravityFieldSquare>();

    private Quaternion stepEndRotation;

    private Vector2 endedCastFromWhere;

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
            foreach(SpiderLegRayCheck legRayCheck in rayChecker)
            {
                for (int i = 0; i < legRayCheck.raycastHits2D.Count; i++)
                {
                    CheckGround(legRayCheck.raycastHits2D[i], legRayCheck.theHitsDirections[i]);
                }
            }
        }
    }

    /// <summary>
    /// Checks ground at the point hit. If the point is too far from the leg, move it there
    /// </summary>
    /// <param name="myRay"></param>
    private void CheckGround(RaycastHit2D myRay, Vector2 directionOfRay)
    {
        // If this ray doesn't hit a collider, ignore it
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

                stepEndRotation = Quaternion.LookRotation(Vector3.forward, -directionOfRay);

                // stepEndRotation = Quaternion.LookRotation(Vector3.forward, endedCastFromWhere - myRay.point);
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
            transform.rotation = stepEndRotation;
        }
        else // If the step is still happening
        {
            // Make a parabola of the step. Make the 0-points be the step end positions, and the top the stepraise
            float verticalOffset = stepRaise * (1f - ((2 * (stepElapsed - 0.5f)) * (2 * (stepElapsed - 0.5f))));

            // Makes sure the step is actually right according to target rotation
            Vector2 targetUpStep = transform.up * verticalOffset;
            
            // Add this to the lerped position
            targetPos = targetPos + targetUpStep;
        }

        // Set the new position
        transform.position = targetPos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("GravityField"))
        {
            if (!gravityFields.Contains(collision.GetComponent<GravityFieldSquare>()))
            {
                gravityFields.Add(collision.GetComponent<GravityFieldSquare>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Removes the current gravityField from memory
        gravityFields.Remove(collision.GetComponent<GravityFieldSquare>());
    }
}
