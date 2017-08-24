using Assets.Code.SpellFramework.SpellTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Code.SpellFramework.SpellNodes;

namespace Assets.Code.SpellFramework.SpellEffects
{
    public enum SpellEffects
    {
        Damage,
        Ward,
        Mind
    }
    [CreateAssetMenu(fileName = "SpellEffect", menuName = "Create New Spell Effect")]
    public class SpellEffectScriptable : SpellNodeScriptable
    {
        public SpellEffects spellEffect;

        public override ISpellNode GetNode()
        {
            return new EffectSpellNode(m_SpellNodeName, m_SpellNodeType, GetSpellNode(), m_ProcessingTime);
        }

        private ISpellEffect GetSpellNode()
        {
            switch(spellEffect)
            {
                default:
                    return new DamageSpellEffect(m_SpellNodeName);
            }
        }
    }
}
