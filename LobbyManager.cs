using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    public Text connectionInfoText;

    private string nickName="";
    public Text nickNameInput;
    public GameObject NickNameInputField;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;

        PhotonNetwork.ConnectUsingSettings();

        connectionInfoText.text = "마스터 서버에 접속중";
        
    }
    void FixedUpdate()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                //로딩씬으로 넘어감
                PhotonNetwork.LoadLevel(1);
            }
        }
    }
    public override void OnConnectedToMaster()
    {
        connectionInfoText.text = "온라인";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectionInfoText.text = "오프라인";

        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        //마스터 서버에 접속중이라면
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = nickName;
            connectionInfoText.text = "룸에 접속...";
            //회전이미지
            GameObject waitImg = GameObject.Find("Canvas").transform.Find("wait").gameObject;
            waitImg.SetActive(true);

            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //마스터 서버에 접속중이 아니라면, 마스터 서버에 접속 시도
            connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";
            //마스터 서버로의 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //접속 상태 표시
        connectionInfoText.text = "빈 방이 없음, 새로운 방 생성";
        //최대 2명 수용가능한 빈방 생성
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    //룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        //접속 상태 표시
        connectionInfoText.text = "방 참가 성공";
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            connectionInfoText.text = "시작";
           
        }
        else
        {
            connectionInfoText.text = "대기중";
        }
    }
    //닉네임입력버튼 활성화
    public void InputNickName()
    {
        //닉네임 저장하고 입력창 비활성화
        nickName = nickNameInput.text;
        NickNameInputField.SetActive(false);
    }
}
