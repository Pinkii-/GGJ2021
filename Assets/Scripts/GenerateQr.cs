using System;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class GenerateQr : MonoBehaviour
{

    public Renderer m_Target;

    public void Generate()
    {
        Generate("Lolaso");
    }

    public void Generate(string text)
    {
        if (!String.IsNullOrEmpty(text))
        {
            m_Target.material.mainTexture = generateQR(text);
        }
    }

    public static Texture2D generateQR(string text) {
        var encoded = new Texture2D (256*2, 256*2);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }
    
    private static Color32[] Encode(string textForEncoding, int width, int height) {
        var writer = new BarcodeWriter {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }
}
