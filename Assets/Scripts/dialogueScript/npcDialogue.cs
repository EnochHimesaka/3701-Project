using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    private bool canTalk = false; // 是否可以交谈
    private bool hasTalked = false; // 防止重复对话
    public bool canTalkAgain = false; // 任务解锁后是否可以重新对话

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = true; // 进入范围，允许交谈
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false; // 离开范围，取消交谈权限
        }
    }

    private void Update()
    {
        // **确保只有在没有对话过，或者任务完成后，才能触发新对话**
        if (canTalk && !hasTalked && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            hasTalked = true; // 记录已经对话，防止重复
        }
    }

    public void UnlockNewDialogue()
    {
        Debug.Log("任务完成，NPC可以进行新对话！");
        canTalkAgain = true;
        hasTalked = false; // 允许新对话
    }
}
