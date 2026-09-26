using UnityEngine;

public class QuestDialogueWindow : MonoBehaviour
{
    [SerializeField] private DialoguePager pager;

    public void Open()
    {
        gameObject.SetActive(true);
        GameEvents.SetPause(true);

        pager.Play(
            new[] { "첫 번째 페이지입니다.", "두 번째 페이지입니다.\n줄바꿈도 됩니다.", "마지막 페이지입니다." }
            );
    }

    public void Close()
    {
        gameObject.SetActive(false);
        GameEvents.SetPause(false);
    }

    public void OnClickNext() => pager.Next();
    public void OnClickPrev() => pager.Prev();
    public void OnClickSkip() => pager.Skip();
}
