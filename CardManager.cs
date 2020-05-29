using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class CardManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject PlayerDeck;   //부모오브젝트
    public GameObject OpponentDeck;   //부모오브젝트
    public GameObject CardBase;     //프리팹 카드베이스
    private int rndNum;             //소환용 넘버
    private int currentCardNum;     //현재 덱에 있는 카드수
    private int cardDeckNum;        //덱에 있는 카드 위치 숫자
    
    
    //덱위치여부 bool값
    public bool deck1;
    public bool deck2;
    public bool deck3;
    public bool deck4;
    public bool deck5;
    public bool deck6;
    public bool deckE1;
    public bool deckE2;
    public bool deckE3;
    public bool deckE4;
    public bool deckE5;
    public bool deckE6;
    GameObject go;
    public Vector3 location;       //소환 위치
    public Vector3 opponentLocation;       //적 소환 위치

    Dictionary<int, Card> deckList = new Dictionary<int, Card>();   //덱리스트

    List<Card> card = new List<Card>    //카드
    {
        new Card()
        {
            Name = "Brynhildr3",
            ImgNum = 1,
            Type = "sky",
            Atk = 8,
            level = 3,
            SkillNum = 1,
            MissileNum = 1
        },
        new Card()
        {
            Name = "Urania3",
            ImgNum = 2,
            Type = "earth",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 2
        },
        new Card()
        {
            Name = "Hercules3",
            ImgNum = 3,
            Type = "earth",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 2
        },
        new Card()
        {
            Name = "Yggdrasil3",
            ImgNum = 4,
            Type = "hell",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 2
        },
        new Card()
        {
            Name = "Batory3",
            ImgNum = 5,
            Type = "hell",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 5
        },
        new Card()
        {
            Name = "Hermes3",
            ImgNum = 6,
            Type = "sky",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 1
        },
        new Card()
        {
            Name = "Amateras3",
            ImgNum = 7,
            Type = "sky",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 3
        },
        new Card()
        {
            Name = "ArianRoad3",
            ImgNum = 8,
            Type = "earth",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 4
        },
        new Card()
        {
            Name = "Cerridwen3",
            ImgNum = 9,
            Type = "hell",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 5
        },
        new Card()
        {
            Name = "Metatron3",
            ImgNum = 10,
            Type = "sky",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 6
        },
        new Card()
        {
            Name = "Izanagi3",
            ImgNum = 11,
            Type = "sky",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 6
        },
        new Card()
        {
            Name = "Lucifer3",
            ImgNum = 12,
            Type = "hell",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 7
        },
        new Card()
        {
            Name = "Nyx3",
            ImgNum = 13,
            Type = "hell",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 7
        },
        new Card()
        {
            Name = "Tsukuyomi3",
            ImgNum = 14,
            Type = "earth",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 4
        },
        new Card()
        {
            Name = "Siegfried3",
            ImgNum = 15,
            Type = "earth",
            Atk = 8,
            level = 3,
            SkillNum = 2,
            MissileNum = 3
        },
        new Card()
        {
            Name = "Brynhildr3",
            ImgNum = 16,
            Type = "sky",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 1
        },
        new Card()
        {
            Name = "Urania2",
            ImgNum = 17,
            Type = "earth",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 2
        },
        new Card()
        {
            Name = "Hercules2",
            ImgNum = 18,
            Type = "earth",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 2
        },
        new Card()
        {
            Name = "Yggdrasil2",
            ImgNum = 19,
            Type = "hell",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 2
        },
        new Card()
        {
            Name = "Batory2",
            ImgNum = 20,
            Type = "hell",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 5
        },
        new Card()
        {
            Name = "Hermes2",
            ImgNum = 21,
            Type = "sky",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 1
        },
        new Card()
        {
            Name = "Amateras2",
            ImgNum = 22,
            Type = "sky",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 3
        },
        new Card()
        {
            Name = "ArianRoad2",
            ImgNum = 23,
            Type = "earth",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 4
        },
        new Card()
        {
            Name = "Cerridwen2",
            ImgNum = 24,
            Type = "hell",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 5
        },
        new Card()
        {
            Name = "Metatron2",
            ImgNum = 25,
            Type = "sky",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 6
        },
        new Card()
        {
            Name = "Izanagi2",
            ImgNum = 26,
            Type = "sky",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 6
        },
        new Card()
        {
            Name = "Lucifer2",
            ImgNum = 27,
            Type = "hell",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 7
        },
        new Card()
        {
            Name = "Nyx2",
            ImgNum = 28,
            Type = "hell",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 7
        },
        new Card()
        {
            Name = "Tsukuyomi2",
            ImgNum = 29,
            Type = "earth",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 4
        },
        new Card()
        {
            Name = "Siegfried2",
            ImgNum = 30,
            Type = "earth",
            Atk = 4,
            level = 2,
            SkillNum = 2,
            MissileNum = 3
        },
        new Card()
        {
            Name = "Brynhildr1",
            ImgNum = 31,
            Type = "sky",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 1
        },
        new Card()
        {
            Name = "Urania1",
            ImgNum = 32,
            Type = "earth",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 2
        },
        new Card()
        {
            Name = "Hercules1",
            ImgNum = 33,
            Type = "earth",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 2
        },
        new Card()
        {
            Name = "Yggdrasil1",
            ImgNum = 34,
            Type = "hell",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 2
        },
        new Card()
        {
            Name = "Batory1",
            ImgNum = 35,
            Type = "hell",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 5
        },
        new Card()
        {
            Name = "Hermes1",
            ImgNum = 36,
            Type = "sky",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 1
        },
        new Card()
        {
            Name = "Amateras1",
            ImgNum = 37,
            Type = "sky",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 3
        },
        new Card()
        {
            Name = "ArianRoad1",
            ImgNum = 38,
            Type = "earth",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 4
        },
        new Card()
        {
            Name = "Cerridwen1",
            ImgNum = 39,
            Type = "hell",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 5
        },
        new Card()
        {
            Name = "Metatron1",
            ImgNum = 40,
            Type = "sky",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 6
        },
        new Card()
        {
            Name = "Izanagi1",
            ImgNum = 41,
            Type = "sky",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 6
        },
        new Card()
        {
            Name = "Lucifer1",
            ImgNum = 42,
            Type = "hell",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 7
        },
        new Card()
        {
            Name = "Nyx1",
            ImgNum = 43,
            Type = "hell",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 7
        },
        new Card()
        {
            Name = "Tsukuyomi1",
            ImgNum = 44,
            Type = "earth",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 4
        },
        new Card()
        {
            Name = "Siegfried1",
            ImgNum = 45,
            Type = "earth",
            Atk = 2,
            level = 1,
            SkillNum = 2,
            MissileNum = 3
        }
    };
    //카드리스트에 카드 담기
    void Start()
    {
        for (int i=0;i<card.Count;i++)
        {
            SetCardInfo(card[i]);
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
    //값에 따른 카드정보 세팅 후 리턴
    GameObject SetCardInfo(Card card)
    {
        //Debug.Log("카드 이미지 번호 : "+card.ImgNum);
        //카드이미지 숫자로 설정
        switch (card.ImgNum)
        {
            case 1:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Brynhildr3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.3f, -2, 0);
                break;
            case 2:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Urania3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 3:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Hercules3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 4:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Yggdrasil3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 5:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Batory3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 6:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Hermes3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.3f, -2, 0);
                break;
            case 7:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Amateras3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 8:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/ArianRoad3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.3f, -2, 0);
                break;
            case 9:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Cerridwen3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0.2f, -2, 0);
                break;
            case 10:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Metatron3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.2f, -2, 0);
                break;
            case 11:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Izanagi3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 12:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Lucifer3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 13:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Nyx3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 14:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Tsukuyomi3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 15:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Siegfried3");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 16:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Brynhildr2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0.1f, -2, 0);
                break;
            case 17:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Urania2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 18:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Hercules2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 19:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Yggdrasil2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 20:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Batory2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 21:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Hermes2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.3f, -2, 0);
                break;
            case 22:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Amateras2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 23:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/ArianRoad2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.3f, -2, 0);
                break;
            case 24:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Cerridwen2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0.2f, -2, 0);
                break;
            case 25:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Metatron2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.2f, -2, 0);
                break;
            case 26:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Izanagi2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 27:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Lucifer2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.3f, -2, 0);
                break;
            case 28:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Nyx2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 29:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Tsukuyomi2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 30:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Siegfried2");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 31:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Brynhildr1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0.1f, -2, 0);
                break;
            case 32:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Urania1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 33:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Hercules1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 34:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Yggdrasil1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 35:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Batory1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 36:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Hermes1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.3f, -2, 0);
                break;
            case 37:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Amateras1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 38:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/ArianRoad1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.3f, -2, 0);
                break;
            case 39:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Cerridwen1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0.2f, -2, 0);
                break;
            case 40:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Metatron1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.2f, -2, 0);
                break;
            case 41:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Izanagi1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 42:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Lucifer1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(-0.3f, -2, 0);
                break;
            case 43:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Nyx1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 44:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Tsukuyomi1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
            case 45:
                CardBase.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CardImg/Siegfried1");
                CardBase.transform.GetChild(0).GetChild(0).transform.position = new Vector3(0, -2, 0);
                break;
        }
        //카드타입으로 프레임 설정
        switch (card.Type)
        {
            case "sky":
                CardBase.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Img/CardFrameS");
                break;
            case "hell":
                CardBase.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Img/CardFrameH");
                break;
            case "earth":
                CardBase.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Img/CardFrameE");
                break;
        }
        //나머지 정보 넣기
        CardBase.GetComponent<CardInfo>().Name = card.Name;
        CardBase.GetComponent<CardInfo>().level = card.level;
        CardBase.GetComponent<CardInfo>().Type = card.Type;
        CardBase.GetComponent<CardInfo>().Atk = card.Atk;
        CardBase.GetComponent<CardInfo>().SkillNum = card.SkillNum;
        CardBase.GetComponent<CardInfo>().MissileNum = card.MissileNum;
        CardBase.GetComponent<CardInfo>().ImgNum = card.ImgNum;

        return CardBase;
    }

    //덱에 있는 n번째 카드정보 리턴
    GameObject ReadCardInDeck(int num)
    {


        return CardBase;
    }
    //카드 소환
    public void Summon()
    {
        //현재 덱의 카드 숫자가 6보다 작거나 sp가 100이상일때
        if (currentCardNum < 6&& GameManager.instance.sp >= 100)
        {
            //sp감소
            GameManager.instance.sp = GameManager.instance.sp - 100;
            //덱 카드 숫자 1증가
            currentCardNum++;
            //랜덤숫자 뽑아서 카드 생성
            rndNum = Random.Range(30, 44);
            //Debug.Log("랜덤넘버 : "+rndNum);
            //카드 생성후 덱오브젝트 하위로 보내기
            //GameObject go = Instantiate(CardBase, location, Quaternion.identity);
            Debug.Log(PhotonNetwork.NickName);
            
            photonView.RPC("SetCard", RpcTarget.All, rndNum);
            //위치 본인한테 쏘고
            photonView.RPC("SetLocationUp", RpcTarget.All);
            //위치 타인한테 쏘고
            //photonView.RPC("SetLocationEnemy", RpcTarget.Others);
            Debug.Log("위치:"+location);
            CardMake(CardBase);
        }
        else
        {
            Debug.Log("소환불가");
        }
    }

    //카드 프리팹에 정보를 갱신해줌
    [PunRPC]
    public void SetCard(int rndNum)
    {
        Debug.Log("SetCard");
        switch (rndNum)
        {
            case 30:
                CardBase = SetCardInfo(card[30]);
                break;
            case 31:
                CardBase = SetCardInfo(card[31]);
                break;
            case 32:
                CardBase = SetCardInfo(card[32]);
                break;
            case 33:
                CardBase = SetCardInfo(card[33]);
                break;
            case 34:
                CardBase = SetCardInfo(card[34]);
                break;
            case 35:
                CardBase = SetCardInfo(card[35]);
                break;
            case 36:
                CardBase = SetCardInfo(card[36]);
                break;
            case 37:
                CardBase = SetCardInfo(card[37]);
                break;
            case 38:
                CardBase = SetCardInfo(card[38]);
                break;
            case 39:
                CardBase = SetCardInfo(card[39]);
                break;
            case 40:
                CardBase = SetCardInfo(card[40]);
                break;
            case 41:
                CardBase = SetCardInfo(card[41]);
                break;
            case 42:
                CardBase = SetCardInfo(card[42]);
                break;
            case 43:
                CardBase = SetCardInfo(card[43]);
                break;
            case 44:
                CardBase = SetCardInfo(card[44]);
                break;
        }
    }
    [PunRPC]
    public void SetLocation()
    {
        go.transform.position = FindMyLocation();
    }
    [PunRPC]
    public void SetLocationUp()
    {
        location = FindUpLocation();
    }
    
    void CardMake(GameObject CardBase)
    {
        //카드 생성
        go = PhotonNetwork.Instantiate(CardBase.name, location, Quaternion.identity);
        //위치 밑으로 보내기
        photonView.RPC("SetLocation", PhotonNetwork.LocalPlayer);
        //본인카드 플레이어덱 하위로 보내기
        go.transform.SetParent(PlayerDeck.transform);
        //미사일 파이어 컴포넌트 바꾸기
        Destroy(go.GetComponent<OpMagicFire>());
        go.AddComponent<magicFire>();
        Debug.Log("카드 정보:" + card[rndNum].Name);
        this.CardBase = CardBase;
    }
    //덱에 빈공간 찾기
    Vector3 FindMyLocation()
    {
        if (deck1 && deck2 && deck3 && deck4 && deck5 && !deck6)
        {
            location = new Vector3(1.575f, -2, 0);
            deck6 = true;
            photonView.RPC("OutputDeckE", RpcTarget.All, 6);
        }
        else if (deck1&&deck2&&deck3&&deck4&&!deck5)
        {
            location = new Vector3(0.95f, -2, 0);
            deck5 = true;
            photonView.RPC("OutputDeckE", RpcTarget.All, 5);
        }
        else if (deck1&&deck2&&deck3&&!deck4)
        {
            location = new Vector3(0.3125f, -2, 0);
            deck4 = true;
            photonView.RPC("OutputDeckE", RpcTarget.All, 4);
        }
        else if (deck1&&deck2&&!deck3)
        {
            location = new Vector3(-0.3125f, -2, 0);
            deck3 = true;
            photonView.RPC("OutputDeckE", RpcTarget.All, 3);
        }
        else if (deck1&&!deck2)
        {
            location = new Vector3(-0.95f, -2, 0);
            deck2 = true;
            photonView.RPC("OutputDeckE", RpcTarget.All, 2);
        }
        else if (!deck1)
        {
            location = new Vector3(-1.575f, -2, 0);
            deck1 = true;
            photonView.RPC("OutputDeckE", RpcTarget.Others, 1);
        }

        return location;
    }
    

    //위쪽덱에 빈공간 찾기
    Vector3 FindUpLocation()
    {
        if (deckE1 && deckE2 && deckE3 && deckE4 && deckE5 && !deckE6)
        {
            location = new Vector3(1.575f, 2.6f, 0);
            deckE6 = true;
        }
        else if (deckE1 && deckE2 && deckE3 && deckE4 && !deckE5)
        {
            location = new Vector3(0.95f, 2.6f, 0);
            deckE5 = true;
        }
        else if (deckE1 && deckE2 && deckE3 && !deckE4)
        {
            location = new Vector3(0.3125f, 2.6f, 0);
            deckE4 = true;
        }
        else if (deckE1 && deckE2 && !deckE3)
        {
            location = new Vector3(-0.3125f, 2.6f, 0);
            deckE3 = true;
        }
        else if (deckE1 && !deckE2)
        {
            location = new Vector3(-0.95f, 2.6f, 0);
            deckE2 = true;
        }
        else if (!deckE1)
        {
            location = new Vector3(-1.575f, 2.6f, 0);
            deckE1 = true;
        }

        return location;
    }

    //카드 조합가능?
    public bool IsMixCard(GameObject subCard, GameObject baseCard)
    {
        //종족과 레벨이 같거나 3레벨이 아닐때 MIX
        if(subCard.GetComponent<CardInfo>().Type == baseCard.GetComponent<CardInfo>().Type &&
            subCard.GetComponent<CardInfo>().level== baseCard.GetComponent<CardInfo>().level&&
            baseCard.GetComponent<CardInfo>().level!=3)
        {
            return true;
        }
        return false;
    }
    
    //조합
    public void MixCard(GameObject subCard, GameObject baseCard)
    {
        currentCardNum--;
        location = baseCard.transform.position;
        GameObject setCard = SetCardInfo(card[(baseCard.GetComponent<CardInfo>().ImgNum - 15) - 1]);
        GameObject newCard = PhotonNetwork.Instantiate(setCard.name, location, Quaternion.identity);
        newCard.transform.SetParent(PlayerDeck.transform);
        //Debug.Log(subCard.GetComponent<CardInfo>().Name);
        //Debug.Log(baseCard.GetComponent<CardInfo>().Name);
        PhotonNetwork.Destroy(subCard);
        PhotonNetwork.Destroy(baseCard);
    }
    //auto 업그레이드
    void UpgradeCard(GameObject subCard, GameObject baseCard)
    {

    }
    //카드 버리기
    public void ReleaseCard(GameObject cardBase)
    {
        //반값 돌려줌
        if(cardBase.GetComponent<CardInfo>().level==1)
        {
            GameManager.instance.sp = GameManager.instance.sp + 50;
        }
        else if(cardBase.GetComponent<CardInfo>().level==2)
        {
            GameManager.instance.sp = GameManager.instance.sp + 100;
        }
        else if(cardBase.GetComponent<CardInfo>().level==3)
        {
            GameManager.instance.sp = GameManager.instance.sp + 200;
        }
        //현재 카드 숫자 -1후 삭제
        currentCardNum--;
        Destroy(cardBase);
    }
    //OutputDeckE
    #region
    [PunRPC]
    public void OutputDeckE(int i)
    {
        switch (i)
        {
            case 1:
                deckE1 = false;
                break;
            case 2:
                deckE2 = false;
                break;
            case 3:
                deckE3 = false;
                break;
            case 4:
                deckE4 = false;
                break;
            case 5:
                deckE5 = false;
                break;
            case 6:
                deckE6 = false;
                break;
        }
    }
    #endregion
    //카드 위치 이동에 따른 deck값 변경 함수
    #region
    public void InputDeck(int i)
    {
        switch (i)
        {
            case 1:
                deck1 = true;
                break;
            case 2:
                deck2 = true;
                break;
            case 3:
                deck3 = true;
                break;
            case 4:
                deck4 = true;
                break;
            case 5:
                deck5 = true;
                break;
            case 6:
                deck6 = true;
                break;
        }
    }
    #endregion
    //카드 위치 이동에 따른 deck값 변경 함수
    #region
    public void OutputDeck(int i)
    {
        switch (i)
        {
            case 1:
                deck1 = false;
                break;
            case 2:
                deck2 = false;
                break;
            case 3:
                deck3 = false;
                break;
            case 4:
                deck4 = false;
                break;
            case 5:
                deck5 = false;
                break;
            case 6:
                deck6 = false;
                break;
        }
    }
    #endregion
    //현재 카드 위치값 반환
    #region
    public bool IsDeck1()
    {
        return deck1;
    }
    public bool IsDeck2()
    {
        return deck2;
    }
    public bool IsDeck3()
    {
        return deck3;
    }
    public bool IsDeck4()
    {
        return deck4;
    }
    public bool IsDeck5()
    {
        return deck5;
    }
    public bool IsDeck6()
    {
        return deck6;
    }
    #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.CardBase);
            stream.SendNext(this.rndNum);
            stream.SendNext(this.location);
        }
        else
        {
            this.CardBase = (GameObject)stream.ReceiveNext();
            this.rndNum = (int)stream.ReceiveNext();
            this.location = (Vector3)stream.ReceiveNext();
        }
    }
}
//Card클래스
public class Card
{
    public string Name { get; set; }
    public int ImgNum { get; set; }
    public string Type { get; set; }
    public int Atk { get; set; }
    public int level { get; set; }
    public int SkillNum { get; set; }
    public int MissileNum { get; set; }
}