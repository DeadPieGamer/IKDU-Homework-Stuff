// Based upon the code by Ryan Hipple from "Unite 2017 - Game Architecture with Scriptable Objects", 10/04/17

using UnityEngine;

[CreateAssetMenu (menuName = "Variables/Float Variable")]
public class FloatVariable : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, Multiline, Tooltip("A description of this variable and its uses, only for Developers to read")] public string DeveloperDescription = "";
#endif

    public float Value;

    public void SetValue(float value)
    {
        Value = value;
    }

    public void SetValue(FloatVariable value)
    {
        Value = value.Value;
    }

    public void ApplyChange(float amount)
    {
        Value += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        Value += amount.Value;
    }
}
