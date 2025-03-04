using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    private bool canTalk = false; // 玩家是否可以交谈
    private bool hasTalked = false; // 是否已经对话过
    public bool canTalkAgain = false; // 任务解锁后可重新对话

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = true; // 允许交互
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false; // 玩家离开，取消交互
        }
    }

    private void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.E) && !hasTalked)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            hasTalked = true; // 标记已经对话
        }
    }

    public void UnlockNewDialogue()
    {
        canTalkAgain = true;
        hasTalked = false; // 允许新对话
    }
}
