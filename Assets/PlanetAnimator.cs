using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAnimator : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        PlayerStateManager.PlayerDocked += OnPlayerDocked;
    }

    private void OnPlayerDocked(Planet p)
    {
        if (GameObject.ReferenceEquals(p.gameObject, gameObject))
        {
            _animator.SetTrigger("PlayerDocked");
        }
    }
}
