using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //outlining variables
    Outline currentOutline;
    bool outlinable;

    public float sensX;
    public float sensY;
    public Transform orientation;
    float xRotation;
    float yRotation;

    [Header("Hold Object")]
    public Transform objectHolder;
    public LayerMask isPickable;
    public LayerMask isPuttable;
    public static GameObject holdingObject = null;
    Rigidbody holdingrb;
    bool holding;
    bool interacting;


    //Raycasting in physics
    Ray ray;
    RaycastHit looking, hitPut;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        interacting = Physics.Raycast(ray, out looking, 10, isPickable);
        ray = new Ray(transform.position, transform.forward);

        //Camera movements
        RotateCamera();
        
        //Picking up all interactable objects
        PickUp();

        //interaction with a place to put
        if (holding)
        {
            PutHolding();
        }
        else if (!holding)
        {
            PickPutted();
        }

        //Outlining interactable objects
        if (interacting && looking.transform.gameObject.TryGetComponent<Outline>(out currentOutline))
        {
            currentOutline.enabled = true;
        } 
        else if (!interacting && currentOutline != null)
        {
            currentOutline.enabled = false;
        }

        //Debug in editor
        DebuggingRay();
    }
    private void PickUp()
    {
        if (interacting && Input.GetKeyDown(KeyCode.E) && !holding)
        {
            holdingObject = looking.transform.gameObject;
            holdingObject.transform.SetParent(objectHolder);
            holdingObject.transform.localPosition = Vector3.zero;
            holdingObject.transform.rotation = Quaternion.identity;
            holdingrb = holdingObject.GetComponent<Rigidbody>();
            
            //changing holding objects's rigidbody
            holdingrb.isKinematic = true;
            holdingObject.GetComponent<Collider>().enabled = false;
            holding = true;

        }
        else if (holding && Input.GetKeyDown(KeyCode.Q)) 
        {
            //Drop the object
            holdingObject.GetComponent<Collider>().enabled = true;
            holdingrb.isKinematic = false;
            holdingObject.transform.SetParent(null);
            holding = false;
            holdingObject = null;
        }
    }
    private void PutHolding()
    {
        if (Physics.Raycast(ray, out hitPut, 10, isPuttable) && holdingObject != null && Input.GetKeyDown(KeyCode.E))
        {
            holdingObject.transform.SetParent(hitPut.transform);
            holdingObject.GetComponent<Collider>().enabled = true;
            holdingrb = holdingObject.gameObject.GetComponent<Rigidbody>();
            holdingrb.isKinematic = true;

            holdingObject.transform.localPosition = Vector3.zero;
            holdingObject.transform.rotation = Quaternion.identity;
            
            holdingObject = null;
            holding = false;
        }
    }
    private void PickPutted()
    {
        if (Physics.Raycast(ray, out hitPut, 10, isPuttable) && holdingObject == null && Input.GetKeyDown(KeyCode.E))
        {
            holdingObject = hitPut.transform.GetChild(0).gameObject;
            holdingObject.transform.SetParent(objectHolder.transform);
            
            holdingrb = holdingObject.gameObject.GetComponent<Rigidbody>();
            holdingrb.isKinematic = true;
            holdingObject.GetComponent<Collider>().enabled = false;

            holdingObject.transform.localPosition = Vector3.zero;
            holdingObject.transform.rotation = Quaternion.identity;
            
            holding = true;
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
    public bool CheckingInteractable() 
    {
        if (Physics.Raycast(ray, out looking,  10, isPickable)) return true;
        else return false;
    }
}
