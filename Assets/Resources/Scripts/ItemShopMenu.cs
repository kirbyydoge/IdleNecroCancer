﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopMenu : MonoBehaviour
{
    public Transform CategoryPanel;
    public Transform SubShopPanel;
    public Transform SubShopPanelContent;
    public Transform SingleItemPanel;
    public Transform DiamondShopPanel;

    public ItemList listOfItems;
    public Sprite buttonBg;

    public IDictionary<string, Color> rarityColours = new Dictionary<string, Color>();


    // Start is called before the first frame update
    private void Start()
    {
        InitShop();
    }

    // Update is called once per frame
    void Update() {}

    private void InitShop()
    {
        if(CategoryPanel == null)
        {
            Debug.Log("No choices selected");
        }

        LoadShopMenuList();
        InitRarityColoursDictionary();

        // Find all children buttons of main shop and add onClick
        int i = 0;
        foreach (Transform trans in CategoryPanel)
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
                print("load diamonds menu");
                category = "Diamond";
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
        if (category == "Diamond")
        {
            LoadDiamondShopPage();
        } else {
            LoadSubShopPanel(category);
        }
    }

    public void LoadSubShopPanel(string category)
    {
        HideAllElements();
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
        uiImage.GetComponent<Image>().sprite = GetItemIcon(icon);
        uiImage.transform.SetParent(uiButton.transform, false);
    }

    public Sprite GetItemIcon(string iconName)
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

    public void TempHideSubMenu()
    {
        HideAllElements();
        SubShopPanel.gameObject.SetActive(false);
        CategoryPanel.gameObject.SetActive(true);

    }

    public void HideSingleItemMenu()
    {
        HideAllElements();
        SingleItemPanel.gameObject.SetActive(false);
        SubShopPanel.gameObject.SetActive(true);
    }

    public void HideAllElements()
    {
        CategoryPanel.gameObject.SetActive(false);
        SubShopPanel.gameObject.SetActive(false);
        SingleItemPanel.gameObject.SetActive(false);
        DiamondShopPanel.gameObject.SetActive(false);
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
        HideAllElements();
        SingleItemPanel.gameObject.SetActive(true);

        Item selectedItem = listOfItems.items[id];
        SingleItemPanel.gameObject.transform.Find("Icon").GetComponent<Image>().sprite = GetItemIcon(selectedItem.icon);
        SingleItemPanel.gameObject.transform.Find("Name").GetComponent<Text>().text = selectedItem.name;
        // append armour if armour, TODO: likely have a different type for armour rather than melee/ranged/mage?
        string desc = selectedItem.rarity + " " + selectedItem.type.ToLower() + (selectedItem.category == "Armour" ? " armour" : "");
        SingleItemPanel.gameObject.transform.Find("RarityType").GetComponent<Text>().text = desc;
        SingleItemPanel.gameObject.transform.Find("RarityType").GetComponent<Text>().color = rarityColours[selectedItem.rarity];
        SingleItemPanel.gameObject.transform.Find("Description").GetComponent<Text>().text = selectedItem.description;
        SingleItemPanel.gameObject.transform.Find("Price").GetComponent<Text>().text = selectedItem.price.ToString();
        SingleItemPanel.gameObject.transform.Find("ID").GetComponent<Text>().text = selectedItem.id.ToString(); // purchase button will pull from this for the selected page, ugly
    }

    public void InitRarityColoursDictionary()
    {
        rarityColours.Add("common", Color.white);
        rarityColours.Add("uncommon", Color.blue);
        rarityColours.Add("rare", Color.yellow);
        rarityColours.Add("very rare", Color.green);
        rarityColours.Add("legendary", new Color(1F, 0.647F, 0.0F)); // orange
    }

    public void LoadDiamondShopPage()
    {
        HideAllElements();
        DiamondShopPanel.gameObject.SetActive(true);

        // TODO: all other important stuff
    }

    public void HideDiamondShopPage()
    {
        HideAllElements();
        CategoryPanel.gameObject.SetActive(true);
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