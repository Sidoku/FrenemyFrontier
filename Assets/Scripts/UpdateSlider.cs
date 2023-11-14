using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UpdateSlider : MonoBehaviourPunCallbacks
{
    public Slider slider;
    private float currentValue;
    GameObject criminal;
    bool releasedbyjail = false;
    public LayerMask layerMask;
    void Start()
    {
       
        currentValue = slider.maxValue;
        slider.value = currentValue;
        slider.gameObject.SetActive(false);
        

    }



    public void MainReduce(int vid)
    {
        this.gameObject.GetComponent<PhotonView>().RPC("SetCriminal", RpcTarget.AllBuffered,vid);
        this.gameObject.GetComponent<PhotonView>().RPC("ReduceSlider", RpcTarget.AllBuffered);
      
        
    }

    [PunRPC]
    public void ReduceSlider()
    {
        if(photonView.IsMine)
        {
            slider.gameObject.SetActive(true);
          
            StartCoroutine(ReduceSliderValue());
        }
     
    }
    IEnumerator ReduceSliderValue()
    {
        if(photonView.IsMine)
        {
            while (true)
            {
              
                    yield return new WaitForSeconds(1f); 

                  
                    currentValue--;

                    
                    slider.value = currentValue;


             
               

                if (releasedbyjail)
                {
                    currentValue = slider.maxValue;
                    slider.value = currentValue;
                    slider.gameObject.SetActive(false);
                    this.gameObject.GetComponent<CatchCrim>().gotCrim = false;
                    releasedbyjail=false;
                    
                    break;
                }

                if (this.GetComponent<DamageControlBon>().Health <= 0)
                {
                    currentValue = slider.maxValue;
                    slider.value = currentValue;
                    slider.gameObject.SetActive(false);
                    this.gameObject.GetComponent<CatchCrim>().gotCrim = false;
                   
                    break;
                }
               

              
                if (currentValue <= slider.minValue)
                {
                    currentValue = slider.maxValue;
                    slider.value = currentValue;
                    slider.gameObject.SetActive(false);
                    this.gameObject.GetComponent<CatchCrim>().gotCrim = false;
                  
                    break;
                }
            }
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "jail")
        {
            releasedbyjail= true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "jail")
        {
            releasedbyjail = false;
        }
    }
}
