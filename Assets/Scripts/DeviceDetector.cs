using System.Runtime.InteropServices;
using UnityEngine;

public static class DeviceDetector
{
#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern bool IsiPad();
#endif

    public static bool IsRunningOniPad()
    {
#if UNITY_EDITOR
        return false; // Return false when running in the Unity Editor
#elif UNITY_WEBGL
        return IsiPad(); // Call the JavaScript function in a WebGL build
#else
        return false; // Return false for other non-WebGL builds
#endif
    }
}