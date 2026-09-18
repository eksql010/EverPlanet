using UnityEngine;

public class PlanetGravityBody : MonoBehaviour
{
    [SerializeField] private Planet planet;
    [SerializeField] private float alignSpeed = 10f;

    private Rigidbody rigid;

    private Vector3 upDir = Vector3.up;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }

    private void FixedUpdate()
    {
        // 중력 적용
        Vector3 gravityDir = planet.GetGravityDirection(transform.position);
        rigid.AddForce(gravityDir * planet.GravityStrength, ForceMode.Acceleration);

        upDir = -gravityDir;

        // 표면에 맞춰 회전 정렬
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, upDir) * transform.rotation;
        Quaternion lerpRotation = Quaternion.Slerp(rigid.rotation, targetRotation, alignSpeed * Time.fixedDeltaTime);
        
        // 회전 관성 제거
        rigid.angularVelocity = Vector3.zero;
        rigid.MoveRotation(lerpRotation);
    }

    // (행성 표면의 노말 벡터 기준)
    // 기존 속도에서는 수직 성분만 보존하고 이동하려는 속도에서는 수평 성분만 보존해서 두 값을 합쳐서 최종 이동 속도를 반환
    public Vector3 ApplyTangentVelocity(Vector3 currentVelocity, Vector3 tangentVelocity)
    {
        Vector3 verticalVelocity = Vector3.Project(currentVelocity, upDir);
        tangentVelocity = Vector3.ProjectOnPlane(tangentVelocity, upDir);
        return tangentVelocity + verticalVelocity;
    }
}
