using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static int id = 0;
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public GameObject DialoguePanel;

    public GameObject choicePanel;
    public Button choiceButton1;
    public Button choiceButton2;

    public float typingSpeed = 0.05f;
    public float sentenceDelay = 1.5f;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool isDialogueActive = false;


    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        sentences = new Queue<string>();
        id = 0;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (isDialogueActive) return;

        isDialogueActive = true;
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DialoguePanel.SetActive(true);
        Debug.Log("开始对话：" + dialogue.name);

        StartCoroutine(WaitForFirstInput());
    }

    IEnumerator WaitForFirstInput()
    {
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        // 设置名字切换（你可以自由扩展）
        if (id == 1)
        {
            nameText.text = "Noa";
        }

        // 触发选项（句子以 "CHOICE:" 开头）
        if (sentence.StartsWith("CHOICE:"))
        {
            ShowChoices(sentence.Substring(7)); // 去掉 "CHOICE:"，只显示文本
            return;
        }

        StartCoroutine(DisplayNextSentenceAuto(sentence));
        id++;
    }

    IEnumerator DisplayNextSentenceAuto(string sentence)
    {
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
        yield return typingCoroutine;
        yield return new WaitForSeconds(sentenceDelay);

        if (sentences.Count > 0)
        {
            DisplayNextSentence();
            choicePanel.SetActive(false);
        }
        else
        {
            EndDialogue();
            choicePanel.SetActive(false);
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

        DialoguePanel.SetActive(false);
        isDialogueActive = false;
        choicePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ShowChoices(string question)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        choicePanel.SetActive(true);

        dialogueText.text = question;

        // 设置两个示例选项（你可以自定义扩展）
        choiceButton1.GetComponentInChildren<Text>().text = "选项一";
        choiceButton2.GetComponentInChildren<Text>().text = "选项二";

        choiceButton1.onClick.RemoveAllListeners();
        choiceButton2.onClick.RemoveAllListeners();

        choiceButton1.onClick.AddListener(() => ChooseOption(1));
        choiceButton2.onClick.AddListener(() => ChooseOption(2));
    }

    void ChooseOption(int option)
    {
       
        choicePanel.SetActive(false);
        choicePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;



        // 继续播放后续句子
        DisplayNextSentence();
    }
}
