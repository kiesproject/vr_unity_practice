using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    enum State
    {
        Dig,
        Ready,
        Play,
        End
    }

    State state;

    public float WaitTime = 5.0f;
    float t;
    public float TotalTime = 0.0f;

    private int Targetcount;

    [SerializeField] GameObject TimeText;
    [SerializeField] GameObject ScoreTime;
    [SerializeField] GameObject Reticle;

    [SerializeField] FPSCam fpsCam;
    [SerializeField] GunController gun;
    [SerializeField] TargetGenerator targetGenerator;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Dig;
        t = WaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Dig:
                Debug.Log("GeneratingStage");

                fpsCam.MoveEneble = false;
                gun.enabled = false;
                Reticle.SetActive(false);
                ScoreTime.SetActive(false);

                break;

            case State.Ready:
                Debug.Log("Ready?");
                targetGenerator.enabled = true;
                Reticle.SetActive(false);
                WaitForPlaying();
                CountTarget();

                fpsCam.MoveEneble = false;
                gun.enabled = false;

                break;

            case State.Play:
                Debug.Log("Play");
                Reticle.SetActive(true);

                TimeText.SetActive(false);

                fpsCam.MoveEneble = true;
                gun.enabled = true;

                TotalTime += Time.deltaTime;

                if (Targetcount <= 0) state = State.End;

                break;

            case State.End:
                Debug.Log("End");
                Reticle.SetActive(false);
                fpsCam.MoveEneble = false;
                gun.enabled = false;

                Ending();

                if (Input.GetKeyDown(KeyCode.L))
                {
                    SceneManager.LoadScene("TestScene");
                }

                break;
        }
    }
    
    //------
    public void GoReady()
    {
        state = State.Ready;
    }

    //------
    private void WaitForPlaying()
    {
        t -= Time.deltaTime;
        TimeText.GetComponent<Text>().text = t.ToString("F1");
        
        if(t <= 0)
        {
            state = State.Play;
        }
    }

    private void CountTarget()
    {
        Targetcount = GameObject.FindGameObjectsWithTag("Target").Length;
    }
    
    //------

    public void HitTarget()
    {
        Targetcount--;
    }

    //------

    void Ending()
    {
        ScoreTime.GetComponent<Text>().text = TotalTime.ToString("F3");
        ScoreTime.SetActive(true);
    }
}
