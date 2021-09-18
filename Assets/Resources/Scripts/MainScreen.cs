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
    //Resource bar
    public Text breadCount;
    public Text woodCount;
    public Text stoneCount;
    public Text goldCount;
    public Text diamondCount;


    // Start is called before the first frame update
    void Start()
    {
        //Bottom navigation bar
        navBarInventoryButton.GetComponent<Button>().onClick.AddListener(onClickInventory);
        navBarBattleButton.GetComponent<Button>().onClick.AddListener(onClickBattle);
        navBarShopButton.GetComponent<Button>().onClick.AddListener(onClickShop);

        //Resources        
        breadCount.text =  Random.Range(0,9999).ToString();
        woodCount.text = Random.Range(0, 9999).ToString();
        stoneCount.text = Random.Range(0, 9999).ToString();
        goldCount.text = Random.Range(0, 9999).ToString();
        diamondCount.text = Random.Range(0, 9999).ToString();

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
