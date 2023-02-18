using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject checkPointFx;
    [SerializeField] private Transform pointToSpawnFx;

    [SerializeField] private Animator checkAnim;
    
    private bool _isActive;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG) &&!_isActive)
        {
            _isActive = true;
            
            LevelManager.Instance.SetNewRespawnPoint(transform.position);
            
            Instantiate(checkPointFx, pointToSpawnFx.position, Quaternion.identity);
            
            checkAnim.SetTrigger(TagManager.ACTIVATE_CHECKPOINT_TRIGGER);
        }
    }
}
