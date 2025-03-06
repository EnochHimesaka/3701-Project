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

    //------------ 交互图标 UI
    public GameObject interactIcon; // 交互提示 UI（例如“按E交互”）

    //------------ 电路谜题 UI
    public GameObject puzzleUI;
    private bool canSolvePuzzle = false; // 是否可以解谜
    private GameObject currentSwitch; // 当前触发的 `powerswitch`

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
        canSolveCircuitPuzzle();
    }

    public void canTalk()
    {
        if (canTalkwith && Input.GetKeyDown(KeyCode.E))
        {
            dialoguePanel.SetActive(true);
            print("DIALOGUE ");
<<<<<<< HEAD
=======
            dialogue.DisplayNextSentence();
>>>>>>> 08ca700ce9ab9caaed22c857773c27a768b9de9e
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

    void canSolveCircuitPuzzle()
    {
        if (canSolvePuzzle && Input.GetKeyDown(KeyCode.E))
        {
            TogglePuzzle();
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

        if (other.CompareTag("powerswitch"))
        {
            canSolvePuzzle = true;
            currentSwitch = other.gameObject;

            // **显示交互 UI**
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

        if (other.CompareTag("powerswitch"))
        {
            canSolvePuzzle = false;
            currentSwitch = null;

            // **隐藏交互 UI**
            if (interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
        }
    }

    void TogglePuzzle()
    {
        if (puzzleUI == null)
        {
            Debug.LogError("Puzzle UI 没有绑定，请检查 Player_PickUP 组件！");
            return;
        }

        bool isActive = puzzleUI.activeSelf;
        puzzleUI.SetActive(!isActive);
        Time.timeScale = isActive ? 1 : 0; // 暂停或恢复游戏

        Debug.Log("Puzzle UI 状态：" + puzzleUI.activeSelf);
    }
}
