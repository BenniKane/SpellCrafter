using Assets.Code.SpellFramework.SpellNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine;

namespace Assets.Code.SpellFramework
{
    public class SpellCrafter
    {
        private SpellCollectionScriptable baseCollection;
        private SpellCollectionScriptable currentCollection;
        private List<ISpellNode> currentSpellQueue;
        private Transform spellCaster;
        private Spell spellPrefab;

        public SpellCrafter(Transform caster, SpellCollectionScriptable initialCollection, Spell spellBase)
        {
            spellCaster = caster;
            baseCollection = initialCollection;
            currentCollection = baseCollection;
            spellPrefab = spellBase;

            currentSpellQueue = new List<ISpellNode>();
        }

        public void ProcessSpellInputRequest(int index)
        {
            currentSpellQueue.Add(currentCollection.GetSpellNode(index));
            currentCollection = currentCollection.GetNextCollection();
        }

        public bool SpellMeetsMinimumProcessRequirements()
        {
            return currentSpellQueue.Count >= 3;
        }

        public void ClearSpellQueue()
        {
            currentSpellQueue.Clear();
            currentCollection = baseCollection;
        }

        public Spell ProcessSpellQueue(Vector3 castingPoint, Quaternion rotation)
        {
            Spell composedSpell = UnityEngine.Object.Instantiate(spellPrefab, castingPoint, rotation);

            for (int i = 0; i < currentSpellQueue.Count; i++)
            {
                currentSpellQueue[i].AppendToSpell(composedSpell);
            }

            composedSpell.SetCaster(spellCaster);

            currentSpellQueue.Clear();
            currentCollection = baseCollection;
            
            return composedSpell;
        }
    }
}
