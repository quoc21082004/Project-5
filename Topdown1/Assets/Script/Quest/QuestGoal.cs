using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoalType
{
    Kill,
    Gathering,
    Script,
}
[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public string what;
    public int requireAmt;
    public int currentAmt;

    public bool IsReached()
    {
        if (currentAmt < requireAmt)
            return false;
        return true;
    }
    public void EnemyKilled(string tag)
    {
        if (goalType == GoalType.Kill && (tag == what || what == ""))  
            currentAmt++;
    }
    public void ItemGet(string tag, int amt)
    {
        if (goalType == GoalType.Gathering && (tag == what) || (what == ""))  
            currentAmt += amt;
    }
}
