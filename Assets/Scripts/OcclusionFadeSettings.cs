using UnityEngine;

[CreateAssetMenu(fileName = "OcclusionFadeSettings", menuName = "Scriptable Objects/OcclusionFadeSettings")]
public class OcclusionFadeSettings : ScriptableObject
{
    [SerializeField] private float fadeAlpha = 0.3f;
    [SerializeField] private float fadeSpeed = 8f;

    public float FadeAlpha => fadeAlpha;
    public float FadeSpeed => fadeSpeed;
}
