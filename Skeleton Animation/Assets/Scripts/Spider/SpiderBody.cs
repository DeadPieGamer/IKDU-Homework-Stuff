using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Body should rotate according to either legs rotation, or its own gravityzone
/// </summary>
public class SpiderBody : MonoBehaviour
{
    [SerializeField, Tooltip("All the feet end points")] private List<Transform> feetPoints = new List<Transform>();
    [SerializeField, Tooltip("The units the main body should float over the foot pos average")] private float bodyHeight = 2.5f;
    [SerializeField, Tooltip("How much the body moves up and down when idle")] private float bodyFloating = 0.5f;
    [SerializeField, Tooltip("How fast the idle plays")] private float idleSpeed = 1f;

    [SerializeField, Tooltip("How far the ray travels")] private float rayLength = 6f;
    [SerializeField, Tooltip("The layers the ray hits")] private LayerMask layersToHit;

    private Vector2 lastOffset = Vector2.zero;

    // Figure out how to have the body be bodyHeight over the average foot position, relative to the rotation

    // Update is called once per frame
    void Update()
    {
        // Get sum up direction of the feet
        Vector2 targetDirection = Vector2.zero;
        foreach (Transform transFeet in feetPoints)
        {
            targetDirection += (Vector2)transFeet.up;
        }

        // Averages those values
        targetDirection /= feetPoints.Count;
        // Makes sure it's normalized
        targetDirection.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);

        transform.rotation = Quaternion.Euler(targetRotation.eulerAngles - new Vector3(0f, 0f, 90f));
    }

    public Vector2 GetOffset()
    {
        // Sin function based on Time.time
        float sinTTBodyOffset = Mathf.Sin(Time.time * idleSpeed) * bodyFloating / 2f;

        // Get sum up direction of the feet
        Vector2 targetDirection = Vector2.zero;
        foreach (Transform transFeet in feetPoints)
        {
            targetDirection += (Vector2)transFeet.up;
        }

        // Averages those values
        targetDirection /= feetPoints.Count;
        // Makes sure it's normalized
        targetDirection.Normalize();

        // Get the ground beneath body, baseod on foot rotation
        RaycastHit2D myRayHit = Physics2D.Raycast(transform.position, -targetDirection, rayLength, layersToHit);

        Vector2 newOffset = myRayHit.point + (bodyHeight + sinTTBodyOffset) * targetDirection;

        // Gets a position that's the regular transform, but with the body 2.5f above the feet
        Vector2 newPos = (Vector2)transform.position - lastOffset + newOffset;

        lastOffset = newOffset;

        return newPos;
    }
}
