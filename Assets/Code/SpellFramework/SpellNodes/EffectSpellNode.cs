using Assets.Code.SpellFramework.SpellEffects;
using Assets.Code.SpellFramework.SpellNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.SpellFramework
{
    public class EffectSpellNode : ISpellNode
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        private SpellNodeType nodeType;
        public SpellNodeType NodeType
        {
            get
            {
                return nodeType;
            }
        }

        private ISpellEffect spellEffect;

        public EffectSpellNode(string effectName, SpellNodeType type, ISpellEffect effect)
        {
            name = effectName;
            nodeType = type;
            spellEffect = effect;
        }

        public void AppendToSpell(Spell parentObject)
        {
            parentObject.AddEffectToSpell(spellEffect);
        }
    }
}
