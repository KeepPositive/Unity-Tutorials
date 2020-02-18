using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _playerMovementSpeed = 2f;
    [SerializeField] private float _playerRotationSpeed = 50f;
    [SerializeField] private float _gravity = -9.72f;
    private CharacterController m_characterController;
    private Vector3 m_velcity = Vector3.zero;
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        m_characterController = GetComponent<CharacterController>();
        Camera.main.transform.parent = transform;
        Camera.main.transform.localPosition = new Vector3(0f, 1.8f, 0f);
    }
    private void Update()
    {
        if (!isClient) return;
        ApplyGravity();
        var inputMove = Vector2.zero;
        var inputRot = 0f;
        if (Input.GetKey(KeyCode.W))
            inputMove.x += 1;
        if (Input.GetKey(KeyCode.S))
            inputMove.x -= 1;
        if (Input.GetKey(KeyCode.A))
            inputMove.y -= 1;
        if (Input.GetKey(KeyCode.D))
            inputMove.y += 1;
        if (Input.GetKey(KeyCode.Q))
            inputRot -= 1f;
        if (Input.GetKey(KeyCode.E))
            inputRot += 1f;
        var move = transform.forward * inputMove.x + transform.right * inputMove.y;
        m_characterController.Move(move * Time.deltaTime * _playerMovementSpeed);
        m_characterController.Move(m_velcity);
        transform.Rotate(transform.up * inputRot * Time.deltaTime * _playerRotationSpeed);
    }
    private void ApplyGravity()
    {
        if (isGrounded())
            m_velcity.y = 0f;
        m_velcity.y += _gravity * Time.deltaTime;
    }

    private bool isGrounded()
    {
        return m_characterController.isGrounded;
    }
}
