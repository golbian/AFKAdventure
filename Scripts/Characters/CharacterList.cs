using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CharacterList: MonoBehaviour
{
    public List<Character> list = new List<Character>();
    private CharacterList() {
    }

    private static CharacterList _instance;
    public static CharacterList GetInstance() {
        if(_instance == null) {
            _instance = new CharacterList();
        }
        return _instance;
    }
}
