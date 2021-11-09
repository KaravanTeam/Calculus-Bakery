using UnityEngine;

namespace Controller
{
    internal abstract class TransporterManipulationButton : MonoBehaviour
    {
        public void SetState(ButtonState state)
        {
            switch (state)
            {
                case ButtonState.Enabled:
                {
                    SetEnabledState();

                    return;
                }
                    

                case ButtonState.Disabled:
                {
                    SetDisabledState();

                    return;
                }
            }

            throw new UnityException($"Unknow transporter button state {state}");
        }

        protected abstract void OnClick();
        protected abstract void SetEnabledState();
        protected abstract void SetDisabledState();
    }
}
