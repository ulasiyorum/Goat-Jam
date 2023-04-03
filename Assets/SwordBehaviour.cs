using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    private float coolDown;
    // Start is called before the first frame update
    void Start()
    {
        coolDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coolDown += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (gameObject.tag == "PlayerSword" && collision.gameObject.tag == "Enemy" && (PlayerMotor.instance.attacking || PlayerMotor.instance.attacking2))
        {
            collision.gameObject.GetComponent<MobMotor>().TakeDamage(PlayerAttributes.instance.Damage);
            PlayerMotor.instance.attacking2 = false;

        } else if(collision.gameObject.tag == "Player" && gameObject.tag == "EnemyAxe" && MobAttackController.attacking && coolDown > 1.75f)
        {
            collision.gameObject.GetComponent<PlayerAttributes>().TakeDamage(MobMotor.damage);
            coolDown = 0f;
        }
    }

}
