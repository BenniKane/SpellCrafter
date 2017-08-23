using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.SpellFramework.SpellNodes
{
    public class DeliveryMethodSpellNode : ISpellNode
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

        private IDeliveryMethod deliveryMethod;

        public DeliveryMethodSpellNode(string deliveryMethodName, SpellNodeType spellNodeType, IDeliveryMethod delivery)
        {
            name = deliveryMethodName;
            nodeType = spellNodeType;
            deliveryMethod = delivery;
        }

        public void AppendToSpell(Subspell parentObject)
        {
            parentObject.SetDeliveryMethod(deliveryMethod);
        }
    }
}
