using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHints : MonoBehaviour
{
    public GameObject FKeyHint;
    public static GameObject self;

    private void Start()
    {
        self = FKeyHint.gameObject;
    }
    public void ShowF() 
    {
        //self = FKeyHint.gameObject;
        self.SetActive(true);
    }
    public void HideF() 
    {
        //self = FKeyHint.gameObject;
        self.SetActive(false);
    }


}
