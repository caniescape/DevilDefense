using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPun//, IPunObservable
{
    public static GameManager instance;

    public Text spText;
    GameObject Hp1;
    GameObject Hp2;
    GameObject Hp3;
    GameObject OpHp1;
    GameObject OpHp2;
    GameObject OpHp3;
    int hp = 3;
    int opHp = 3;
    public int sp;
    //종족값 Upgrade
    public int skyAtkPlus;
    public int earthAtkPlus;
    public int hellAtkPlus;
    //upgrade필요 sp
    int skyNeedSp=100;
    int earthNeedSp=100;
    int hellNeedSp=100;
    //upgrade Level
    int skyLv;
    int earthLv;
    int hellLv;
    //upgrade UI Level
    public Text skyLvText;
    public Text earthLvText;
    public Text hellLvText;

    //Timer관련
    public float defaultLimitTime;
    public float limitTime;
    float limitTimeSec;
    int limitTimeMin;
    public Text textTimer;

    public bool isLose;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        defaultLimitTime = limitTime;
        //초기값 할당
        sp = 5000;
        Hp1 = GameObject.Find("PHp (1)");
        Hp2 = GameObject.Find("PHp (2)");
        Hp3 = GameObject.Find("PHp (3)");
        OpHp1 = GameObject.Find("OpHp (1)");
        OpHp2 = GameObject.Find("OpHp (2)");
        OpHp3 = GameObject.Find("OpHp (3)");
    }
    
    void FixedUpdate()
    {
        //Timer
        //Wave가 시작되면 초기화
        limitTime -= Time.deltaTime;
        limitTimeSec = (int)limitTime % 60;
        limitTimeMin = (int)limitTime / 60;

        textTimer.text = "남은시간 " +limitTimeMin+" : "+ limitTimeSec;
        //시간초과면 GameOver
        if (limitTime<=0||isLose)
        {
            GameOver();
        }
        //텍스트 갱신
        spText.text = sp.ToString();
        skyLvText.text = "Lv." + skyLv.ToString();
        earthLvText.text = "Lv." + earthLv.ToString();
        hellLvText.text = "Lv." + hellLv.ToString();
        //Debug.Log(hp);
    }
    //체력 감소
    public void DecreaseHp()
    {
        if(hp == 3)
        {
            hp--;
            Hp1.gameObject.SetActive(false);
        }
        else if (hp == 2)
        {
            hp--;
            Hp2.gameObject.SetActive(false);
        }
        else if (hp == 1)
        {
            hp--;
            Hp3.gameObject.SetActive(false);
            Debug.Log("게임오버");
            GameOver();
        }
    }
    //체력 감소
    public void OpDecreaseHp()
    {
        if (opHp == 3)
        {
            opHp--;
            OpHp1.gameObject.SetActive(false);
        }
        else if (opHp == 2)
        {
            opHp--;
            OpHp2.gameObject.SetActive(false);
        }
        else if (opHp == 1)
        {
            opHp--;
            OpHp3.gameObject.SetActive(false);
            Debug.Log("게임오버");
        }
    }

    //sky업그레이드
    public void SkyUpgrade()
    {
        //sp가 upgrade필요 sp이상일때
        if (sp >= skyNeedSp)
        {
            //어택포인트 증가시키고 sp소모 및 필요sp증가
            skyAtkPlus = skyAtkPlus+1;
            Debug.Log(skyAtkPlus);
            sp = sp - skyNeedSp;
            skyNeedSp += skyNeedSp + 100;
            //UI레벨증가
            skyLv++;
        }
        else
        {
            Debug.Log("업그레이드비용 부족");
        }
    }
    //Earth업그레이드
    public void EarthUpgrade()
    {
        //sp가 upgrade필요 sp이상일때
        if (sp >= earthNeedSp)
        {
            //어택포인트 증가시키고 필요sp증가
            earthAtkPlus += earthAtkPlus;
            sp = sp - earthNeedSp;
            earthNeedSp += earthNeedSp + 100;
            //UI레벨증가
            earthLv++;
        }
        else
        {
            Debug.Log("업그레이드비용 부족");
        }
    }
    //Hell업그레이드
    public void HellUpgrade()
    {
        //sp가 upgrade필요 sp이상일때
        if (sp >= hellNeedSp)
        {
            //어택포인트 증가시키고 필요sp증가
            hellAtkPlus += hellAtkPlus;
            sp = sp - hellNeedSp;
            hellNeedSp += hellNeedSp + 100;
            //UI레벨증가
            hellLv++;
        }
        else
        {
            Debug.Log("업그레이드비용 부족");
        }
    }

    public void ResetTime()
    {
        limitTime = defaultLimitTime;
    }

    //항복함수
    public void Surrender()
    {
        GameOver();
    }
    //GameOver함수
    public void GameOver()
    {
        //다른사람에게 Win함수를 실행하고 나는 Defeat 출력
        photonView.RPC("Win", RpcTarget.Others);
        GameObject.Find("WaveText").transform.Find("Defeat").gameObject.SetActive(true);
    }
    //다른사람에게 Win함수를 실행
    [PunRPC]
    public void Win()
    {
        GameObject.Find("WaveText").transform.Find("Win").gameObject.SetActive(true);
    }
}
