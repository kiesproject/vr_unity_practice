using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : ItemObject
{
    

    private void Start()
    {
        item = new Gold();
    }

    private void Update()
    {
        if (GameManager.instance.throughMassage != null)
        {
            if (tm == null) tm = GameManager.instance.throughMassage;

            if (isMess)
            {
                if (tm.accese == null && !tm.onDisplay)
                {
                    GameManager.instance.player.culletTarget = this;
                    tm.Call_Message(string.Format("転送装置だ、あと{0}個の鍵が必要そうだ", 3 - GameManager.instance.Get_KeyState()), "次のフロアへ", GameManager.instance.Get_KeyState() >= 3);
                    tm.accese = this;

                }
            }
            else
            {
                if (tm.accese == this && tm.onDisplay)
                {
                    GameManager.instance.player.culletTarget = null;
                    tm.Clear_Message();
                    tm.accese = null;
                }
            }

        }

        if (isMess_dump != isMess)
        {
            if (isMess)
            {
                GameManager.instance.player.culletTarget = this;
                GameManager.instance.throughMassage.Call_Message(string.Format( "転送装置だ、あと{0}個の鍵が必要そうだ", 3 - GameManager.instance.Get_KeyState()), "次のフロアへ", GameManager.instance.Get_KeyState() >= 3);

            }
            else
            {
                GameManager.instance.player.culletTarget = null;
                GameManager.instance.throughMassage.Clear_Message();

            }
        }

        isMess_dump = isMess;
    }

    public override void Get_Item(bool destroy)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.player.culletTarget = null;
            GameManager.instance.throughMassage.accese = null;
            GameManager.instance.throughMassage.Clear_Message();
            GameManager.instance.NextStage();
        }
    }
}
