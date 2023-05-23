using System.Collections;
using System.Collections.Generic;
using Gaia;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class ScreenshotHelper : MonoBehaviour
{
    [Button]
    void TakeCoolerScreenshot()
    {
        ScreenCapture.CaptureScreenshot("test.png");
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    public static void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot("test.png");
        AssetDatabase.Refresh();
    }
}