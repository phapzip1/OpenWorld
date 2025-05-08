namespace Phac.Character
{
    public interface IState {
        public void OnEnter();
        public void Update();
        public void FixedUpate();
        public void OnExit();
    }
}