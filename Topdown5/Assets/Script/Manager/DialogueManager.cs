using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class DialogueManager : Singleton<DialogueManager>
{
    public List<string> senteces = new List<string>();
    public bool autoDialogue;
    public float autoSpeed;
    public float typeSpeed;
    public GameObject dialogueBox;
    bool isDialogueBox = false;
    public TextMeshProUGUI name_txt;
    public TextMeshProUGUI dialogue_txt;
    public string currentSentence;
    public bool inDialogue;
    public bool typingDialogue;
    private Action onDialogueEnd;
    public static bool isDialogueOpen;
    public GameObject pressToInteractprefab;
    private void Start()
    {
        typeSpeed = PlayerPrefs.GetFloat("typeSpd", 2f);
        autoSpeed = PlayerPrefs.GetFloat("autoSpd", 6f);
        autoDialogue = (PlayerPrefs.GetInt("autodiag", 0) == 1);
    }
    private void Update()
    {
        if (!Player.near)
        {
            HideTag();
            return;
        }
        else
        {
            ShowTag();
            if (Player.near)
                if (isDialogueBox)
                    HideTag();
        }
    }
    public void StartDialogue(Dialogue dialogue, Action nextAction = null)
    {
        Time.timeScale = 0;
        inDialogue = true;
        onDialogueEnd = nextAction;
        name_txt.text = dialogue.name;
        senteces.Clear();
        foreach (var sentence in dialogue.text)
        {
            senteces.Add(sentence);
        }
        isDialogueBox = true;
        dialogueBox.gameObject.SetActive(true);
        DisplayNextDialogue();
    }

    private void DisplayNextDialogue()
    {
        isDialogueOpen = true;
        if (senteces.Count == 0)
        {
            EndDialogue();
            return;
        }
        currentSentence = senteces[0];
        senteces.RemoveAt(0);
        StopAllCoroutines();
        autoDialogue = true;
        if (typeSpeed > 0) 
            StartCoroutine(TypeSentence(currentSentence)); 
    }
    IEnumerator TypeSentence(string sentence)
    {
        typingDialogue = true;
        dialogue_txt.text = "";
        foreach (var letter in sentence.ToCharArray()) 
        {
            float actualSpeed = 1f / (typeSpeed * 20f);
            dialogue_txt.text += letter;
            yield return new WaitForSecondsRealtime(actualSpeed);
        }
        typingDialogue = false;
    }
    public void ContinueDialogue()
    {
        if (senteces.Count == 0)
        {
            StartCoroutine(EndDialogue());
            return;
        }
        if (typingDialogue)
        {
            StopAllCoroutines();
            dialogue_txt.text = currentSentence;
            typingDialogue = false;
        }
        else
            DisplayNextDialogue();
    }
    public IEnumerator EndDialogue()
    {
        inDialogue = false;
        if (onDialogueEnd != null)
            onDialogueEnd();
        isDialogueBox = false;
        dialogueBox.gameObject.SetActive(false);
        Time.timeScale = 1;
        yield return new WaitForSeconds(2f);
        isDialogueOpen = false;
    }
    public void SkipDialogue(bool skipInput)
    {
        if (skipInput)
        {
            PlayerPrefs.SetFloat("typeSpd", typeSpeed);
            PlayerPrefs.SetFloat("autoSpd", autoSpeed);
            typeSpeed = 200f;
            autoSpeed = 20f;
            autoDialogue = true;
            ContinueDialogue();
        }
        else
        {
            typeSpeed = PlayerPrefs.GetFloat("typespd", 6f);
            autoSpeed = PlayerPrefs.GetFloat("autospd", 4f);
            autoDialogue = (PlayerPrefs.GetInt("autodiag", 0) == 1);
            ContinueDialogue();
        }
    }
    public void ShowTag()
    {
        pressToInteractprefab.gameObject.SetActive(true);
    }
    public void HideTag()
    {
        pressToInteractprefab.gameObject.SetActive(false);
    }
}
