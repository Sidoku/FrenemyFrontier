using System;
using System.Collections;
using Photon.Pun;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class TaskInteraction : MonoBehaviour
{
   private float _timer = 0.0f;
   public bool _isTimerStarted = false;
   private IEnumerator _taskTimer;
   public bool _isTaskCompleted = false;
   private EarningCoins earnCoins;
   
   public TaskSpawner taskSpawner;
    // Update is called once per frame

    
    private void Start()
    {
        taskSpawner = GetComponent<TaskSpawner>();
    }

    void Update()
    {
        if (_isTimerStarted)
        {
            _timer += Time.deltaTime;
        }

        if (_timer > 10f)
        {
            Debug.LogWarning("Task completed");
            ResetTimer();
            earnCoins.EarnCoins();
            taskSpawner.TaskRespawn(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {

       // Debug.LogError(other.gameObject.tag);

        if (other.gameObject.CompareTag("CR") && Input.GetKey(KeyCode.E))
        {
            other.gameObject.GetComponent<ThirdPersonController>().onHold = true;
           //Debug.LogWarning("Timer is running");
            if (!_isTimerStarted)
            _isTimerStarted = true;
            earnCoins = other.gameObject.GetComponent<EarningCoins>();
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
       _isTimerStarted = false;
       _timer = 0.0f;
   }
   
}
