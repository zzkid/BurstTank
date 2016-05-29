using UnityEngine;
using System.Collections;

public class ScOpEnumCommon
{
   public enum OpCodeEnum : byte
    {
        //login  
        Login = 249,
        LoginSuccess = 248,
        LoginFailed = 247,

        //room  
        Create = 250,
        Join = 255,
        Leave = 254,
        RaiseEvent = 253,
        SetProperties = 252,
        GetProperties = 251
    }


   public enum OpKeyEnum : byte
    {
        RoomId = 251,
        UserName = 252,
        PassWord = 253
    }
}  
