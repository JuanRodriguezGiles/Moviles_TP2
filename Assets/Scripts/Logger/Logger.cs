#if UNITY_ANDROID
using System;

using UnityEngine;

public class Logger : Singleton<Logger>
{
    #region PRIVATE_FIELDS
    private AndroidJavaClass loggerClass;
    private AndroidJavaObject loggerObject;
    #endregion

    #region CONSTANTS
    private const string loggerClassName = "com.moviles.logger.logger";
    #endregion

    #region UNITY_CALLS
    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        loggerClass = new AndroidJavaClass(loggerClassName);
        loggerObject = loggerClass.CallStatic<AndroidJavaObject>("GetInstance");

        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

        loggerClass.SetStatic("mainActivity", activity);
#endif
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    #endregion

    #region PUBLIC_METHODS
    public void ShowAlertDialog(string[] strings, Action<int> handler = null)
    {
        if (strings.Length < 3)
        {
            Debug.LogError("AlertView requires at least 3 strings");
            return;
        }

        loggerObject.Call("ShowAlertView", strings, new AlertViewCallback(handler));
    }

    public string GetLogs()
    {
        return loggerObject.Call<string>("ReadFile");
    }

    public void ClearLogs()
    {
        loggerObject.Call("ClearLogs");
    }

    public void LogButton(string button)
    {
        Debug.Log("PRESSED " + button + " BUTTON");
    }
    #endregion

    #region PRIVATE_METHODS
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
         loggerObject.Call("MyLog", logString);
#else
        Debug.Log("Logger not active in editor");
#endif
    }
    #endregion
}
#endif