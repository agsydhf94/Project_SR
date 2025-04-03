using UnityEngine;

namespace SR
{
    public class InputManager : SingletonBase<InputManager>
    {
        public string horizontalInputName = "Horizontal";
        public string verticalInputName = "Vertical";
        public string mouseXInputName = "Mouse X";
        public string mouseYInputName = "Mouse Y";
        public string fireInputName = "Fire1";
        public string reloadInputName = "Reload";
        public string jumpInputName = "Jump";

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool fire { get; private set; }
        public bool reload { get; private set; }
        public bool JumpPressed { get; private set; }

        void Update()
        {
            MoveInput = new Vector2(Input.GetAxis(horizontalInputName), Input.GetAxis(verticalInputName));
            LookInput = new Vector2(Input.GetAxis(mouseXInputName), Input.GetAxis(mouseYInputName));
            fire = Input.GetButtonDown(fireInputName);
            reload = Input.GetButtonDown(reloadInputName);
            JumpPressed = Input.GetButtonDown(jumpInputName);
        }
    }
}

