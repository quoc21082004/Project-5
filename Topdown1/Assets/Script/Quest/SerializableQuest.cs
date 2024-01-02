using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableQuest
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
}
