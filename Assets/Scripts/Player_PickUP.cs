using UnityEngine;
using UnityEngine.UI;

public class Player_PickUP : MonoBehaviour
{
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
    public GameObject npc1;
    public GameObject dialoguePanel;
    public DialogueManager dialogue;

    //------------ 交互图标 UI（新增）
    public GameObject interactIcon; // 绑定交互图标（例如“按E交互”UI）

    void Start()
    {
        image.gameObject.SetActive(false);

        // **初始隐藏交互图标**
        if (interactIcon != null)
        {
            interactIcon.SetActive(false);
        }
    }

    void Update()
    {
        canPick();
        canTalk();
    }

    public void canTalk()
    {
        if (canTalkwith && Input.GetKeyDown(KeyCode.E))
        {
            dialoguePanel.SetActive(true);
            print("DIALOGUE ");

            dialogue.DisplayNextSentence();

        }
    }

    void canPick()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            canPickUp = false;
            print("PICK UP");

            if (wrench != null && wrench.CompareTag("items1"))
            {
                Destroy(wrench);
                haswrench += 1;
                image.gameObject.SetActive(true);
            }

            if (wrench != null && wrench.CompareTag("flashlight") && !isHoldingFlashlight)
            {
                TryPickupFlashlight();
            }
        }
    }

    void TryPickupFlashlight()
    {
        if (flashlight != null)
        {
            flashlight.transform.SetParent(flashlightSlot);
            flashlight.transform.localPosition = Vector3.zero;
            flashlight.transform.localRotation = Quaternion.identity;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("items1") || other.CompareTag("flashlight"))
        {
            wrench = other.gameObject;
            canPickUp = true;

            // **进入拾取范围时，显示交互图标**
            if (interactIcon != null)
            {
                interactIcon.SetActive(true);
            }
        }

        if (other.CompareTag("npc1"))
        {
            npc1 = other.gameObject;
            canTalkwith = true;

            // **进入 NPC 交互范围时，显示交互图标**
            if (interactIcon != null)
            {
                interactIcon.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("items1") || other.CompareTag("flashlight"))
        {
            wrench = null;
            canPickUp = false;

            // **离开拾取范围时，隐藏交互图标**
            if (interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
        }

        if (other.CompareTag("npc1"))
        {
            npc1 = null;
            canTalkwith = false;

            // **离开 NPC 交互范围时，隐藏交互图标**
            if (interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
        }
    }
}