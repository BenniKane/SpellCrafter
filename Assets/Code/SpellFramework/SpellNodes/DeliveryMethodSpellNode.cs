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

        private IDeliveryMethod deliveryMethod;

        public DeliveryMethodSpellNode(string deliveryMethodName, SpellNodeType spellNodeType, IDeliveryMethod delivery, float processTime, GameObject appearance)
        {
            name = deliveryMethodName;
            nodeType = spellNodeType;
            deliveryMethod = delivery;
            processingTime = processTime;
            displayParticle = appearance;
        }
        
        public void AppendToSpell(Subspell parentObject)
        {
            parentObject.SetDeliveryMethod(deliveryMethod);
        }
    }
}
