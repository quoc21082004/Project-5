using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class QuestManager : Singleton<QuestManager>
{
    public GameObject questWindowPanel;
    public GameObject completeWindowPanel;

    public TextMeshProUGUI questTitle_txt;
    public TextMeshProUGUI questDescription_txt;
    public TextMeshProUGUI goldReward_txt;
    public TextMeshProUGUI expReward_txt;
    public Button acceptQuest_btn;

    public TextMeshProUGUI completeGold;
    public TextMeshProUGUI completeExp;
    public TextMeshProUGUI completeDescription;
    public Button acceptComplete_btn;

    public static bool isOpen;
    private void Start()
    {
        questWindowPanel = transform.GetChild(0).gameObject;
        completeWindowPanel = transform.GetChild(1).gameObject;
    }
    public void OpenQuest(QuestSO questinfo, QuestChain questchain = null)
    {
        Time.timeScale = 0;
        questWindowPanel.gameObject.SetActive(true);
        isOpen = true;
        questTitle_txt.text = questinfo.title;
        questDescription_txt.text = questinfo.description;
        goldReward_txt.text = questinfo.goldReward.ToString() + " <sprite=5>";
        expReward_txt.text = questinfo.expReward.ToString() + " EXP";
        acceptQuest_btn.onClick.RemoveAllListeners();
        acceptQuest_btn.onClick.AddListener(() =>
        {
            questWindowPanel.gameObject.SetActive(false);
            isOpen = false;
            questinfo.questGoal.currentAmt = 0;
            PartyController.questChain = questchain;
            PartyController.quest = questinfo;
            CheckQuestManager.instance.SetAcceptQuest(questinfo);
            questinfo.AcceptQuest(() => { });
            Time.timeScale = 1;
        });
        acceptQuest_btn.Select();
        acceptQuest_btn.OnSelect(null);
    }
    public void OpenComplete(QuestSO questinfo, QuestChain questChain = null)
    {
        Time.timeScale = 0;
        completeWindowPanel.gameObject.SetActive(true);
        isOpen = true;
        completeGold.text = questinfo.goldReward.ToString() + "<sprite=3>";
        completeExp.text = questinfo.expReward.ToString() + " EXP";
        completeDescription.text = questinfo.description;
        acceptComplete_btn.onClick.RemoveAllListeners();
        acceptComplete_btn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            completeWindowPanel.gameObject.SetActive(false);
            CheckQuestManager.instance.SetCompleteQuest(questinfo);
            isOpen = false;
            questinfo.CompleteQuest(() =>
            {
                if (questChain != null)
                {
                    Debug.Log("next questchain");
                    QuestSO nextQuest = questChain.NextQuest();
                    if (nextQuest == null)
                    {
                        PartyController.questChain = null;
                        Debug.Log("quest chain end");
                        return;
                    }
                    OpenQuest(nextQuest, questChain);
                }
                else if (questChain == null)
                {
                    Debug.Log("no quest chain");
                }
            });
        });
        acceptComplete_btn.Select();
        acceptComplete_btn.OnSelect(null);
    }
}
