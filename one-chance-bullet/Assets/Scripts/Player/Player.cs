using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerID = 1;

    public CharacterMovement2D Movement { get; private set; }
    public CharacterAnimation Animation { get; private set; }
    public Health Health { get; private set; }
    public Character character;

    private void Awake() {
        Movement = GetComponent<CharacterMovement2D>();
        Animation = GetComponent<CharacterAnimation>();
        Health = GetComponent<Health>();
        character = GetComponent<Character>();
    }

    public bool IsDead => Health.HealthValue <= 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
