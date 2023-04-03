using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public double health;
    public int damage;
    public float stamina;
    public int balance;
    public int defence;

    [SerializeField] Transform attackPoint;
    public static PlayerAttributes instance;
    public bool tutorial;
    public int temporaryDamage;
    [SerializeField] GameObject endPanel;
    [SerializeField] TMP_Text coinText;
    [SerializeField] Image healthBar;
    [SerializeField] Image staminaBar;
    [SerializeField] TMP_Text defenceText;

    public double Health
    {
        get => health;
        set
        {
            health = value;
            healthBar.fillAmount = (float)health / 100;
        }
    }

    public int Damage
    {
        get => damage + temporaryDamage;
    }

    private void Awake()
    {
        endPanel.SetActive(false);
        tutorial = SceneManager.GetActiveScene().name == "DemoScene";
        temporaryDamage = 0;
        instance = this;
        stamina = 100;
        health = 100;
        damage = 35;
    }

    private void Update()
    {
        if (tutorial)
            return;

        defenceText.text = defence.ToString();
        coinText.text = balance.ToString();
        staminaBar.fillAmount = stamina / 100f;
        if (stamina < 100)
            stamina += Time.deltaTime * 2f;
    }
    public void TakeDamage(int damage) 
    {

        double actualDamage = damage / (1 + (defence * 0.025));
        health -= actualDamage;

        healthBar.fillAmount = (float)health / 100f;

        AudioManager.instance.Play(1);

        if(health <= 0)
        {
            GetComponent<Animator>().Play("Death");
            endPanel.SetActive(true);
            GetComponent<PlayerMotor>().enabled = false;
        }
        else
        {
            GetComponent<Animator>().Play("GetHit");
        }
    }
}
