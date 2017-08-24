using Assets.Code.SpellFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.SpellFramework.SpellEffects;
using System;
using Assets.Code.GeneralFramework;

namespace PlayerFramework
{
    [RequireComponent(typeof(SpellCrafter))]
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour, IOnDeathNotify, ISpellCaster
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
        private Subspell subspellBase;
        
        [SerializeField]
        private MasterSpell spellBase;
        private MasterSpell activeSpell;
        private bool hasSpellPrepared = false;

        public Quaternion LookRotation
        {
            get
            {
                return playerCameraFocus.rotation;
            }
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
        }

        private void Awake()
        {
            movementController = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            spellCrafter = GetComponent<SpellCrafter>();
            spellCrafter.InitializeSpellCrafter(this, generalSpellCollection, spellBase, subspellBase);

            currentPitch = 0f;

            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            bool wasInCastMode = inCastMode;
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                inCastMode = true;

                animator.SetBool("inCastMode", true);

                if (activeSpell != null)
                {
                    Destroy(activeSpell.gameObject);
                    activeSpell = null;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                inCastMode = false;
                animator.SetBool("inCastMode", false);
                spellCrafter.ExitingCastMode();
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
            else if (spellCrafter.HasComponentsToProcess())
            {
                // This is our empty state to display holding the spell...
            }
            else
            {
                HandleRotation();
                HandleTranslation();
                if (activeSpell != null)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        activeSpell.transform.position = transform.position;
                        activeSpell.transform.rotation = playerCameraFocus.transform.rotation;
                        activeSpell.ActivateSpell();

                        activeSpell = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    cursorLocked = !cursorLocked;

                    Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
                    Cursor.visible = !cursorLocked;
                }
            }
        }

        public void ReceiveSpell(MasterSpell masterspell)
        {
            activeSpell = masterspell;
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

        public bool HoldingCastModeButton()
        {
            return Input.GetKey(KeyCode.Mouse1);
        }

        public bool HasSpellComponentsToProcess()
        {
            return spellCrafter.HasComponentsToProcess();
        }
    }
}