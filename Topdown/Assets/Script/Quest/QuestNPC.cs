using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : Interactable
{
    public QuestSO quest;
    public Dialogue whileQuestDialogue;
    public Dialogue defaultDialogue;
    public override void Interact()
    {
        base.Interact();
        if (CheckQuestManager.instance.CheckCompleteQuest(quest)) 
        {
            DialogueManager.instance.StartDialogue(defaultDialogue);
            return;
        }
        if (PartyController.quest != null && PartyController.quest.isActive && PartyController.quest.id == quest.id)
            DialogueManager.instance.StartDialogue(whileQuestDialogue);
        else if (!CheckQuestManager.instance.CheckAcceptQuest(quest)) 
        {
            QuestGiver qg = GetComponent<QuestGiver>();
            qg.OpenQuestWindow();
        }
    }
}
