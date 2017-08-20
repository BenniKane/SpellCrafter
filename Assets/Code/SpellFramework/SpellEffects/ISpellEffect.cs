using Assets.Code.SpellFramework.SpellTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.SpellFramework.SpellEffects
{
    public interface ISpellEffect
    {
        string EffectName { get; }

        ISpellType SpellType { get; set; }

        void ProcessOnTarget(ISpellTarget target);
    }
}
