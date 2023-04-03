using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            fadeOut.SetActive(true);
    }
}
