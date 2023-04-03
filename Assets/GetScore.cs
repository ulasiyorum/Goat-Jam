using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_Text>().text = "You've completed the storyline in: " + (int)Time.time + " seconds";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
