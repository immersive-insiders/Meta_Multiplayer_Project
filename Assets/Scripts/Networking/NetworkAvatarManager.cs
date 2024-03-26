using Fusion;
using Oculus.Movement.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAvatarManager : NetworkBehaviour
{

    [SerializeField] private LinkPlayerAvater playerAvater;

    [SerializeField] private List<AvatarMeshAndMaterial> avatarMeshAndMaterialList;

    [SerializeField] private List<GameObject> avatarList;

    [SerializeField] private List<Avatar> avatarGender;

    [SerializeField] protected SkinnedMeshRenderer avatarMeshRenderer;
    public bool IsLocalNetworkRig => Object.HasStateAuthority;

    private int avatarIndex;

    private void Start()
    {
        
    }

    public override void Spawned()
    {
        base.Spawned();

        if (IsLocalNetworkRig)
        {
            avatarIndex = FindObjectOfType<AvatarManager>().AvatarIndex;
            Debug.Log(" <<< on start avatar index is " + avatarIndex);

            playerAvater.LinkPlayerAvatarToVR();
            Debug.Log("linked player to VR ");

            //UpdateAvatarApreance(avatarIndex);
            //Debug.Log("Updated apreance ");

        }
        if(HasInputAuthority)
        {
            RPC_UpdateAvatar(avatarIndex);

        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_UpdateAvatar(int index) 
    {
        UpdateAvatarApreance(index);
    }


    private void UpdateAvatarApreance(int  avatarIndex) 
    {
        avatarMeshRenderer.sharedMesh = avatarMeshAndMaterialList[avatarIndex].avatarMesh;
        avatarMeshRenderer.sharedMaterial = avatarMeshAndMaterialList[avatarIndex].avatarMaterial;
        //for (int i = 0; i < avatarList.Count;i++)
        //{
        //    Debug.Log("<<<<< Avatar Name " + avatarList[i].name);

        //    Debug.Log(" i = " + i +" avatarInedex= "+ avatarIndex);
        //    if( i == avatarIndex )
        //    {
        //        avatarList[i].SetActive(true);
        //        Debug.Log("<<<<<<  Avatar is true" + avatarList[i].name);
        //    }
        //    else
        //    {
        //        avatarList[i].SetActive(false);
        //        Debug.Log(" Avatar is false" + avatarList[i].name);

        //    }
        //}
    }
}

[Serializable]
public class AvatarMeshAndMaterial
{
    public Mesh avatarMesh;
    public Material avatarMaterial;
    public int gender;
}
