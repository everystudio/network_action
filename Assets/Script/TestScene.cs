using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TestScene : MonoBehaviour
{
    public Text m_txtNickname;
    // Start is called before the first frame update
    void Start()
    {
        m_txtNickname.text = PhotonNetwork.NickName;

        Debug.Log(string.Format("PlayerCount:{0}", PhotonNetwork.CurrentRoom.PlayerCount));
        foreach(KeyValuePair<int,Player> pair in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(pair.Value.NickName);
        }
        GameObject monster = PhotonNetwork.Instantiate("unitychan_net", Vector3.zero, Quaternion.identity, 0);
        //自分だけが操作できるようにスクリプトを有効にする
        monster.SetActive(true);
        PlayerControl monsterScript = monster.GetComponent<PlayerControl>();
        monsterScript.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
