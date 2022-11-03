using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimator : MonoBehaviour
{
    [SerializeField, Tooltip("All the feet end points")] private List<Transform> feetPoints = new List<Transform>();
    [SerializeField, Tooltip("The units the main body should float over the foot pos average")] private float bodyHeight = 2.5f;
    [SerializeField, Tooltip("How much the body moves up and down when idle")] private float bodyFloating = 0.5f;
    [SerializeField, Tooltip("How fast the idle plays")] private float idleSpeed = 1f;
    [SerializeField, Tooltip("Main player controller things")] private Transform mainPlayCon;

    // Figure out how to have the body be bodyHeight over the average foot position, relative to the rotation

    // Update is called once per frame
    void Update()
    {
        // Sin function based on Time.time
        float sinTTBodyOffset = Mathf.Sin(Time.time * idleSpeed) * bodyFloating / 2f;

        // Gets the average placement of the feet
        Vector2 feetPos = new Vector2(0f ,0f);
        foreach (Transform transFeet in feetPoints)
        {
            feetPos += (Vector2)transFeet.position;
        }

        feetPos = feetPos / (float)feetPoints.Count;

        // Gets a position that's the regular transform, but with the body 2.5f above the feet
        Vector3 newPos = feetPos + (bodyHeight + sinTTBodyOffset) * (Vector2)mainPlayCon.up;

        // Sets the position to be bodyHeight above average feetPoints position
        transform.position = newPos;
    }
}
