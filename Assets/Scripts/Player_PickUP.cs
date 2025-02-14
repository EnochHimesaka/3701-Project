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

    //------------ Crosshair UI
    public Image crosshair;  // 绑定准星 UI
    public Color defaultColor = new Color(1f, 1f, 1f, 0.1f); // 透明白色（默认状态）
    public Color highlightColor = Color.red; // 交互时的颜色

    void Start()
    {
        image.gameObject.SetActive(false);
        if (crosshair != null)
        {
            SetCrosshairColor(defaultColor); // 确保准星初始颜色
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
            SetCrosshairColor(highlightColor); // 进入可拾取范围，准星变色
        }

        if (other.CompareTag("npc1"))
        {
            npc1 = other.gameObject;
            canTalkwith = true;
            SetCrosshairColor(highlightColor); // 进入 NPC 交互范围，准星变色
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("items1") || other.CompareTag("flashlight"))
        {
            wrench = null;
            canPickUp = false;
            SetCrosshairColor(defaultColor); // 离开拾取范围，准星恢复
        }
        if (other.CompareTag("npc1"))
        {
            npc1 = null;
            canTalkwith = false;
            SetCrosshairColor(defaultColor); // 离开 NPC 交互范围，准星恢复
        }
    }

    // 统一修改准星颜色的方法
    private void SetCrosshairColor(Color color)
    {
        if (crosshair != null)
        {
            crosshair.color = color;
        }
    }
}
