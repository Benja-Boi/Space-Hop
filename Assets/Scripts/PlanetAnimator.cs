using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAnimator : MonoBehaviour
{

    private Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerStateManager.PlayerDocked += OnPlayerDocked;
    }

    private void OnDisable()
    {
        PlayerStateManager.PlayerDocked -= OnPlayerDocked;
    }

    private void OnPlayerDocked(Planet p)
    {
        if (GameObject.ReferenceEquals(p.gameObject, gameObject))
        {
            _animator.SetTrigger("PlayerDocked");
        }
    }
}
