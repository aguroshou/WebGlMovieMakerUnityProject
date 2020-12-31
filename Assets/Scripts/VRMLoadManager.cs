using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using VRM;

namespace KiliWare.SampleVRMApp
{
    /*
    * You have to attch this script to GameObject named "VRMLoader"
    * because the FileImporterPlugin uses the name "VRMLoader" as a clue when calling the callback.
    *
    * このスクリプトはシーン上の"VRMLoader"という名前のついたGameObjectにアタッチしてください。
    * FileImporterPlugin.jslibの中で、"VRMLoader"と名のつくGameObjectに対してコールバックの呼び出しを行っているためです。
    */
    public class VRMLoadManager : MonoBehaviour
    {
        /* Reference:
        * https://qiita.com/mechamogera/items/89d4555b202af96810af
        * https://forum.unity.com/threads/how-do-i-let-the-user-load-an-image-from-their-harddrive-into-a-webgl-app.380985/
        */
        [DllImport("__Internal")]
        protected static extern void FileImporterCaptureClick();

        [DllImport("__Internal")]
        protected static extern void ImageFileImporterCaptureClick();

        public event Action<GameObject> OnModelLoaded;
        public event Action<VRMMetaObject> OnMetaDataLoaded;
        public SpriteRenderer LoadSpriteRenderer;

        [SerializeField] private LoadBackgroundImage _loadBackgroundImage;

        public void OnLoadButtonClicked()
        {
#if UNITY_EDITOR
            var url = Application.dataPath + "/SampleModel/Sample.vrm";
            FileSelected(url);
#elif UNITY_WEBGL
                FileImporterCaptureClick();
#endif
        }

        public void OnSampleLoadButtonClicked()
        {
            var model = Instantiate(Resources.Load("Sample")) as GameObject;
            OnLoaded(model);
        }

        /*
        * This method will be called by FileImporterPlugin.jslib 
        *
        * このメソッドはFileImpoorterPlugin.jslibから呼ばれます
        */
        public void FileSelected(string url)
        {
            StartCoroutine(LoadJson(url));
        }

        protected IEnumerator LoadJson(string url)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.LogError("There is something wrong with loading VRM");
                }
                else
                {
                    LoadVRMFromBytes(webRequest.downloadHandler.data);
                }
            }
        }

        protected void LoadVRMFromBytes(Byte[] bytes)
        {
            var context = new VRMImporterContext();
            try
            {
                context.ParseGlb(bytes);

                context.Load();

                var model = context.Root;

                var meta = context.ReadMeta(true);
                model.name = meta.Title;

                context.ShowMeshes();
                OnLoaded(model, meta);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        protected virtual void OnLoaded(GameObject model, VRMMetaObject meta = null)
        {
            Debug.Log(model + "has been loaded");
            OnModelLoaded?.Invoke(model);
            if (meta != null)
            {
                OnMetaDataLoaded?.Invoke(meta);
            }
        }

        public void OnLoadImageButtonClicked()
        {
#if UNITY_EDITOR
            var url = Application.dataPath + "/SampleModel/Sample.png";
            ImageFileSelected(url);
#elif UNITY_WEBGL
                ImageFileImporterCaptureClick();
#endif
        }

        public void ImageFileSelected(string url)
        {
            StartCoroutine(LoadImageFileJson(url));
        }

        protected IEnumerator LoadImageFileJson(string url)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.LogError("There is something wrong with loading Image");
                }
                else
                {
                    LoadImageFromBytes(webRequest.downloadHandler.data);
                }
            }
        }

        protected void LoadImageFromBytes(Byte[] bytes)
        {
            // Create a texture. Texture size does not matter, since
            // LoadImage will replace with with incoming image size.
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);

            //Texture2DをSpriteに変換
            LoadSpriteRenderer.sprite = Sprite.Create(tex,
                new Rect(0.0f, 0.0f, tex.width, tex.height),
                new Vector2(0.5f, 0.5f));
            _loadBackgroundImage.ChangeBackgroundImageScale();
        }
    }
}