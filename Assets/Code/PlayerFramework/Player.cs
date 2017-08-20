using Assets.Code.SpellFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.SpellFramework.SpellEffects;
using System;
using Assets.Code.GeneralFramework;

namespace PlayerFramework
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour, IOnDeathNotify
    {
        private CharacterController movementController;

        [SerializeField]
        private Transform playerCameraFocus;

        [SerializeField]
        private float playerSpeed = 5;

        private Animator animator;

        private float currentPitch;

        private bool cursorLocked = true;
        
        private bool inCastMode = false;
        [SerializeField]
        private SpellCollectionScriptable generalSpellCollection;

        private SpellCrafter spellCrafter;

        [SerializeField]
        private Spell spellBase;
        private Spell activeSpell;
        private bool hasSpellPrepared = false;

        private void Awake()
        {
            movementController = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
            spellCrafter = new SpellCrafter(transform, generalSpellCollection, spellBase);
        }

        void Start()
        {
            currentPitch = 0f;

            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            bool wasInCastMode = inCastMode;
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                inCastMode = true;
                if(activeSpell != null)
                {
                    Destroy(activeSpell.gameObject);
                    activeSpell = null;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                inCastMode = false;
            }

            if (inCastMode)
            {
                int spellIndex = -1;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    spellIndex = ConvertKeyCodeIntoIndex(KeyCode.Q);
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    spellIndex = ConvertKeyCodeIntoIndex(KeyCode.W);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    spellIndex = ConvertKeyCodeIntoIndex(KeyCode.E);
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    spellIndex = ConvertKeyCodeIntoIndex(KeyCode.R);
                }

                if (spellIndex >= 0)
                {
                    spellCrafter.ProcessSpellInputRequest(spellIndex);
                }
            }
            else if (wasInCastMode && !inCastMode)
            {
                if (spellCrafter.SpellMeetsMinimumProcessRequirements())
                {
                    Spell newSpell = spellCrafter.ProcessSpellQueue(transform.position, playerCameraFocus.transform.rotation);

                    if (newSpell != null)
                    {
                        Debug.Log("Created a new spell...");
                    }
                    
                    activeSpell = newSpell;
                    
                }
                else
                {
                    spellCrafter.ClearSpellQueue();
                }
            }
            else if(Input.GetKeyDown(KeyCode.Mouse0) && activeSpell != null)
            {
                activeSpell.transform.position = transform.position;
                activeSpell.transform.rotation = playerCameraFocus.transform.rotation;
                activeSpell.ActivateSpell();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = !cursorLocked;

                Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = !cursorLocked;
            }
            else
            {
                HandleRotation();
                HandleTranslation();
            }
            
        }

        // Simple, just enough
        private void HandleRotation()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            transform.Rotate(Vector3.up, mouseX);

            currentPitch = Mathf.Clamp(currentPitch + mouseY, -60f, 60f);

            Vector3 newRotation = new Vector3(-currentPitch, 0f, 0f);

            playerCameraFocus.localRotation = Quaternion.Euler(newRotation);            
        }

        // Simple, just enough
        private void HandleTranslation()
        {
            float forwardMove = Input.GetAxis("Vertical");
            float strafeMove = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(strafeMove, 0f, forwardMove);

            movement = transform.TransformDirection(movement);

            movementController.Move(movement * playerSpeed * Time.deltaTime);
        }

        private int ConvertKeyCodeIntoIndex(KeyCode keycode)
        {
            int index = -1;
            switch(keycode)
            {
                case KeyCode.Q:
                    index = 0;
                    break;
                case KeyCode.W:
                    index = 1;
                    break;
                case KeyCode.E:
                    index = 2;
                    break;
                case KeyCode.R:
                    index = 3;
                    break;
            }

            return index;
        }

        public void ReceiveSpellEffects(List<ISpellEffect> spellEffects)
        {
            throw new NotImplementedException();
        }

        public void OnNotificationOfDeath()
        {
            Debug.Log("Player has Died");
        }
    }
}