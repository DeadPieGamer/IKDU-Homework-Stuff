using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderInputScript : MonoBehaviour
{
    // This script should get inputs, and move the main spider body.
    // From said body there should be 4 places from where foot positions are checked (2 for each direction)
    // A public enum with accepted directions should exist, which is the inputs accepted (Vertical and horizontal as options)
    // When the main body is within a zone, have that zone dictate the inputs
    // Movement direction needs to be based on the gravity fields the spider is stepping on

    private Rigidbody2D myRB;
    [SerializeField] private float moveSpeed = 5f;
    public bool isWalking = false;

    // A list of the gravity fields the spider finds itself in
    private List<GravityFieldSquare> fieldsImIn = new List<GravityFieldSquare>();

    private PlayerControls pControls;

    private void Awake()
    {
        pControls = new PlayerControls();
        myRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        pControls.Enable();
    }

    private void OnDisable()
    {
        pControls.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Adds this as a gravity field
        if (collision.CompareTag("GravityField"))
        {
            fieldsImIn.Add(collision.GetComponent<GravityFieldSquare>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Adds this as a gravity field
        if (collision.CompareTag("GravityField"))
        {
            fieldsImIn.Remove(collision.GetComponent<GravityFieldSquare>());
        }
    }

    // FixedUpdate is called once per physics iteration
    private void FixedUpdate()
    {
        // Gets the input value
        Vector2 targetVelocity = pControls.GroundMovement.Movement.ReadValue<Vector2>();

        bool useHori = false;
        bool useVerti = false;

        foreach (GravityFieldSquare gFSquare in fieldsImIn)
        {
            // If a direction is to be used, set it to true 
            if (gFSquare.horizontalInputs)
            {
                useHori = true;
            }
            else
            {
                useVerti = true;
            }

            // If both directions can be used, stop checking the rest of the colliding gravity fields
            if (useHori && useVerti)
            {
                break;
            }
        }

        // If one is to be ignored, remove it from the inputs
        if (!useHori)
        {
            targetVelocity = new Vector2(0f, targetVelocity.y);
        }

        if (!useVerti)
        {
            targetVelocity = new Vector2(targetVelocity.x, 0f);
        }

        // Sets isWalking to true if movement is > 0f
        isWalking = targetVelocity.magnitude > 0f ? true : false;

        // Normalises the movement vector
        targetVelocity.Normalize();

        // Sets the velocity to the movement speed * the input
        myRB.velocity = targetVelocity * moveSpeed;
    }
}
