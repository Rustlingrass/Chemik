using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetilTemporary : MonoBehaviour
{
    public GameObject sphere;

    private GameObject sphere1;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerCam playerCam = GetComponent<PlayerCam>();
            if (playerCam.holdingObject != null && Input.GetKeyDown(KeyCode.E))
            {
                sphere1 = Instantiate(sphere, this.transform.parent);
                sphere1.transform.localPosition = Vector3.zero;
                sphere1.transform.localRotation = Quaternion.identity;
                Destroy(this);
            }
        }
    }
}
