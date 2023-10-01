using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [Space]    
    [SerializeField] string animationTriggerName;
    [SerializeField] float delay;
    [Space]
    [SerializeField] State timeOutState;
    [SerializeField] float timeOutTime = -1;
    [Space]
    [SerializeField] LifeCycleEvents events;

    protected bool isInState;

    protected Transform rootObject;
    protected Animator animator;
    protected StateMachine stateMachine;

    public void Init(StateMachine stateMachine, Transform rootObject)
    {
        this.rootObject = rootObject;
        this.animator = rootObject.GetComponent<Animator>();
        this.stateMachine = stateMachine;
        AfterInit();
    }

    protected virtual void AfterInit()
    {

    }

    public void Enter()
    {
        if(delay >= 0)
            Invoke(nameof(DelayEnter), delay);
    }

    void DelayEnter()
    {
        isInState = true;
        //Debug.Log($"StateMachine - Enter State: {name}");

        if(animator != null && !string.IsNullOrEmpty(animationTriggerName))
            animator.SetTrigger(animationTriggerName);

        if(gameObject.activeInHierarchy == false)
            return;

        events.OnEnter.Invoke();
        StartCoroutine(StateCO());
        StartCoroutine(TimeOutTransitionCO(timeOutState, timeOutTime));

        AfterEnter();
    }

    protected virtual void AfterEnter()
    {

    }

    public void Exit()
    {
        //Debug.Log($"StateMachine - Exit State: {name}");

        events.OnExit.Invoke();

        StopAllCoroutines();
        isInState = false;

        AfterExit();
    }

    protected virtual void AfterExit()
    {

    }

    public virtual void Tick()
    {

    }

    private IEnumerator TimeOutTransitionCO(State nextState, float time)
    {
        if (nextState == null)
            yield break;

        if (time <= 0)
            yield break;

        yield return new WaitForSeconds(time);
        stateMachine.TransitionTo(nextState);
    }

    protected virtual IEnumerator StateCO()
    {
        yield break;
    }
}
