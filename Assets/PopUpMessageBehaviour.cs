using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpMessageBehaviour : MonoBehaviour
{
    private TMP_Text instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        instance.color -= new Color(0, 0, 0, Time.deltaTime / 2);

        transform.position += new Vector3(0, Time.deltaTime * 12f, 0);

        if (instance.color.a <= 0)
            Destroy(gameObject);
    }
}
