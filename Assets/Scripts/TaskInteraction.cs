using System;
using System.Collections;
using Photon.Pun;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.UI;
using TMPro;
using Unity.VisualScripting;

public class TaskInteraction : MonoBehaviourPunCallbacks
{
   private float _timer = 10.0f;
  // public bool _isTimerStarted = false;
  // private IEnumerator _taskTimer;
  // public bool _isTaskCompleted = false;
    private bool treasureIsTaken = false;
    private EarningCoins earnCoins;
    [SerializeField] GameObject closedTressure;
    [SerializeField] GameObject OpenedTressure;
    public TaskSpawner taskSpawner;
    GameObject currentCrim;
    [SerializeField] TMP_Text timertext;
    GameObject FirstCrim;
       
    private enum InteractionState
    {
        Idle,
        Interacting
    }
    
    private InteractionState currentState = InteractionState.Idle;
    
    private void Start()
    {
        taskSpawner = GetComponent<TaskSpawner>();
        if(!photonView.IsMine)
        {
            this.gameObject.SetActive(false);
        }

    }
    
// Update is called once per frame
    void Update()
    {
       
       if (photonView.IsMine)
       {
           if(currentState== InteractionState.Interacting) 
           {
               _timer -= Time.deltaTime;
               SetPlayerInteractions(currentCrim);
               if (_timer <= 0f)
               {
                   Debug.LogWarning("Task completed");
                   //ResetTimer();
                   //StartCoroutine(EarncoinsAnDChangeLocation());
                   //photonView.RPC("ReleaseTreasure", RpcTarget.All);
                   ReleaseTreasure();
                   earnCoins.EarnCoins();
                   taskSpawner.GetComponent<PhotonView>().RPC("TaskRespawn", RpcTarget.AllBuffered);
                   // currentState = InteractionState.Completed;
                 
               }

               timertext.text = _timer.ToString();
               timertext.text = Math.Floor(_timer).ToString();
           }

       }
       else
       {
            this.gameObject.SetActive(false);   
       }
     
    }
    private void OnTriggerStay(Collider other)
    {

        // Debug.LogError(other.gameObject.tag);
        /*
        if (photonView.IsMine)
        {
            if (other.gameObject.CompareTag("CR") && Input.GetKey(KeyCode.E) && !treasureIsTaken)
            {

               
                // currentCrim = other.gameObject;
                // FirstCrim = currentCrim;
                other.gameObject.GetComponent<ThirdPersonController>().onHold = true;
                other.gameObject.GetComponentInChildren<AnimationsManager>().anim.Play("Task");
                //Debug.LogWarning("Timer is running");
                if (!_isTimerStarted)
                {
                    //timertext.transform.LookAt(other.transform.FindRecursively("PlayerCamera").position);
                    _isTimerStarted = true;
                   
                }
                // _isTimerStarted = true;

                earnCoins = other.gameObject.GetComponent<EarningCoins>();
            }
            else
            {
              // if (other.gameObject.CompareTag("CR") && !Input.GetKey(KeyCode.E))
               // {
                   other.gameObject.GetComponent<ThirdPersonController>().onHold = false;
                   Debug.LogWarning("Player hold removed");
                   ResetTimer();
              // }


            }
        }
        */

        if (photonView.IsMine && other.CompareTag("CR"))
        {
            
            if (Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<ThirdPersonController>().onHold = true;
                if (currentState == InteractionState.Idle)
                {
                   // photonView.RPC("SetInteractingState", RpcTarget.All);
                    SetInteractingState();
                    currentCrim = other.gameObject;
                    
                }
            }
            else
            {
                other.gameObject.GetComponent<ThirdPersonController>().onHold = false;
                // Reset the interaction state and timer if the player releases the 'E' button.
                if (currentState == InteractionState.Interacting)
                {
                    ResetInteractionState();
                   
                    
                    //photonView.RPC("ResetInteractionState", RpcTarget.All);
                  
                   
                }
                
            }
        }



    }
    
    private void OnTriggerExit(Collider other)
    {
        if (photonView.IsMine && other.CompareTag("CR"))
        {
            other.gameObject.GetComponent<ThirdPersonController>().onHold = false;
            ResetInteractionState();
            ReleaseTreasure();
        }
           


    }
    

    /*
   private void ResetTimer()
   {
        photonView.RPC("SetTressureFree", RpcTarget.AllBuffered);
        _isTimerStarted = false;
     
        _timer = 10.0f;
   }
    */
 
    public void ResetInteractionState()
    {
        currentState = InteractionState.Idle;
        // Inform other players that the interaction state has been reset.
       treasureIsTaken= false;
    }

  
    public void ReleaseTreasure()
    {
        // Perform actions to give the treasure to the player.
        // Reset the treasure state.
        currentState = InteractionState.Idle;
        _timer = 10.0f;
        treasureIsTaken = false;
       // photonView.RPC("UpdateTreasureState", RpcTarget.All, false);
        closedTressure.SetActive(true);
        OpenedTressure.SetActive(false);
        // Update the visual state of the treasure to indicate it's available.
    }


    
    public void SetInteractingState()
    {
        currentState = InteractionState.Interacting;
        treasureIsTaken= true;
        //photonView.RPC("UpdateTreasureState", RpcTarget.All, true);
        _timer = 10.0f;
      
        // Inform other players that the interaction state has changed.
    }

    void SetPlayerInteractions(GameObject player)
    {
        
            player.GetComponent<ThirdPersonController>().onHold = true;
            player.GetComponentInChildren<AnimationsManager>().anim.Play("Task");
            earnCoins = player.GetComponent<EarningCoins>();
            closedTressure.SetActive(false);
            OpenedTressure.SetActive(true);
       
        
         
        
   
    }
}
