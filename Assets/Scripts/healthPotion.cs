using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPotion : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject effectGo;
    

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Luna_controller luna = collision.GetComponent<Luna_controller>();
    //    if (luna != null)
    //    {
    //        luna.HealthCaulator(1);
    //        Instantiate(effectGo, transform.position, Quaternion.identity);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Luna"))
        {
            if (GameManager.Instance.lunaCurrentHP != 10)
            {
                GameManager.Instance.AddOrDecreaseHealth(4);
                Destroy(this.gameObject);
            }
        }
    }
}
