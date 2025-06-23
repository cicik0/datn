using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offSet;
    [SerializeField] float smoothSpeed;

    private void LateUpdate()
    {
        if(target == null) return;

        Vector3 cameraPos = target.transform.position + offSet;
        transform.position = Vector3.Lerp(transform.position, cameraPos, smoothSpeed);
    }

    public void SetTarget(Transform follower)
    {
        this.target = follower;
    }
}
