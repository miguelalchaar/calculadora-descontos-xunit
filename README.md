# CalculadoraDescontos

![C#](https://img.shields.io/badge/C%23-.NET%2010-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-Testes%20Unit%C3%A1rios-5E2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![Git](https://img.shields.io/badge/Git-Versionamento-orange?style=for-the-badge&logo=git&logoColor=white)
![GitHub](https://img.shields.io/badge/GitHub-Reposit%C3%B3rio-black?style=for-the-badge&logo=github&logoColor=white)
![License](https://img.shields.io/badge/Licen%C3%A7a-MIT-blue?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Conclu%C3%ADdo-brightgreen?style=for-the-badge)

Projeto desenvolvido para a disciplina de **Garantia da Qualidade de Software**, com foco na prática de **testes parametrizados com xUnit** em uma solução **.NET 10** criada inteiramente pela linha de comando (**.NET CLI**).

A atividade simula a implementação de uma **calculadora de descontos para e-commerce**, com métodos que retornam diferentes tipos de dados (`string`, `int` e `bool`), todos cobertos por testes automatizados que utilizam `[Theory]` e `[InlineData]`.

Além da implementação do serviço, o principal objetivo da atividade é compreender a diferença entre **`[Fact]`** (teste único, sem parâmetros) e **`[Theory]`** (teste parametrizado que executa múltiplas vezes com os dados informados em `[InlineData]`), evitando a duplicação de métodos de teste para cada cenário.

## ▸ Conceito da atividade

A atividade consiste na criação de um repositório chamado `calculadora-descontos-xunit` e na implementação de uma solução composta por dois projetos:

| Projeto                      | Tipo                  | Responsabilidade                            |
| ---------------------------- | --------------------- | ------------------------------------------- |
| `CalculadoraDescontos.App`   | Aplicação de console  | Código de produção com as regras de negócio |
| `CalculadoraDescontos.Tests` | Projeto de testes xUnit | Validação automatizada das regras         |

Cada projeto possui uma responsabilidade específica dentro da solução.

O projeto `CalculadoraDescontos.App` contém a classe `DescontoService`, com as regras de negócio. O projeto `CalculadoraDescontos.Tests` referencia o projeto da aplicação e valida cada uma dessas regras por meio de testes parametrizados.

Essa separação simula a estrutura utilizada em projetos reais, onde o código de produção e o código de teste ficam isolados, mas conectados por uma referência.

## ▸ Camadas da solução

### `CalculadoraDescontos.App` - Código de Produção

O projeto `CalculadoraDescontos.App` é uma aplicação de console que reúne a lógica do serviço de descontos.

Neste projeto são implementados:

- A classe `DescontoService`;
- As regras de categoria de cliente, cálculo de desconto e elegibilidade a cupom;
- Métodos com retornos dos tipos `string`, `int` e `bool`.

É a camada que representa o comportamento real do sistema.

### `CalculadoraDescontos.Tests` - Testes Unitários

O projeto `CalculadoraDescontos.Tests` representa o ambiente de **validação automatizada**.

Neste projeto ocorre:

- A criação de testes parametrizados com o atributo `[Theory]`;
- O fornecimento de múltiplos cenários com `[InlineData(...)]`;
- A verificação dos retornos esperados com `Assert.Equal`;
- A garantia de que as regras de negócio continuam corretas após qualquer alteração.

Os testes funcionam como uma camada de segurança entre o código escrito e a confiança de que ele se comporta como esperado.

## ▸ Diferença entre `[Fact]` e `[Theory]`

| Característica        | `[Fact]`                                   | `[Theory]`                                                  |
| --------------------- | ------------------------------------------ | ----------------------------------------------------------- |
| **Parâmetros**        | Não recebe parâmetros                      | Recebe parâmetros no método de teste                        |
| **Fonte dos dados**   | Valores fixos escritos dentro do teste     | Atributos como `[InlineData]`, `[MemberData]`, `[ClassData]` |
| **Execuções**         | Uma única execução                         | Uma execução para cada conjunto de dados                    |
| **Quando usar**       | Cenário único e bem definido               | Mesma lógica validada com várias entradas e saídas          |
| **Relatório**         | Aparece como 1 teste                       | Cada `[InlineData]` aparece como um teste individual        |

**Exemplo com `[Fact]`** - um método para cada cenário (gera duplicação):

```csharp
[Fact]
public void ObterCategoriaCliente_DeveRetornarBronze()
{
    var service = new DescontoService();
    Assert.Equal("BRONZE", service.ObterCategoriaCliente(2));
}

[Fact]
public void ObterCategoriaCliente_DeveRetornarPrata()
{
    var service = new DescontoService();
    Assert.Equal("PRATA", service.ObterCategoriaCliente(7));
}
```

**Exemplo com `[Theory]`** - um único método para vários cenários:

```csharp
[Theory]
[InlineData(2, "BRONZE")]
[InlineData(7, "PRATA")]
[InlineData(15, "OURO")]
public void ObterCategoriaCliente_DeveRetornarCategoriaCorreta(int totalCompras, string esperado)
{
    var service = new DescontoService();
    var resultado = service.ObterCategoriaCliente(totalCompras);
    Assert.Equal(esperado, resultado);
}
```

> Em resumo: use `[Fact]` quando o teste não depende de dados variáveis e use `[Theory]` quando o mesmo comportamento precisa ser validado com diferentes entradas.

## ▸ Serviço de Descontos

A classe `DescontoService` é responsável por centralizar as regras relacionadas a descontos e cupons da loja online.

| Método                                                        | Retorno  | Regra                                                                                                   |
| ------------------------------------------------------------- | -------- | ------------------------------------------------------------------------------------------------------- |
| `ObterCategoriaCliente(totalCompras)`                         | `string` | `"BRONZE"` para menos de 5 compras, `"PRATA"` de 5 a 10 (inclusive) e `"OURO"` para mais de 10 compras  |
| `CalcularDescontoPorPercentual(valorOriginal, percentualDesconto)` | `int` | Retorna o valor final com o desconto aplicado                                                       |
| `EValidoParaCupom(idade, primeiraCompra)`                     | `bool`   | `true` se o cliente tiver 18 anos ou mais **ou** se for a primeira compra; caso contrário, `false`      |

**Exemplos:**

```text
ObterCategoriaCliente(2)                   →  "BRONZE"
ObterCategoriaCliente(7)                   →  "PRATA"
ObterCategoriaCliente(15)                  →  "OURO"
CalcularDescontoPorPercentual(100, 10)     →  90   (100 - 10% de 100)
EValidoParaCupom(16, true)                 →  true
EValidoParaCupom(17, false)                →  false
```

## ▸ Testes unitários realizados

Foram escritos três testes parametrizados na classe `DescontoServiceTests`, cada um com três `[InlineData]`, totalizando **9 casos de teste** executados individualmente.

| Tipo       | Método testado                  | Asserção utilizada | O que valida                                                  |
| ---------- | ------------------------------- | ------------------ | ------------------------------------------------------------- |
| **string** | `ObterCategoriaCliente`         | `Assert.Equal`     | Categorias BRONZE, PRATA e OURO                               |
| **int**    | `CalcularDescontoPorPercentual` | `Assert.Equal`     | Valor final após o desconto, inclusive com 0% de desconto     |
| **bool**   | `EValidoParaCupom`              | `Assert.Equal`     | Combinações de idade e primeira compra para elegibilidade     |

### Teste 1 - string

Valida se `ObterCategoriaCliente` retorna a categoria correta para cada faixa de compras:

| `totalCompras` | Resultado esperado |
| -------------- | ------------------ |
| `2`            | `"BRONZE"`         |
| `7`            | `"PRATA"`          |
| `15`           | `"OURO"`           |

### Teste 2 - int

Valida se `CalcularDescontoPorPercentual` calcula o valor final corretamente:

| `valorOriginal` | `percentualDesconto` | Resultado esperado |
| --------------- | -------------------- | ------------------ |
| `100`           | `10`                 | `90`               |
| `200`           | `20`                 | `160`              |
| `50`            | `0`                  | `50`               |

### Teste 3 - bool

Valida a elegibilidade ao cupom em diferentes combinações:

| `idade` | `primeiraCompra` | Resultado esperado | Cenário                                    |
| ------- | ---------------- | ------------------ | ------------------------------------------ |
| `20`    | `false`          | `true`             | Maior de idade, não é primeira compra      |
| `16`    | `true`           | `true`             | Menor de idade, primeira compra            |
| `17`    | `false`          | `false`            | Menor de idade, não é primeira compra      |

## ▸ Configuração via .NET CLI

Comandos utilizados para criar a estrutura da solução:

```bash
# 1. Cria a solução
dotnet new sln -n CalculadoraDescontos

# 2. Cria o projeto da aplicação (código de produção)
dotnet new console -n CalculadoraDescontos.App -f net10.0

# 3. Cria o projeto de testes unitários com xUnit
dotnet new xunit -n CalculadoraDescontos.Tests -f net10.0

# 4. Adiciona ambos os projetos à solução
dotnet sln add CalculadoraDescontos.App/CalculadoraDescontos.App.csproj
dotnet sln add CalculadoraDescontos.Tests/CalculadoraDescontos.Tests.csproj

# 5. Adiciona a referência do projeto de produção no projeto de testes
dotnet add CalculadoraDescontos.Tests/CalculadoraDescontos.Tests.csproj reference CalculadoraDescontos.App/CalculadoraDescontos.App.csproj
```

## ▸ Como executar

**Pré-requisito:** possuir o [SDK do .NET 10](https://dotnet.microsoft.com/download) instalado na máquina.

1. Clone o repositório:

```bash
git clone https://github.com/miguelalchaar/calculadora-descontos-xunit.git
```

2. Acesse a pasta do projeto:

```bash
cd calculadora-descontos-xunit
```

3. Execute os testes unitários:

```bash
dotnet test
```

Para visualizar cada `[InlineData]` como um teste individual, execute com saída detalhada:

```bash
dotnet test --logger "console;verbosity=detailed"
```

**Resultado esperado:**

```text
Passed!  - Failed: 0, Passed: 9, Skipped: 0, Total: 9
```


## ▸ Licença

Este projeto está licenciado sob a **Licença MIT**. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.

## ▸ Autor

Projeto desenvolvido pelos responsáveis pela implementação e manutenção do serviço:

- **miguelalchaar** - Desenvolvedor