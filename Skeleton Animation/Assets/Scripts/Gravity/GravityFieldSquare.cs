using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldSquare : MonoBehaviour
{
    [HideInInspector] public enum PossibleDirections { down, right, up, left };

    [Header("Gravity field variables")]
    [SerializeField, Tooltip("How high priority this gravity field is. Higher # = Used before others")] public int priority = 0;
    [SerializeField, Tooltip("What type of inputs does this gravity use")] public bool horizontalInputs = true;
    [SerializeField, Tooltip("What direction does this gravity point")] public PossibleDirections whatDirection = PossibleDirections.down;
    [HideInInspector] public Quaternion myRotation;

    private void Awake()
    {
        myRotation = transform.rotation;
    }
}
