using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearWhileQuest : MonoBehaviour
{
    public QuestSO quest;
    public GameObject gameobj;

    private void Update()
    {
        if (PartyController.instance == null)
            return;
        if (gameobj != null)
        {
            if (PartyController.quest != null && PartyController.quest.title == quest.title && PartyController.quest.isActive)   
                gameobj.SetActive(true);
            else
                gameobj.SetActive(false);
        }
        else
        {
            SpriteRenderer mySR = GetComponent<SpriteRenderer>();
            if (PartyController.quest != null && PartyController.quest.title == quest.title && PartyController.quest.isActive)
                mySR.enabled = true;
            else
                mySR.enabled = false;
        }
    }
}
