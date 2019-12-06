using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム内でのBGMやSE呼び出しを行うプログラム
/// </summary>
//シングルトン、このオブジェクトは一つのみあることを保証する


public class SoundManager : MonoBehaviour
{
    //*** ===========================================================================================================================
    //*** [改善]どうせならAudioClip群もSoundManagerで管理した方がスッキリいくと思います。
    //***       音を鳴らす処理は全部このクラスに書いておいて、プレイヤー等はメソッドを呼び出すだけにした方が見やすくもなるかと思います。
    //*** ===========================================================================================================================

    public AudioSource SE;
    public static SoundManager instance = null;

    void Awake()
    {//ゲーム開始時にSoundManagerをinstanceに指定
        if (instance == null)
        {
            instance = this;
        }
        //もしこのオブジェクト以外にsoundmanagaerが存在するなら破壊
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //シーンを変遷してもこのオブジェクトは受け継ぐ
        DontDestroyOnLoad(this);
    }

    public void RandomSE(params AudioClip[] clips){
        //受け取った目的地到着時の効果音をランダムで指定
        int randomIndex = Random.Range(0, clips.Length);
        //効果音を選択する
        SE.clip = clips[randomIndex];
        //再生
        SE.Play();

    }
}
