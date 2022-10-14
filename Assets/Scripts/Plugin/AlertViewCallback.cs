using System;

using UnityEngine;

public class AlertViewCallback : AndroidJavaProxy
{
    #region PRIVATE_FIELDS
    private Action<int> alertHandler; 

    #endregion
    
    #region CONSTANTS
    private const string loggerClassName = "com.moviles.logger.logger";
    #endregion

    #region CONSTRUCTOR
    public AlertViewCallback(Action<int> alertHandler) : base(loggerClassName + "$AlertViewCallback")
    {
        this.alertHandler = alertHandler;
    }
    
    public AlertViewCallback(string javaInterface) : base(javaInterface)
    {
        
    }

    public AlertViewCallback(AndroidJavaClass javaInterface) : base(javaInterface)
    {
        
    }
    #endregion

    #region PUBLIC_METHODS
    public void OnButtonTapped(int index)
    {
        Debug.Log("Button tapped: " + index);
        if (alertHandler != null)
        {
            alertHandler(index);
        }
    }
    #endregion
}