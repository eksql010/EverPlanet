using UnityEngine;

public class OcclusionFadeTarget : MonoBehaviour
{
    [SerializeField] private float fadeAlpha = 0.3f;
    [SerializeField] private float fadeSpeed = 8f;

    private Renderer renderer;
    private float curAlpha = 1f;
    private float targetAlpha = 1f;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Mathf.Approximately(curAlpha, targetAlpha))
            return;

        curAlpha = Mathf.MoveTowards(curAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
        ApplyAlpha(curAlpha);
    }

    public void FadeOut()
    {
        targetAlpha = fadeAlpha;
    }

    public void FadeIn()
    {
        targetAlpha = 1f;
    }

    private void ApplyAlpha(float alpha)
    {
        // 이 렌더러에만 적용할 덮어쓸 값 묶음
        MaterialPropertyBlock block = new MaterialPropertyBlock();

        // 기존에 세팅된 값을 불러오고 원본 색상에서 알파만 교체
        renderer.GetPropertyBlock(block);
        Color color = renderer.sharedMaterial.color;
        color.a = alpha;

        // 그릴 때 BaseColor를 방금 만든 color로 대신 쓰도록 기록
        block.SetColor("_BaseColor", color);

        // 렌더러에 최종 반영
        renderer.SetPropertyBlock(block);
    }
}
