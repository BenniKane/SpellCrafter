using Assets.Code.SpellFramework;
using Assets.Code.SpellFramework.SpellEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subspell : MonoBehaviour
{
    private ISpellCaster spellCaster;

    private IDeliveryMethod spellDeliveryMethod;
    private List<ISpellEffect> spellEffects;
    public List<ISpellEffect> SpellEffects
    {
        get { return spellEffects; }
    }

    public ISpellCaster Caster
    {
        get
        {
            return spellCaster;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        spellEffects = new List<ISpellEffect>();
    }

    public void SetDeliveryMethod(IDeliveryMethod deliveryMethod)
    {
        spellDeliveryMethod = deliveryMethod;
    }

    public void AddEffectToSpell(ISpellEffect spellEffect)
    {
        spellEffects.Add(spellEffect);
    }

    public void SetCaster(ISpellCaster caster)
    {
        spellCaster = caster;
    }

    public void ApplySpellToTarget()
    {
        // Iterate over each effect, calling ApplyToTarget(collided object)
    }

    public void ActivateSpell()
    {
        spellDeliveryMethod.Activate(this);

        ParticleSystem spellVisual = Instantiate(spellEffects[0].SpellType.GetParticleSystemForDeliveryMethod(spellDeliveryMethod.DeliveryMethod));
        spellVisual.transform.SetParent(transform, false);

    }

    private void OnTriggerEnter(Collider other)
    {
        ISpellCaster[] spellCasterComponentsOnOther = other.transform.GetComponentsInChildren<ISpellCaster>();

        if (spellCasterComponentsOnOther.Length == 0)
            return;

        for(int i = 0; i < spellCasterComponentsOnOther.Length; i++)
        {
            if(spellCasterComponentsOnOther[i] == spellCaster)
            {
                return;
            }
        }

        ISpellTarget target = other.GetComponent<ISpellTarget>();

        if (target != null)
        {
            for (int i = 0; i < spellEffects.Count; i++)
            {
                spellEffects[i].ProcessOnTarget(target);
            }
        }
    }
}
