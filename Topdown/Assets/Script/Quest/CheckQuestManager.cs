using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckQuestManager : Singleton<CheckQuestManager>
{
    public Dictionary<int, bool> acceptedQuest = new Dictionary<int, bool>();
    public Dictionary<int, bool> completedQuest = new Dictionary<int, bool>();

    public void SetAcceptQuest(QuestSO questinfo)
    {
        if (acceptedQuest.ContainsKey(questinfo.id))
            acceptedQuest[questinfo.id] = true;
        else if (!acceptedQuest.ContainsKey(questinfo.id))
            acceptedQuest.Add(questinfo.id, true);
    }
    public bool CheckAcceptQuest(QuestSO quest)
    {
        if (acceptedQuest.ContainsKey(quest.id))
            return acceptedQuest[quest.id];
        else
            return false;
    }
    public void SetCompleteQuest(QuestSO questinfo)
    {
        if (completedQuest.ContainsKey(questinfo.id))
            completedQuest[questinfo.id] = true;
        else if (!completedQuest.ContainsKey(questinfo.id))
            completedQuest.Add(questinfo.id, true);
    }
    public bool CheckCompleteQuest(QuestSO questinfo)
    {
        if (completedQuest.ContainsKey(questinfo.id))
            return completedQuest[questinfo.id] = true;
        else
            return false;
    }
}
