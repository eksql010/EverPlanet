using System;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestDialogueWindow : MonoBehaviour
{
    [SerializeField] private DialoguePager greetingPager;
    [SerializeField] private GameObject greetingGroup;
    [SerializeField] private Transform questListParent;
    [SerializeField] private GameObject questLinkButton;

    [SerializeField] private DialoguePager questPager;
    [SerializeField] private GameObject pageGroup;
    [SerializeField] private GameObject reportButton;

    [SerializeField] private GameObject infoGroup;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descText;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private TMP_Text infoActionText;
    [SerializeField] private GameObject descriptionGroup;

    private QuestData currentQuest;
    private NPC currentNpc;

    private bool isRewardInfo;
    private bool canReport;

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
        currentQuest = null;
    }

    public void OnClickNext() => questPager.Next();
    public void OnClickPrev() => questPager.Prev();
    public void OnClickSkip() => questPager.Skip();

    private void Update()
    {
        if (pageGroup.activeSelf)
        {
            bool isActive = canReport && questPager.isLastPage && !questPager.isTyping;
            reportButton.SetActive(isActive);
        }
    }

    private void ShowGreeting()
    {
        // 인사말 페이지 외 전부 OFF
        pageGroup.SetActive(false);
        infoGroup.SetActive(false);
        greetingGroup.SetActive(true);

        // 인사말 타이핑 효과 실행
        greetingPager.Play(new[] { currentNpc.GreetingDialogue }, null);
        
        // 기존 퀘스트 목록 버튼 정리
        foreach (Transform child in questListParent)
            Destroy(child.gameObject);

        // 새로운 퀘스트 목록 버튼 추가
        var quests = currentNpc.GetVisibleQuests();

        for (int i = 0; i < quests.Count; ++i)
        {
            QuestData questdata = quests[i];
            QuestState state = QuestManager.instance.GetState(questdata.questId);

            // 퀘스트 링크 버튼 생성
            var newButton = Instantiate(questLinkButton, questListParent);
            var label = newButton.GetComponentInChildren<TMP_Text>();

            // 퀘스트 상태에 맞춰 라벨, 텍스트 설정
            label.text = $"{i + 1}. {GetStateLabel(state)} {questdata.title}";
            label.color = GetStateColor(state);

            // 퀘스트 링크 버튼 클릭 시 이벤트 연결
            newButton.GetComponent<Button>().onClick.AddListener(() => OnQuestLinkClicked(questdata));
        }
    }

    private void ShowPages(string[] pages, Action onEnd, bool reportable = false)
    {
        // 퀘스트 관련 NPC 대사 페이지 외 전부 OFF
        greetingGroup.SetActive(false);
        infoGroup.SetActive(false);
        pageGroup.SetActive(true);
        
        // 퀘스트 보고 버튼 OFF
        canReport = reportable;
        reportButton.SetActive(false);

        // 퀘스트 관련 대사 페이지 타이핑 실행
        questPager.Play(pages, onEnd);
    }

    private void ShowInfo(bool isComplete)
    {
        // 퀘스트 정보 페이지 외 전부 OFF
        isRewardInfo = isComplete;
        pageGroup.SetActive(false);
        infoGroup.SetActive(true);

        titleText.text = currentQuest.title;

        // 완료 화면에선 설명, 목표 숨기고 보상만 표시
        descriptionGroup.SetActive(!isComplete);

        if(!isComplete)
        {
            descText.text = currentQuest.description;
            objectiveText.text = currentQuest.objectiveText;
        }

        rewardText.text = $"경험치 : {currentQuest.rewardExp}\n마블 : {currentQuest.rewardMoney}";
        infoActionText.text = isComplete ? "퀘스트 완료" : "퀘스트 수락";
    }

    // 퀘스트 링크 클릭 이벤트
    // 상태별로 startDialogue(미수락), progressDialogue(수락 이후) 분기
    private void OnQuestLinkClicked(QuestData quest)
    {
        currentQuest = quest;
        QuestState state = QuestManager.instance.GetState(quest.questId);

        if (state == QuestState.NotStarted)
        {
            ShowPages(quest.startDialogue, () => ShowInfo(false));
        }
        else
        {
            ShowPages(quest.progressDialogue, null, state == QuestState.ObjectiveComplete);
        }
    }

    // 퀘스트 보고 버튼 이벤트
    // 완료 대사 실행, 끝나면 보상 화면으로
    public void OnClickReport()
    {
        ShowPages(currentQuest.completeDialogue, () => ShowInfo(true));
    }

    // 퀘스트 수락/완료 버튼 이벤트
    // 퀘스트 매니저에 반영 후 창 닫기
    public void OnClickInfoAction()
    {
        if (isRewardInfo)
            QuestManager.instance.CompleteQuest(currentQuest.questId);
        else
            QuestManager.instance.AcceptQuest(currentQuest);

        Close();
    }

    // 퀘스트 정보 페이지에서의 뒤로가기 버튼 이벤트
    // 마지막 대사로 타이핑 없이 복귀
    public void OnClickInfoBack()
    {
        string[] pages = isRewardInfo ? currentQuest.completeDialogue : currentQuest.startDialogue;

        infoGroup.SetActive(false);
        pageGroup.SetActive(true);
        canReport = false;

        questPager.Play(pages, () => ShowInfo(isRewardInfo), true);
    }

    // 퀘스트 상태 -> 링크 버튼 라벨 텍스트
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

    // 퀘스트 상태 -> 링크 버튼 텍스트 색상
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
