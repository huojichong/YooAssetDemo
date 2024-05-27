using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject followParent;

    private void LateUpdate()
    {
        if (followParent != null)
        {
            transform.position = CameraController.Instance.WorldPosToUIScreenWorldPos(followParent.transform.position);
        }

        // Debug.Log(CameraController.Instance.UICamera.WorldToViewportPoint(transform.position));
        // Debug.Log(CameraController.Instance.MainCamera.WorldToViewportPoint(transform.position));
    }

    public void SetFollowParent(Transform worldTrans)
    {
        followParent = worldTrans.gameObject;
    }
}

