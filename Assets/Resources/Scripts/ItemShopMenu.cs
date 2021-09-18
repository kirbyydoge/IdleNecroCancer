using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopMenu : MonoBehaviour
{
    public Transform ShopMenu;
    public ItemList listOfItems;
    public Transform SubShopPanel;
    public Transform SubShopPanelContent;
    public Sprite buttonBg;


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
        string type = "";
        
        switch(currentIndex)
        {
            case 0:
                print("load weapons menu");
                type = "Weapon";
                break;
            case 1:
                print("load characters menu");
                type = "Character";
                break;
            case 2:
                print("load armour menu");
                type = "Armour";
                break;
            case 3:
                print("load potions menu");
                type = "Potion";
                break;
            case 4:
                print("load boosters menu");
                type = "Booster";
                break;
            case 5:
                print("load skins menu");
                type = "Skin";
                break;
            default:
                break;

        }
        LoadSubShopPanel(type);

    }

    public void LoadSubShopPanel(string type)
    {
        SubShopPanel.gameObject.SetActive(true);

        print(type);

        foreach (var item in listOfItems.items)
        {
            print(item.category);
            if (item.category == type)
            {
                print("match, creating button");
                CreateButton(item.name);
            }
        }

    }

    public void CreateButton(string text)
    {
        DefaultControls.Resources uiResources = new DefaultControls.Resources();
        uiResources.standard = buttonBg;
        GameObject uiButton = DefaultControls.CreateButton(uiResources);
        
        uiButton.transform.SetParent(SubShopPanelContent.transform, false);
        uiButton.GetComponentInChildren<Text>().text = text;
    }

    public void OnBuyButtonClick()
    {
        print("Buy button clicked.");
    }

    private void populateItemList() // temporary function to fill the shop item list
    {

    }


    [SerializeField]
    private TextAsset itemListJSON = null;

    public void LoadShopMenuList()
    {
        if (itemListJSON != null)
        {           
            string dataAsJSON = itemListJSON.text;            
            listOfItems = JsonUtility.FromJson<ItemList>("{\"items\":" + dataAsJSON + "}");
            /*
            foreach (var item in listOfItems.items)
            {
                item.printItem();
            }
            */
        }
        else
        {
            print("No internet connection or something?");
        }
    }

}

[System.Serializable]
public class ItemList
{
    public Item[] items;
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string category;
    public string type;
    public string bodySlot;
    public string rarity;
    public int price;
    public string icon;
    public string description;

    public void printItem()
    {
        Debug.Log(
            id + "\n" +
            name + "\n" +
            category + "\n" +
            type + "\n" +
            bodySlot + "\n" +
            rarity + "\n" +
            price + "\n" +
            icon + "\n" +
            description + "\n"
            );
    }
}