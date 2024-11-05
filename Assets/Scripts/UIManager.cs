using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public Image hpMaskImage;
    public Image mpMaskImage;
    public float hpOriginalSize;
    public float mpOriginalSize;
    public GameObject battleGround;
    public GameObject battleScene;
    public GameObject talkPanel;

    public Image characterImage;
    public List<Sprite> characterSprites = new List<Sprite>();
    public Image NalaSprite;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentText;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        hpOriginalSize = hpMaskImage.rectTransform.rect.width;
        mpOriginalSize = mpMaskImage.rectTransform.rect.width;
         
    }

    public void SetHpValue(float fillPercent)
    {
        hpMaskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fillPercent * hpOriginalSize);
    }


    public void SetMpValue(float fillpercent)
    {
        mpMaskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fillpercent * mpOriginalSize);
    }
    // Update is called once per frame
    public void SetBattleScene(bool show)
    {
        battleScene.SetActive(show);
    }

    public void SetBattleBackground(bool show)
    {
        battleGround.SetActive(show);
    }

    public void ShowDialog(string content = null,string name = null)
    {
        if(content == null)
        {
            talkPanel.SetActive(false);
        }
        else
        {
            talkPanel.SetActive(true);
            if(name != null)
            {
                if(name == "Luna")
                {
                    characterImage.sprite = characterSprites[0]; 
                }
                else
                {

                    characterImage.sprite = characterSprites[1];
                }
                characterImage.SetNativeSize();
            }
            nameText.text = name;
            contentText.text = content;
        }
    }
}
