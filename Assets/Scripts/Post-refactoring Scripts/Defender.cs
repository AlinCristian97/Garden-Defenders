using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    //TODO: Does this variable make sense here?
    [field:SerializeField] public Sprite Avatar { private set; get; }
    
    [field:SerializeField] public int Cost { private set; get; }
}
