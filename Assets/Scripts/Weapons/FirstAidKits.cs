using System.Collections;
using UnityEngine;

public class FirstAidKits : Weapon
{
    [SerializeField] private Health playeHealth;
    [SerializeField] private float healPercent = 10f;
    [SerializeField] private float cooldown = 10f;

    private bool canUse = true;

    public override void AttackPerformed()
    {
        if (!canUse) return;
        playeHealth.HealPercentFromMissing(healPercent);
        canUse = false;
        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(ResetUse());
    }

    private IEnumerator ResetUse()
    {
        yield return new WaitForSeconds(cooldown);
        canUse = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
