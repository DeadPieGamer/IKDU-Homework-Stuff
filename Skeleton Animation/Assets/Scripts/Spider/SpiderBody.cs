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

        //transform.position = GetOffset();
    }

    public Vector2 GetOffset()
    {
        // Sin function based on Time.time
        float sinTTBodyOffset = Mathf.Sin(Time.time * idleSpeed) * bodyFloating / 2f;

        Vector2 downForThis = (Vector2)transform.right;

        // Get the ground beneath body, based on foot rotation
        RaycastHit2D myRayHit = Physics2D.Raycast(transform.position, downForThis, rayLength, layersToHit);

        // Get the same ground but with a ray in the direction of the grounds gravity field
        myRayHit = Physics2D.Raycast(transform.position, -myRayHit.transform.up, rayLength, layersToHit);
        Debug.DrawRay(transform.position, downForThis * rayLength, Color.magenta);

        Vector2 newOffset = myRayHit.point + (bodyHeight + sinTTBodyOffset) * (Vector2)myRayHit.transform.up;

        // Gets a position that's the regular transform, but with the body X amount above the feet
        Vector2 newPos = (Vector2)transform.position - lastOffset + newOffset;

        lastOffset = newOffset;

        return newPos;
    }
}
