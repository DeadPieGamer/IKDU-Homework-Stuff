// Based upon the code by Ryan Hipple from "Unite 2017 - Game Architecture with Scriptable Objects", 10/04/17

using System;

[Serializable]
public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    public FloatReference()
    { }

    public FloatReference(float value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public float Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    // This piece of code allows me to write
    // ```cs
    // FloatReference myRef = new FloatReference();
    // float myFloat = myRef;
    // ```
    // Without the editor complaining
    /// <summary>
    /// Allows easy conversion from FloatReference to Float
    /// </summary>
    /// <param name="reference"></param>
    public static implicit operator float(FloatReference reference)
    {
        return reference.Value;
    }
}
