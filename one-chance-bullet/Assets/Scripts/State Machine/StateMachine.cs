using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StateMachine : MonoBehaviour
{

    [SerializeField] Transform rootObject;
    [SerializeField] Transform statesParent;

    [Space]
    [SerializeField] State initialState;
    
    List<State> states;
    private State currentState;
    public State CurrentState { get => currentState; set => currentState = value; }

    Animator animator;

    private void Awake()
    {
        if(rootObject == null)
            rootObject = transform;
        
        if (statesParent == null)
            statesParent = transform;

        animator = rootObject.GetComponent<Animator>();
        states = new List<State>(statesParent.GetComponentsInChildren<State>());
    }

    private void Start()
    {
        states.ForEach(s => s.Init(this, rootObject));    
    }

    private void OnEnable() {
        StartStateMachine();
    }

    private void Update()
    {
        currentState?.Tick();
    }

    public void StartStateMachine()
    {
        animator.Rebind();
        animator.Update(0);
        CurrentState = initialState;
        CurrentState.Enter();
    }

    public void TransitionTo(State state)
    {
        if (state == null)
            return;

        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }

    public void ExitStateMachine()
    {
        CurrentState?.Exit();
    }
}
