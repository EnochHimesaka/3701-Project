using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoubleBlueTrigger : MonoBehaviour
{
    public GameObject door2;
    
    public DoorController doorController;


    public void OnTriggerEnter(Collider other2)
    {
        if (other2.transform.tag == "bluecube")
       
            {
                doorController.ActivateBlueSwitch();
           }
        
    }

    public void OnTriggerExit(Collider other2)
    {
        if (other2.transform.tag == "bluecube")

        {
            doorController.DeactivateBlueSwitch();
        }

    }

}
