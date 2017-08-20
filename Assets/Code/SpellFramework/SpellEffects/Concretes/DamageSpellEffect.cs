using Assets.Code.SpellFramework.SpellEffects;
using Assets.Code.SpellFramework.SpellTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.SpellFramework
{
    public class DamageSpellEffect : ISpellEffect
    {
        private string effectName;
        public string EffectName
        {
            get { return effectName; }
        }

        private ISpellType spellType;
        public ISpellType SpellType
        {
            get { return spellType; }
            set { spellType = value; }
        }

        public DamageSpellEffect(string name)
        {
            effectName = name;
        }

        public void ProcessOnTarget(ISpellTarget target)
        {
            spellType.AffectTarget(target);
        }
    }
}
