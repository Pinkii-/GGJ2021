using System;
using System.IO;
using System.Runtime.InteropServices;
using SFB;
using UnityEngine;

namespace QrStuff
{
    public class WebGLFileSaver : MonoBehaviour
    {

        public string m_filename;

        public static void SaveFile(string text)
        {
            if (String.IsNullOrEmpty(text)) return;

            var qr = GenerateQr.generateQR(text);

            var pngBytes = qr.EncodeToPNG();
            
            
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                var extensionList = new [] {
                    new ExtensionFilter("Image", "png"),
                };
                var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "memories", extensionList);
                File.WriteAllBytes(path, pngBytes);
            }
            else
            {
                var encodedText = System.Convert.ToBase64String(pngBytes);
                var image_url = "data:image/png;base64," + encodedText;
                SaveScreenshotWebGL("memories.png", image_url);
            }
        }
    
        [DllImport("__Internal")]
        private static extern void SaveScreenshotWebGL(string filename, string data);
    }
}
