using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class SimplePUN : MonoBehaviourPunCallbacks
{
    public Text m_txtNickname;
    public Text m_txtMessage;

    public Button m_btnJoinRoom;
    public Button m_btnJoinArena;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log(PhotonNetwork.AutomaticallySyncScene);
        m_btnJoinRoom.interactable = false;
        PhotonNetwork.PhotonServerSettings.AppSettings.EnableLobbyStatistics = true;

        //PlayerPrefs.SetString("photon_nickname", "Unityエディター");
        //PlayerPrefs.Save();
        if (!PlayerPrefs.HasKey("photon_nickname"))
        {
            PlayerPrefs.SetString("photon_nickname", "実行ファイル");
        }
        PhotonNetwork.NickName = PlayerPrefs.GetString("photon_nickname", "noname");
        m_txtNickname.text = PhotonNetwork.NickName;

        //旧バージョンでは引数必須でしたが、PUN2では不要です。
        PhotonNetwork.ConnectUsingSettings();

        m_btnJoinRoom.onClick.AddListener(() =>
        {
            PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
        });

        m_btnJoinArena.onClick.AddListener(() =>
        {
            LoadArena();
        });
        m_btnJoinArena.interactable = false;
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        Debug.Log("OnRoomPropertiesUpdate");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        Debug.Log("OnPlayerPropertiesUpdate");
    }


    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("OnJoinedLobby");
        m_btnJoinRoom.interactable = true;
    }
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        base.OnLobbyStatisticsUpdate(lobbyStatistics);
        Debug.Log("OnLobbyStatisticsUpdate");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
        base.OnRoomListUpdate(roomList);
        foreach(RoomInfo info in roomList)
        {
            Debug.Log(string.Format("roomname:{0} ,PlayerCount:{1}",
                info.Name,
                info.PlayerCount));
        }
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        //キャラクターを生成
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (2 <= PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                m_btnJoinArena.interactable = true;
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            }
        }

    }

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("test");
    }

}