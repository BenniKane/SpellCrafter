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

        private ISpellType spellType;

        public TypeSpellNode(string deliveryMethodName, SpellNodeType spellNodeType, ISpellType type)
        {
            name = deliveryMethodName;
            nodeType = spellNodeType;
            spellType = type;
        }

        public void AppendToSpell(Subspell parentObject)
        {
            //Find the last effect, add this as its spell type
            parentObject.SpellEffects[parentObject.SpellEffects.Count - 1].SpellType = spellType;
        }
    }
}
