using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDialogueWindow : MonoBehaviour
{
    [SerializeField] private DialoguePager greetingPager;
    [SerializeField] private GameObject greetingGroup;
    [SerializeField] private TMP_Text greetingText;
    [SerializeField] private Transform questListParent;
    [SerializeField] private GameObject questLinkButton;

    [SerializeField] private DialoguePager questPager;
    [SerializeField] private GameObject pageGroup;

    private NPC currentNpc;

    public void Open(NPC npc)
    {
        currentNpc = npc;
        gameObject.SetActive(true);
        GameEvents.SetPause(true);
        ShowGreeting();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        GameEvents.SetPause(false);
    }

    public void OnClickNext() => questPager.Next();
    public void OnClickPrev() => questPager.Prev();
    public void OnClickSkip() => questPager.Skip();

    private void ShowGreeting()
    {
        pageGroup.SetActive(false);
        greetingGroup.SetActive(true);

        greetingPager.Play(new[] { currentNpc.GreetingDialogue });

        foreach (Transform child in questListParent)
            Destroy(child.gameObject);

        var quests = currentNpc.GetVisibleQuests();

        for (int i = 0; i < quests.Count; ++i)
        {
            QuestData questdata = quests[i];
            QuestState state = QuestManager.instance.GetState(questdata.questId);

            var newButton = Instantiate(questLinkButton, questListParent);
            var label = newButton.GetComponentInChildren<TMP_Text>();
            label.text = $"{i + 1}. {GetStateLabel(state)} {questdata.title}";
            label.color = GetStateColor(state);

            newButton.GetComponent<Button>().onClick.AddListener(() => OnQuestLinkClicked(questdata));
        }
    }

    private void OnQuestLinkClicked(QuestData quest)
    {
        greetingGroup.SetActive(false);
        pageGroup.SetActive(true);
        questPager.Play(quest.startDialogue);
    }

    private string GetStateLabel(QuestState state)
    {
        string curState = "";

        switch (state)
        {
            case QuestState.NotStarted:
                curState = "[시작]";
                break;
            case QuestState.InProgress:
                curState = "[진행]";
                break;
            case QuestState.ObjectiveComplete:
                curState = "[완료]";
                break;
        }

        return curState;
    }

    private Color GetStateColor(QuestState state)
    {
        Color color = Color.magenta;

        switch (state)
        {
            case QuestState.NotStarted:
                color = Color.green;
                break;
            case QuestState.InProgress:
                color = Color.white;
                break;
            case QuestState.ObjectiveComplete:
                color = Color.yellow;
                break;
        }

        return color;
    }
}
