using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float sensX;
    public float sensY;
    
    public Transform orientation;

    private float _xRotation;
    private float _yRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
        
        // Calculate rotation
        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        // Rotate camera
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        orientation.localRotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
