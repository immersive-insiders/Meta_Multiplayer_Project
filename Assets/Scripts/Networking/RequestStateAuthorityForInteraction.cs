using Fusion;
using Fusion.XR.Shared;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestStateAuthorityForInteraction : NetworkBehaviour
{
    [SerializeField] private PointableUnityEventWrapper handGrabInteractor;
    [SerializeField] private Rigidbody _rb;

    [Networked]
    private bool isGrabbed
    {
        get => _isGrabbed;
        set => _isGrabbed = value;
    }
  

    private bool _isGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        handGrabInteractor.WhenHover.AddListener(RequestAuthorityToInteract);
        handGrabInteractor.WhenSelect.AddListener(OnSelected);
        handGrabInteractor.WhenUnselect.AddListener(OnUnslected);
    }

    private void OnSelected(PointerEvent arg0)
    {
        _isGrabbed = true;
        Debug.Log("<<< Cube is grabbed " + _isGrabbed);
    }

    private void OnUnslected(PointerEvent arg0)
    {
        _isGrabbed = false;
        Debug.Log("<<< Cube is released " + _isGrabbed);

        if (_rb.isKinematic)
        {
            _rb.isKinematic = false;
        }

        if (!_rb.useGravity)
        {
            _rb.useGravity = true;
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void Grabbed()
    {

    }


    private void RequestAuthorityToInteract(PointerEvent arg0)
    {
        if (!_isGrabbed)
            Object.RequestStateAuthority();
        else
        {
            Debug.Log("<<<< Cube is grabbed you can't get authority");
        }
            //Grab();
    }

    public async void Grab()
    {
        await Object.WaitForStateAuthority();
        if (Object.HasStateAuthority == false)
        {
            Debug.LogError("Unable to receive state authority");
            return;
        }
    }
}
