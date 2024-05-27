﻿using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniFramework.Event;
using UniFramework.Machine;
using YooAsset;

internal class SceneBattle : MonoBehaviour
{
    public GameObject CanvasDesktop;

    private AssetHandle _windowHandle;
    private AssetHandle _musicHandle;
    private BattleRoom _battleRoom;

    private async UniTaskVoid Start()
    {
        // 加载战斗页面
        _windowHandle = YooAssets.LoadAssetAsync<GameObject>("UIBattle");
        await _windowHandle.ToUniTask();
        _windowHandle.InstantiateSync(CanvasDesktop.transform);

        // 加载背景音乐
        var package = YooAssets.GetPackage("DefaultPackage");
        _musicHandle = package.LoadAssetAsync<AudioClip>("music_background");
        await _musicHandle.ToUniTask();

        // 播放背景音乐
        var audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = _musicHandle.AssetObject as AudioClip;
        audioSource.Play();

        _battleRoom = new BattleRoom();
        _battleRoom.IntRoom();
    }
    private void OnDestroy()
    {
        if (_windowHandle != null)
        {
            _windowHandle.Release();
            _windowHandle = null;
        }

        if (_musicHandle != null)
        {
            _musicHandle.Release();
            _musicHandle = null;
        }

        if (_battleRoom != null)
        {
            _battleRoom.DestroyRoom();
            _battleRoom = null;
        }

        // 切换场景的时候释放资源
        if (YooAssets.Initialized)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            package.UnloadUnusedAssets();
        }
    }
    private void Update()
    {
        if (_battleRoom != null)
            _battleRoom.UpdateRoom();
    }
}