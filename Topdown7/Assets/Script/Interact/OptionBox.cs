using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
public class OptionBox : MonoBehaviour
{
    [SerializeField] SelectOption selectprefab;
    private List<SelectOption> selectOptions = new List<SelectOption>();
    public bool isShowing = false;
    public event Action onComplete;
    public void ShowOption(DialogueObject dialogueObject)
    {
        gameObject.SetActive(true);
        isShowing = true;
        ChangeOptionAmount(dialogueObject.options.Count);
        int i = 0;
        foreach (var dialogueOptions in dialogueObject.options)
        {
            Action chooseEvent = () => dialogueOptions.chooseEvent?.Invoke();       // convert UnityEvent into Action
            selectOptions[i].ShowOptionBox(dialogueOptions.message, chooseEvent);
            selectOptions[i].ClickAction += OnComplete;
            i++;
        }
    }
    private void OnComplete()
    {
        onComplete?.Invoke();
        gameObject.SetActive(false);
        isShowing = false;
    }
    private void ChangeOptionAmount(int amount)
    {
        if (amount < 0)
            return;
        while (selectOptions.Count < amount) 
        {
            var prefab = Instantiate(selectprefab);
            prefab.transform.parent = transform;
            prefab.transform.localScale = new Vector3(1f, 1f, 1f);
            selectOptions.Add(prefab);
        }
        while (selectOptions.Count > amount) 
        {
            var lastIndex = selectOptions.Count - 1;
            Destroy(selectOptions[lastIndex].gameObject);
            selectOptions.RemoveAt(lastIndex);
        }
    }
}
