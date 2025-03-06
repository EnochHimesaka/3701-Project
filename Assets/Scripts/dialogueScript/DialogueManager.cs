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

    public float typingSpeed = 0.05f;  // 自定义文本显示速度
    private Coroutine typingCoroutine;
    private bool isTyping = false;  // 用于检测是否正在打字

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

    }

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        sentences = new Queue<string>();
        id = 0;
    }  

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            // 如果正在打字，直接跳过，显示完整句子
            StopCoroutine(typingCoroutine);
            dialogueText.text = sentences.Peek(); // 显示完整的当前句子
            isTyping = false;
            return;
        }

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

        // 开始打字机效果
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
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
        if (id == 1)
        {
            DialoguePanel.SetActive(false);
        }
    }
}
