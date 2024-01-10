using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
    public void TriggerDialogue(Action nextAction = null)
    {
        DialogueManager.instance.StartDialogue(dialogue, nextAction);
    }
}
