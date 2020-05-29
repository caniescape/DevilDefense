using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LoadingManager : MonoBehaviourPun
{


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadGameScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(0.1f);
        while(true)
        {
            //if(!PhotonNetwork.IsMasterClient)
            //{
            //    yield return new WaitForSeconds(1.0f);
            //}
            PhotonNetwork.LoadLevel("BattleScene");
            break;
        }
    }
}
