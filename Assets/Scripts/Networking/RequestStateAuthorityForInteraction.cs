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

    enum Status
    {
        NotGrabbed,
        Grabbed,
    }
    Status status = Status.NotGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        handGrabInteractor.WhenHover.AddListener(RequestAuthorityToInteract);
        handGrabInteractor.WhenSelect.AddListener(OnSelected);
        handGrabInteractor.WhenUnselect.AddListener(OnUnslected);
    }

    private void OnSelected(PointerEvent arg0)
    {
        status = Status.Grabbed;
    }

    private void OnUnslected(PointerEvent arg0)
    {
        status = Status.NotGrabbed;
        if (_rb.isKinematic)
        {
            _rb.isKinematic = false;
        }
        if (!_rb.useGravity)
        {
            _rb.useGravity = true;
        }
    }


    private void RequestAuthorityToInteract(PointerEvent arg0)
    {
        if(status == Status.NotGrabbed)
            Grab();
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
