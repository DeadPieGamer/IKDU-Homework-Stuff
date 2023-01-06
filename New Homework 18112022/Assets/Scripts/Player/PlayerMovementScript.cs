using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Base stats")]
    [SerializeField, Tooltip("Speed at which the player moves")] private FloatReference moveSpeed;
    [SerializeField, Tooltip("Max amount of health player has")] private IntReference maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
