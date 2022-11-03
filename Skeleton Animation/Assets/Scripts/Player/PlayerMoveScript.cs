using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private Rigidbody2D myRB;
    [SerializeField] private float moveSpeed = 5f;
    public bool isWalking = false;

    private PlayerControls pControls;

    /*
    private bool isLettingGo = false;
    private bool isFalling = false;
    [SerializeField, Tooltip("Used to disable and enable the leg animators when falling")] private List<LegAnimator> legAnimators = new List<LegAnimator>();
    [SerializeField, Tooltip("Used to disable and enable the body animator when falling")] private BodyAnimator mainBodyAnimator;
    */

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

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
        myRB.velocity = targetVelocity.x * moveSpeed * transform.right;
    }

    /*
    // Start is called before the first frame update
    private void Start()
    {
        pControls.GroundMovement.LetGo.started += _ => isLettingGo = true;
        pControls.GroundMovement.LetGo.canceled += _ => isLettingGo = false;
    }

    // Update is called once per frame
    private void Update()
    {
        /*
        if (isLettingGo)
        {
            if (!isFalling)
            {
                LetGoOfSurface(false);
            }
        }
        else
        {
            if (isFalling)
            {
                LetGoOfSurface(true);
            }
        }
        /
    }

    private void LetGoOfSurface(bool landing)
    {
        // Disables the animator scripts, so that the spider will freefall
        mainBodyAnimator.enabled = landing;
        foreach(LegAnimator leg in legAnimators)
        {
            leg.enabled = landing;
        }

        // Enables or disables gravity
        myRB.gravityScale = landing ? 0f : 1f;

        isFalling = !landing;
    }
    */
}
