using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Playerを制御するクラス </summary>
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerContoller : MonoBehaviour
{
    /// <summary> 参照しているクラスの変数 </summary>
    [SerializeField] DynamicJoystick _DynamicJoystick = null;
    /// <summary> アニメーターの変数 </summary>
    //[SerializeField] Animator _Anim = null;
    /// <summary> プレイヤーのスピード </summary>
    [SerializeField] float _Speed = 1f;
    /// <summary> 参照しているクラスの変数 </summary>
    private CharacterController _CharacterController = null;
    /// <summary> RigidBodyの変数 </summary>
    private Rigidbody _Rb = null;
    private Vector3 _Direction;
    
    private void Start()
    {
        _CharacterController = GetComponent<CharacterController>();
        _Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //TODO: 後で書く
        if (_Direction != new Vector3(0, 0, 0))
        {
            transform.localRotation = Quaternion.LookRotation(_Direction);
        }

        _CharacterController.Move(_Direction * _Speed * Time.deltaTime);
    }

    public void FixedUpdate()
    {
        //TODO: 後で書く
        _Direction = Vector3.forward * _DynamicJoystick.Vertical + Vector3.right * _DynamicJoystick.Horizontal;
        _Rb.AddForce(_Direction * _Speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
