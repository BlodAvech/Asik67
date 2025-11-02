using System.Linq;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons = new Weapon[1];
    private Weapon currentWeapon;
    [SerializeField] Weapon secondaryWeapon;
    private int currentWeaponIndex = 0;

    void Awake()
    {
        InputManager.Controls.Player.SwitchWeapon.performed += ctx => SwitchWeapon();
        InputManager.Controls.Player.Attack.performed += ctx => PrimaryAttackPerformed();
        InputManager.Controls.Player.Attack.canceled += ctx => PrimaryAttackCanceled();
        InputManager.Controls.Player.SecondaryAttack.performed += ctx => SecondaryAttackPerformed();
        InputManager.Controls.Player.SecondaryAttack.canceled += ctx => SecondaryAttackCanceled();
    }

    void Start()
    {
        InitWeapons();
    }

    private void PrimaryAttackPerformed() => currentWeapon?.AttackPerformed();
    private void PrimaryAttackCanceled() => currentWeapon?.AttackCanceled();
    private void SecondaryAttackPerformed() => secondaryWeapon?.AttackCanceled();
    private void SecondaryAttackCanceled() => secondaryWeapon?.AttackCanceled();

    private void SwitchWeapon()
    {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        currentWeaponIndex = ++currentWeaponIndex % weapons.Length;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
        currentWeapon = weapons[currentWeaponIndex];
    }
    
    private void InitWeapons()
	{
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        currentWeapon = weapons[currentWeaponIndex];
        weapons[currentWeaponIndex].gameObject.SetActive(true);
	}
}
