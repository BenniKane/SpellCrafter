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

        private float processingTime;
        public float ProcessingTime
        {
            get { return processingTime; }
        }

        private ISpellEffect spellEffect;

        public EffectSpellNode(string effectName, SpellNodeType type, ISpellEffect effect, float processTime)
        {
            name = effectName;
            nodeType = type;
            spellEffect = effect;
            processingTime = processTime;
        }

        public void AppendToSpell(Subspell parentObject)
        {
            parentObject.AddEffectToSpell(spellEffect);
        }
    }
}
