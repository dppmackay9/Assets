using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
    private WeaponScript weapon;
    public int awake = 0;

    void Awake()
    {
        // Retrieve the weapon only once
        weapon = GetComponent<WeaponScript>();
    }

    void Update()
    {
        // Auto-fire si l'ennemi est réveillé
        if (this.isAwake)
        {
            if (weapon != null && weapon.CanAttack)
            {
                weapon.Attack(true, this);
            }
        }

    }


    /// <summary>
    /// est-ce que l'ennemi est réveillé ?
    /// </summary>
    public bool isAwake
    {
        get
        {
            if (awake == 1) return true;
            else return false;
        }

    }
}

// test github
