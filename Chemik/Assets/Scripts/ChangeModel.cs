using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeModel : MonoBehaviour
{
    public GameObject manager;
    public GameObject keyModel;
    public GameObject nextModel;
    public GameObject createdModel;


    public static Outline thisOutline = null;
    
    
    private static InteractionHints intHints;
    void Start()
    {
        thisOutline = GetComponent<Outline>();
        intHints = manager.GetComponent<InteractionHints>();
    }
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                if (PlayerCam.holdingObject != null)
                {
                    if (PlayerCam.holdingObject.name == keyModel.name)
                    {
                        thisOutline.enabled = true;
                        intHints.ShowF();
                        
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            if (nextModel.TryGetComponent<Outline>(out _))
                            {
                                thisOutline = nextModel.GetComponent<Outline>();
                            }
                            thisOutline.enabled = false;
                            
                            //interaction hint
                            intHints.HideF();

                            //changing the task message
                            intHints.ChangeTaskMessage();

                            //changing the model
                            createdModel = Instantiate(nextModel, transform.position, Quaternion.identity);
                            createdModel.transform.SetParent(transform.parent, true);
                            //createdModel.transform.localPosition = transform.localPosition;
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
        }
    }
}
