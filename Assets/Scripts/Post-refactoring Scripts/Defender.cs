using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    //TODO: Does this variable make sense here?
    [field:SerializeField] public Sprite Avatar { get; private set; }
    
    [field:SerializeField] public int Cost { get; private set; }
}
