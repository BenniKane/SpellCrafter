using Assets.Code.SpellFramework.SpellNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.SpellFramework
{
    [CreateAssetMenu(fileName = "DefaultSpellCollection", menuName = "Create Spell Collection")]
    public class SpellCollectionScriptable : ScriptableObject
    {
        private SpellCollectionScriptable followupCollection;

        [SerializeField]
        private SpellNodeScriptable[] spellNodesInCollection;

        private int lastIndex;

        public ISpellNode GetSpellNode(int index)
        {
            followupCollection = spellNodesInCollection[index].GetFollowUpCollection();
            return spellNodesInCollection[index].GetNode();
        }

        public SpellCollectionScriptable GetNextCollection()
        {
            return followupCollection;
        }
    }
}
