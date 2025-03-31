using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleRedTrigger : MonoBehaviour
{
    public GameObject door;
    bool isOpened = false;
    float x = 1.325f;
    float z = -0.25f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "redcube")
       
            if (!isOpened)
            {
                isOpened = true;
                door.transform.position += new Vector3(x, 0, z);
            }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "redcube")

            if (isOpened)
            {
                isOpened = false;
                door.transform.position += new Vector3(-x, 0, -z);
            }

    }

}
