using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    //public bool isDialogue = false;

    private void OnTriggerEnter(Collider other)
    {
        switch (transform.parent.tag)
        {
            case "npc1":
              
                DialogueManager.id = 1;
                break;
            case "npc2":
                DialogueManager.id = 2;
                break;

        }
        if (other.CompareTag("Player"))// && isDialogue == false
        {
           
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            //isDialogue = true;
        }
    }
}
