using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimator : MonoBehaviour
{
    [SerializeField, Tooltip("All the feet end points")] private List<Transform> feetPoints = new List<Transform>();
    [SerializeField, Tooltip("The units the main body should float over the foot pos average")] private float bodyHeight = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the average y placement of the feet
        float feetYPos = 0;
        foreach (Transform transFeet in feetPoints)
        {
            feetYPos += transFeet.position.y;
        }

        feetYPos = feetYPos / feetPoints.Count;

        // Gets a position that's the regular transform, but with the body 2.5f above the feet
        Vector3 newPos = new Vector3(transform.position.x, feetYPos + bodyHeight, transform.position.z);

        // Sets the position to be bodyHeight above average feetPoints position
        transform.position = newPos;
    }
}
