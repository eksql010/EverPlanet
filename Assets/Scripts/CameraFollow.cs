using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 positionOffset = new Vector3(0f, 7f, -15f);
    [SerializeField] private Vector3 rotationOffset = new Vector3(25f, 0f, 0f);
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float rotateSpeed = 10f;

    private void LateUpdate()
    {
        Vector3 newPos = target.TransformPoint(positionOffset);
        Quaternion newRot = target.rotation * Quaternion.Euler(rotationOffset);

        transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
    }
}
