using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiQuestNPC : MonoBehaviour
{
    public QuestSO[] acceptedQuest;
    public QuestSO[] notCompletedQuest;
    public GameObject[] order;
    private void Update()
    {
        for (int i = 0; i < order.Length; i++)
        {
            if (i == order.Length - 1) // order.length = 4 // i = 0 , order = 3
            {
                order[i].gameObject.SetActive(true);
                break;
            }
            bool haveAccepted = (acceptedQuest[i] == null) || (CheckQuestManager.instance.CheckAcceptQuest(acceptedQuest[i]));
            bool havenotCompleted = (notCompletedQuest[i] == null) || (!CheckQuestManager.instance.CheckCompleteQuest(notCompletedQuest[i]));
            if (haveAccepted && havenotCompleted)
            {
                order[i].gameObject.SetActive(true);
                break;
            }
            order[i].gameObject.SetActive(false);
        }
    }
}
