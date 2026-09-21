using System;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraOcclusionDetector : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask occlusionMask;

    private Dictionary<Collider, OcclusionFadeTarget> detectCache = new();
    private HashSet<OcclusionFadeTarget> curHits = new();
    private HashSet<OcclusionFadeTarget> prevHits = new();

    private void LateUpdate()
    {
        // 현재 충돌 목록 초기화
        curHits.Clear();

        // 카메라 -> 플레이어 방향 벡터
        Vector3 dir = target.position - transform.position;

        // 카메라와 플레이어 사이의 실제 거리
        float distance = dir.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, dir.normalized, distance, occlusionMask);

        Debug.DrawRay(transform.position, dir, Color.red);

        foreach (var hit in hits)
        {
            OcclusionFadeTarget fadeTarget = GetTarget(hit.collider);
            if(fadeTarget == null)
                continue;

            curHits.Add(fadeTarget);
        }

        // 새로 가리기 시작하는 대상만 FadeOut 호출
        foreach (var hit in curHits)
        {
            if (!prevHits.Contains(hit))
                hit.FadeOut();
        }

        // 더 이상 가리지 않는 대상만 FadeIn 호출
        foreach (var hit in prevHits)
        {
            if (!curHits.Contains(hit))
                hit.FadeIn();
        }

        // 이전 충돌 목록 갱신
        prevHits.Clear();
        foreach(var hit in curHits)
            prevHits.Add(hit);
    }

    private OcclusionFadeTarget GetTarget(Collider collider)
    {
        // 이미 저장된 타겟이면 바로 찾아서 리턴
        if (detectCache.TryGetValue(collider, out var cached))
            return cached;

        // 아니라면 감지된 목록에 캐싱
        OcclusionFadeTarget target = collider.GetComponent<OcclusionFadeTarget>();
        detectCache.Add(collider, target);
        return target;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        ClearCollections();
    }

    private void ClearCollections()
    {
        detectCache.Clear();
        curHits.Clear();
        prevHits.Clear();
    }
}
