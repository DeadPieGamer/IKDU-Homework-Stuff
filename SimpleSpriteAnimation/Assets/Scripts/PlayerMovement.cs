using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField, Tooltip("Speed at which the player moves")] private float moveSpeed = 5f;

    [Tooltip("What animates the player")] private Animator playerAnimator;
    [Tooltip("What makes the player interact with the physics system")] private Rigidbody2D playerRigidbody2D;

    [Tooltip("What registers inputs")] private PlayerControls controlsOfPlayer;

    [Tooltip("A Vector2 carrying the last direction")] private Vector2 lastInput = Vector2.down / 20f;

    // Awake is the very first thing called
    private void Awake()
    {
        controlsOfPlayer = new PlayerControls();
    }

    // Listen for inputs when enabled
    private void OnEnable()
    {
        controlsOfPlayer.Enable();
    }

    // Don't listen for inputs when disabled
    private void OnDisable()
    {
        controlsOfPlayer.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Gets the Animator and Rigidbody2D
        playerAnimator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called once per physics cycle
    private void FixedUpdate()
    {
        // Gets the input directions
        Vector2 targetDirection = controlsOfPlayer.BaseMap.Movement.ReadValue<Vector2>();

        // Normalises the input directions to avoid strafing
        targetDirection.Normalize();

        // Tells the rigidbody to move in the input direction with the moveSpeed
        playerRigidbody2D.velocity = targetDirection * moveSpeed;

        // If there is currently an input
        if (targetDirection != Vector2.zero)
        {
            // Tells the animator what direction the player is trying to move
            playerAnimator.SetFloat("HorizontalSpeed", targetDirection.x);
            playerAnimator.SetFloat("VerticalSpeed", targetDirection.y);

            // Remembers the input, but at 1/20th the length
            lastInput = targetDirection / 20f;
        }
        else // If there is no current input
        {
            // Tells the animator what direction the player last moved, but low enough for them to just stand idle
            playerAnimator.SetFloat("HorizontalSpeed", lastInput.x);
            playerAnimator.SetFloat("VerticalSpeed", lastInput.y);
        }
    }
}
