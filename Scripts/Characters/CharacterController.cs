using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public Character character;
    // Start is called before the first frame update
    void Start()
    {   
        CharacterList instance = CharacterList.GetInstance();
        instance.list.Add(character);
        Debug.Log(character.spell1);
    }

        void Update()
    {
     if(Input.GetKeyDown("space")) 
     {
        Debug.Log(character.spell1.SpellName);
        GetComponent<Animator> ().SetTrigger (character.spell1.SpellName);
     }
     if(Input.GetKeyDown("d")) 
     {
        Debug.Log("t'es mort gros con");
        GetComponent<Animator> ().SetTrigger ("dead");
     }
    }
}
