using CalculadoraDescontos.App;

namespace CalculadoraDescontos.Tests;

public class DescontoServiceTests
{
    private readonly DescontoService _sut = new();

    [Theory]
    [InlineData(2, "BRONZE")]
    [InlineData(7, "PRATA")]
    [InlineData(15, "OURO")]

    public void ObterCategoriaCliente_DeveRetornarCategoriaEsperada(int totalCompras, string categoriaEsperada)
    {
        var resultado = _sut.ObterCategoriaCliente(totalCompras);
        Assert.Equal(categoriaEsperada, resultado);
    }

    [Theory]
    [InlineData(100, 10, 90)]
    [InlineData(200, 20, 160)]
    [InlineData(50, 0, 50)]
    
    public void CalcularDescontoPorPercentual_DeveRetornarValorFinal(int valorOriginal, int percentualDesconto, int valorEsperado)
    {
        var resultado = _sut.CalcularDescontoPorPercentual(valorOriginal, percentualDesconto);
        Assert.Equal(valorEsperado, resultado);
    }

    [Theory]
    [InlineData(20, false, true)]
    [InlineData(16, true, true)]
    [InlineData(17, false, false)]

    public void EValidoParaCupom_DeveValidarElegibilidade(int idade, bool primeiraCompra, bool esperado)
    {
        var resultado = _sut.EValidoParaCupom(idade, primeiraCompra);
        Assert.Equal(esperado, resultado);
    }
}
