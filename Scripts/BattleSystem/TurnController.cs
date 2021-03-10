using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public List<Character> characters = new List<Character>();
    public CharacterController character;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(characters);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
