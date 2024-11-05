using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArea : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform JumpPointA;
    public Transform JumpPointB;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Luna"))
        {
            Luna_controller.Instance.Jump(true);

            float disA = Vector3.Distance(collision.transform.position, JumpPointA.position);
            float disB = Vector3.Distance(collision.transform.position, JumpPointB.position);
            Transform targetTrans;
            //if(disA > disB)
            //{
            //    targetTrans = JumpPointA.transform;
            //}
            //else
            //{
            //    targetTrans = JumpPointB.transform;
            //}
            targetTrans = disA > disB ? JumpPointA.transform : JumpPointB.transform;

            Luna_controller.Instance.transform.DOMove(targetTrans.position, 0.5f).OnComplete(() => { JumpEnd(Luna_controller.Instance); });
            Transform LocalTransform = Luna_controller.Instance.transform.GetChild(0);
            //        LocalTransform.DOLocalMoveY(1.5f, 0.25f).OnComplete(() => { LocalTransform.DOLocalMoveY(0.61f, 0.25f); });        }
            //}}
            Sequence sequence = DOTween.Sequence();
            sequence.Append(LocalTransform.DOLocalMoveY(1.5f, 0.25f)).SetEase(Ease.InOutSine);
            sequence.Append(LocalTransform.DOLocalMoveY(0.61f, 0.25f)).SetEase(Ease.InOutSine);
            sequence.Play();

        }

        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    if (collision.CompareTag("Luna"))
        //    {
        //       collision.GetComponent<Luna_controller>().Jump(false);
        //    }
        //}

        void JumpEnd(Luna_controller controller)
        {
            controller.Jump(false);
        }
    }
}
