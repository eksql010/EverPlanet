using UnityEngine;

public class QuestDialogueWindow : MonoBehaviour
{
    [SerializeField] private DialoguePager pager;

    public void Open()
    {
        gameObject.SetActive(true);
        GameEvents.SetPause(true);

        pager.Play(
            new[] { 
                "첫 번째 페이지입니다. 타이핑 이펙트를 위해 조금만 더 길게 써볼까 합니다.",
                "두 번째 페이지입니다.\n줄바꿈도 됩니다.\n줄바꿈도 됩니다.\n줄바꿈도 됩니다.", 
                "마지막 페이지입니다. 마지막 페이지에는 다음 버튼과 건너뛰기 버튼이 비활성화 되어야 합니다. 타이핑 이펙트 구현 후에 적용해볼 계획입니다. 얼른 만들어봅시다. ^^" 
            }
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
