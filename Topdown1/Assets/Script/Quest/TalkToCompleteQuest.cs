using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToCompleteQuest : Interactable
{
    public QuestSO questinfo;
    public GameObject questCompleteprefab;
    private GameObject instantiateprefab;
    private void Update()
    {
        if (instantiateprefab == null && !CheckQuestManager.instance.CheckCompleteQuest(questinfo))
            instantiateprefab = Instantiate(questCompleteprefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        else if (instantiateprefab != null && CheckQuestManager.instance.CheckCompleteQuest(questinfo))
            Destroy(instantiateprefab);
    }
    public override void Interact()
    {
        base.Interact();
        if (PartyController.quest != null || PartyController.quest.id == questinfo.id || PartyController.quest.isActive)
        {
            PartyController.AddGold(questinfo.goldReward);
            PartyController.AddExperience(questinfo.expReward);
            QuestManager.instance.OpenComplete(PartyController.quest, PartyController.questChain);
        }
    }
}
