using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    private AudioSource source;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void Play(int id)
    {
        source.PlayOneShot(clips[id]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
