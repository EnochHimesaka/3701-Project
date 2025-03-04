using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static int id = 0;
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public GameObject DialoguePanel;

    public float typingSpeed = 0.05f;  // 打字速度
    public float sentenceDelay = 1.5f; // 句子播放完后等待时间
    private Coroutine typingCoroutine;
    private bool isTyping = false;  // 是否正在打字
    private bool isDialogueActive = false; // 对话是否进行中

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        sentences = new Queue<string>();
        id = 0;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (isDialogueActive) return; // 防止重复触发对话

        isDialogueActive = true;
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DialoguePanel.SetActive(true);
        Debug.Log("开始对话：" + dialogue.name);

        // **等待玩家按 E 触发第一句话**
        StartCoroutine(WaitForFirstInput());
    }

    IEnumerator WaitForFirstInput()
    {
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null; // 等待玩家按 E
        }

        DisplayNextSentence(); // 触发第一句
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        if (id == 1)
        {
            nameText.text = "Noa";
        }

        // **第一句话之后，自动播放剩下的句子**
        StartCoroutine(DisplayNextSentenceAuto(sentence));
    }

    IEnumerator DisplayNextSentenceAuto(string sentence)
    {
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
        yield return typingCoroutine; // 等待当前句子播放完
        yield return new WaitForSeconds(sentenceDelay); // 额外等待时间

        if (sentences.Count > 0)
        {
            DisplayNextSentence(); // 自动播放下一句
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        Debug.Log("对话结束，隐藏对话框");
        DialoguePanel.SetActive(false);
        isDialogueActive = false;
    }
}
