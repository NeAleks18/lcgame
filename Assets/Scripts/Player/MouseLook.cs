using System;
using UnityEngine;

namespace Q3Movement
{
    /// <summary>
    /// Custom script based on the version from the Standard Assets.
    /// </summary>
    [Serializable]
    public class MouseLook
    {
        [SerializeField] private float m_XSensitivity = 1f;
        [SerializeField] private float m_YSensitivity = 1f;
        [SerializeField] private bool m_ClampVerticalRotation = true;
        [SerializeField] private float m_MinimumX = -90F;
        [SerializeField] private float m_MaximumX = 90F;
        [SerializeField] private bool m_Smooth = false;
        [SerializeField] private float m_SmoothTime = 5f;
        [SerializeField] private bool m_LockCursor = true;

        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        
        bool m_CursorIsLocked = true;
        PlayerInputManager m_PlayerInputManager; 
        
        public void Init(Transform character, Transform camera, PlayerInputManager playerInputManager)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
            m_PlayerInputManager = playerInputManager;
            
        }

        public void LookRotation(Transform character, Transform camera, Vector2 mouseDelta)
        {
            float yRot = mouseDelta.x * m_XSensitivity;
            float xRot = mouseDelta.y * m_YSensitivity;
            
            m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            if (m_ClampVerticalRotation)
            {
                m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);
            }

            if (m_Smooth)
            {
                character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                    m_SmoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot,
                    m_SmoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }

            UpdateCursorLock();
        }

        public void SetCursorLock(bool value)
        {
            m_LockCursor = value;
            if (!m_LockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
            if (m_LockCursor)
            {
                InternalLockUpdate();
            }
        }

        private void InternalLockUpdate()
        {
            if (m_PlayerInputManager.EscapeButtonDown())
                m_CursorIsLocked = false;
            else if (m_PlayerInputManager.LeftMouseDown())
                m_CursorIsLocked = true;

            switch (m_CursorIsLocked)
            {
                case true:
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
                case false:
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
            }
        }

        private Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, m_MinimumX, m_MaximumX);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
    }
}
