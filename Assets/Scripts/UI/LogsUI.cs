using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class LogsUI : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private Button clearLogsBtn = null;
    [SerializeField] private Button mainMenuBtn = null;
    [SerializeField] private TMP_Text logsTxt = null;
    #endregion

    #region CONSTANTs
    private const int yesCode = -3;
    #endregion

    #region UNITY_CALLS
    private void Start()
    {
        logsTxt.text = Logger.Instance.GetLogs();

        clearLogsBtn.onClick.AddListener((() =>
        {
            Logger.Instance.LogButton("CLEAR LOGS");
            Logger.Instance.ShowAlertDialog(new string[] { "CLEAR LOGS", "This will delete all logs, are you sure?", "YES", "NO" }, obj =>
            {
                Debug.Log("Local Handler called: " + obj);
                if (obj == yesCode)
                {
                    Logger.Instance.ClearLogs();
                    logsTxt.text = Logger.Instance.GetLogs();
                }
            });
        }));

        mainMenuBtn.onClick.AddListener((() =>
        {
            Logger.Instance.LogButton("MAIN MENU");
            GameManager.Instance.LoadScene(SCENES.MAIN_MENU);
        }));
    }
    #endregion
}