#if UNITY_ANDROID
using System;

using UnityEngine;

public class Plugin : MonoBehaviour
{
    #region PRIVATE_FIELDS
    private AndroidJavaClass loggerClass;
    private AndroidJavaObject loggerObject;
    
    private string output;
    private string stack;
    #endregion

    #region CONSTANTS
    private const string loggerClassName = "com.moviles.logger.logger";
    #endregion

    #region UNITY_CALLS
    void Start()
    {
        loggerClass = new AndroidJavaClass(loggerClassName);
        loggerObject = loggerClass.CallStatic<AndroidJavaObject>("GetInstance");

        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        loggerClass.SetStatic("mainActivity", activity);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowAlertDialog(new string[] { "Alert Title", "Alert Message", "Button 1", "Button 2 " }, obj =>
            {
                Debug.Log("Local Handler called: " + obj);
            });
        }
    }
    
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    #endregion

    #region PUBLIC_METHODS
    public void ButtonPressed()
    {
        Debug.Log("Pepega");
        //loggerObject.Call("MyLog", "BOTON");
    }

    public void ShowAlertDialog(string[] strings, Action<int> handler = null)
    {
        if (strings.Length < 3)
        {
            Debug.LogError("AlertView requires at least 3 strings");
            return;
        }

        loggerObject.Call("ShowAlertView", new object[] { strings, new AlertViewCallback(handler) });
    }
    #endregion

    #region PRIVATE_METHODS
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        Debug.Log("HANDLING LOG: " + output + " " + stack);
    }
    #endregion
}
#endif