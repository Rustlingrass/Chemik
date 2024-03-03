using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    [Header("Hold Object")]
    public Transform objectHolder;
    public LayerMask isPickable;
    GameObject holdingObject = null;
    Rigidbody holdingrb;
    bool holding;
    bool interacting;

    float xRotation;
    float yRotation;
    Ray ray;
    RaycastHit hit;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        interacting = Physics.Raycast(ray,out hit, 5, isPickable);
        ray = new Ray(transform.position, transform.forward);
        RotateCamera();
        PickUp();
        DebuggingRay();
    }
    private void PickUp()
    {
        if (interacting && Input.GetKeyDown(KeyCode.E) && !holding)
        {
            holdingObject = Instantiate(hit.transform.gameObject, objectHolder);
            holdingObject.transform.SetParent(objectHolder);
            holdingObject.transform.localPosition = Vector3.zero;
            holdingObject.transform.rotation = Quaternion.identity;
            holdingrb = holdingObject.GetComponent<Rigidbody>();
            //changing holding objects's rigidbody
            holdingrb.isKinematic = true;
            holding = true;

            //Destroy the previous object
            Destroy(hit.transform.gameObject);

        }
        else if (holding && Input.GetKeyDown(KeyCode.E)) 
        {
            //Drop the object
            holdingrb.isKinematic = false;
            holdingObject.transform.SetParent(null);
            holding = false;
        }
    }
    private void RotateCamera()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
    }
    private void DebuggingRay()
    {
        if (interacting)
        {
            Debug.DrawRay(transform.position, transform.forward * 4, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 4, Color.red);
        }
    }
}
