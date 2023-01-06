// Based upon the code by Ryan Hipple from "Unite 2017 - Game Architecture with Scriptable Objects", 10/04/17

using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int Variable")]
public class IntVariable : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, Multiline, Tooltip("A description of this variable and its uses, only for Developers to read")] public string DeveloperDescription = "";
#endif

    public int Value;

    public void SetValue(int value)
    {
        Value = value;
    }

    public void SetValue(IntVariable value)
    {
        Value = value.Value;
    }

    public void ApplyChange(int amount)
    {
        Value += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        Value += amount.Value;
    }
}
