using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private Rigidbody2D myRB;
    [SerializeField] private float moveSpeed = 5f;
    public bool isWalking = false;

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

    // Try to make walls walkable?

    // FixedUpdate is called once per physics iteration
    private void FixedUpdate()
    {
        // Gets the input value
        Vector2 targetVelocity = pControls.GroundMovement.Movement.ReadValue<Vector2>();
        // Isolates the x-movement
        targetVelocity = new Vector2(targetVelocity.x, 0f);
        // Isolates the y-movement
        // targetVelocity = new Vector2(0f, targetVelocity.y);
        // Sets isWalking to true if movement is > 0f
        isWalking = targetVelocity.magnitude > 0f ? true : false;
        // Sets the velocity to the movement speed * the input
        myRB.velocity = targetVelocity * moveSpeed;
    }
}
