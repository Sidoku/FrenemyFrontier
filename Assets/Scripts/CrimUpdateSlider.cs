using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class CrimUpdateSlider : MonoBehaviourPunCallbacks
{
    public Slider slider;
    private float currentValue;
    

    void Start()
    {
        // Set the initial value of the slider to its max value (30 in this case)
        currentValue = slider.maxValue;
        slider.value = currentValue;
        slider.gameObject.SetActive(false);
        // Start the coroutine to reduce the slider value

    }


    public void mainReduceSlider()
    {
        //this.GetComponent<PhotonView>().RPC("CrimReduceSlider", RpcTarget.AllBuffered);
        CrimReduceSlider();
    }


    //[PunRPC]
    public void CrimReduceSlider()
    {
        if (photonView.IsMine)
        {
            slider.gameObject.SetActive(true);
         
            StartCoroutine(CrimReduceSliderValue());
        }

    }
    IEnumerator CrimReduceSliderValue()
    {
        if(photonView.IsMine)
        {
            while (true)
            {
              
                    yield return new WaitForSeconds(1f); // Wait for 1 second

                    // Reduce the slider value by 1
                    currentValue--;

                    // Update the slider value
                    slider.value = currentValue;
                
               


                if (this.GetComponent<GotHold>().gotHold == false)
                {
                    currentValue = slider.maxValue;
                    slider.value = currentValue;
                    slider.gameObject.SetActive(false);
                    
                    break;
                }

                // If the slider value reaches the minimum value (0), reset it to the maximum value (30)
                if (currentValue <= slider.minValue)
                {
                    currentValue = slider.maxValue;
                    slider.value = currentValue;
                    slider.gameObject.SetActive(false);
                   
                    break;
                }
            }
        }

        
    }
}
