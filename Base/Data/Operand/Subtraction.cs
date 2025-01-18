namespace ClussPro.Base.Data.Operand
{
    public class Subtraction : IOperand
    {
        public IOperand[] SubtractionOperands { get; set; }

        public Subtraction(params IOperand[] subtractionOperands)
        {
            SubtractionOperands = subtractionOperands;
        }
    }
}
