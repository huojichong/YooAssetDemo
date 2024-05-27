
    using UnityEditor;

    public class AssetUploadTools : UnityEditor.Editor
    {


        public static string path()
        {
            if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
                return $"/CDN/Android/";
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
                return $"/CDN/IPhone/";
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
                return $"/CDN/WebGL/";
            else
                return $"/CDN/PC/";
        }
        
        [MenuItem("AssetUpload/UploadLocal")]
        public static void UploadLocal()
        {
            
        }

        [MenuItem("AssetUpload/UploadRemote")]
        public static void UploadRemote()
        {
            
        }
                
    }
