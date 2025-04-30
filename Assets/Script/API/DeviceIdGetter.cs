using UnityEngine;
using UnityEngine.Android;
using System;
using System.Reflection;

public class DeviceIdGetter : MonoBehaviour
{
    private const string ANDROID_ID_METHOD_NAME = "getAndroidId";
    private const string TELEPHONY_MANAGER_CLASS_NAME = "android.telephony.TelephonyManager";
    private const string READ_PHONE_STATE_PERMISSION = "android.permission.READ_PHONE_STATE";

    // 静态变量用于存储设备 ID
    public static string DeviceId="testUser";

    private void Start()
    {
        TryGetDeviceId((deviceId) =>
        {
            if (!string.IsNullOrEmpty(deviceId))
            {
                Debug.Log($"设备 ID: {deviceId}");
                DeviceId = deviceId; // 将获取到的设备 ID 赋值给静态变量
            }
            else
            {
                Debug.Log("未能获取到设备 ID。");
                DeviceId = "8888888";
            }
        });
    }

    public void TryGetDeviceId(Action<string> onDeviceIdReceived)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                TryGetAndroidDeviceId(onDeviceIdReceived);
                break;
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                string windowsDeviceId = GetWindowsDeviceId();
                onDeviceIdReceived?.Invoke(windowsDeviceId);
                break;
            default:
                Debug.LogError($"不支持的平台: {Application.platform}");
                onDeviceIdReceived?.Invoke(null);
                break;
        }
    }

    public void TryGetAndroidDeviceId(Action<string> onDeviceIdReceived)
    {
        if (Permission.HasUserAuthorizedPermission(READ_PHONE_STATE_PERMISSION))
        {
            string deviceId = GetAndroidId();
            onDeviceIdReceived?.Invoke(deviceId);
        }
        else
        {
            Permission.RequestUserPermission(READ_PHONE_STATE_PERMISSION);
            PermissionCallbacks callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += (permissionName) =>
            {
                if (permissionName == READ_PHONE_STATE_PERMISSION)
                {
                    string deviceId = GetAndroidId();
                    onDeviceIdReceived?.Invoke(deviceId);
                }
            };
            callbacks.PermissionDenied += (permissionName) =>
            {
                Debug.LogError("用户拒绝了读取手机状态权限，无法获取设备 ID。");
                onDeviceIdReceived?.Invoke(null);
            };
            Permission.RequestUserPermission(READ_PHONE_STATE_PERMISSION, callbacks);
        }
    }

    private string GetAndroidId()
    {
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject telephonyManager = currentActivity.Call<AndroidJavaObject>("getSystemService", "phone");

            if (telephonyManager != null)
            {
                string androidId = telephonyManager.Call<string>(ANDROID_ID_METHOD_NAME);
                return androidId;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("获取 Android ID 失败: " + e.Message);
        }

        return "8888888";
    }

    private string GetWindowsDeviceId()
    {
        try
        {
            string deviceId = SystemInfo.deviceUniqueIdentifier;
            if (!string.IsNullOrEmpty(deviceId))
            {
                return deviceId;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("获取 Windows 设备 ID 失败: " + e.Message);
        }
        return "8888888";
    }
}    