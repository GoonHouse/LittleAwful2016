using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            // Read the jump input in Update so button presses aren't missed.
            if (!m_Jump && !EventSystem.current.IsPointerOverGameObject()) {
                m_Jump = Input.GetMouseButton(0);
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            float h = 0.0f;
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) {
                float offsetDistance = 1.0f;
                var mp = Input.mousePosition;
                mp.z = Camera.main.nearClipPlane;
                var p = Camera.main.ScreenToWorldPoint(mp);

                if (p.x - transform.position.x > offsetDistance) {
                    h = 1;
                } else if (p.x - transform.position.x < -offsetDistance) {
                    h = -1;
                }
            }

            // Pass all parameters to the character control script.
            m_Character.Move(h, false, m_Jump);
            m_Jump = false;
        }
    }
}
