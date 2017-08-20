using Assets.Code.SpellFramework.DeliveryMethods.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.SpellFramework.SpellTypes
{
    public interface ISpellType
    {
        void AffectTarget(ISpellTarget target);

        ParticleSystem GetParticleSystemForDeliveryMethod(EDeliveryMethods deliveryMethod);
    }
}
