using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialInputCheck : MonoBehaviour
{
    private CubeControls cubeControls;
    private PlayerFollow playerFollowScript;

    private void Awake()
    {
        cubeControls = new CubeControls();
        playerFollowScript = GetComponent<PlayerFollow>();
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
    void Start()
    {
        cubeControls.Air_Map.AnyInput.started += _ => StartCam();
    }

    private void StartCam()
    {
        playerFollowScript.enabled = true;
        this.enabled = false;
    }
}
