using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopMenu : MonoBehaviour
{
    public Transform ShopMenu;


    // Start is called before the first frame update
    private void Start()
    {
        InitShop();
    }

    // Update is called once per frame
    void Update() {}

    private void InitShop()
    {
        if(ShopMenu == null)
        {
            Debug.Log("No choices selected");
        }

        // Find all children buttons and add onClick
        int i = 0;
        foreach (Transform t in ShopMenu)
        {
            int currentIndex = i;

            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnItemSelect(currentIndex));

            i++;

        }
    }

    public void OnItemSelect(int currentIndex)
    {
        print("Selected item: " + currentIndex);
        Debug.Log("Selected item: " + currentIndex);
    }

    public void OnBuyButtonClick()
    {
        print("Buy button clicked.");
        Debug.Log("Buy button clicked.");
    }

}
