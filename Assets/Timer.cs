using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Timer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private float maxTime = 60.0f;
    private float currentTime = 0.0f;
    private bool isTimerRunning = false;
    public TMP_Text timerText;

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        currentTime = maxTime;
        isTimerRunning = true;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void Update()
    {
        if (isTimerRunning)
        {
            
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                // Perform any actions you want to do when the timer runs out here
            }
        }

        // Convert the remaining time to minutes and seconds and display it in the format "00:00"
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeString;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentTime);
            stream.SendNext(isTimerRunning);
        }
        else
        {
            currentTime = (float)stream.ReceiveNext();
            isTimerRunning = (bool)stream.ReceiveNext();
        }
    }
}
