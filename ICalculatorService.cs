using CoreWCF;

namespace PseudoRMI_CalculatorServer
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        double Calculate(string input);

        [OperationContract]
        double Add(double a, double b);

        [OperationContract]
        double Subtract(double a, double b);

        [OperationContract]
        double Multiply(double a, double b);

        [OperationContract]
        double Divide(double a, double b);
    }
}