using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618
public class TimerNetwork : NetworkBehaviour
#pragma warning restore 618
{
    //private float m_time;

#pragma warning disable 618
    [Command]
#pragma warning restore 618
    public void CmdSetTimer(float time)
    {
        RpcTimerSet(time);
    }

#pragma warning disable 618
    [ClientRpc]
#pragma warning restore 618
    public void RpcTimerSet(float time)
    {
        if (!isLocalPlayer)
        {
            GetComponent<TimerController>().SetTimer(time);
        }
    }
}
