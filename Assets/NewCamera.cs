using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target;

    [Header("Follow Settings")]
    public float distance = 5f;
    public float height = 2f;
    public float rotationAngle = 0f; 

    [Header("Smoothness")]
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;
        Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        offset.y += height;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}