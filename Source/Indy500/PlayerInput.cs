namespace Indy500
{
    public class PlayerInput
    {
        public float AccelerationAmount { get; }
        public float TurnAmount { get; }

        public PlayerInput(float accelerationAmount, float turnAmount)
        {
            AccelerationAmount = accelerationAmount;
            TurnAmount = turnAmount;
        }
    }
}
