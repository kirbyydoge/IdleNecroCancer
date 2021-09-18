using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopMenu : MonoBehaviour
{
    public Transform ShopMenu;
    public Transform SubShopPanel;
    public Transform SubShopPanelContent;
    public Transform SingleItemPanel;

    public ItemList listOfItems;
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
        foreach (Transform trans in ShopMenu)
        {
            int currentIndex = i;

            Button butt = trans.GetComponent<Button>();
            butt.onClick.AddListener(() => OnShopLabelSelect(currentIndex));

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
        hideAllElements();
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
                CreateButton(item.name, item.icon, item.id);
            }
        }

    }

    public void CreateButton(string text, string icon, int id)
    {
        DefaultControls.Resources uiResources = new DefaultControls.Resources();
        uiResources.standard = buttonBg;

        // Create a button for SubShopMenu
        GameObject uiButton = DefaultControls.CreateButton(uiResources);
        uiButton.transform.SetParent(SubShopPanelContent.transform, false);
        uiButton.GetComponentInChildren<Text>().text = text;

        uiButton.GetComponent<Button>().onClick.AddListener(() => DisplaySingleItemPage(id));

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
        hideAllElements();
        SubShopPanel.gameObject.SetActive(false);
        ShopMenu.gameObject.SetActive(true);

    }

    public void hideSingleItemMenu()
    {
        hideAllElements();
        SingleItemPanel.gameObject.SetActive(false);
        SubShopPanel.gameObject.SetActive(true);
    }

    public void hideAllElements()
    {
        ShopMenu.gameObject.SetActive(false);
        SubShopPanel.gameObject.SetActive(false);
        SingleItemPanel.gameObject.SetActive(false);
    }

    public void OnBuyButtonClick()
    {
        print("Buy button clicked.");
        // pull the id of selected item from the invisible id text on single item page
        Item selectedItem = listOfItems.items[int.Parse(SingleItemPanel.gameObject.transform.Find("ID").GetComponent<Text>().text)];
        print("Selected item name, price, id:" + selectedItem.name + "," + selectedItem.price.ToString() + "," + selectedItem.id.ToString());
    }

    public void DisplaySingleItemPage(int id)
    {
        hideAllElements();
        SingleItemPanel.gameObject.SetActive(true);

        Item selectedItem = listOfItems.items[id];
        SingleItemPanel.gameObject.transform.Find("Icon").GetComponent<Image>().sprite = getItemIcon(selectedItem.icon);
        SingleItemPanel.gameObject.transform.Find("Name").GetComponent<Text>().text = selectedItem.name;
        SingleItemPanel.gameObject.transform.Find("RarityType").GetComponent<Text>().text = selectedItem.rarity + " " + selectedItem.type;
        SingleItemPanel.gameObject.transform.Find("RarityType").GetComponent<Text>().color = getRarityColour(selectedItem.rarity);
        SingleItemPanel.gameObject.transform.Find("Description").GetComponent<Text>().text = selectedItem.description;
        SingleItemPanel.gameObject.transform.Find("Price").GetComponent<Text>().text = selectedItem.price.ToString();
        SingleItemPanel.gameObject.transform.Find("ID").GetComponent<Text>().text = selectedItem.id.ToString(); // purchase button will pull from this for the selected page, ugly
    }

    public Color getRarityColour(string rarity)
    {
        Color itemColour = Color.white;
        switch (rarity)
        {
            case "common":
                itemColour = Color.white;
                break;
            case "uncommon":
                itemColour = Color.blue;
                break;
            case "rare":
                itemColour = Color.yellow;
                break;
            case "very rare":
                itemColour = Color.green;
                break;
            case "legendary":
                itemColour = new Color(1F, 0.647F, 0.0F); // orange
                break;
            default:
                break;
        }
        return itemColour;
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