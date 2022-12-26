using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class SelectHeros : MonoBehaviour, IDragHandler, IEndDragHandler
{
  //-------------------------- Unity obj ------------
  public Transform contant;
  public Transform center;
  Transform _select;
  public List<Transform> objs;
  Coroutine moveToCenter;

  public Transform select { get { return _select; } }
  public int objCount { get { return objs.Count; } }
  public int selcetIndex { get { return objs.IndexOf(_select); } }

  Transform leftTrans;
  Transform rightTrans;
  //------------------------- init ------

  void Awake()
  {
    SetRightLeft();
    FindSelectObj();

  }


  private void Update()
  {
  }

  void SetRightLeft()
  {
    float right = -9999;
    float left = 9999;
    Transform rightObj = null;
    Transform leftObj = null;
    for (int i = 0; i < objs.Count; i++)
    {
      if (objs[i].position.x > right)
      {
        rightObj = objs[i];
        right = objs[i].transform.position.x;
      }
      if (objs[i].position.x < left)
      {
        leftObj = objs[i];
        left = objs[i].transform.position.x;
      }
    }
    leftTrans = leftObj;
    rightTrans = rightObj;
  }

  //------------------ function ---------


  Transform FindSelectObj()
  {
    Transform nearly = null;
    float minDis = 999;
    for(int i =0;i< objs.Count;i++)
    {
      float dis = Vector2.Distance(objs[i].position, center.position);
      if(dis < minDis)
      {
        nearly = objs[i];
        minDis = dis;
      }
    }
    _select = nearly;
    return nearly;
  }

  IEnumerator MoveToSelect()
  {
    Vector3 centerPos = center.position;
    Vector3 targetPos = _select.position;

    float distance = Vector3.Distance(centerPos, targetPos);
    while(distance > 0.03f)
    {
      centerPos = center.position;
      targetPos = _select.position;

      Vector3 pos = Vector3.Lerp(targetPos, centerPos, 0.3f);

      Vector3 contantMove = pos - _select.position;
      contant.Translate(contantMove);

      distance = Vector3.Distance(centerPos, _select.position);

      ScaleTheSize();
      yield return null;
    }
    moveToCenter = null;
  }

  void ScaleTheSize()
  {
    foreach (Transform trans in objs)
    {
      float distance = Vector2.Distance(trans.position, center.position);
      float tLerp = distance / (Screen.width / 2);
      float size = Mathf.Lerp(1f, 0.65f, tLerp);
      trans.localScale = new Vector3(1, 1, 1) * size;
    }
  }


  //------------------------- ui event ---------------
  public void OnDrag(PointerEventData data)
  {
    Vector3 delta = data.delta;
    float xMove = delta.x;

    if (leftTrans.position.x + xMove >= (center.position.x + Screen.width / 4))
      xMove = (center.position.x + Screen.width / 4) - leftTrans.position.x;
    if (rightTrans.position.x + xMove <= (center.position.x - Screen.width / 4))
      xMove = (center.position.x - Screen.width / 4) - rightTrans.position.x;

    contant.Translate(xMove * Vector3.right);
    FindSelectObj();
    ScaleTheSize();
    if (moveToCenter != null) StopCoroutine(moveToCenter);
  }

  public void OnEndDrag(PointerEventData data)
  {
    moveToCenter =  StartCoroutine(MoveToSelect());
  }
}

