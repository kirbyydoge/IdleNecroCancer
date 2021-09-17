using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopMenu : MonoBehaviour
{
    public Transform ShopMenu;
    public Item ItemList;


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

        LoadShopMenuList();

        // Find all children buttons of main shop and add onClick
        int i = 0;
        foreach (Transform t in ShopMenu)
        {
            int currentIndex = i;

            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnShopLabelSelect(currentIndex));

            i++;

        }
    }

    public void OnShopLabelSelect(int currentIndex)
    {
        print("Selected item: " + currentIndex);
        
        switch(currentIndex)
        {
            case 0:
                print("load weapons menu");
                break;
            case 1:
                print("load characters menu");
                break;
            case 2:
                print("load armour menu");
                break;
            case 3:
                print("load potions menu");
                break;
            case 4:
                print("load boosters menu");
                break;
            case 5:
                print("load skins menu");
                break;
            default:
                break;

        }
    }

    public void OnBuyButtonClick()
    {
        print("Buy button clicked.");
    }

    private void populateItemList() // temporary function to fill the shop item list
    {

    }


    [SerializeField]
    private TextAsset itemList = null;

    public void LoadShopMenuList()
    {
        if (itemList != null)
        {
            string dataAsJSON = itemList.text;
            // print(dataAsJSON);
            ItemList = JsonUtility.FromJson<Item>(dataAsJSON);
            print(ItemList); // FIXME: how to get data from this?
        }
        else
        {
            print("No internet connection or something?");
        }
    }

}

public class Item
{
    private int id;
    private string name;
    private string category;
    private string type;
    private string bodySlot;
    private string rarity;
    private int price;
    private string icon;
    private string description;
}