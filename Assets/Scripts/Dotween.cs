using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform targetTrans;
    private Tween tween;// Start is called before the first frame update
    void Start()
    {
        tween = transform.DOMove(targetTrans.position,2);
        tween.Pause();
        //transform.DORotate(new Vector3(0, 90, 0), 1);
        //transform.DOShakePosition(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //tween.Play();
            tween.PlayForward();
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            tween.PlayBackwards();
        }
    }
}
