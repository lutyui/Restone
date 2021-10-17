using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject centerObject;

    private Vector3 initialRerativePosition;
    private float currentAngle;
    public float CurrentAngle
    {
        get
        {
            return currentAngle;
        }
        set
        {
            var relativePosition = Quaternion.Euler(new Vector3(0, value, 0)) * initialRerativePosition;
            transform.position = centerObject.transform.position + relativePosition;
            transform.LookAt(centerObject.transform.position);
            currentAngle = value;
        }
    }
    void Start()
    {
        initialRerativePosition = transform.position - centerObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentAngle += Input.GetAxisRaw("Horizontal");
    }
}
