using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToCompleteQuestRemoveItem : TalkToCompleteQuest
{
    public ItemSO itemCompeleteRemove;
    public override void Interact()
    {
        base.Interact();
        PartyController.inventoryG.Remove(itemCompeleteRemove, true);
    }
}
