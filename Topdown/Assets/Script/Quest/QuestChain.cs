using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestChain
{
    public List<QuestSO> quests;
    public int currentQuest;

    public static QuestChain LoadQuestChain(SerializableQuestChain squest)
    {
        if (squest == null)
            return null;
        QuestChain qc = new QuestChain();
        qc.quests = new List<QuestSO>();
        foreach (SerializableQuest s in squest.quests)
        {
            qc.quests.Add(QuestSO.LoadQuest(s));
        }
        qc.currentQuest = squest.currentQuest;
        return qc;
    }
    public bool IsEnd()
    {
        return currentQuest > quests.Count;
    }
    public QuestSO NextQuest()
    {
        currentQuest++;
        if (currentQuest > quests.Count - 1)  
            return null;
        return quests[currentQuest];
    }
    public QuestSO First()
    {
        currentQuest = 0;
        return quests[currentQuest];
    }
}
