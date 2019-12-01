using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGeffect : MonoBehaviour
{
    [SerializeField]
    public Text textF;
    [SerializeField]
    private Image imageFrame;
    [SerializeField]
    private Image imageFlush;
    [SerializeField]
    private Image imageBG;

    // Start is called before the first frame update
    void Start()
    {
        imageBG = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void E_BG_Effect(int i, bool onEffect)
    {
        if (onEffect)
        {
            imageBG.fillAmount = (float)i / 360;
            imageFlush.fillAmount = (float)i / 360;
        }
        else
        {
            imageBG.fillAmount = 1f - (float)i / 360f;
            imageFlush.fillAmount = 1f - (float)i / 360f;
        }
    }

    public void E_Floor_Effect(int i, bool onEffect)
    {
        if (onEffect)
        {
            imageFrame.color = new Color(imageFrame.color.r, imageFrame.color.g, imageFrame.color.b, (float)i / 100);
            textF.color = new Color(textF.color.r, textF.color.g, textF.color.b, (float)i / 100);
        }
        else
        {
            imageFrame.color = new Color(imageFrame.color.r, imageFrame.color.g, imageFrame.color.b, 1f - (float)i / 100);
            textF.color = new Color(textF.color.r, textF.color.g, textF.color.b, 1f - (float)i / 100);

        }
    }

    
}
