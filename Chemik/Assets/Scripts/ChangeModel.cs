using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModel : MonoBehaviour
{
    public GameObject keyModel;
    public GameObject nextModel;
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
        
        if (other.gameObject != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                if (PlayerCam.holdingObject != null)
                {
                    Debug.Log("1");
                    if (PlayerCam.holdingObject.name == keyModel.name && Input.GetKeyDown(KeyCode.F))
                    {
                        Debug.Log("2");
                        createdModel = Instantiate(nextModel, transform.position, Quaternion.identity);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
