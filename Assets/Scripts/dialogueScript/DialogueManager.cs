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
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        if(id == 1)
        {
            //print("id = 1");
            nameText.text = "Noa";

        }
    }


    public void EndDialogue()
    {
 
        if (id == 1)
        {
            DialoguePanel.SetActive(false);
        }

        
    } 
}
