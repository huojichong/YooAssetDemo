using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera MainCamera;
    public Camera UICamera;

    public static CameraController Instance;

    private void Start()
    {
        Instance = this;
    }

    /// <summary>
    /// 3D的世界坐标转换为屏幕的世界坐标
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public Vector3 WorldPosToUIScreenWorldPos(Vector3 worldPos)
    {
        Vector3 viewportPos = MainCamera.WorldToViewportPoint(worldPos);
        // Vector3 screenPos = new Vector3(viewportPos.x * Screen.width, viewportPos.y * Screen.height, 0);
        // Vector3 uiWorldPos = UICamera.ScreenToWorldPoint(screenPos);

        viewportPos.z = 0;
        Vector3 uiWorldPos = UICamera.ViewportToWorldPoint(viewportPos);
        return new Vector3(uiWorldPos.x, uiWorldPos.y, 0);
    }


}
