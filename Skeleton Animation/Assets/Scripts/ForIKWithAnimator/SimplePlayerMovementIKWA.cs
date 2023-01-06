using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovementIKWA : MonoBehaviour
{
    [Header("Player stats")]
    [SerializeField, Tooltip("Speed at which player moves")] private float moveSpeed = 5f;

    [Tooltip("What animates the player")] private Animator playerAnimator;
    [Tooltip("What controls the player physics")] private Rigidbody2D playerRigidbody2D;
    [Tooltip("What registers inputs")] private PlayerControls controlsOfPlayer;

    private void Awake()
    {
        controlsOfPlayer = new PlayerControls();
    }

    private void OnEnable()
    {
        controlsOfPlayer.Enable();
    }

    private void OnDisable()
    {
        controlsOfPlayer.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Gets the animator and rigidbody
        playerAnimator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called once per physics cycle
    private void FixedUpdate()
    {
        // Gets the input as a float
        float inputValue = controlsOfPlayer.AvatarMovement.Walk.ReadValue<float>();

        // Gets movement direction and multiplies it by the movespeed, while also keeping vertical speed
        Vector2 targetMovement = new Vector2(inputValue * moveSpeed, playerRigidbody2D.velocity.y);

        // Tells the animator the movement info
        playerAnimator.SetFloat("moveSpeed", inputValue);

        // If the player is moving to the left, make them look that way
        if (inputValue < -0.1f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (inputValue > 0.1f) // If the player is moving to the right, make them look that way
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}
