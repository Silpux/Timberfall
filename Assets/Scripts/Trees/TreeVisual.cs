using System;
using UnityEngine;

public class TreeVisual : MonoBehaviour{

    private const string HIT_ANIMATION = "Hit";
    private const string DEATH_ANIMATION = "Death";

    private TreeObj tree;
    private Animator animator;

    public event Action OnDeathAnimationFinish;
    public void OnDeathFinish(){
        OnDeathAnimationFinish?.Invoke();
    }

    private void PlayHit(){
        animator.Play(HIT_ANIMATION, 0, 0);
    }
    private void PlayDeath(){
        animator.Play(DEATH_ANIMATION, 0, 0);
    }

    private void Awake(){
        tree = transform.parent.GetComponent<TreeObj>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable(){
        tree.OnHit += PlayHit;
        tree.OnDeath += PlayDeath;
    }

    private void OnDisable(){
        tree.OnHit -= PlayHit;
        tree.OnDeath -= PlayDeath;
    }


}
