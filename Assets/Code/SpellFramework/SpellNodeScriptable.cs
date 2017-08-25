using Assets.Code.SpellFramework.SpellNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.SpellFramework
{
    public enum SpellNodeType
    {
        DeliveryMethod,
        Effect,
        Type
    }

    public abstract class SpellNodeScriptable : ScriptableObject
    {
        public string m_SpellNodeName;

        public SpellNodeType m_SpellNodeType;

        public float m_ProcessingTime;

        public GameObject m_DisplayParticle;

        public SpellCollectionScriptable m_FollowUpCollection;

        public abstract ISpellNode GetNode();
        
        public SpellCollectionScriptable GetFollowUpCollection()
        {
            return m_FollowUpCollection;
        }
    }
}
