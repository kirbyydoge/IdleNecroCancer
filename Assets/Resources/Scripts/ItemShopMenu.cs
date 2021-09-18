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
        string category = "";
        
        switch(currentIndex)
        {
            case 0:
                print("load weapons menu");
                category = "Weapon";
                break;
            case 1:
                print("load characters menu");
                category = "Character";
                break;
            case 2:
                print("load armour menu");
                category = "Armour";
                break;
            case 3:
                print("load potions menu");
                category = "Potion";
                break;
            case 4:
                print("load boosters menu");
                category = "Booster";
                break;
            case 5:
                print("load skins menu");
                category = "Skin";
                break;
            default:
                break;

        }
        LoadSubShopPanel(category);
    }

    public void LoadSubShopPanel(string category)
    {
        SubShopPanel.gameObject.SetActive(true);
        
        // Destroy all previously created buttons except for tempButton
        foreach (Transform child in SubShopPanelContent.transform)
        {
            if (child.gameObject.name != "tempButton")
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        // Iterate through the items list to create items of the selected category
        foreach (var item in listOfItems.items)
        {
            if (item.category == category)
            {
                CreateButton(item.name, item.icon);
            }
        }

    }

    public void CreateButton(string text, string icon)
    {
        DefaultControls.Resources uiResources = new DefaultControls.Resources();
        uiResources.standard = buttonBg;

        // Create a button for SubShopMenu
        GameObject uiButton = DefaultControls.CreateButton(uiResources);
        uiButton.transform.SetParent(SubShopPanelContent.transform, false);
        uiButton.GetComponentInChildren<Text>().text = text;

        // Create an image for that button
        GameObject uiImage = DefaultControls.CreateImage(uiResources);
        uiImage.GetComponent<Image>().sprite = getItemIcon(icon);
        uiImage.transform.SetParent(uiButton.transform, false);
    }

    public Sprite getItemIcon(string iconName)
    {
        Texture2D tex = null;
        Sprite sp = null;
        byte[] fileData;

        string iconPath = "Assets\\Resources\\Assets\\Items\\" + iconName + ".png";

        if (System.IO.File.Exists(iconPath))
        {
            fileData = System.IO.File.ReadAllBytes(iconPath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            tex.filterMode = FilterMode.Point; // pixelated graphics for pixel art
        }

        sp = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        return sp;
    }

    public void tempHideSubMenu()
    {
        SubShopPanel.gameObject.SetActive(false);
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