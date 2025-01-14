﻿using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniFramework.Machine;
using YooAsset;

/// <summary>
/// 更新资源清单
/// </summary>
public class FsmUpdatePackageManifest : IStateNode
{
    private StateMachine _machine;

    void IStateNode.OnCreate(StateMachine machine)
    {
        _machine = machine;
    }
    void IStateNode.OnEnter()
    {
        PatchEventDefine.PatchStatesChange.SendEventMessage("更新资源清单！");
        // GameManager.Instance.StartCoroutine(UpdateManifest());
        UpdateManifestAsync().Forget();
    }
    void IStateNode.OnUpdate()
    {
    }
    void IStateNode.OnExit()
    {
    }

    private async UniTaskVoid UpdateManifestAsync()
    {
        // yield return new WaitForSecondsRealtime(0.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), DelayType.Realtime);
        
        var packageName = (string)_machine.GetBlackboardValue("PackageName");
        var packageVersion = (string)_machine.GetBlackboardValue("PackageVersion");
        var package = YooAssets.GetPackage(packageName);
        bool savePackageVersion = true;
        var operation = package.UpdatePackageManifestAsync(packageVersion, savePackageVersion);
        await operation.ToUniTask();

        if (operation.Status != EOperationStatus.Succeed)
        {
            Debug.LogWarning(operation.Error);
            PatchEventDefine.PatchManifestUpdateFailed.SendEventMessage();
        }
        else
        {
            _machine.ChangeState<FsmCreatePackageDownloader>();
        }
    }
}