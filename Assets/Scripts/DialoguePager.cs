using System.Collections;
using TMPro;
using UnityEngine;

public class DialoguePager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingInterval = 0.05f;

    private string[] pages;
    private int pageIndex;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private bool isLastPage => pageIndex == pages.Length - 1;

    public void Play(string[] newPages)
    {
        pages = newPages;
        ShowPage(0);
    }

    public void Next()
    {
        if (isTyping)
        {
            CompleteTyping();
            return;
        }

        if (!isLastPage)
            ShowPage(pageIndex + 1);
    }

    public void Prev()
    {
        if (pageIndex > 0)
            ShowPage(pageIndex - 1, false);
    }

    public void Skip()
    {
        if (isLastPage)
            return;

        ShowPage(pages.Length - 1, false);
    }

    private void ShowPage(int index, bool useTyping = true)
    {
        pageIndex = index;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        dialogueText.text = pages[pageIndex];
        
        if (useTyping)
        {
            typingCoroutine = StartCoroutine(TypeText());
        }
        else
        {
            dialogueText.ForceMeshUpdate();
            dialogueText.maxVisibleCharacters = int.MaxValue;
            isTyping = false;
        }
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        dialogueText.maxVisibleCharacters = 0;
        dialogueText.ForceMeshUpdate();
        int count = dialogueText.textInfo.characterCount;

        for(int i = 0; i <= count; ++i)
        {
            dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingInterval);
        }

        isTyping = false;
    }

    private void CompleteTyping()
    {
        StopCoroutine(typingCoroutine);
        dialogueText.maxVisibleCharacters = int.MaxValue;
        isTyping = false;
        typingCoroutine = null;
    }
}
