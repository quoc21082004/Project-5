using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : Interactable
{
    public override void Interact()
    {
        base.Interact();
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
