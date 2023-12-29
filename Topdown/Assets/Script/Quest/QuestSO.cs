using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Quest Info")]
public class QuestSO : ScriptableObject
{
    public int id;
    public bool isActive;
    public string title;
    [TextArea(0, 3)] public string description;
    public int goldReward;
    public int expReward;
    public int startChapter;
    public int startScenario;
    public int endChapter;
    public int endScenario;
    public QuestGoal questGoal;
    public static QuestSO LoadQuest(SerializableQuest squest)
    {
        if (squest == null)
            return null;
        QuestSO q = CreateInstance<QuestSO>();
        return q;
    }
    public void AcceptQuest(Action action = null)
    {
        isActive = true;
    }
    public void CompleteQuest(Action nextAction = null)
    {
        isActive = false;
        if (nextAction != null)
            nextAction.Invoke();
    }
}

