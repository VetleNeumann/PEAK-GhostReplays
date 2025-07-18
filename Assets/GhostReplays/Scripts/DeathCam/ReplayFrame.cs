using UnityEngine;

class ReplayFrame
{
    float timestamp;
    UnityEngine.Vector3 position;
    UnityEngine.Quaternion rotation;

    public ReplayFrame(Transform animationPosition, Transform animationLook)
    {
        timestamp = Time.time;
        position = animationPosition.position;
        rotation = animationLook.rotation;
    }
}