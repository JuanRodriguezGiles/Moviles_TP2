using UnityEngine;

public class Plugin : MonoBehaviour
{
#if UNITY_ANDROID
    private const string loggerClassName = "com.example.logger2022.mainLogger";

    private AndroidJavaClass loggerClass;
    private AndroidJavaObject loggerObject;

    void Start()
    {
        loggerClass = new AndroidJavaClass(loggerClassName);
        loggerObject = loggerClass.CallStatic<AndroidJavaObject>("GetInstance");
    }
    
    public void ButtonPressed()
    {
        loggerObject.Call("MyLog", "BOTON");
    }
#endif
}