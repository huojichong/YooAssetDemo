using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniFramework.Machine;
using YooAsset;

/// <summary>
/// 更新资源版本号
/// </summary>
internal class FsmUpdatePackageVersion : IStateNode
{
    private StateMachine _machine;

    void IStateNode.OnCreate(StateMachine machine)
    {
        _machine = machine;
    }
    void IStateNode.OnEnter()
    {
        PatchEventDefine.PatchStatesChange.SendEventMessage("获取最新的资源版本 !");
        // GameManager.Instance.StartCoroutine(UpdatePackageVersion());
        UpdatePackageVersionAsync().Forget();
    }
    void IStateNode.OnUpdate()
    {
    }
    void IStateNode.OnExit()
    {
    }

    private async UniTaskVoid UpdatePackageVersionAsync()
    {
        // yield return new WaitForSecondsRealtime(0.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), DelayType.Realtime);
        
        var packageName = (string)_machine.GetBlackboardValue("PackageName");
        var package = YooAssets.GetPackage(packageName);
        var operation = package.UpdatePackageVersionAsync();
        await operation.ToUniTask();

        if (operation.Status != EOperationStatus.Succeed)
        {
            Debug.LogWarning(operation.Error);
            PatchEventDefine.PackageVersionUpdateFailed.SendEventMessage();
        }
        else
        {
            _machine.SetBlackboardValue("PackageVersion", operation.PackageVersion);
            _machine.ChangeState<FsmUpdatePackageManifest>();
        }
    }
}