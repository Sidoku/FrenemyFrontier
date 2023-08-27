using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class LagCompensation : MonoBehaviourPunCallbacks, IPunObservable
{
    // Variables to store the transform and rotation data
    private Vector3 networkPosition;
    private Quaternion networkRotation;

    // Variables to store the interpolation data
    private float interpolationFactor = 0f;
    private float interpolationTime = 0f;
    private Queue<NetworkState> networkStates = new Queue<NetworkState>();

    // Struct to hold networked state data
    private struct NetworkState
    {
        public float time;
        public Vector3 position;
        public Quaternion rotation;
        public int state;
        public float normalizedTime;
    }

    // Variables to store the animator data
    private Animator animator;
    private int currentState;
    private float currentNormalizedTime;
    private int lastState = -1;
    private float lastNormalizedTime = 0f;

    private void Awake()
    {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;
        animator = GetComponentInChildren<Animator>();
    }

    // Called by Photon to serialize data
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send transform, rotation, and animator data to other clients
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
            stream.SendNext(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
        else
        {
            // Receive transform, rotation, and animator data from other clients
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            currentState = (int)stream.ReceiveNext();
            currentNormalizedTime = (float)stream.ReceiveNext();

            // Calculate the time it took to receive the data
            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));

            // Store the networked state data
            NetworkState state = new NetworkState();
            state.time = Time.time - lag;
            state.position = networkPosition;
            state.rotation = networkRotation;
            state.state = currentState;
            state.normalizedTime = currentNormalizedTime;
            networkStates.Enqueue(state);
        }
    }

    private void Update()
    {
        // If there are no networked states, return
        if (networkStates.Count == 0)
        {
            return;
        }

        // Calculate the interpolation factor and time
        interpolationFactor = Mathf.Clamp01((Time.time - interpolationTime) / (networkStates.Peek().time - interpolationTime));
        interpolationTime = Time.time;

        // Interpolate between the current and next networked state
        transform.position = Vector3.Lerp(transform.position, networkStates.Peek().position, interpolationFactor);
        transform.rotation = Quaternion.Lerp(transform.rotation, networkStates.Peek().rotation, interpolationFactor);

        // If the current time is greater than the next networked state time, dequeue it
        if (Time.time >= networkStates.Peek().time)
        {
            NetworkState state = networkStates.Dequeue();

            // If the animator state has changed, set the new state and normalized time
            if (state.state != lastState)
            {
                animator.Play(state.state, 0, state.normalizedTime);
            }
            else
            {
                // Otherwise, interpolate the normalized time
                animator.Play(state.state, 0, Mathf.Lerp(lastNormalizedTime, state.normalizedTime, interpolationFactor));
            }

            // Update the last state and normalized time
            lastState = state.state;
            lastNormalizedTime = state.normalizedTime;
        }
    }
}

