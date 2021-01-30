using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using ZXing;

public class LoadFile : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ImageUploaderCaptureClick();
    public string FilePath;
    //For Game Object where you want to apply the texture
    public GameObject Player;
    private Renderer renderer;
    private int i=0;
 
    //  Call on Start
    void Start()
    {
//  Set player object and textures
//  In this case, I have assigned the tag "Player" to my game object of interest, where the texture is applied.
        renderer = Player.GetComponent<Renderer>();
    }
 
    public void OnButtonPointerDown () {
        //File browser when using Unity
#if UNITY_EDITOR
        FilePath = UnityEditor.EditorUtility.OpenFilePanel("Open image","","jpg,png");
        if (!System.String.IsNullOrEmpty (FilePath))
            FileSelected (FilePath);
#else
    //File browser when using WebGL
    ImageUploaderCaptureClick ();
#endif
    }
    // Function reads your selected file and loads it as a texture, so it's ready to be used in a material.
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
                renderer.materials[i].mainTexture = texture;
                
                IBarcodeReader barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(texture.GetPixels32(), texture.width, texture.height);
                
                if (result != null) {
                    Debug.Log("DECODED TEXT FROM QR: " + result.Text);
                }
                else
                {
                    Debug.Log("A iorar");
                }
            }
        }
    }
}
