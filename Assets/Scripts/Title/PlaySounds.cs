using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{

    public List<string> filename;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        filename.ForEach(file =>
        {
            Play(this.index, file);
            this.index++;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(int index, string name)
    {

        AudioSource[] audio = gameObject.GetComponents<AudioSource>();
        AudioClip clip = (AudioClip)Resources.Load(name);

        audio[index].clip = clip;
        audio[index].volume = 0.5f;
        audio[index].Play();
    }
}
