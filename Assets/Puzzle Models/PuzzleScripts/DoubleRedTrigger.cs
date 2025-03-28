using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoubleRedTrigger : MonoBehaviour
{
    public GameObject door3;
    public DoorController doorController;

    private void OnTriggerEnter(Collider other3)
    {
        if (other3.transform.tag == "redcube")

            {

            doorController.ActivateRedSwitch();
        }

    }

    private void OnTriggerExit(Collider other3)
    {
        if (other3.transform.tag == "redcube")

        {

            doorController.DeactivateRedSwitch();
        }

    }

}