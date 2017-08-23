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
        private ISpellCaster spellCaster;
        private MasterSpell spellPrefab;
        private Subspell subSpellPrefab;

        public SpellCrafter(ISpellCaster caster, SpellCollectionScriptable initialCollection, MasterSpell spellBase, Subspell subSpellBase)
        {
            spellCaster = caster;
            baseCollection = initialCollection;
            currentCollection = baseCollection;
            spellPrefab = spellBase;
            subSpellPrefab = subSpellBase;

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

        public MasterSpell ProcessSpellQueue()
        {
            MasterSpell composedSpell = UnityEngine.Object.Instantiate(spellPrefab, spellCaster.Position, spellCaster.LookRotation);
            Subspell subSpell = UnityEngine.Object.Instantiate(subSpellPrefab, spellCaster.Position, spellCaster.LookRotation);
            subSpell.SetCaster(spellCaster);

            List<Subspell> subSpells = new List<Subspell>();

            for (int i = 0; i < currentSpellQueue.Count; i++)
            {
                currentSpellQueue[i].AppendToSpell(subSpell);

                if(currentSpellQueue[i].NodeType == SpellNodeType.Type)
                {
                    subSpells.Add(subSpell);
                    subSpell = UnityEngine.Object.Instantiate(subSpellPrefab, spellCaster.Position, spellCaster.LookRotation);
                    subSpell.SetCaster(spellCaster);
                }
            }

            composedSpell.Caster = spellCaster;
            composedSpell.SubSpells = subSpells.ToArray();

            currentSpellQueue.Clear();
            currentCollection = baseCollection;
            
            return composedSpell;
        }
    }
}
