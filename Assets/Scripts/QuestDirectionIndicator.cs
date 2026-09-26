using Newtonsoft.Json.Bson;
using UnityEngine;

public class QuestDirectionIndicator : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private float heightOffset = 2f;   // 타겟 위로 뛰우는 기본 높이
    [SerializeField] private float floatRange = 0.3f;   // 두둥실 범위
    [SerializeField] private float floatSpeed = 2f;     // 두둥실 속도

    private Transform target;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        image.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnQuestAccepted += HandleQuestAccepted;
        GameEvents.OnQuestObjectiveAchieved += HandleQuestObjectiveAchieved;
    }

    private void OnDisable()
    {
        GameEvents.OnQuestAccepted -= HandleQuestAccepted;
        GameEvents.OnQuestObjectiveAchieved -= HandleQuestObjectiveAchieved;
    }

    private void HandleQuestAccepted(QuestData quest)
    {
        Transform location = QuestManager.instance.GetObjectiveLocation(quest.questId);
        SetTarget(location);
    }

    private void HandleQuestObjectiveAchieved(string questId)
    {
        ClearTarget();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        image.SetActive(target != null);
    }

    public void ClearTarget()
    {
        target = null;
        image.SetActive(false);
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        float floatY = Mathf.Sin(Time.time * floatSpeed) * floatRange;

        transform.position = target.position + target.up * (heightOffset + floatY);
        transform.rotation = Quaternion.LookRotation(camera.transform.forward, target.up);
    }
}
