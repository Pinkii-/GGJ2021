using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using ZXing;

namespace QrStuff
{
    [RequireComponent(typeof(Button))]
    public class LoadFile : MonoBehaviour, IPointerClickHandler
    {
        private string m_FilePath;
        public UnityEvent<string> m_OnQrLoaded;


        [DllImport("__Internal")]
        private static extern void ImageUploaderCaptureClick();
        
        public void OnButtonPointerDown () {
            //File browser when using Unity
#if UNITY_EDITOR
            m_FilePath = UnityEditor.EditorUtility.OpenFilePanel("Open image","","jpg,png");
            if (!System.String.IsNullOrEmpty (m_FilePath))
                FileSelected (m_FilePath);
#else
    //File browser when using WebGL
    ImageUploaderCaptureClick ();
#endif
        }
        
        void FileSelected (string url) {
            StartCoroutine(LoadTexture (url));
        }
        IEnumerator LoadTexture (string url) {
            using(UnityWebRequest www = UnityWebRequestTexture.GetTexture(url)){
                yield return www.SendWebRequest();
                if(www.isNetworkError || www.isHttpError) {
                    Debug.Log(www.error);
                }
                else {
                    //Apply texture to specific material slot
                    var texture = DownloadHandlerTexture.GetContent(www);
                
                    IBarcodeReader barcodeReader = new BarcodeReader();
                    var result = barcodeReader.Decode(texture.GetPixels32(), texture.width, texture.height);
                
                    if (result != null) {
                        m_OnQrLoaded.Invoke(result.Text);
                    }
                    else
                    {
                        Debug.Log("Not being able to read QR");
                    }
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnButtonPointerDown();
            }
        }
    }
}
