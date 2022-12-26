using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] Sounds;//0 - звук улучшения, 1 - звук просыпания по утрам

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(int id)
    {
        Sounds[id].Play();
    }
}
