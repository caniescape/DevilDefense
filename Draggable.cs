using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Photon.Pun;
using Photon.Realtime;
public class Draggable : MonoBehaviourPun//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static Vector2 defaultPosition;
    SortingGroup sr; //클릭시 위로 가게 소팅그룹 변경
    float distance = 10;
    CardManager cm;
    GameObject subCard;
    GameObject baseCard;
    Vector2 deckPos1 = new Vector2(-1.575f, -2f);
    Vector2 deckPos2 = new Vector2(-0.95f, -2f);
    Vector2 deckPos3 = new Vector2(-0.3125f, -2f);
    Vector2 deckPos4 = new Vector2(0.3125f, -2f);
    Vector2 deckPos5 = new Vector2(0.95f, -2f);
    Vector2 deckPos6 = new Vector2(1.575f, -2f);
    void Start()
    {
        cm = FindObjectOfType<CardManager>();
        sr = gameObject.transform.GetChild(0).GetComponent<SortingGroup>();
    }
    //마우스 클릭했을때
    void OnMouseDown()
    {
        if(!photonView.IsMine)
        {
            return;
        }
        sr.sortingOrder++;
        //클릭했을때 중점 저장
        defaultPosition = this.transform.position;
        //Debug.Log(defaultPosition);
        //중점위치에 따른 덱 bool값 변환
        if (defaultPosition.y > -3.3f && defaultPosition.y < 0.6f)
        {
            SizeOutputDeck();
        }
    }
    //마우스 드래그시 오브젝스 위치 갱신
    void OnMouseDrag()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        Vector2 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
    //마우스가 떨어졌을때 1에서 5 박스 의 범위 사이에 있을 경우
    void OnMouseUp()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        sr.sortingOrder--;
        //마우스 떼었을시 무덤안에 중점이 있다면
        Collider2D[] hit = Physics2D.OverlapPointAll(transform.position);
        if (hit != null)
        {
            //hit한 오브젝트중
            for (int i = 0; i < hit.Length; i++)
            {
                //CaveBox가 있다면
                if(hit[i].gameObject.name=="CaveBox")
                {
                    //맞는 위치에 덱 bool값 변환
                    SizeOutputDeck();
                    //카드 삭제 후 이후 코드는 동작 x
                    cm.ReleaseCard(this.gameObject);
                    return;
                }
            }
        }
        //마우스클릭 떼었을시 덱 안에 있다면
        if (transform.position.y>-3.3f&&transform.position.y<0.6f&&transform.position.x>-1.8875f&&transform.position.x<1.8875f)
        {
            //마우스 위치가 1번째 위치
            if(transform.position.x < -1.2625f && transform.position.x > -1.8875f)
            {
                //덱에 아무것도 없다면 위치 이동 
                if (!cm.IsDeck1())
                {
                    transform.position = deckPos1;
                    //원래 위치에 따라 bool값 변환
                    if (defaultPosition.x < -1.2625f && defaultPosition.x > -1.8875f)
                    {
                        cm.OutputDeck(1);
                        cm.InputDeck(1);
                    }
                    if (defaultPosition.x < -0.6375f && defaultPosition.x > -1.2625f)
                    {
                        cm.InputDeck(1);
                        cm.OutputDeck(2);
                    }
                    if (defaultPosition.x < 0f && defaultPosition.x > -0.6375f)
                    {
                        cm.InputDeck(1);
                        cm.OutputDeck(3);
                    }
                    if (defaultPosition.x < 0.6375f && defaultPosition.x > 0f)
                    {
                        cm.InputDeck(1);
                        cm.OutputDeck(4);
                    }
                    if (defaultPosition.x < 1.2625f && defaultPosition.x > 0.6375f)
                    {
                        cm.InputDeck(1);
                        cm.OutputDeck(5);
                    }
                    if (defaultPosition.x < 1.8875f && defaultPosition.x > 1.2625f)
                    {
                        cm.InputDeck(1);
                        cm.OutputDeck(6);
                    }
                }
                //있다면 조합검사
                else
                {
                    //중점과 부딪히는 카드의 충돌체
                    Collider2D[] colCard = Physics2D.OverlapPointAll(transform.position);
                    for(int i=0;i<colCard.Length;i++)
                    {
                        //중에 카드가 있다면
                        //Debug.Log(colCard[i].GetComponent<CardInfo>().Name);
                        if(colCard[i].name=="CardBase(Clone)"&&colCard[i].gameObject!=gameObject)
                        {
                            //baseCard에 오브젝트 저장
                            baseCard = colCard[i].gameObject;
                        }
                    }
                    //Debug.Log(baseCard);
                    //조합검사
                    if (cm.IsMixCard(gameObject, baseCard))
                    {
                        //참이면 조합
                        cm.MixCard(gameObject, baseCard);
                    }
                    else
                    {
                        //아니면 원래자리로 돌아감
                        ReturnLocation();
                    }
                }
            }
            if(transform.position.x < -0.6375f && transform.position.x > -1.2625f)
            {
                //덱에 아무것도 없다면 위치 이동 
                if (!cm.IsDeck2())
                {
                    transform.position = deckPos2;
                    if (defaultPosition.x < -1.2625f && defaultPosition.x > -1.8875f)
                    {
                        cm.InputDeck(2);
                        cm.OutputDeck(1);
                    }
                    if (defaultPosition.x < -0.6375f && defaultPosition.x > -1.2625f)
                    {
                        cm.OutputDeck(2);
                        cm.InputDeck(2);
                    }
                    if (defaultPosition.x < 0f && defaultPosition.x > -0.6375f)
                    {
                        cm.InputDeck(2);
                        cm.OutputDeck(3);
                    }
                    if (defaultPosition.x < 0.6375f && defaultPosition.x > 0f)
                    {
                        cm.InputDeck(2);
                        cm.OutputDeck(4);
                    }
                    if (defaultPosition.x < 1.2625f && defaultPosition.x > 0.6375f)
                    {
                        cm.InputDeck(2);
                        cm.OutputDeck(5);
                    }
                    if (defaultPosition.x < 1.8875f && defaultPosition.x > 1.2625f)
                    {
                        cm.InputDeck(2);
                        cm.OutputDeck(6);
                    }
                }
                //있다면 조합검사
                else
                {
                    Collider2D[] colCard = Physics2D.OverlapPointAll(transform.position);
                    for (int i = 0; i < colCard.Length; i++)
                    {
                        Debug.Log(colCard[i].name);
                        if (colCard[i].name == "CardBase(Clone)" && colCard[i].gameObject != gameObject)
                        {
                            baseCard = colCard[i].gameObject;
                        }
                    }
                    //Debug.Log(subCard);
                    //Debug.Log(baseCard);
                    if (cm.IsMixCard(gameObject, baseCard))
                    {
                        cm.MixCard(gameObject, baseCard);
                    }
                    else
                    {
                        ReturnLocation();
                    }
                }
            }
            if (transform.position.x < 0f && transform.position.x > -0.6375f)
            {
                //덱에 아무것도 없다면 위치 이동 
                if (!cm.IsDeck3())
                {
                    transform.position = deckPos3;
                    if (defaultPosition.x < -1.2625f && defaultPosition.x > -1.8875f)
                    {
                        cm.InputDeck(3);
                        cm.OutputDeck(1);
                    }
                    if (defaultPosition.x < -0.6375f && defaultPosition.x > -1.2625f)
                    {
                        cm.InputDeck(3);
                        cm.OutputDeck(2);
                    }
                    if (defaultPosition.x < 0f && defaultPosition.x > -0.6375f)
                    {
                        cm.OutputDeck(3);
                        cm.InputDeck(3);
                    }
                    if (defaultPosition.x < 0.6375f && defaultPosition.x > 0f)
                    {
                        cm.InputDeck(3);
                        cm.OutputDeck(4);
                    }
                    if (defaultPosition.x < 1.2625f && defaultPosition.x > 0.6375f)
                    {
                        cm.InputDeck(3);
                        cm.OutputDeck(5);
                    }
                    if (defaultPosition.x < 1.8875f && defaultPosition.x > 1.2625f)
                    {
                        cm.InputDeck(3);
                        cm.OutputDeck(6);
                    }
                }
                //있다면 조합검사
                else
                {
                    Collider2D[] colCard = Physics2D.OverlapPointAll(transform.position);
                    for (int i = 0; i < colCard.Length; i++)
                    {
                        Debug.Log(colCard[i].name);
                        if (colCard[i].name == "CardBase(Clone)" && colCard[i].gameObject != gameObject)
                        {
                            baseCard = colCard[i].gameObject;
                        }
                    }
                    //Debug.Log(subCard);
                    //Debug.Log(baseCard);
                    if (cm.IsMixCard(gameObject, baseCard))
                    {
                        cm.MixCard(gameObject, baseCard);
                    }
                    else
                    {
                        ReturnLocation();
                    }
                }
            }
            if (transform.position.x < 0.6375f && transform.position.x > 0f)
            {
                //덱에 아무것도 없다면 위치 이동 
                if (!cm.IsDeck4())
                {
                    transform.position = deckPos4;
                    if (defaultPosition.x < -1.2625f && defaultPosition.x > -1.8875f)
                    {
                        cm.InputDeck(4);
                        cm.OutputDeck(1);
                    }
                    if (defaultPosition.x < -0.6375f && defaultPosition.x > -1.2625f)
                    {
                        cm.InputDeck(4);
                        cm.OutputDeck(2);
                    }
                    if (defaultPosition.x < 0f && defaultPosition.x > -0.6375f)
                    {
                        cm.InputDeck(4);
                        cm.OutputDeck(3);
                    }
                    if (defaultPosition.x < 0.6375f && defaultPosition.x > 0f)
                    {
                        cm.OutputDeck(4);
                        cm.InputDeck(4);
                    }
                    if (defaultPosition.x < 1.2625f && defaultPosition.x > 0.6375f)
                    {
                        cm.InputDeck(4);
                        cm.OutputDeck(5);
                    }
                    if (defaultPosition.x < 1.8875f && defaultPosition.x > 1.2625f)
                    {
                        cm.InputDeck(4);
                        cm.OutputDeck(6);
                    }
                }
                //있다면 조합검사
                else
                {
                    Collider2D[] colCard = Physics2D.OverlapPointAll(transform.position);
                    for (int i = 0; i < colCard.Length; i++)
                    {
                        Debug.Log(colCard[i].name);
                        if (colCard[i].name == "CardBase(Clone)" && colCard[i].gameObject != gameObject)
                        {
                            baseCard = colCard[i].gameObject;
                        }
                    }
                    //Debug.Log(subCard);
                    //Debug.Log(baseCard);
                    if (cm.IsMixCard(gameObject, baseCard))
                    {
                        cm.MixCard(gameObject, baseCard);
                    }
                    else
                    {
                        ReturnLocation();
                    }
                }
            }
            if (transform.position.x < 1.2625f && transform.position.x > 0.6375f)
            {
                //덱에 아무것도 없다면 위치 이동 
                if (!cm.IsDeck5())
                {
                    transform.position = deckPos5;
                    if (defaultPosition.x < -1.2625f && defaultPosition.x > -1.8875f)
                    {
                        cm.InputDeck(5);
                        cm.OutputDeck(1);
                    }
                    if (defaultPosition.x < -0.6375f && defaultPosition.x > -1.2625f)
                    {
                        cm.InputDeck(5);
                        cm.OutputDeck(2);
                    }
                    if (defaultPosition.x < 0f && defaultPosition.x > -0.6375f)
                    {
                        cm.InputDeck(5);
                        cm.OutputDeck(3);
                    }
                    if (defaultPosition.x < 0.6375f && defaultPosition.x > 0f)
                    {
                        cm.InputDeck(5);
                        cm.OutputDeck(4);
                    }
                    if (defaultPosition.x < 1.2625f && defaultPosition.x > 0.6375f)
                    {
                        cm.OutputDeck(5);
                        cm.InputDeck(5);
                    }
                    if (defaultPosition.x < 1.8875f && defaultPosition.x > 1.2625f)
                    {
                        cm.InputDeck(5);
                        cm.OutputDeck(6);
                    }
                }
                //있다면 조합검사
                else
                {
                    Collider2D[] colCard = Physics2D.OverlapPointAll(transform.position);
                    for (int i = 0; i < colCard.Length; i++)
                    {
                        Debug.Log(colCard[i].name);
                        if (colCard[i].name == "CardBase(Clone)" && colCard[i].gameObject != gameObject)
                        {
                            baseCard = colCard[i].gameObject;
                        }
                    }
                    //Debug.Log(subCard);
                    //Debug.Log(baseCard);
                    if (cm.IsMixCard(gameObject, baseCard))
                    {
                        cm.MixCard(gameObject, baseCard);
                    }
                    else
                    {
                        ReturnLocation();
                    }
                }
            }
            if (transform.position.x < 1.8875f && transform.position.x > 1.2625f)
            {
                //덱에 아무것도 없다면 위치 이동 
                if (!cm.IsDeck6())
                {
                    transform.position = deckPos6;
                    if (defaultPosition.x < -1.2625f && defaultPosition.x > -1.8875f)
                    {
                        cm.InputDeck(6);
                        cm.OutputDeck(1);
                    }
                    if (defaultPosition.x < -0.6375f && defaultPosition.x > -1.2625f)
                    {
                        cm.InputDeck(6);
                        cm.OutputDeck(2);
                    }
                    if (defaultPosition.x < 0f && defaultPosition.x > -0.6375f)
                    {
                        cm.InputDeck(6);
                        cm.OutputDeck(3);
                    }
                    if (defaultPosition.x < 0.6375f && defaultPosition.x > 0f)
                    {
                        cm.InputDeck(6);
                        cm.OutputDeck(4);
                    }
                    if (defaultPosition.x < 1.2625f && defaultPosition.x > 0.6375f)
                    {
                        cm.InputDeck(6);
                        cm.OutputDeck(5);
                    }
                    if (defaultPosition.x < 1.8875f && defaultPosition.x > 1.2625f)
                    {
                        cm.OutputDeck(6);
                        cm.InputDeck(6);
                    }
                }
                //있다면 조합검사
                else
                {
                    Collider2D[] colCard = Physics2D.OverlapPointAll(transform.position);
                    for (int i = 0; i < colCard.Length; i++)
                    {
                        Debug.Log(colCard[i].name);
                        if (colCard[i].name == "CardBase(Clone)" && colCard[i].gameObject != gameObject)
                        {
                            baseCard = colCard[i].gameObject;
                        }
                    }
                    //Debug.Log(subCard);
                    //Debug.Log(baseCard);
                    if (cm.IsMixCard(gameObject, baseCard))
                    {
                        cm.MixCard(gameObject, baseCard);
                    }
                    else
                    {
                        ReturnLocation();
                    }
                }
            }
        }
        else
        {
            ReturnLocation();
        }
    }
    void SizeOutputDeck()
    {
        if (cm.IsDeck1() && defaultPosition.x < -1.2625f && defaultPosition.x > -1.8875f)
        {
            cm.OutputDeck(1);
        }
        if (cm.IsDeck2() && defaultPosition.x < -0.6375f && defaultPosition.x > -1.2625f)
        {
            cm.OutputDeck(2);
        }
        if (cm.IsDeck3() && defaultPosition.x < 0f && defaultPosition.x > -0.6375f)
        {
            cm.OutputDeck(3);
        }
        if (cm.IsDeck4() && defaultPosition.x < 0.6375f && defaultPosition.x > 0f)
        {
            cm.OutputDeck(4);
        }
        if (cm.IsDeck5() && defaultPosition.x < 1.2625f && defaultPosition.x > 0.6375f)
        {
            cm.OutputDeck(5);
        }
        if (cm.IsDeck6() && defaultPosition.x < 1.8875f && defaultPosition.x > 1.2625f)
        {
            cm.OutputDeck(6);
        }
    }
    //원래자리로
    void ReturnLocation()
    {
        //없으면 원래 자리로
        if (!cm.IsDeck1() && defaultPosition.x < -1.2625f && defaultPosition.x > -1.8875f)
        {
            cm.InputDeck(1);
        }
        if (!cm.IsDeck2() && defaultPosition.x < -0.6375f && defaultPosition.x > -1.2625f)
        {
            cm.InputDeck(2);
        }
        if (!cm.IsDeck3() && defaultPosition.x < 0f && defaultPosition.x > -0.6375f)
        {
            cm.InputDeck(3);
        }
        if (!cm.IsDeck4() && defaultPosition.x < 0.6375f && defaultPosition.x > 0f)
        {
            cm.InputDeck(4);
        }
        if (!cm.IsDeck5() && defaultPosition.x < 1.2625f && defaultPosition.x > 0.6375f)
        {
            cm.InputDeck(5);
        }
        if (!cm.IsDeck6() && defaultPosition.x < 1.8875f && defaultPosition.x > 1.2625f)
        {
            cm.InputDeck(6);
        }
        transform.position = defaultPosition;
    }
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Debug.Log("드래그시작");
    //    defaultposition = this.transform.position;
    //    throw new System.NotImplementedException();
    //}
    ////드래그 함에 따라 카드가 따라감
    //public void OnDrag(PointerEventData eventData)
    //{
    //    Vector2 currentPos = Input.mousePosition;
    //    Debug.Log("드래그");
    //    transform.position = currentPos;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.Log("드래그끝");
    //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    this.transform.position = defaultposition;
    //    throw new System.NotImplementedException();
    //}

}
