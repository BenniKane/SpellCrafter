using Assets.Code.SpellFramework.SpellEffects;
using Assets.Code.SpellFramework.SpellNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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

        private GameObject displayParticle;
        public GameObject DisplayParticle
        {
            get { return displayParticle; }
        }

        private ISpellEffect spellEffect;

        public EffectSpellNode(string effectName, SpellNodeType type, ISpellEffect effect, float processTime, GameObject appearance)
        {
            name = effectName;
            nodeType = type;
            spellEffect = effect;
            processingTime = processTime;
            displayParticle = appearance;
        }

        public void AppendToSpell(Subspell parentObject)
        {
            parentObject.AddEffectToSpell(spellEffect);
        }
    }
}
