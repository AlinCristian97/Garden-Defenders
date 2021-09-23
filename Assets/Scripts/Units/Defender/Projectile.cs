using System;
using Audio;
using General.Patterns.Singleton;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [field:SerializeField] public string AliasIdentifier { get; private set; }
    [SerializeField] private float _speed = 1f;
    private int _damage = 50;
    [SerializeField] private string _impactSoundName;
    
    private Unit _target;
    
    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if (_target != null)
        {
            if (!_target.IsDead && TargetReached())
            {
                _target.TakeDamage(_damage);
                PlayImpactSFX();

                gameObject.SetActive(false);
            }
            else if (_target.IsDead)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void PlayImpactSFX()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.Miscellaneous, _impactSoundName);
    }

    private bool TargetReached() => transform.position.x >= _target.transform.position.x;

    public void SetDamage(int damageValue)
    {
        if (damageValue <= 0)
        {
            _damage = 0;
        }
        else
        {
            _damage = damageValue;
        }
    }

    public void SetTarget(Attacker target)
    {
        _target = target;
    }
}
