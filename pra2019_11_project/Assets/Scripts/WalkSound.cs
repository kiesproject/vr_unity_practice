using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アニメーションイベント用　足音を鳴らす
public class WalkSound : MonoBehaviour
{
    public AudioClip walk;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void aruku() {
            audioSource.PlayOneShot(walk);

    }
}
