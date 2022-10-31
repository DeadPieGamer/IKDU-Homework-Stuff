using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField, Tooltip("Speed at which the player will move")] private float moveSpeed = 5f;

    private Vector3 v3MoveInput;

    private CubeControls cubeControls;
    private Rigidbody myRb;

    private PlayerLevelTracker expTracker;
    [Header("Miscellaneous")]
    [SerializeField, Tooltip("EXP gain per input")] private int expToGain = 1;

    private void Awake()
    {
        cubeControls = new CubeControls();
        myRb = GetComponent<Rigidbody>();
        expTracker = GetComponent<PlayerLevelTracker>();
    }

    private void OnEnable()
    {
        cubeControls.Enable();
    }

    private void OnDisable()
    {
        cubeControls.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Calls the GainEXP function from PlayerLevelTracker whenever the GainEXP button is pressed
        cubeControls.Air_Map.GainEXP.performed += _ => expTracker.GainEXP(expToGain);

        // Calls the LevelUp function from PlayerLevelTracker whenever the LevelUp button is pressed
        cubeControls.Air_Map.LevelUp.performed += _ => expTracker.LevelUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called once per physics iteration
    private void FixedUpdate()
    {
        v3MoveInput = cubeControls.Air_Map.Movement.ReadValue<Vector3>();
        myRb.velocity = v3MoveInput * moveSpeed;
    }
}
