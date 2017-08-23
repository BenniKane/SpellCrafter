using Assets.Code.SpellFramework;
using Assets.Code.SpellFramework.SpellEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpell : MonoBehaviour
{
    private ISpellCaster spellCaster;
    public ISpellCaster Caster
    {
        get { return spellCaster; }
        set { spellCaster = value; }
    }

    private Subspell[] subSpells;
    public Subspell[] SubSpells
    {
        set { subSpells = value; }
    }

    private void Awake()
    {
        subSpells = new Subspell[0];
    }
    
    public void ActivateSpell()
    {
        for (int i = 0; i < subSpells.Length; i++)
        {
            subSpells[i].ActivateSpell();
        }
    }
}
