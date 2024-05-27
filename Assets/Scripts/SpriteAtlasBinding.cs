using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using YooAsset;

public class SpriteAtlasBinding 
{
    public static void Init()
    {
        SpriteAtlasManager.atlasRequested += SpriteAtlasManagerOnAtlasRequested;
    }

    private void OnDestroy()
    {
        SpriteAtlasManager.atlasRequested -= SpriteAtlasManagerOnAtlasRequested;
    }

    private static void SpriteAtlasManagerOnAtlasRequested(string arg1, Action<SpriteAtlas> arg2)
    {
        Debug.Log("SpriteAtlasManagerOnAtlasRequested:" + arg1);
        var spriteAtlasHandle = YooAssets.LoadAssetAsync<SpriteAtlas>(arg1);
        spriteAtlasHandle.Completed += (handle) =>
        {
            arg2(handle.AssetObject as SpriteAtlas);
        };
    }
}
