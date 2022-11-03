using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField, Tooltip("The transform this will follow")] private Transform playerTrans;
    [SerializeField, Tooltip("The amount of time it takes from initial input for this to reach the player position. Must be above 0")] private float toReachPlayer = 0.5f;
    private Vector3 startPosition;
    private float startMomentReach = 0f;
    private bool hasReachedPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if (hasReachedPlayer)
        {
            transform.position = playerTrans.position;
        }
        else
        {
            float spentTravelling = (Time.time - startMomentReach) / toReachPlayer;
            if (spentTravelling >= 1f)
            {
                hasReachedPlayer = true;
                transform.position = playerTrans.position;
            }
            else
            {
                transform.position = Vector3.Lerp(startPosition, playerTrans.position, spentTravelling);
            }
        }
    }

    private void OnEnable()
    {
        startMomentReach = Time.time;
        startPosition = transform.position;
        if (toReachPlayer <= 0)
        {
            toReachPlayer = 0.001f;
        }
    }
}
