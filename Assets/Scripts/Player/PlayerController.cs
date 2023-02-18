using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private CameraController _cameraController;

    [SerializeField]private Animator anim;
    
    [SerializeField] private float moveSpeed;
    
    private Vector3 _movePosition;
    private float _zAxis;
    private float _xAxis;
    
    private float _yStore;

    [SerializeField] private float jumpForce,gravityScale;

    [SerializeField] private float rotateSpeed=10f;

    [SerializeField] private GameObject jumpParticle;
    [SerializeField] private Transform pointToSpawnJumpParticle;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        PlayerMovement();
        AnimatePlayer();
    }

    private void FixedUpdate()
    {
       ApplyGravity();
    }

    private void PlayerMovement()
    {
        _yStore = _movePosition.y;

        _xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        _zAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);
        
        _movePosition = _cameraController.transform.forward * _zAxis+
                      _cameraController.transform.right*_xAxis;
        _movePosition.y = 0f;
        _movePosition = _movePosition.normalized;

        if (_movePosition.magnitude>0.1f && _movePosition!=Vector3.zero)
        { 
            RotatePlayer();
        }

        _movePosition.y = _yStore;

        if (_characterController.isGrounded &&Input.GetKeyDown(KeyCode.Space))
        {
           Jump();
        }

        _characterController.Move(new Vector3(_movePosition.x*moveSpeed,_movePosition.y,_movePosition.z*moveSpeed)*Time.deltaTime);
    }

    private void RotatePlayer()
    {
        Quaternion newRot = Quaternion.LookRotation(_movePosition);
                
        transform.rotation=Quaternion.Slerp(transform.rotation,newRot,rotateSpeed*Time.deltaTime);
    }

    private void AnimatePlayer()
    {
        float moveVel = new Vector3(_movePosition.x, 0f, _movePosition.z).magnitude * moveSpeed;
        
        anim.SetFloat(TagManager.PLAYER_SPEED_ANIM_PARAMETR,moveVel);
        anim.SetBool(TagManager.PLAYER_IS_GROUNDED_PARAMETR,_characterController.isGrounded);
        anim.SetFloat(TagManager.PLAYER_Y_VELOCITY_PARAMETR,_movePosition.y);
    }

    private void Jump()
    { 
        _movePosition.y = jumpForce;

        Instantiate(jumpParticle, pointToSpawnJumpParticle.position, Quaternion.identity);
    }

    private void ApplyGravity()
    {
        if (!_characterController.isGrounded)
        {
            _movePosition.y +=Physics.gravity.y*gravityScale*Time.deltaTime;
        }
        else
        {
            _movePosition.y =Physics.gravity.y*gravityScale*Time.deltaTime;
        }
    }
}
