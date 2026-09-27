using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [SerializeField] private GameObject questNotStartedIcon;
    [SerializeField] private GameObject questCompleteIcon;

    private Camera camera;
    private NPC npc;

    private void Start()
    {
        camera = Camera.main;
        npc = GetComponentInParent<NPC>();
        UpdateIcon();

        // 이벤트 등록
        QuestManager.instance.OnQuestAccepted += HandleQuestChanged;
        QuestManager.instance.OnObjectiveComplete += HandleQuestChanged;
        QuestManager.instance.OnQuestCompleted += HandleQuestChanged;
    }

    private void OnDestroy()
    {
        // 이벤트 해제
        QuestManager.instance.OnQuestAccepted -= HandleQuestChanged;
        QuestManager.instance.OnObjectiveComplete -= HandleQuestChanged;
        QuestManager.instance.OnQuestCompleted -= HandleQuestChanged;
    }

    private void LateUpdate()
    {
        // 빌보드
        //  transform.forward = camera.transform.forward;
    }

    private void HandleQuestChanged(QuestData questData) => UpdateIcon();

    private void UpdateIcon()
    {
        bool hasNotStarted = false;
        bool hasComplete = false;

        foreach (var quest in npc.GiveQuests)
        {
            if (QuestManager.instance.GetState(quest.questId) == QuestState.NotStarted)
            {
                hasNotStarted = true;
                break;
            }
        }

        foreach (var quest in npc.ReceiveQuests)
        {
            if (QuestManager.instance.GetState(quest.questId) == QuestState.ObjectiveComplete)
            {
                hasComplete = true;
                break;
            }
        }

        questCompleteIcon.SetActive(hasComplete);
        questNotStartedIcon.SetActive(!hasComplete && hasNotStarted);
    }
}
