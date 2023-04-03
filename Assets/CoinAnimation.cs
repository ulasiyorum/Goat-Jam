using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{

    private float up = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * 200f);

        if (up + Time.deltaTime < 0.8f)
        {
            transform.position += new Vector3(0, Time.deltaTime / 1.125f, 0);
            up += Time.deltaTime;
        }
        else if (up + Time.deltaTime < 1.6f)
        {
            transform.position -= new Vector3(0, Time.deltaTime / 1.125f, 0);
            up += Time.deltaTime;
        }
        else
        {
            up = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        PlayerAttributes.instance.balance += 10;
        Destroy(gameObject);
    }
}
