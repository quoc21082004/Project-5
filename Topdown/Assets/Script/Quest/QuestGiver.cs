using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestSO quest;
    public QuestChain questChain;
    public GameObject questAvaiableprefab;
    private GameObject instantiatedprefab;
    private void Update()
    {
        if (questChain != null)
            quest = questChain.First();

        if (instantiatedprefab == null && !CheckQuestManager.instance.CheckAcceptQuest(quest))
            instantiatedprefab = Instantiate(questAvaiableprefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        else if (instantiatedprefab != null && CheckQuestManager.instance.CheckAcceptQuest(quest))
            Destroy(instantiatedprefab);
    }
    public void OpenQuestWindow()
    {
        if (QuestManager.instance == null)
            return;

        if (questChain == null)
            QuestManager.instance.OpenQuest(quest);
        else if (questChain != null)
            QuestManager.instance.OpenQuest(questChain.First(), questChain);
    }
}
