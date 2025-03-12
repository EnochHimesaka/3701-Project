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


        if (canSolvePuzzle && Input.GetKeyDown(KeyCode.E))
        {
            TogglePuzzle();
        }
    }


    public void canTalk()
    {
        if (canTalkwith && Input.GetKeyDown(KeyCode.E))
        {
            dialoguePanel.SetActive(true);
            print("DIALOGUE ");
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
                interactIcon.SetActive(false);
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
            // 取消父级，防止抖动
            flashlight.transform.SetParent(null);

            // 让手电筒跟随手部位置
            flashlight.transform.position = flashlightSlot.position;
            flashlight.transform.rotation = flashlightSlot.rotation;

            // 关闭物理影响
            Rigidbody rb = flashlight.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // 使其静止
            }

            isHoldingFlashlight = true;

            // **禁用手电筒的碰撞器，防止触发拾取逻辑**
            Collider flashlightCollider = flashlight.GetComponent<Collider>();
            if (flashlightCollider != null)
            {
                flashlightCollider.enabled = false;
            }

            // **隐藏交互 UI**
            if (interactIcon != null)
            {
                interactIcon.SetActive(false);
            }

            Debug.Log("手电筒已拾取");
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
        // 🛠 **通用退出逻辑**
        if (other.CompareTag("items1") || other.CompareTag("flashlight"))
        {
            canPickUp = false;
            wrench = null;  // 取消当前物品的引用

            // **隐藏交互 UI**
            if (!isHoldingFlashlight && interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
        }

        if (other.CompareTag("npc1"))
        {
            canTalkwith = false;
            npc1 = null;  // 取消 NPC 的引用

            // **隐藏交互 UI**
            if (interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
        }

        if (other.CompareTag("powerswitch"))
        {
            canSolvePuzzle = false;
            currentSwitch = null; // 取消开关的引用


            // **隐藏交互 UI**
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
            // 关闭 UI，恢复游戏 & 锁定鼠标 & 重新启用角色控制
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // 重新启用玩家移动和视角
            GetComponent<PlayerController>().enabled = true;
        }
        else
        {
            // 打开 UI，暂停游戏 & 释放鼠标 & 停止角色控制
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 禁用玩家移动和视角控制
            GetComponent<PlayerController>().enabled = false;
        }

        Debug.Log("Puzzle UI 状态：" + puzzleUI.activeSelf);
    }

}
