using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidPermissionChecker : MonoBehaviour {

    public static int RUNTIME_PERMISSIONS_MIN_SDK_LEVEL = 23;
    public static string PERMISSION_RECORD_AUDIO = "android.permission.RECORD_AUDIO";

    public static int GetSDKLevel()
    {
        var clazz = AndroidJNI.FindClass("android/os/Build$VERSION");
        var fieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
        var sdkLevel = AndroidJNI.GetStaticIntField(clazz, fieldID);
        return sdkLevel;
    }

    public static AndroidJavaObject GetCurrentActivity()
    {
        return new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    }

    public static bool RuntimePermissionGranted(string permission)
    {
        if (GetSDKLevel() < RUNTIME_PERMISSIONS_MIN_SDK_LEVEL)
        {
            return true;
        }

        return GetCurrentActivity().Call<int>("checkSelfPermission", new object[] { permission }) == 0;
    }
}
