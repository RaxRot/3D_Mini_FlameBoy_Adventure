using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
     private Animator _anim;
    
    [SerializeField] private GameObject coinFx;
    

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            Instantiate(coinFx, transform.position, Quaternion.identity);
            
            UIManager.Instance.CollectCoin();
            
            Destroy(gameObject);
        }
    }
}
