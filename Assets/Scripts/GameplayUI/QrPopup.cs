using QrStuff;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayUI
{
    public class QrPopup : MonoBehaviour
    {
        public Image m_QrVisualizer;
        private static readonly int MaterialTexture = Shader.PropertyToID("QrTexture");

        private void OnEnable()
        {
            var password = GameManagerScript.gameManagerRef.GeneratePasswordFromMessages();
            var qrTexture = GenerateQr.generateQR(password);

            m_QrVisualizer.material.SetTexture(MaterialTexture, qrTexture);
        }

        public void DownloadQR()
        {
            WebGLFileSaver.SaveFile(GameManagerScript.gameManagerRef.GeneratePasswordFromMessages());
        }
    }
}
