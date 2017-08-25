using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.SpellFramework.SpellNodes;
using UnityEngine;

namespace Assets.Code.SpellFramework.SpellTypes.Scriptables
{
    public enum SpellTypes
    {
        ElementalDamage        
    }
    [CreateAssetMenu(fileName = "Elemental Damage Type", menuName = "Create New Elemental Damage Type")]
    public class ElementalDamageTypeScriptable : SpellNodeScriptable
    {
        public int m_DamageValue;
        public ElementalType m_ElementalType;

        public ParticleSystem[] m_DeliveryMethodParticleSystems;

        public override ISpellNode GetNode()
        {
            return new TypeSpellNode(m_SpellNodeName, m_SpellNodeType, new ElementalDamageType(m_DamageValue, m_ElementalType, m_DeliveryMethodParticleSystems), m_ProcessingTime, m_DisplayParticle);
        }
    }
}
