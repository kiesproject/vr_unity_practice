using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゴールにいる猫
public class Cat : MonoBehaviour
{
    public AudioClip myao;
    AudioSource audioSource;

    public float span = 3f;
    private float currentTime = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()//一定時間ごとに鳴き声
    {
        currentTime += Time.deltaTime;

        if (currentTime > span)
        {
            audioSource.PlayOneShot(myao);
           // Debug.LogFormat("{0}秒経過", span);
            currentTime = 0f;
        }
    }
}
