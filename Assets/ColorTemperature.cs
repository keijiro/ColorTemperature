using UnityEngine;
using System.Collections;

public class ColorTemperature : MonoBehaviour
{
    Texture2D texture;

    static float Gamma_sRGB(float c)
    {
        c = Mathf.Clamp01(c);
        if (c <= 0.0031308f)
        {
            return 12.92f * c;
        }
        else
        {
            var a = 0.055f;
            return (1.0f + a) * Mathf.Pow(c, 1.0f / 2.4f) - a;
        }
    }

    static Color CIExy_To_sRGB(float x, float y)
    {
        var Y = 0.5f;
        var X = Y * x / y;
        var Z = Y * (1.0f - x - y) / y;

        var R = Gamma_sRGB( 3.2406f * X - 1.5372f * Y - 0.4986f * Z);
        var G = Gamma_sRGB(-0.9689f * X + 1.8758f * Y + 0.0415f * Z);
        var B = Gamma_sRGB( 0.0557f * X - 0.2040f * Y + 1.0570f * Z);

        return new Color(R, G, B);
    }

    static float GetY_StandardIlluminant(float x)
    {
        // An analytical model of chromaticity of the standard illuminant by Judd et al.
        // http://en.wikipedia.org/wiki/Standard_illuminant#Illuminant_series_D
        // Slightly modifed to adjust it with D65 white point (x=0.31271, y=0.32902).
        return 2.87f * x - 3.0f * x * x - 0.27509507f;
    }

    void Awake()
    {
        texture = new Texture2D(512, 256);
        renderer.material.mainTexture = texture;
    }

    void Start()
    {
        for (var sy = 0; sy < texture.height; sy++)
        {
            for (var sx = 0; sx < texture.width; sx++)
            {
                var rx = 2.0f * sx / texture.width  - 1.0f;
                var ry = 3.0f * sy / texture.height - 1.0f;

                // 0.31271 = x value on the D65 white point.
                var x = 0.31271f - (rx < 0.0f ? 0.15f : 0.1f) * rx;
                var y = GetY_StandardIlluminant(x);

                // Apply the color tint.
                if (ry < 1.0f)
                    y += (ry < 0.0f ?  0.1f  :  0.2f) * ry;

                texture.SetPixel(sx, sy, CIExy_To_sRGB(x, y));
            }
        }
        texture.Apply();
    }

    void OnGUI()
    {
        var sw = Screen.width;
        var sh = Screen.height;
        GUI.color = Color.black;
        GUI.Label(new Rect(0, 0, sw, 100), "Color temperature curve with an approximation of the CIE standard illuminant.\nThe white point (D65) is placed at the center of screen.");
        GUI.Label(new Rect(0, sh / 3, sw, 100), "Color temperature + tint (magenta-green).");
    }
}
