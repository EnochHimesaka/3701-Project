using UnityEngine;
using UnityEngine.UI;

public class Player_PickUP : MonoBehaviour
{
   
    public GameObject Pickup_Box;
    public GameObject wrench;
    public GameObject OpenSwitch;
    public Text itemsText;
    public Image image;
 
    public Transform flashlightSlot; 
    public GameObject flashlight;


    private bool isHoldingFlashlight = false;
    public bool canPickUp;
    public int haswrench;

    //------------ dialogue system variables
    public bool canTalkwith;
    public GameObject convensationBox;
    public GameObject npc1;
    public GameObject dialoguePanel;
    public DialogueManager dialogue;


    //public int itemsNum; // 替换为图片


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        canPick();
        Switch();

        canTalk();
    }

    public void canTalk()
    {
        if(canTalkwith == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialoguePanel.SetActive(true);
                print("DIALOGUE ");
                dialogue.DisplayNextSentence();

            }
        }

        if(canTalkwith == false)
        {
            convensationBox.SetActive(false);
        }

        if (canTalkwith == true)
        {
            convensationBox.SetActive(true);
        }
    }

    void canPick()
    {
        if(canPickUp == true)
        {
            Pickup_Box.SetActive(true);
        }
        else
        {
            Pickup_Box.SetActive(false);
        }

        if(canPickUp == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                canPickUp = false;
                print("PICK UP");
                if (wrench != null && wrench.CompareTag("items1"))
                {
                    
                    Destroy(wrench);
                    haswrench += 1;
                    image.gameObject.SetActive(true);
                    //itemsText.text = " ITEM O: " + itemsNum;
                }

                if (wrench != null && wrench.CompareTag("flashlight") && !isHoldingFlashlight)
                {
                    TryPickupFlashlight();
                }


            }
        }
    }


    void TryPickupFlashlight()
    {
        if (flashlight != null)
        {
            // flashlight
            flashlight.transform.SetParent(flashlightSlot);

            // postion
            flashlight.transform.localPosition = Vector3.zero;
            flashlight.transform.localRotation = Quaternion.identity;

            // ban rgbody
            Rigidbody rb = flashlight.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            isHoldingFlashlight = true;
        }
    }

    void Switch()
    {
        //Pickup_Box.gameObject.SetActive(true);

       // if(haswrench == 1) image.gameObject.SetActive(false); haswrench = 0;
        

     

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "items1" || other.tag == "flashlight")
        {
            wrench = other.gameObject;
            canPickUp = true;
        }

        if(other.tag == "npc1")
        {
            npc1 = other.gameObject;
            canTalkwith = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "items1" || other.tag == "flashlight")
        {
            wrench = null;
            canPickUp = false;
        }
        if (other.tag == "npc1")
        {
            npc1 = null;
            canTalkwith = false;
        }
    }


}
