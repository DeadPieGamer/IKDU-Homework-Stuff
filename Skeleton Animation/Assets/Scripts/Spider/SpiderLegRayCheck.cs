using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To be attached to the different checks, from where this will raycast in multiple directions, depending on which gravity field this is in
/// </summary>
public class SpiderLegRayCheck : MonoBehaviour
{
    [SerializeField, Tooltip("How far the ray travels")] private float rayLength = 6f;
    [SerializeField, Tooltip("The layers the ray hits")] private LayerMask layersToHit;
    private Dictionary<GameObject, Vector2> rayDirections = new Dictionary<GameObject, Vector2>();
    [HideInInspector] public List<RaycastHit2D> raycastHits2D = new List<RaycastHit2D>();
    [HideInInspector] public List<Vector2> theHitsDirections = new List<Vector2>();

    private void FixedUpdate()
    {
        raycastHits2D.Clear();
        theHitsDirections.Clear();

        // Do this for every place a ray check should happen
        foreach (Vector2 toRayToward in rayDirections.Values)
        {
            // endedCastFromWhere = toRayFrom.position;
            // Draw the ray that'll be shown
            Debug.DrawRay(transform.position, toRayToward * rayLength, Color.red);
            // Raycast in the gravity field direction from this position and act upon its results

            raycastHits2D.Add(Physics2D.Raycast(transform.position, toRayToward, rayLength, layersToHit));
            theHitsDirections.Add(toRayToward);
        }
    }

    // Called the check enters a gravity field
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GravityField"))
        {
            rayDirections.Add(collision.gameObject, -collision.transform.up);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GravityField"))
        {
            rayDirections.Remove(collision.gameObject);
        }
    }
}
