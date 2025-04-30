using UnityEngine;
using UnityEngine.Android;

public class PermissionRequestManager : MonoBehaviour
{
    // 静态单例实例
    public static PermissionRequestManager Instance { get; private set; }

    private string[] requiredPermissions = new string[]
    {
        Permission.ExternalStorageRead,
        Permission.ExternalStorageWrite
    };

    private void Awake()
    {
        // 确保单例的唯一性
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CheckAndRequestPermissions();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 检查并请求权限
    /// </summary>
    private void CheckAndRequestPermissions()
    {
        foreach (string permission in requiredPermissions)
        {
            if (!Permission.HasUserAuthorizedPermission(permission))
            {
                Permission.RequestUserPermission(permission);
            }
        }
    }
    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) ||
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
         
        }
        else
        {
           
        }
    }
    /// <summary>
    /// 手动调用请求权限
    /// </summary>
    public static void ManualRequestPermissions()
    {
        if (Instance != null)
        {
            Instance.CheckAndRequestPermissions();
        }
        else
        {
            Debug.LogError("PermissionRequestManager 实例未初始化，无法手动请求权限");
        }
    }
}