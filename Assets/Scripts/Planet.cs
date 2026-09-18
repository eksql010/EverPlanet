using UnityEngine;

public class Planet : MonoBehaviour
{
    // private 필드 -> 어트리뷰트로 인스펙터 노출, 외부 스크립트에서 읽기 및 쓰기 불가능
    [SerializeField] private float gravityStrength = 20f;

    public Vector3 GetGravityDirection(Vector3 worldPosition)
    {
        return (transform.position - worldPosition).normalized;
    }

    // 접근 불가한 값을 외부에서 읽을 수 있게 만들어주는 프로퍼티를 람다 연산자로 간결하게 표현
    public float GravityStrength => gravityStrength;
}
