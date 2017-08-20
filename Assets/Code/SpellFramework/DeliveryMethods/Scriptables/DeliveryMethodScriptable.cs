using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.SpellFramework.SpellNodes;
using UnityEngine;

namespace Assets.Code.SpellFramework.DeliveryMethods.Scriptables
{
    public enum EDeliveryMethods
    {
        Projectile,
        Self,
        Raycast,
        Cone
    }
    
    [CreateAssetMenu(fileName = "DeliveryMethod", menuName = "Create New Delivery method")]
    public class DeliveryMethodScriptable : SpellNodeScriptable
    {
        public EDeliveryMethods deliveryMethod;

        public override ISpellNode GetNode()
        {
            return new DeliveryMethodSpellNode(m_SpellNodeName, m_SpellNodeType, new ProjectileDeliveryMethod());
        }

        private IDeliveryMethod GetDeliveryMethod()
        {
            switch(deliveryMethod)
            {
                default:
                    return new ProjectileDeliveryMethod();
            }
        }
    }
}
