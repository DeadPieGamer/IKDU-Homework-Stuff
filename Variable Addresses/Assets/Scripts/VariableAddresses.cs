using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableAddresses : MonoBehaviour
{
    [SerializeField] private int currentAge = 19;

    [SerializeField, Multiline] private string testString = "A message with number #, which should be 19";


    // Start is called before the first frame update
    void Start()
    {
        // $ before string to allow for variables to be included within squiggly brackets
        string stringToPrint = "";
        for (int i = 0; i < testString.Length; i++)
        {
            if (testString[i] == '#')
            {
                stringToPrint += currentAge.ToString();
            }
            else
            {
                stringToPrint += testString[i];
            }
        }

        ComputeAge(currentAge, 5);

        Debug.Log(stringToPrint);
        // It is possible to say the variable address instead of the name.
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ComputeAge(int a, int b)
    {
        Debug.Log(a + b);
    }
}
