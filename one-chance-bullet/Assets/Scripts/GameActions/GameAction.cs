using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class GameActionBase : MonoBehaviour
{
    [SerializeField] protected float delay;
    [SerializeField] protected bool executeOnEnable = false;

    public virtual void OnEnable()
    {
        if (executeOnEnable == true)
            StartCoroutine(ExecuteWithDelayCO());
    }

    protected IEnumerator ExecuteWithDelayCO()
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        yield return StartCoroutine(ExecuteCO());
    }

    protected abstract IEnumerator ExecuteCO();
}

public abstract class GameAction : GameActionBase
{   
    public void Execute()
    {
        StartCoroutine(ExecuteWithDelayCO());
    }
}

public abstract class GameAction<T> : MonoBehaviour
{
    [SerializeField] protected float delay;
    protected T actOn;

    public void Execute(T actOn)
    {
        this.actOn = actOn;
        StartCoroutine(ExecuteWithDelayCO());
    }

    protected IEnumerator ExecuteWithDelayCO()
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        yield return StartCoroutine(ExecuteCO());
    }

    protected abstract IEnumerator ExecuteCO();
}



public abstract class GameActionComponent<T> : GameActionBase where T : Component
{
    [SerializeField] protected T component;
    //[SerializeField] protected bool executeOnEnable = false;

    //public virtual void OnEnable()
    //{
    //    if (executeOnEnable == true)
    //        Execute();
    //}

    private void Awake()
    {
        if (component == null)
            component = GetComponent<T>();

        if (component == null)
            Debug.LogError($"Componente do tipo {typeof(T)} não foi referênciado ou não faz parte do GameObject", this);
    }

    public void Execute()
    {
        StartCoroutine(ExecuteWithDelayCO());
    }
}
