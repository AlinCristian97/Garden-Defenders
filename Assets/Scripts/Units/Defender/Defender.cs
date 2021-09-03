using UnityEngine;

public abstract class Defender : Unit
{
    [field:SerializeField] public Sprite Avatar { get; private set; }
    [field:SerializeField] public int Cost { get; private set; }
    public Tile Tile => GetComponentInParent<Tile>();
}