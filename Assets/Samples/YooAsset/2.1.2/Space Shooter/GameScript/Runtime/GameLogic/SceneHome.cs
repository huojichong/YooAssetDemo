using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

public class SceneHome : MonoBehaviour
{
    public GameObject CanvasDesktop;

    private AssetHandle _windowHandle;

    private async UniTaskVoid Start()
    {
        // 加载登录页面
        _windowHandle = YooAssets.LoadAssetAsync<GameObject>("UIHome");
        await _windowHandle.ToUniTask();
        _windowHandle.InstantiateSync(CanvasDesktop.transform);
    }
    private void OnDestroy()
    {
        if (_windowHandle != null)
        {
            _windowHandle.Release();
            _windowHandle = null;
        }

        // 切换场景的时候释放资源
        if (YooAssets.Initialized)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            package.UnloadUnusedAssets();
        }
    }
}