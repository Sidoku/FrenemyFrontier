using System;
using System.Collections;
using Photon.Pun;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Photon.Pun;
using TMPro;

public class TaskInteraction : MonoBehaviourPunCallbacks
{
   private float _timer = 10.0f;
   public bool _isTimerStarted = false;
   private IEnumerator _taskTimer;
   public bool _isTaskCompleted = false;
    bool firstAttempt = false;
   private EarningCoins earnCoins;
    [SerializeField] GameObject closedTressure;
    [SerializeField] GameObject OpenedTressure;
    public TaskSpawner taskSpawner;
    GameObject currentCrim;
    [SerializeField] TMP_Text timertext;
    GameObject FirstCrim;
    // Update is called once per frame

    
    private void Start()
    {
        taskSpawner = GetComponent<TaskSpawner>();
    }

    void Update()
    {
       
        if (_isTimerStarted)
        {
            _timer -= Time.deltaTime;
        
        }

        if (_timer <= 0f)
        {
            Debug.LogWarning("Task completed");
            ResetTimer();
            //StartCoroutine(EarncoinsAnDChangeLocation());
            if(photonView.IsMine)
            {
                earnCoins.EarnCoins();
                
            }
            taskSpawner.GetComponent<PhotonView>().RPC("TaskRespawn", RpcTarget.AllBuffered);
        }

        timertext.text = _timer.ToString();
    }
    private void OnTriggerStay(Collider other)
    {

       // Debug.LogError(other.gameObject.tag);

        if (other.gameObject.CompareTag("CR") && Input.GetKey(KeyCode.E) && !firstAttempt)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                firstAttempt = true;
                currentCrim = other.gameObject;
                earnCoins = currentCrim.GetComponent<EarningCoins>();
            }

            // currentCrim = other.gameObject;
            // FirstCrim = currentCrim;
            currentCrim.GetComponent<ThirdPersonController>().onHold = true;
            currentCrim.GetComponentInChildren<AnimationsManager>().anim.Play("Task");
            //Debug.LogWarning("Timer is running");
            if (!_isTimerStarted)
            {
                //timertext.transform.LookAt(other.transform.FindRecursively("PlayerCamera").position);
                _isTimerStarted = true;
                closedTressure.SetActive(false);
                OpenedTressure.SetActive(true);
            }
            // _isTimerStarted = true;
           
            
        }
        else
        {
            {
                other.gameObject.GetComponent<ThirdPersonController>().onHold = false;
               // Debug.LogWarning("Player hold removed");
                ResetTimer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.LogWarning("Exiting trigger");
        other.gameObject.GetComponent<ThirdPersonController>().onHold = false;
        ResetTimer();
    }

   private void ResetTimer()
   {
        firstAttempt = false;
       _isTimerStarted = false;
        closedTressure.SetActive(true);
        OpenedTressure.SetActive(false);
        _timer = 10.0f;
   }
   
    IEnumerator EarncoinsAnDChangeLocation()
    {
        yield return new WaitForSeconds(2f);
       
    }

}
