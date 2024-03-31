using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderTemp : MonoBehaviour
{
    public GameObject nextModel;
    public PlayerCam playerCam;
    public GameObject createdModel;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != null)
        {
            if (other.gameObject.tag == "Player")
            {
                
                if (playerCam.holdingObject != null && Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("11");
                    createdModel = Instantiate(nextModel, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
