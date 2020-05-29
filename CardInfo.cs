using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CardInfo : MonoBehaviour//PunCallbacks, IPunObservable
{
    public string Name;
    public string Type;
    public int Atk;
    public int AtkUp;
    public int level;
    public int SkillNum;
    public int MissileNum;
    public int ImgNum;

    private GameObject star1;
    private GameObject star2;
    private GameObject star3;
    //public string Name { get; set; }
    //public int ImgNum { get; set; }
    //public string Type { get; set; }
    //public int Atk { get; set; }
    //public int level { get; set; }
    //public int SkillNum { get; set; }
    //public int MixNum { get; set; }

    private void Start()
    {
        //별 이미지 보여주기
        star1 = transform.GetChild(0).GetChild(2).gameObject;
        star2 = transform.GetChild(0).GetChild(3).gameObject;
        star3 = transform.GetChild(0).GetChild(4).gameObject;
        if(level<=2)
        {
            star3.SetActive(false);
        }
        if(level==1)
        {
            star2.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        //공격력 총합
        AtkUp = AtkUpgrade();
    }
    //공격력 총합
    private int AtkUpgrade()
    {
        if (Type == "sky")
        {
            AtkUp = Atk + GameManager.instance.skyAtkPlus;
        }
        if (Type == "earth")
        {
            AtkUp = Atk + GameManager.instance.earthAtkPlus;
        }
        if (Type == "hell")
        {
            AtkUp = Atk + GameManager.instance.hellAtkPlus;
        }
        return AtkUp;
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if(stream.IsWriting)
    //    {
    //        stream.SendNext(this.transform.position);
    //        stream.SendNext(this.Name);
    //        stream.SendNext(this.transform.position);
    //        stream.SendNext(this.transform.position);
    //        stream.SendNext(this.transform.position);
    //
    //    }
    //    else
    //    {
    //
    //    }
    //}
}
