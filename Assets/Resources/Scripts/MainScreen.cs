using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    
    //Bottom navigation bar buttons
    public Transform navBarInventoryButton;
    public Transform navBarBattleButton;
    public Transform navBarShopButton;

    // Start is called before the first frame update
    void Start()
    {
        //Bottom navigation bar
        navBarInventoryButton.GetComponent<Button>().onClick.AddListener(onClickInventory);
        navBarBattleButton.GetComponent<Button>().onClick.AddListener(onClickBattle);
        navBarShopButton.GetComponent<Button>().onClick.AddListener(onClickShop);


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Bottom navigation bar button on click methods
    public void onClickInventory()
    {
        print("Inventory?");
    }
    public void onClickBattle()
    {
        print("Battle!");
    }
    public void onClickShop()
    {
        print("Shop:)");
    }
}
