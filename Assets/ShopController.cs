using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] GameObject shop;

    [SerializeField] TMP_Text info;
    [SerializeField] TMP_Text healthPotionText;
    [SerializeField] TMP_Text defText;
    [SerializeField] TMP_Text adText;
    private int adCount;
    private int healthPotionCount;
    private int defCount;
    private bool trigger;
    // Start is called before the first frame update
    void Start()
    {

        adText.text = "20";
        defText.text = "30";
        healthPotionText.text = "20";
        healthPotionCount = 0;
        defCount = 0;
        adCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!trigger)
            return;
        bool shopActive = shop.activeInHierarchy;
        if (Input.GetKeyDown(KeyCode.E))
        {
            shop.SetActive(!shopActive);
        }

        if (shopActive)
            info.text = "Press 'E' to close shop";
        else if (!shopActive)
            info.text = "Press 'E' to open shop";
    }

    public void BuyAttribute(int id)
    {
        if(id == 0)
        {
            if (PlayerAttributes.instance.balance < 30 + (10 * defCount))
            {
                StartPopUpMessage.Message("Not Enough Balance!", Color.red);
                return;
            }
            StartPopUpMessage.Message("Bought!", Color.green);
            PlayerAttributes.instance.balance -= (30 + (10 * defCount));
            defCount++;
            PlayerAttributes.instance.defence += 10;
            defText.text = (30 + (10 * defCount)).ToString();
        }
        else
        {
            if (PlayerAttributes.instance.balance < 20 + (15 * adCount))
            {
                StartPopUpMessage.Message("Not Enough Balance!", Color.red);
                return;
            }
            StartPopUpMessage.Message("Bought!", Color.green);
            PlayerAttributes.instance.balance -= (20 + (15 * adCount));
            adCount++;
            PlayerAttributes.instance.damage += 10;
            adText.text = (20 + (15 * adCount)).ToString();
        }
    }

    public void BuyPotion(int potion)
    {
        if(potion == 0)
        {
            if (PlayerAttributes.instance.balance < 20 + (5 * healthPotionCount))
            {
                StartPopUpMessage.Message("Not Enough Balance!", Color.red);
                return;
            }
            StartPopUpMessage.Message("Bought!", Color.green);
            PlayerAttributes.instance.balance -= (20 + (5 * healthPotionCount));
            healthPotionCount++;
            PlayerAttributes.instance.Health = 100;
            healthPotionText.text = (20 + (5 * healthPotionCount)).ToString();  
        } 
        else
        {
            if (PlayerAttributes.instance.balance < 30)
            {
                StartPopUpMessage.Message("Not Enough Balance!", Color.red);
                return;
            }
            StartPopUpMessage.Message("Bought!", Color.green);
            PlayerAttributes.instance.balance -= 30;
            PlayerAttributes.instance.stamina = 100;
            PlayerAttributes.instance.temporaryDamage = 15;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        trigger = true;
        info.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;
        trigger = false;
        info.gameObject.SetActive(false);
        shop.SetActive(false);
    }

}
