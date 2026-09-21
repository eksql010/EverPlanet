using UnityEngine;

// 퀘스트 상태 : 시작 전, 진행 중, 목표 달성(보상 받기 전), 완료
public enum QuestState { NotStarted, InProgress, ObjectiveComplete, Completed }

public class QuestProgress
{
    public QuestData data;
    public QuestState state;
}
