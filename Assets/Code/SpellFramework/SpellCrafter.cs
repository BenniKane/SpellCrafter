using Assets.Code.SpellFramework.SpellNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Assets.Code.SpellFramework
{
    public class SpellCrafter : MonoBehaviour
    {
        private SpellCollectionScriptable baseCollection;
        private SpellCollectionScriptable currentCollection;
        private List<ISpellNode> currentSpellQueue;
        private ISpellCaster spellCaster;
        private MasterSpell spellPrefab;
        private Subspell subSpellPrefab;

        private List<ISpellNode> spellInputsToProcess;
        private bool processingComponentInput;
        private bool bPrepareSpellWhenFinishedProcessing = false;
        private void Awake()
        {
            processingComponentInput = false;
            spellInputsToProcess = new List<ISpellNode>();
            currentSpellQueue = new List<ISpellNode>();
        }

        public void InitializeSpellCrafter(ISpellCaster caster, SpellCollectionScriptable initialCollection, MasterSpell spellBase, Subspell subSpellBase)
        {
            spellCaster = caster;
            baseCollection = initialCollection;
            currentCollection = baseCollection;
            spellPrefab = spellBase;
            subSpellPrefab = subSpellBase;
        }

        public void ProcessSpellInputRequest(int index)
        {
            if (processingComponentInput)
            {
                spellInputsToProcess.Add(currentCollection.GetSpellNode(index));
                currentCollection = currentCollection.GetNextCollection();

            }
            else
            {
                processingComponentInput = true;

                ISpellNode spellNode = currentCollection.GetSpellNode(index);

                currentSpellQueue.Add(spellNode);
                currentCollection = currentCollection.GetNextCollection();

                StartCoroutine(ProcessSpellComponentInput(spellNode));
            }
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

        public void ExitingCastMode()
        {
            bPrepareSpellWhenFinishedProcessing = true;
        }

        public MasterSpell ProcessSpellQueue()
        {
            MasterSpell composedSpell = UnityEngine.Object.Instantiate(spellPrefab, spellCaster.Position, spellCaster.LookRotation);
            Subspell subSpell = UnityEngine.Object.Instantiate(subSpellPrefab, spellCaster.Position, spellCaster.LookRotation);
            subSpell.SetCaster(spellCaster);

            List<Subspell> subSpells = new List<Subspell>();

            GetComponentInChildren<Animator>().SetBool("processingSpellComponents", false);
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

        public bool HasComponentsToProcess()
        {
            return processingComponentInput || spellInputsToProcess.Count > 0 ;
        }

        private IEnumerator ProcessSpellComponentInput(ISpellNode spellNode)
        {
            // Visually Display the rune. Get the Particle System for it, put it in front of the player, etc

            GameObject visual = Instantiate(spellNode.DisplayParticle, spellCaster.Position + (transform.forward * 1.5f), Quaternion.identity);

            UnityEngine.Debug.Log("Processing Spell Node:" + spellNode.Name);

            GetComponentInChildren<Animator>().SetBool("processingSpellComponents", true);
            currentSpellQueue.Add(spellNode);

            float currentTimeElapsed = 0f;
            float timeRequiredForSpellNodeProcessing = spellNode.ProcessingTime;
            while(currentTimeElapsed < timeRequiredForSpellNodeProcessing)
            {
                yield return new WaitForEndOfFrame();
                currentTimeElapsed += Time.deltaTime;
                
                if(currentTimeElapsed >= timeRequiredForSpellNodeProcessing && spellInputsToProcess.Count > 0)
                {
                    ISpellNode nextNode = spellInputsToProcess[0];
                    spellInputsToProcess.RemoveAt(0);
                    Destroy(visual);
                    visual = Instantiate(nextNode.DisplayParticle, spellCaster.Position + (transform.forward * 1.5f), Quaternion.identity);
                    UnityEngine.Debug.Log("Processing Spell Node:" + nextNode.Name);

                    currentTimeElapsed = 0f;
                    timeRequiredForSpellNodeProcessing = nextNode.ProcessingTime;

                    currentSpellQueue.Add(nextNode);

                    // Visually Display the rune. Get the Particle System for it, put it in front of the player, etc
                }
            }

            processingComponentInput = false;
            if(visual != null)
            {
                Destroy(visual);
            }

            if(bPrepareSpellWhenFinishedProcessing)
            {
                bPrepareSpellWhenFinishedProcessing = false;
                MasterSpell finalSpell = ProcessSpellQueue();
                spellCaster.ReceiveSpell(finalSpell);
            }
        }
    }
}
