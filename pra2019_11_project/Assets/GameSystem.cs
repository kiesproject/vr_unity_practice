using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Practice");
    }
    public void EndGame()
    {
        //*** =====================================================================================
        //*** [注意]UnityEditorはUnityエディターでゲームを動かしている時にのみ使うことができます。
        //***       すなわち、ビルドして実行ファイル化した時には使えなくなるので注意です。
        //*** =====================================================================================

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
