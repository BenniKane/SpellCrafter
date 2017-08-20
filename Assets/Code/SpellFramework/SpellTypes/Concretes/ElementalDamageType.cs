using Assets.Code.SpellFramework.DeliveryMethods.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.SpellFramework.SpellTypes
{
    public enum ElementalType
    {
        Fire,
        Water,
        Earth,
        Air,
        None
    }

    public class ElementalDamageType : ISpellType
    {
        private int baseDamageValue;
        private ElementalType elementType;
        private ParticleSystem[] deliveryMethodParticles;
        public ElementalDamageType(int baseValue, ElementalType type, ParticleSystem[] deliveryParticles)
        {
            baseDamageValue = baseValue;
            elementType = type;
            deliveryMethodParticles = deliveryParticles;
        }

        public void AffectTarget(ISpellTarget target)
        {
            target.TakeDamage(baseDamageValue, elementType);
        }

        public ParticleSystem GetParticleSystemForDeliveryMethod(EDeliveryMethods deliveryMethod)
        {
            return deliveryMethodParticles[(int)deliveryMethod];
        }
    }
}
