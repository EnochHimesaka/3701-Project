using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorController : MonoBehaviour
{
    public GameObject door2;
    bool isOpened = false;
    public bool isBlueTriggered = false;
    public bool isRedTriggered = false;
    float x = -1.325f;
    float z = 0.25f;

    public void tryOpen()
    {
        if (isRedTriggered && isBlueTriggered)

            {
                Open();
            }

    }

    public void Open()
    {
        

            if (!isOpened)
            {
                isOpened = true;
                door2.transform.position += new Vector3(z, 0, x);
            }

    }

    public void ActivateRedSwitch()
    {
        isRedTriggered = true;
        tryOpen();
    }

    public void ActivateBlueSwitch()
    {
        isBlueTriggered = true;
        tryOpen();
    }

    public void DeactivateRedSwitch()
    {
        isRedTriggered = false;
        tryOpen();
        if (isOpened)
        {
            isOpened = false;
            door2.transform.position += new Vector3(-z, 0, -x);
        }
    }

    public void DeactivateBlueSwitch()
    {
        isBlueTriggered = false;
        tryOpen();
        if (isOpened)
        {
            isOpened = false;
            door2.transform.position += new Vector3(-z, 0, -x);
        }
    }



}