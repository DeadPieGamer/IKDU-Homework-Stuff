using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pie.Math;

public class SpiderRotater : MonoBehaviour
{
    [SerializeField, Tooltip("The objects that should get rotated to fit feet positions")] private List<Transform> objToRot = new List<Transform>();
    [SerializeField, Tooltip("Current feet positions")] private List<Transform> actualFeetTips = new List<Transform>();
    [SerializeField, Tooltip("The body of the spider")] private Transform spiderBody;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke("RotateObjectToFeet", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        RotateObjectToFeet();
    }

    private void RotateObjectToFeet()
    {
        Quaternion targetRotation = MyMaths.rotationFromPoints(actualFeetTips, spiderBody);

        foreach (Transform objectsToRotate in objToRot)
        {
            objectsToRotate.rotation = targetRotation;
        }
    }
}
