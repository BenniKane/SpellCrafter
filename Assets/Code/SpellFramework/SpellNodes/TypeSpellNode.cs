using Assets.Code.SpellFramework.SpellTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.SpellFramework.SpellNodes
{
    public class TypeSpellNode : ISpellNode
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
            get { return nodeType; }
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

        private ISpellType spellType;

        public TypeSpellNode(string deliveryMethodName, SpellNodeType spellNodeType, ISpellType type, float processTime, GameObject appearance)
        {
            name = deliveryMethodName;
            nodeType = spellNodeType;
            spellType = type;
            processingTime = processTime;
            displayParticle = appearance;
        }

        public void AppendToSpell(Subspell parentObject)
        {
            //Find the last effect, add this as its spell type
            parentObject.SpellEffects[parentObject.SpellEffects.Count - 1].SpellType = spellType;
        }
    }
}
