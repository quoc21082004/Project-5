using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questTitle_txt;
    public TextMeshProUGUI questDescription_txt;
    public TextMeshProUGUI goldReward_txt;
    public TextMeshProUGUI expReward_txt;
    public TextMeshProUGUI progress_txt;
    public TextMeshProUGUI remaining_txt;

    private void OnEnable()
    {
        if (PartyController.quest == null || PartyController.instance == null || !PartyController.quest.isActive) 
        {
            questTitle_txt.text = "No Quest Currently";
            questDescription_txt.text = "";
            goldReward_txt.text = "";
            expReward_txt.text = "";
            remaining_txt.text = "";
            progress_txt.text = "";
            return;
        }
        QuestSO quest = PartyController.quest;
        if (quest != null)
        {
            questTitle_txt.text = quest.title;
            questDescription_txt.text = quest.description;
            goldReward_txt.text = quest.goldReward.ToString() + "<sprite=3>";
            expReward_txt.text = quest.expReward.ToString();
            if ((quest.questGoal.goalType == GoalType.Gathering) || (quest.questGoal.goalType == GoalType.Kill))
            {
                remaining_txt.text = "" + quest.questGoal.currentAmt + " / " + quest.questGoal.requireAmt;
                progress_txt.text = "Progress:";
            }
        }
    }
}
