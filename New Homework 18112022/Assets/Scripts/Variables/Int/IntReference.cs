// Based upon the code by Ryan Hipple from "Unite 2017 - Game Architecture with Scriptable Objects", 10/04/17

using System;

[Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntVariable Variable;

    public IntReference()
    { }

    public IntReference(int value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public int Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    // This piece of code allows me to write
    // ```cs
    // IntReference myRef = new IntReference();
    // int myInt = myRef;
    // ```
    // Without the editor complaining
    /// <summary>
    /// Allows easy conversion from IntReference to int
    /// </summary>
    /// <param name="reference"></param>
    public static implicit operator int(IntReference reference)
    {
        return reference.Value;
    }
}
