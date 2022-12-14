using System.Collections.Generic;

using Facebook.Unity;

using TMPro;

using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private ShopItem[] items = null;
    [SerializeField] private TextMeshProUGUI moneyTxt = null;
    
    private ShopData shopData;

    public void Init()
    {
        moneyTxt.text = "$" + GameManager.Instance.money;

        if (PlayerPrefs.HasKey("ShopData"))
        {
            string json = PlayerPrefs.GetString("ShopData");
            shopData = JsonUtility.FromJson<ShopData>(json);
            
            for (int i = 0; i < items.Length; i++)
            {
                if (shopData.purchasedIds.Contains(items[i].id))
                {
                    items[i].gameObject.SetActive(false);
                }
                else
                {
                    items[i].Init(PurchaseItem);
                }
            }
        }
        else
        {
            shopData = new ShopData();
            shopData.purchasedIds = new List<string>();
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Init(PurchaseItem);
            }
        }
    }

    private void PurchaseItem(string id)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].id == id)
            {
                if (GameManager.Instance.money >= items[i].price)
                {
                    GameManager.Instance.money -= items[i].price;
                    PlayerPrefs.SetInt("Money", GameManager.Instance.money);
                    moneyTxt.text = "$" + GameManager.Instance.money.ToString();
                    shopData.purchasedIds.Add(items[i].id);
                    items[i].gameObject.SetActive(false);
                    PlayerPrefs.SetString("ShopData", JsonUtility.ToJson(shopData));

                    if (shopData.purchasedIds.Count == 1)
                    {
                        Social.ReportProgress(GPGSIds.achievement_ballin, 100.0f, success =>
                        {
                            if (success)
                            {
                                Debug.Log("Unlocked achievement");
                            }
                            else
                            {
                                Debug.Log("Failed to unlock achievement");
                            }
                        });
                    }

                    if (shopData.purchasedIds.Count == items.Length)
                    {
                        Social.ReportProgress(GPGSIds.achievement_shopping_spree, 100.0f, success =>
                        {
                            if (success)
                            {
                                Debug.Log("Unlocked achievement");
                            }
                            else
                            {
                                Debug.Log("Failed to unlock achievement");
                            }
                        });
                    }
                    
                    var softPurchaseParameters = new Dictionary<string, object>();
                    softPurchaseParameters["BallBouncer_purchased_item"] = items[i];
                    FB.LogAppEvent(Facebook.Unity.AppEventName.SpentCredits, (float)items[i].price, softPurchaseParameters);
                }
            }
        }
    }
}