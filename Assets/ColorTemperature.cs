using UnityEngine;
using System.Collections;

public class ColorTemperature : MonoBehaviour
{
    //
    // Converts a color temperature (Kelvin) to sRGB value.
    //
    // This implementation is based on a Tanner Helland's approximation.
    // http://www.tannerhelland.com/4435/convert-temperature-rgb-algorithm-code/
    //
    // The coefficients have been slightly modified to get a continuous gradient,
    // and therefore it shouldn't be considered as a scientifically accurate color
    // temperature model.
    //
    static Color KelvinToColor(float k)
    {
        float r, g, b;

        k *= 0.01f;

        if (k < 66)
        {
            r = 1;
            g = 0.38855782260195315f * Mathf.Log(k) - 0.6279231240157355f;
            if (k < 19)
                b = 0;
            else
                b = 0.5410848875902343f * Mathf.Log(k - 10) - 1.1888850134384685f;
        }
        else
        {
            r = Mathf.Pow(k - 60, -0.1332047592f) / 0.7876740722020901f;
            g = Mathf.Pow(k - 60, -0.0755148492f) / 0.8734499527546277f;
            b = 1;
        }

        return new Color(r, g, b);
    }

    public float minTemp = 3000;
    public float maxTemp = 13000;

    public GUIStyle minLabel;
    public GUIStyle maxLabel;

    Texture2D texture;

    void Awake()
    {
        texture = new Texture2D(512, 1);
        renderer.material.mainTexture = texture;
    }

    void Update()
    {
        for (var x = 0; x < texture.width; x++)
        {
            var k = 1.0f * x / texture.width;
            var rgb = KelvinToColor(Mathf.Lerp(minTemp, maxTemp, k));
            texture.SetPixel(x, 0, rgb);
        }
        texture.Apply();
    }

    void OnGUI()
    {
        var rect = new Rect(0, 0, Screen.width, Screen.height);
        GUI.Label(rect, "<- " + minTemp + " [K]", minLabel);
        GUI.Label(rect, maxTemp + " [K] ->", maxLabel);
    }
}
