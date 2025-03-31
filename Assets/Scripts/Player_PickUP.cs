using UnityEngine;
using UnityEngine.UI;

public class Player_PickUP : MonoBehaviour
{
    public GameObject wrench;
    public GameObject screwdriver;
    public GameObject OpenSwitch;
    public Text itemsText;
    public Image image;
    public Transform flashlightSlot;
    public GameObject flashlight;

    private bool isHoldingFlashlight = false;
    public bool canPickUp;
    public int haswrench;
    public bool hasScrewdriver;

    //------------ dialogue system variables
    public bool canTalkwith;
    public GameObject npc1;
    public GameObject dialoguePanel;
    public DialogueManager dialogue;

    //------------ 交互图标 UI
    public GameObject interactIcon;

    //------------ 电路谜题 UI
    public GameObject puzzleUI;
    private bool canSolvePuzzle = false;
    private GameObject currentSwitch;

    //-----------------------audio
    public AudioSource pickSFX;
    public AudioSource talkSFX;
    void Start()
    {
        if (image != null) {
            image.gameObject.SetActive(false);
        }
        if (interactIcon != null)
        {
            interactIcon.SetActive(false);
        }
    }

    void Update()
    {
        canPick();
        canTalk();

        if (isHoldingFlashlight && flashlight != null)
        {
            flashlight.transform.position = flashlightSlot.position;
            flashlight.transform.rotation = flashlightSlot.rotation;
        }

        if (canSolvePuzzle && Input.GetKeyDown(KeyCode.E))
        {
            pickSFX.Play();
            TogglePuzzle();
        }
    }

    public void canTalk()
    {
        if (canTalkwith && Input.GetKeyDown(KeyCode.F))
        {
            dialoguePanel.SetActive(true);
            print("DIALOGUE ");
            talkSFX.Play();
        }
    }

    void canPick()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            pickSFX.Play();
            canPickUp = false;
            print("PICK UP");

            if (wrench != null && wrench.CompareTag("items1"))
            {
                Destroy(wrench);
                haswrench += 1;
                image.gameObject.SetActive(true);
                interactIcon.SetActive(false);
            }

            if (wrench != null && wrench.CompareTag("flashlight") && !isHoldingFlashlight)
            {
                TryPickupFlashlight();
            }

            if (wrench != null && wrench.CompareTag("Sd"))
            {
                Destroy(wrench);
                hasScrewdriver = true;
                interactIcon.SetActive(false);
          
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
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            Collider flashlightCollider = flashlight.GetComponent<Collider>();
            if (flashlightCollider != null)
            {
                flashlightCollider.enabled = false;
            }

            isHoldingFlashlight = true;

            if (interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("items1") || other.CompareTag("flashlight") || other.CompareTag("Sd"))
        {
            wrench = other.gameObject;
            canPickUp = true;

            if (interactIcon != null)
            {
                interactIcon.SetActive(true);
            }
        }

        if (other.CompareTag("npc1"))
        {
            npc1 = other.gameObject;
            canTalkwith = true;

            if (interactIcon != null)
            {
                interactIcon.SetActive(true);
            }
        }

        if (other.CompareTag("powerswitch"))
        {
            currentSwitch = other.gameObject;

           
            if (interactIcon != null)
            {
                interactIcon.SetActive(true);
            }

           
            if (haswrench > 0 && hasScrewdriver)
            {
                canSolvePuzzle = true;
            }
            else
            {
                canSolvePuzzle = false;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("items1") || other.CompareTag("flashlight") || other.CompareTag("Sd"))
        {
            canPickUp = false;
            wrench = null;

            if (!isHoldingFlashlight && interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
        }

        if (other.CompareTag("npc1"))
        {
            canTalkwith = false;
            npc1 = null;

            if (interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
        }

        if (other.CompareTag("powerswitch"))
        {
            canSolvePuzzle = false;
            currentSwitch = null;

            if (interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
        }
    }

    void TogglePuzzle()
    {
        bool isActive = puzzleUI.activeSelf;
        puzzleUI.SetActive(!isActive);

        if (isActive)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GetComponent<PlayerController>().enabled = true;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponent<PlayerController>().enabled = false;
        }

        Debug.Log("Puzzle UI 状态：" + puzzleUI.activeSelf);
    }
}
