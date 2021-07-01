using UnityEngine;

public abstract class PrefManager 
{

    public static void SetInt(string key, int value) => PlayerPrefs.SetInt(key, value);

    public static int GetInt(string key, int defValue = 0) => PlayerPrefs.GetInt(key, defValue);

    public static void SetFloat(string key, float value) => PlayerPrefs.SetFloat(key, value);

    public static float GetFloat(string key, float defValue =0) => PlayerPrefs.GetFloat(key, defValue);

    public static void SetString(string key, string value) => PlayerPrefs.SetString(key, value);

    public static string GetString(string key, string defValue = "") => PlayerPrefs.GetString(key, defValue);

    public static void SetBool(string key, bool value) => PlayerPrefs.SetInt(key, value ? 1 : 0);

    public static bool GetBool(string key, bool defValue=false) => PlayerPrefs.GetInt(key, defValue ? 1 : 0) == 1 ? true : false;

    public static bool HasKey(string key) => PlayerPrefs.HasKey(key);

    public static void ClearAll() => PlayerPrefs.DeleteAll();

    public static void Clear(string key) => PlayerPrefs.DeleteKey(key);

    public static void Save() => PlayerPrefs.Save();

}
