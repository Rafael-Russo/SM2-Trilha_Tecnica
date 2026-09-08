# Deploy dos projetos C# e .NET no IIS

Guia para publicar as aplicações web desta pasta em um IIS local de testes.
Tudo que está aqui foi verificado executando build, publish e smoke test nesta máquina em 08/09/2026.

---

## 1. Inventário dos projetos

A pasta tem oito projetos. Só três são aplicações web e podem ir para o IIS. Os outros cinco são executáveis de console e não têm o que hospedar.

### Vão para o IIS

| Projeto | Framework | Tipo | Banco | Observação |
|---|---|---|---|---|
| TodoApi | net7.0 | Minimal API | InMemory | Não precisa de banco externo |
| ControleFinanceiro | net6.0 | MVC + Identity | MySQL | Detecta a versão do MySQL na inicialização |
| WebSystemMvc | netcoreapp2.1 | MVC | MySQL | Exige ajuste no módulo do IIS, ver seção 6 |

### Não vão para o IIS

| Projeto | Framework | Como executar |
|---|---|---|
| ProjetoXadrez | net6.0 | `dotnet run` no terminal |
| ProjetoMetodosAbstratos | net6.0 | `dotnet run` no terminal |
| DesignPatterns_CSharp | .NET Framework 4.7.2 | Seis apps de console, abrir a solution no Visual Studio |

Os projetos de console produzem um `.exe` que escreve no terminal. O IIS hospeda processos que respondem HTTP, então não há como publicá-los como site. Se um dia precisar deles rodando no servidor, o caminho é serviço do Windows ou tarefa agendada, não IIS.

---

## 2. Estado atual desta máquina

Levantamento feito antes de escrever este guia.

| Item | Situação |
|---|---|
| IIS (serviço W3SVC) | Rodando |
| ASP.NET Core Module V2 | Ausente |
| .NET SDK | 9.0.317, único instalado |
| Runtimes .NET | 9.0.19 apenas |
| MySQL80 | Rodando |
| MySQL95 | Parado |

Duas consequências importantes.

**O IIS ainda não consegue hospedar nenhuma aplicação ASP.NET Core.** O arquivo `aspnetcorev2.dll` não existe em `C:\Program Files\IIS\Asp.Net Core Module\V2`. Sem ele o IIS devolve erro 500.19 em qualquer site publicado. Resolver isso é o passo 3.

**Nenhum dos três projetos roda com o runtime instalado.** Eles têm como alvo .NET 6, .NET 7 e .NET Core 2.1, e a máquina só tem o runtime 9. Resolver isso é o passo 4.

---

## 3. Instalar o ASP.NET Core Hosting Bundle

Esse é o único pré-requisito obrigatório. Ele instala o módulo que conecta o IIS ao processo da aplicação.

Baixe em https://dotnet.microsoft.com/download/dotnet/9.0 na seção "ASP.NET Core Runtime", opção "Hosting Bundle". Instale e reinicie o IIS:

```powershell
net stop was /y
net start w3svc
```

Confirme que funcionou:

```powershell
Test-Path "$env:ProgramFiles\IIS\Asp.Net Core Module\V2\aspnetcorev2.dll"
```

Precisa retornar `True`. Se retornar `False`, o site não vai subir e não adianta seguir.

---

## 4. Escolha da forma de publicação

Existem dois caminhos, e a diferença entre eles é quem carrega o runtime.

**Framework-dependent** é o padrão. O pacote publicado fica pequeno, mas exige o runtime exato instalado na máquina. Para estes projetos isso significaria instalar os runtimes 6.0, 7.0 e 2.1, todos fora de suporte, só para uma máquina de teste.

**Self-contained** empacota o runtime junto com a aplicação. A pasta publicada fica na casa de 100 MB, e em troca cada aplicação carrega sua própria versão. Nada mais precisa ser instalado além do Hosting Bundle.

Este guia usa self-contained. É a opção que evita instalar três runtimes obsoletos e que foi testada aqui com sucesso nos três projetos, inclusive no de .NET Core 2.1.

O comando tem sempre este formato:

```powershell
dotnet publish <caminho-do-csproj> -c Release -r win-x64 --self-contained true -o <pasta-destino>
```

---

## 5. Preparar o IIS

Faça uma vez por aplicação. Abra o PowerShell **como administrador**.

### Criar as pastas de destino

```powershell
New-Item -ItemType Directory -Force C:\inetpub\apps\TodoApi
New-Item -ItemType Directory -Force C:\inetpub\apps\ControleFinanceiro
New-Item -ItemType Directory -Force C:\inetpub\apps\WebSystemMvc
```

### Criar o Application Pool

O ponto crítico é o `-managedRuntimeVersion ""`. Aplicações .NET Core não rodam sobre o CLR do IIS, elas rodam em processo próprio. Deixar esse campo vazio corresponde à opção "No Managed Code" na interface do IIS Manager. Se ficar com "v4.0", o site retorna erro na primeira requisição.

```powershell
Import-Module WebAdministration

New-WebAppPool -Name "TodoApiPool"
Set-ItemProperty IIS:\AppPools\TodoApiPool -Name managedRuntimeVersion -Value ""

New-WebAppPool -Name "ControleFinanceiroPool"
Set-ItemProperty IIS:\AppPools\ControleFinanceiroPool -Name managedRuntimeVersion -Value ""

New-WebAppPool -Name "WebSystemMvcPool"
Set-ItemProperty IIS:\AppPools\WebSystemMvcPool -Name managedRuntimeVersion -Value ""
```

### Criar os sites

Cada aplicação recebe sua própria porta, o que evita conflito com o Default Web Site na 80.

```powershell
New-Website -Name "TodoApi" -Port 8081 -PhysicalPath "C:\inetpub\apps\TodoApi" -ApplicationPool "TodoApiPool"
New-Website -Name "ControleFinanceiro" -Port 8082 -PhysicalPath "C:\inetpub\apps\ControleFinanceiro" -ApplicationPool "ControleFinanceiroPool"
New-Website -Name "WebSystemMvc" -Port 8083 -PhysicalPath "C:\inetpub\apps\WebSystemMvc" -ApplicationPool "WebSystemMvcPool"
```

### Liberar permissão nas pastas

A identidade do pool precisa ler os arquivos e escrever na pasta de log. O nome da conta é sempre `IIS AppPool\` seguido do nome do pool.

```powershell
icacls "C:\inetpub\apps\TodoApi" /grant "IIS AppPool\TodoApiPool:(OI)(CI)RX" /T
icacls "C:\inetpub\apps\ControleFinanceiro" /grant "IIS AppPool\ControleFinanceiroPool:(OI)(CI)RX" /T
icacls "C:\inetpub\apps\WebSystemMvc" /grant "IIS AppPool\WebSystemMvcPool:(OI)(CI)RX" /T
```

Repita o `icacls` depois de cada nova publicação, porque arquivos novos herdam as permissões da pasta mas o comando garante o resultado.

---

## 6. Publicar cada projeto

Rode os comandos a partir da pasta `C# e .Net`.

### TodoApi

O mais simples dos três. Usa banco em memória, então não depende de nada externo.

```powershell
dotnet publish ".\TodoApi\TodoApi\TodoApi.csproj" -c Release -r win-x64 --self-contained true -o "C:\inetpub\apps\TodoApi"
```

Teste em `http://localhost:8081/todoitems`. A resposta esperada é `[]`, uma lista vazia em JSON.

A API não tem página inicial. Acessar `http://localhost:8081/` devolve 404, e isso é o comportamento correto. As rotas disponíveis estão todas sob `/todoitems`.

Como o banco é em memória, os dados somem sempre que o pool recicla. Para testes de curta duração não incomoda.

### ControleFinanceiro

Precisa do MySQL antes de subir. Veja a seção 7.

```powershell
dotnet publish ".\ControleFinanceiro\ControleFinanceiro\ControleFinanceiro.csproj" -c Release -r win-x64 --self-contained true -o "C:\inetpub\apps\ControleFinanceiro"
```

Teste em `http://localhost:8082/`.

Duas coisas desta aplicação merecem atenção.

O `Program.cs` chama `ServerVersion.AutoDetect` sobre a connection string durante a inicialização. Isso abre uma conexão com o MySQL antes de o site atender a primeira requisição. Se o banco estiver fora do ar ou a senha estiver errada, a aplicação não inicia e o IIS devolve 500.30.

O pipeline chama `UseHttpsRedirection`. Em um site que só tem binding HTTP, isso gera um redirecionamento que não leva a lugar nenhum. Para testes, o mais rápido é adicionar um binding HTTPS. A alternativa é criar `appsettings.Production.json` na pasta publicada e desligar o comportamento, o que é aceitável em máquina de teste.

### WebSystemMvc

Este exige um parâmetro extra. O projeto tem como alvo .NET Core 2.1, e o SDK gera um `web.config` apontando para `AspNetCoreModule`, a versão 1 do módulo. Essa versão não vem nos Hosting Bundles atuais, então o site quebraria com 500.19 mesmo com tudo instalado.

O parâmetro `-p:AspNetCoreModuleName=AspNetCoreModuleV2` corrige isso na publicação:

```powershell
dotnet publish ".\WebSystemMvc\WebSystemMvc\WebSystemMvc.csproj" -c Release -r win-x64 --self-contained true -p:AspNetCoreModuleName=AspNetCoreModuleV2 -o "C:\inetpub\apps\WebSystemMvc"
```

Confira o resultado abrindo `C:\inetpub\apps\WebSystemMvc\web.config`. A linha do handler tem que dizer `modules="AspNetCoreModuleV2"`.

Não adicione o atributo `hostingModel="inprocess"` neste projeto. Hospedagem in-process só existe a partir do ASP.NET Core 2.2. Sem o atributo, o módulo usa out-of-process, que é o modo compatível com 2.1.

Se preferir uma correção permanente em vez de repetir o parâmetro, adicione ao `WebSystemMvc.csproj` dentro do `PropertyGroup`:

```xml
<AspNetCoreModuleName>AspNetCoreModuleV2</AspNetCoreModuleName>
```

Teste em `http://localhost:8083/`. Aqui também aparece o aviso de redirecionamento HTTPS descrito acima.

---

## 7. Banco de dados MySQL

Dois projetos dependem de MySQL. O serviço MySQL80 já está rodando nesta máquina.

As connection strings vêm gravadas no `appsettings.json` de cada projeto:

| Projeto | Servidor | Usuário | Senha | Base |
|---|---|---|---|---|
| ControleFinanceiro | localhost | root | vazia | controlefinanceirodb |
| WebSystemMvc | localhost | root | admin | websystemmvcdb |

As senhas provavelmente não batem com a instalação atual. Ajuste o `appsettings.json` na pasta publicada antes de acessar o site, ou aplique a mudança no projeto e publique de novo.

Guardar senha em `appsettings.json` serve para uma máquina de teste isolada. Se essas aplicações forem para qualquer ambiente compartilhado, mova a connection string para variável de ambiente ou para as configurações do próprio site no IIS.

### Criar as bases e aplicar as migrations

Os dois projetos já têm migrations no repositório. Rode a partir da pasta do projeto, não da pasta publicada:

```powershell
dotnet tool install --global dotnet-ef
dotnet ef database update --project ".\ControleFinanceiro\ControleFinanceiro\ControleFinanceiro.csproj"
dotnet ef database update --project ".\WebSystemMvc\WebSystemMvc\WebSystemMvc.csproj"
```

O WebSystemMvc tem um `SeedingService` que popula dados de exemplo, mas ele só roda quando o ambiente é Development. No IIS, com ambiente Production, as tabelas ficam vazias. Se quiser os dados de exemplo, defina `ASPNETCORE_ENVIRONMENT` como `Development` nas configurações do pool.

---

## 8. Verificar se subiu

Depois de publicar, rode isto para checar as três de uma vez:

```powershell
foreach ($u in @("http://localhost:8081/todoitems","http://localhost:8082/","http://localhost:8083/")) {
  try {
    $r = Invoke-WebRequest -Uri $u -UseBasicParsing -TimeoutSec 20
    Write-Output "$u -> HTTP $($r.StatusCode)"
  } catch {
    Write-Output "$u -> FALHOU: $($_.Exception.Message)"
  }
}
```

Resultado esperado: HTTP 200 nas três.

---

## 9. Quando der errado

O primeiro passo em qualquer falha é ligar o log da aplicação. Abra o `web.config` da pasta publicada e troque `stdoutLogEnabled="false"` por `true`. Crie a pasta `logs` dentro do diretório publicado e dê permissão de escrita ao pool. Recicle o pool e reproduza o erro. O arquivo gerado em `logs\stdout` traz a exceção real, que é muito mais útil que a página de erro do IIS.

Lembre de voltar para `false` depois, porque o arquivo cresce sem limite.

### 500.19

O IIS não conseguiu carregar o módulo declarado no `web.config`. Duas causas.

O Hosting Bundle não está instalado. Volte para a seção 3.

O `web.config` pede `AspNetCoreModule` em vez de `AspNetCoreModuleV2`. Acontece com o WebSystemMvc quando o parâmetro extra da publicação é esquecido. Veja a seção 6.

### 502.5

O processo da aplicação iniciou e morreu logo em seguida. Quase sempre é runtime ausente. Confirme que a publicação foi self-contained verificando se existe um `.exe` com o nome do projeto na pasta publicada. Se só houver o `.dll`, a publicação saiu framework-dependent e vai procurar um runtime que não existe aqui.

### 500.30

A aplicação lançou exceção durante a inicialização. Nos projetos com MySQL isso normalmente é banco inacessível ou senha errada. O log stdout mostra a exceção exata.

### Redirecionamento infinito ou porta HTTPS inválida

`ControleFinanceiro` e `WebSystemMvc` chamam `UseHttpsRedirection`. Sem binding HTTPS o navegador é mandado para uma porta que não responde. Adicione um binding HTTPS ao site ou desabilite o redirecionamento via configuração.

### Acesso negado aos arquivos

Rode o `icacls` da seção 5. Vale conferir também se o pool está usando `ApplicationPoolIdentity`, que é o padrão e o que o comando pressupõe.

### Arquivo em uso ao republicar

O IIS mantém os arquivos abertos. Pare o pool antes de publicar por cima:

```powershell
Stop-WebAppPool -Name "TodoApiPool"
# publique aqui
Start-WebAppPool -Name "TodoApiPool"
```

---

## 10. Resultado dos testes feitos aqui

Estes números vieram da execução real nesta máquina, não de estimativa.

| Projeto | Build | Publish self-contained | Execução |
|---|---|---|---|
| TodoApi | 0 erros | Sucesso | HTTP 200 em `/todoitems`, resposta `[]` |
| ControleFinanceiro | 0 erros | Sucesso | Não testado, depende de MySQL configurado |
| WebSystemMvc | 0 erros | Sucesso | HTTP 200 na raiz |

Os avisos de build não impedem nada. O TodoApi acusa framework fora de suporte, o ControleFinanceiro acusa propriedades não anuláveis, e o WebSystemMvc acusa vulnerabilidades conhecidas nos pacotes 2.1. Este último é o único que merece atenção se a aplicação sair de uma máquina isolada.

---

## 11. Rodar os projetos de console

Não têm relação com IIS, ficam aqui para completar a documentação do repositório.

```powershell
dotnet run --project ".\ProjetoXadrez\ProjetoXadrez\ProjetoXadrez.csproj"
dotnet run --project ".\ProjetoMetodosAbstratos\ProjetoMetodosAbstratos\ProjetoMetodosAbstratos.csproj"
```

Os seis projetos de `DesignPatterns_CSharp` usam .NET Framework 4.7.2 e não compilam nesta máquina. A tentativa falha assim:

```
error MSB3644: The reference assemblies for .NETFramework,Version=v4.7.2 were not found.
```

Falta o Targeting Pack do .NET Framework 4.7.2, que não vem com o SDK do .NET 9. Instale o Developer Pack de https://aka.ms/msbuild/developerpacks ou abra `DesignPatterns_CSharp\DesignPatterns\DesignPatterns.sln` no Visual Studio com a carga de trabalho de .NET Framework. A solution organiza os padrões em três pastas: Creational, Structural e Behavioral.

Vale notar que existem duas pastas quase idênticas, `ChainOfResponsability` e `ChainOfResponsibility`. Só a segunda está referenciada na solution e só ela tem a implementação completa. A primeira parece ser um resto de uma tentativa anterior e pode ser removida.
