using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestDialogueWindow dialogueWindow;
    [SerializeField, TextArea] private string greetingDialogue;
    [SerializeField] private QuestData[] startQuests;

    public string GreetingDialogue => greetingDialogue;

    public void Interact()
    {
        dialogueWindow.Open(this);
    }

    public List<QuestData> GetVisibleQuests()
    {
        var result = new List<QuestData>();

        foreach (var quest in startQuests)
        {
            if (QuestManager.instance.GetState(quest.questId) != QuestState.Completed)
                result.Add(quest);
        }

        return result;
    }
}
