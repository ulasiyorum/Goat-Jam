using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MobMotor : MonoBehaviour
{
    private Transform target;
    private Animator anim;

    public int health = 100;
    public static int damage = 10;

    private int combo = 0;
    private async void Start()
    {
        anim = GetComponent<Animator>();
        await Task.Delay(3500);
        target = PlayerMotor.instance.transform;
    }

    private void Update()
    {
        if (target == null || PlayerAttributes.instance.tutorial)
            return;

        Chase();
    }
    private void Chase()
    {
        if (PlayerAttributes.instance.health <= 0)
            return;

        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) < 2f)
        {
            if (MobAttackController.isPlaying)
                return;

            anim.Play("Attack0" + combo);
            combo++;
            if (combo >= 3)
                combo = 0;

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 1.25f);
            anim.Play("Walk00");
        }
    }


    public void TakeDamage(int damage)
    {
        if(PlayerAttributes.instance.tutorial)
        {
            anim.Play("Dead00");
            Destroy(gameObject, 2.5f);
            return;
        }


        if (health <= 0)
            return;

        AudioManager.instance.Play(2);
        int random = Random.Range(0, 2);
        if (damage > health)
        {
            MobSpawner.instance.enemies.Remove(gameObject);
            anim.Play("Dead0" + random);
            Destroy(gameObject, 2.5f);
            health -= damage;
            GetComponent<CapsuleCollider>().isTrigger = true;
            GameObject go = Instantiate(GameManager.instance.coinPrefab);
            go.transform.position = transform.position;
            this.enabled = false;
        } else
        {
            anim.Play("Damage0" + random);
            health -= damage;
        }
    }
}
