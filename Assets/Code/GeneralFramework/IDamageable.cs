using Assets.Code.SpellFramework.SpellTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.GeneralFramework
{
    public interface IDamageable
    {
        bool TakeDamage(float damageVal, ElementalType damageType = ElementalType.None);
    }
}
