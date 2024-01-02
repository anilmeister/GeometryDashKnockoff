using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;

    private bool isTracking = true;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("No target object assigned to the camera tracker.");
            isTracking = false;
        }
    }

    private void Update()
    {
        if (isTracking && target != null)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
        }
    }

    public void setTarget(Transform newTarget)
    {
        target = newTarget;
        isTracking = true;
    }

    public void stopTracking()
    {
        target = null;
        isTracking = false;
    }


    public bool IsTracking()
    {
        return isTracking;
    }
}
