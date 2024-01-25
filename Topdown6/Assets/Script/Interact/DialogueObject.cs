using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class DialogueObject
{
    [TextArea] public string[] sentences;
    [SerializeField] public string name;
    [SerializeField] public List<Option> options;
}
[System.Serializable]
public class Option
{
    public string message;
    public UnityEvent chooseEvent;
}
