using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    // Start is called before the first frame update
    public List<DialogInfo[]> dialogInfoList;
    private int contentIndex;
    public int dialogInfoIndex;

    private void Start()
    {
        dialogInfoList = new List<DialogInfo[]>()
        {
            new DialogInfo[]
            {
             new DialogInfo() {name="Luna",content="Hi! I'm Luna. You can use 'WASD' to control me. Press shift to run and " +
             "press space key to interact with NPC! "}
            },

            new DialogInfo[]
            {
                new DialogInfo() {name="Luna",content="Good morning, Nala! It's really a sunshine day, right?"},
                new DialogInfo(){name="Nala",content="Morning, Luna. Yeah, today's weather is so sweet."},
                new DialogInfo(){name="Luna",content="Yes! I cannot wait to pick up some strawbarrys," +
                " The strawberries in the morning are filled with sunlight, and are still cold and dripping with dew. It couldn¡¯t be more perfect."},
                new DialogInfo(){name="Nala",content="Is that so? Then I wish you have a great harvest."},
                new DialogInfo(){name="Nala",content="Ahh. almost forget, my dog miss you so much, go say hi, will you?"},
                new DialogInfo(){name="Luna",content="Of course."}

            },

            new DialogInfo[]
            {
             new DialogInfo() {name="Nala",content="Boy is waiting."}
            },

            new DialogInfo[]
            {
             new DialogInfo() {name="Luna",content="He is cute as always."},
             new DialogInfo() {name="Nala",content="I can tell he is really happy to see you, look at that tale."},
             new DialogInfo() {name="Luna",content="Hhhh, I'm happy too."},
             new DialogInfo() {name="Nala",content="By the way, can you do me a favor?"},
             new DialogInfo() {name="Luna",content="Sure thing, tell me"},
             new DialogInfo() {name="Nala",content="Well, I accidently lost some candle in the road, silly me. Can you help me find it?" +
             "I have to look after my shop, so..."},
             new DialogInfo() {name="Luna",content="No problem, leave it to me!"},
             new DialogInfo() {name="Nala",content="Thanks! Luna."},
            },

            new DialogInfo[]
            {
             new DialogInfo() {name="Nala",content="I lost 5 candles I think."}
            },

            new DialogInfo[]
            {
             new DialogInfo() {name="Luna",content="Here you go, your candles!"},
             new DialogInfo() {name="Nala",content="Great! Luna. I really appreciate it."},
             new DialogInfo() {name="Luna",content="Don't mention it. Well, I heared about the rumor that " +
             "there's been monster attacking event in this area, have you heared about it?"},
             new DialogInfo() {name="Nala",content="Yes, monster's amount raised kind of high these days," +
             "it can be pretty dangerous for someone who doesn't know how to fight to encounter them."},
             new DialogInfo() {name="Luna",content="This is truely a problem, I will handle it"},
             new DialogInfo() {name="Nala",content="I know we can count on you!"},
            },

            new DialogInfo[]
            {
             new DialogInfo() {name="Nala",content="Be careful!"}},
             };
        dialogInfoIndex = 0;
        contentIndex = 0;
        DisplayDialog();
    }

    public void DisplayDialog()
    {
        if(dialogInfoIndex > 7)
        {
            return;
        }
        if(contentIndex >= dialogInfoList[dialogInfoIndex].Length)
        {
            if(dialogInfoIndex == 2 &&
                !GameManager.Instance.hasPetTheDog) { }
            else if(dialogInfoIndex == 4 && GameManager.Instance.candleNum < 5) { }
            else if(dialogInfoIndex == 6 && GameManager.Instance.killNum < 5) { }
            else if (dialogInfoIndex == 7) { }
            else { dialogInfoIndex++; }
            if(dialogInfoIndex == 6) { GameManager.Instance.ShowMonsters(); }
            contentIndex = 0;
            UIManager.Instance.ShowDialog();
            GameManager.Instance.canControlLuna = true;
        }
        else
        {
            DialogInfo dialogInfo = dialogInfoList[dialogInfoIndex][contentIndex];
            UIManager.Instance.ShowDialog(dialogInfo.content, dialogInfo.name);
            contentIndex++;
        }
    }

    public void SetContentIndex()
    {
        dialogInfoIndex++;
    }
}
public struct DialogInfo
    {
        public string name;
        public string content;
    }

