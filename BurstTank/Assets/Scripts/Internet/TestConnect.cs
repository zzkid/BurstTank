using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon; 
public class TestConnect : MonoBehaviour,IPhotonPeerListener
{
    #region--Valiable
    public PhotonPeer peer;
    public enum ClientState : byte
    {
        DisConnect,
        Connecting,
        Connected,
        ConnectFailed,
        LoginSuccess,
        LoginFailed
    }
    ClientState state = ClientState.DisConnect;

    GameObject userboard;
    GameObject pwdboard;
    GameObject loginBtn;
    GameObject youkeBtn;
    GameObject registerBtn;
    #endregion

    #region--Unity Func
    void OnGUI()
    {
    }
    void Awake()
    {

    }
    void Start () {
        peer = new PhotonPeer(this,ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "MyServer");
        state = ClientState.Connecting;

        userboard = GameObject.Find("userBoard");
        pwdboard = GameObject.Find("pwdBoard");
        loginBtn = GameObject.Find("LoginBtn");
        youkeBtn = GameObject.Find("youkeLoginBtn");
        registerBtn = GameObject.Find("RegisterBtn");
        UIEventListener.Get(loginBtn).onClick = OnLoginBtnClick;
        UIEventListener.Get(youkeBtn).onClick = OnyoukeBtnClick;
        UIEventListener.Get(registerBtn).onClick = OnRegisterBtnClick;
	}
	void Update () {
        peer.Service();
	}
    #endregion

    #region--Photon Func
    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnEvent(EventData eventData)
    {
        
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        switch (operationResponse.OperationCode)
        {
            case (byte)ScOpEnumCommon.OpCodeEnum.LoginSuccess:
                Debug.Log("login success!");
                state = ClientState.LoginSuccess;
                Application.LoadLevel("Lobby");
                break;
            case (byte)ScOpEnumCommon.OpCodeEnum.LoginFailed:
                Debug.Log("login Failed!");
                state = ClientState.LoginFailed;
                StartCoroutine(loginFailedHandle());
                break;
        }  
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                Debug.Log("Connect Success! Time:" + Time.time);
                state = ClientState.Connected;
                break;
            case StatusCode.Disconnect:
                state = ClientState.ConnectFailed;
                StartCoroutine(connectFailedHandle());
                Debug.Log("Disconnect! Time:" + Time.time);
                break;
        }  
    }
    #endregion

    #region--My Func
    IEnumerator connectFailedHandle()
    {
        yield return new WaitForSeconds(1);
        state = ClientState.DisConnect;
    }

    void userLogin(string uname, string pwd)
    {
        Dictionary<byte, object> param = new Dictionary<byte, object>();
        param.Add((byte)ScOpEnumCommon.OpKeyEnum.UserName, uname);
        param.Add((byte)ScOpEnumCommon.OpKeyEnum.PassWord, pwd);
        peer.OpCustom((byte)ScOpEnumCommon.OpCodeEnum.Login, param, true);
    }

    IEnumerator loginFailedHandle()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("loginFailedHandle");
        state = ClientState.Connected;
    }

    void OnLoginBtnClick(GameObject go)
    {
        string username = userboard.GetComponent<UIInput>().value;
        string pwd = pwdboard.GetComponent<UIInput>().value;
        userLogin(username, pwd);
    }
    void OnyoukeBtnClick(GameObject go)
    {
        Debug.Log("you ke denglu!");
    }
    void OnRegisterBtnClick(GameObject go)
    {
        Debug.Log("Register!");
    }
    #endregion
}
