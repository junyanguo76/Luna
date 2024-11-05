using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject effectGo;
    public AudioClip pickClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.candleNum = 5;
        if(GameManager.Instance.candleNum == 5)
        {
            GameManager.Instance.SetContenIndex();
        }
        Instantiate(effectGo,transform.position,Quaternion.identity);
        GameManager.Instance.PlayShot(pickClip);
        Destroy(gameObject);
    }
}
