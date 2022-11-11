using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public string id = string.Empty;
    public int price = 0;
    [SerializeField] private TextMeshProUGUI priceTxt = null;
    [SerializeField] private Button button = null;

    private void Start()
    {
        priceTxt.text = "$" + price;
    }

    public void Init(Action<string> purchaseItem)
    {
        button.onClick.AddListener((() =>
        {
            purchaseItem?.Invoke(id);
        }));
    }
}