using UnityEngine;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public class SHA1Getter : MonoBehaviour
{
    [SerializeField] private string keystorePath;
    [SerializeField] private string keystorePassword;
    [SerializeField] private string keyAlias;
    [SerializeField] private string keyPassword;

    void Start()
    {
        GetSHA1();
    }

    void GetSHA1()
    {
        try
        {
            byte[] keystoreBytes = File.ReadAllBytes(keystorePath);
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(keystoreBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                    if (i < hashBytes.Length - 1)
                    {
                        sb.Append(":");
                    }
                }
                string sha1Hash = sb.ToString();
                Debug.Log("SHA1: " + sha1Hash);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error getting SHA1: " + e.Message);
        }
    }
}