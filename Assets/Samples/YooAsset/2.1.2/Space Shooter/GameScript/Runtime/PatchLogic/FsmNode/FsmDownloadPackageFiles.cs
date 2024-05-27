using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniFramework.Machine;
using YooAsset;

/// <summary>
/// 下载更新文件
/// </summary>
public class FsmDownloadPackageFiles : IStateNode
{
    private StateMachine _machine;

    void IStateNode.OnCreate(StateMachine machine)
    {
        _machine = machine;
    }
    void IStateNode.OnEnter()
    {
        PatchEventDefine.PatchStatesChange.SendEventMessage("开始下载补丁文件！");
        // GameManager.Instance.StartCoroutine(BeginDownload());
        BeginDownloadAsync().Forget();
    }
    void IStateNode.OnUpdate()
    {
    }
    void IStateNode.OnExit()
    {
    }

    private async UniTaskVoid BeginDownloadAsync()
    {
        var downloader = (ResourceDownloaderOperation)_machine.GetBlackboardValue("Downloader");
        downloader.OnDownloadErrorCallback = PatchEventDefine.WebFileDownloadFailed.SendEventMessage;
        downloader.OnDownloadProgressCallback = PatchEventDefine.DownloadProgressUpdate.SendEventMessage;
        downloader.BeginDownload();
        await downloader.ToUniTask();

        // 检测下载结果
        if (downloader.Status != EOperationStatus.Succeed)
            return;
            // await Task.CompletedTask;

        _machine.ChangeState<FsmDownloadPackageOver>();
    }
}