using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] private Animator _scaleAnimator;

    [HideInInspector] public SpriteRenderer SpriteRenderer;
    [HideInInspector] public int Side;

    private Animator _animator;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    public void SetHeightTriggter(string triggerName) => _scaleAnimator.SetTrigger(triggerName);
}
