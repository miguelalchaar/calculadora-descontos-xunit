namespace CalculadoraDescontos.App;

public class DescontoService
{
    public string ObterCategoriaCliente(int totalCompras)
    {
        if (totalCompras < 5)
        {
            return "BRONZE";
        }

        if (totalCompras <= 10)
        {
            return "PRATA";
        }

        return "OURO";
    }

    public int CalcularDescontoPorPercentual(int valorOriginal, int percentualDesconto)
    {
        return valorOriginal - (valorOriginal * percentualDesconto / 100);
    }

    public bool EValidoParaCupom(int idade, bool primeiraCompra)
    {
        return idade >= 18 || primeiraCompra;
    }
}
