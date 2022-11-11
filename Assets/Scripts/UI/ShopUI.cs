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
            }
        }
        else
        {
            shopData = new ShopData();
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
                    moneyTxt.text = "$" + GameManager.Instance.money.ToString();
                    items[i].gameObject.SetActive(false);
                    shopData.purchasedIds.Add(items[i].id);
                    PlayerPrefs.SetString("ShopData", JsonUtility.ToJson(shopData));
                }
            }
        }
    }
}