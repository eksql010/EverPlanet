using System;
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
    private Action OnEnd;

    public bool isTyping;
    public bool isLastPage => pageIndex == pages.Length - 1;

    public void Play(string[] newPages, Action onEndCallback, bool isInfoPage = false)
    {
        pages = newPages;
        OnEnd = onEndCallback;

        if (isInfoPage)
            ShowPage(pages.Length - 1, false);
        else
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
        else
            OnEnd?.Invoke();
    }

    public void Prev()
    {
        if (pageIndex > 0)
            ShowPage(pageIndex - 1, false);
    }

    public void Skip()
    {
        ShowPage(pages.Length - 1, false);
        OnEnd?.Invoke();
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
