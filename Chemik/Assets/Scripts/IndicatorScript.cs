using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
/*    public GameObject manager;
    public GameObject keyModel;
    public GameObject nextModel;
    public GameObject createdModel;


    public static Outline thisOutline = null;


    private static InteractionHints intHints;

    private void Start()
    {
        thisOutline = GetComponent<Outline>();
        intHints = manager.GetComponent<InteractionHints>();
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
                            Debug.Log("dfasadasdsad");
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
                            Destroy(PlayerCam.holdingObject);
                            PlayerCam.holdingObject = Instantiate(nextModel, PlayerCam.objectHolder.position, Quaternion.Euler(0,0,90));
                            PlayerCam.holdingObject.transform.SetParent(PlayerCam.objectHolder);
                            PlayerCam.holdingObject.transform.localScale = new Vector3(3,3,3);
                            PlayerCam.holdingObject.transform.localRotation = new Quaternion(0,0,90,0);
                            PlayerCam.OnIndicator();
                            //createdModel.transform.SetParent(PlayerCam.holdingObject.transform, true);
                            //createdModel.transform.localPosition = transform.localPosition;
                            //Destroy(PlayerCam.holdingObject);
                        }
                    }
                }
            }
        }
    }*/
}
