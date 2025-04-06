using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    private bool canTalk = false; // 是否可以交谈
    private bool hasTalked = false; // 防止重复对话
    public bool canTalkAgain = false; // 任务解锁后是否可以重新对话

    public GameObject interactHintUI; // ✅ 提示图标对象（可交互时显示）

    private void Start()
    {
        if (interactHintUI != null)
        {
            interactHintUI.SetActive(false); // 初始隐藏
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = true;

            if (interactHintUI != null && (!hasTalked || canTalkAgain))
            {
                interactHintUI.SetActive(true); // ✅ 显示交互提示
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false;

            if (interactHintUI != null)
            {
                interactHintUI.SetActive(false); // ✅ 离开时隐藏
            }
        }
    }

    private void Update()
    {
        // **确保只有在没有对话过，或者任务完成后，才能触发新对话**
        if (canTalk && !hasTalked && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            hasTalked = true;

            if (interactHintUI != null)
            {
                interactHintUI.SetActive(false); // ✅ 开始对话后隐藏提示
            }
        }
    }

    public void UnlockNewDialogue()
    {
        Debug.Log("任务完成，NPC可以进行新对话！");
        canTalkAgain = true;
        hasTalked = false;
    }
}
