using ReadyPlayerMe.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] private List<Button> avatarButtons = new List<Button>();
    private Characters characters;
    private List<Transform> avatarObjects = new List<Transform>();
    private Animator avatarAnimator;

    private void Start()
    {
        characters = FindObjectOfType<Characters>();
        foreach (Transform avatar in characters.transform)
        {
            avatarObjects.Add(avatar);
            Debug.Log(" avatar name =" +  avatar.name);
        }
        for (int i = 0; i < avatarButtons.Count; i++)
        {
            int index = i; // Capturing the current value of i for the lambda expression
            avatarButtons[i].onClick.AddListener(() => SwitchAvatar(index));
        }
        avatarAnimator = avatarObjects[0].gameObject.GetComponent<Animator>();
    }

    private void SwitchAvatar(int index)
    {
        
        avatarAnimator.avatar = avatarObjects[index + 1].GetComponent<Avatar>();
        AvatarMeshHelper.TransferMesh(avatarObjects[index + 1].gameObject, avatarObjects[0].gameObject);
    }
}
