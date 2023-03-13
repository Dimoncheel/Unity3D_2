using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject character;
    Vector3 camOffset;
    private float camAngleH;
    private float camAngleH0;
    private float charAngleH0;
    private float camAngleV;
    private float camAngleV0;
    private float charAngleV0;
    private float camSensX=2;
    private float camSensY=2;

    void Start()
    {
        character = GameObject.Find("Character");
        camOffset= this.transform.position-character.transform.position;
        camAngleV0=camAngleV = this.transform.eulerAngles.x;
        camAngleH0=camAngleH= this.transform.eulerAngles.y;

        charAngleV0 = character.transform.eulerAngles.x;
        charAngleH0 = character.transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");


        camAngleH += mx*camSensX;
        camAngleV -= my*camSensY;
        if (camAngleV > 360)
        {
            camAngleV -= 360;
        }
        else if (camAngleV < 0)
        {
            camAngleV += 360;
        }

     
        Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel")*3.0f;
        
       
    }
    private void LateUpdate()
    {
        this.transform.position = character.transform.position + Quaternion.Euler(0, camAngleH-camAngleH0, 0)*camOffset;
        this.transform.eulerAngles = new Vector3(camAngleV, camAngleH, 0);
        if (Input.GetMouseButton(0))
        {
            
        }
        else if (Input.GetMouseButton(2))
        {

        }
        else
        {
            character.transform.eulerAngles=new Vector3(0, charAngleH0+(camAngleH-camAngleH0), 0);
        }
       
    }
}
