using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [SerializeField] private QuestData quest;
    [SerializeField] private GameObject questNotStartedIcon;
    [SerializeField] private GameObject questCompleteIcon;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        UpdateIcon(QuestManager.instance.GetState(quest.questId));

        // 이벤트 등록
        QuestManager.instance.OnQuestAccepted += HandleQuestAccepted;
        QuestManager.instance.OnObjectiveComplete += HandleObjectiveComplete;
        QuestManager.instance.OnQuestCompleted += HandleQuestCompleted;
    }

    private void OnDestroy()
    {
        // 이벤트 해제
        QuestManager.instance.OnQuestAccepted -= HandleQuestAccepted;
        QuestManager.instance.OnObjectiveComplete -= HandleObjectiveComplete;
        QuestManager.instance.OnQuestCompleted -= HandleQuestCompleted;
    }

    private void LateUpdate()
    {
        // 빌보드
        //  transform.forward = camera.transform.forward;
    }

    private void HandleQuestAccepted(QuestData questData)
    {
        if (questData.questId == quest.questId)
            UpdateIcon(QuestState.InProgress);
    }

    private void HandleObjectiveComplete(QuestData questData)
    {
        if (questData.questId == quest.questId)
            UpdateIcon(QuestState.ObjectiveComplete);
    }

    private void HandleQuestCompleted(QuestData questData)
    {
        if (questData.questId == quest.questId)
            UpdateIcon(QuestState.Completed);
    }

    private void UpdateIcon(QuestState state)
    {
        questNotStartedIcon.SetActive(state == QuestState.NotStarted);
        questCompleteIcon.SetActive(state == QuestState.ObjectiveComplete);
    }
}
