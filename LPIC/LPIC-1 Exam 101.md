# LPIC-1 Exam 101
#### Version: 5.0
LPIC-1 é a primeira certificação no programa de certificação profissional Linux multinível da LPI. O LPIC-1 validará a capacidade do candidato de realizar tarefas de manutenção na linha de comando, instalar e configurar um computador executando Linux e configurar a rede básica.

#### Anotações para servir de resumo e consulta futuramente

## Tópico 101: Arquitetura de Sistema

101.1 Identificar e editar configurações de hardware
 101.1 Lição 1
101.2 Início (boot) do sistema
 101.2 Lição 1
101.3 Alternar runlevels/boot targets, desligar e reiniciar o sistema
 101.3 Lição 1

### 101.1 Identificar e editar configurações de hardware

Resumidamente, precisamos saber que:

- ***IRQ*** -> `/proc/interrupts`
- **Endereços de I/O (E/S)** -> `/proc/ioports` (executar como root)
- **DMA** -> `/proc/dma`

**DMA**: Direct Memory Addressing (um canal que permite que os dispositivos transmitam os dados diretamente para a memória, sem utilizar a CPU)
*Não é utilizado por todos os dispositivos
Fica em `/proc/dma`

Endereços de I/O (E/S), lista de endereços de memória utilizadas para comunicação entre o CPU e os demais dispositivos de hardware. Fica em `/proc/ioports`

Ex. `sudo cat /proc/ioports`

0000-0001f : dma

10060-006f: keyboard

etc.



**IRQ** -> Interrupt Request (é um sinal enviado à CPU), podemos ver em /proc/interrupts

Principais IRQs:
IRQ0 - Sinal de clock da placa mãe
**IRQ1 - Teclado**
IRQ2 - Cascateador de IRQs
**IRQ3 - Porta serial 2 (RS-232)**
**IRQ4 - Porta serial 1 (RS-232)**
IRQ5 - Livre
IRQ6 - Drive de disquetes
IRQ7 - Porta paralela (impressora)
IRQ8 - Relógio do CMOS
IRQ9 - Placa de vídeo
IRQ10 - Livre
IRQ11 - Controlador USB
IRQ12 - Porta PS/2
IRQ13 - Coprocessador aritmético
**IRQ14 - IDE Primária**
**IRQ15 - IDE Secundária**

*o número do IRQ é uma prioridade, do 0 ao 2, que é o cascateador, ele pula para o 8 até o 15, depois continua do 3 ao 7. Ficando, **1,2,8,9,10,11,12,13,14,15,3,4,5,6,7**



Partições Virtuais: montado dinamicamente conforme a execução do SO

/proc: informações dos processos ativos e recursos de hardware

/sys: informações sobre dispositivos de hardware (sysfs) - similar ao /proc

/dev: referências aos dispositivos do sistema, inclusive de armazenamento (udev)



* udev: Device Manager

* dbus ou hald: Comunicação entre processos. Informa os processos a situação dos dispositivos de hardware.



***Os arquivos dentro do diretório /sys têm funções semelhantes às do /proc. No entanto, o diretório /sys tem o objetivo específico de armazenar informações do dispositivo e dados do kernel relacionados ao hardware.

Ao passo que /proc também contém informações sobre diversas estruturas de dados do kernel, incluindo processos em execução e configurações.


lspci: lista todos os dispositivos PCI

- lspci -s 00:01.1 -v: lista os detalhes do dispositivo de id 00:01.1



lsusb: lista todos os dispositivos USB

- lsusb: mostra o Barramento (Bus 001), o Dispositivo (001), o ID e o nome

- lsusb -s 001:001 -v: mostra detalhes do dispositivo 001:001 **(cuidado que não é o id, e sim a junção de Barramento e Dispositivo). O -s vem de [[bus]:][devnum]

*Nos barramentos USB, podemos ter um ou mais Barramentos com diversos dispositivos dentro dele:

**Para usar o id do USB para busca, usamos o -d (device): lsusb -v -d 413c:2113

***Com a opção -t, o comando lsusb mostra os mapeamentos do dispositivo USB atual na forma de árvore hierárquica

Ex.

Bus 001 Device 001

Bus 001 Device 002

Bus 002 Device 001

etc


Barramentos: interface de comunicação física entre os dispositivos e a placa mãe:

- PCI: Peripheral Component Interconnect (lspci)

- USB: Universal Serial Bus (lsusb)

## 102-500

### 105.1 Personalizar e trabalhar no ambiente shell

Relembrando:
- **echo $TESTE**: lê o valor da variável TESTE
- **TESTE=valor1**: define um valor para a variável TESTE
- **env | grep TESTE**: não encontra nada da variável TESTE pois ela não foi exportada, está localmente apenas
- **set | grep TESTE**: mostra tanto as locais de ambiente, como as exportadas de usuário.

Se tivermos um script que lê essa variável TESTE, ao executá-lo ele não irá ler, pois quando é executado um script ele cria abre em uma nova sessão do shell/bash, logo essa variável teria que estar exportada.

É possível contornar isso com o comando **source**. Ele faz com que seja executado no mesmo shell da sessão atual, dessa forma retornando o valor de TESTE.

Outra forma é utilizar o ponto (.): **. script.sh** (faz rodar/executar localmente o script, sem chamar outra sessão de bash) (**NÃO** confundir com **./script.sh**)

Comando alias: cria alias para os comandos (sinônimos de comandos)

**alias dt="date +%H:%M"**: cria um alias chamado dt que vai executar o comando date mostrando apenas hora e minuto (se deslogar, perde a configuração, é apenas temporário)

Se digitar somente **alias**, é mostrado os alias cadastrados no bash atual.

**function**: usado para criar uma rotina de comandos.

Ex. Criando uma função e a chamando no final<br> 
```
$ function funcao1 {
>    date;
>   uptime;
>    uname -a
>    echo "Fim funcao";
>}

$ funcao1
```
Para visualizar a função é só digitar **set**

>[!NOTE]
>
>É possível começar uma função digitando tudo em uma linha ou omitindo a palavra **function**. Ex. **funcao3 () {date; uptime; }**

Para configuração/customização do ambiente shell, usamos os seguintes arquivos:
- **/etc/profile**: usado para quando um usuário faz o procedimento de login, seja por interface gráfica ou por terminal.
- **/etc/bash.bashrc**: aplicado quando se abre uma nova sessão de bash/shell (sempre que ver **algo.bashrc**, está ligado a um novo shell/bash e **algo.profile** é um novo login)

Por exemplo, caso adicionar uma variável no final do arquivo **/etc/profile**, chamada E**TCPROFILE=Valor1** e tentarmos com o **echo** ver o valor dessa variável, não será retornado nada. No entanto se for mudado para qualquer outro terminal (com CTRL+ALT+F1) e efetuar o login, e exibir a variável com o comando **echo**, a variável aparecerá. 
Isso deixa claro que **profile** está ligado com login realmente, no caso quando qualquer usuário fizer login.

Ainda no **/etc/profile** que é possível incluir um alias, uma função, para que seja comum a todos que se logarem no sistema.

É possível notar que ao se fazer o mesmo teste, agora com **/etc/bash.bashrc**, é possível perceber que a variável apenas irá aparecer quando for aberto um novo shell (e **NÃO** no shell atual) ou um novo login, que automaticamente irá abrir um novo shell.

Dentro de **/etc/profile.d/** há scripts que são carregados ao fazer login. Lembrando que o que foi visto é de definição geral, para todos os usuários.

Para configurações individuais de login (correspondente ao **/etc/profile**), geralmente em **/home** de cada usuário, temos os arquivos, nessa ordem, dando prioridade ainda nessa ordem:
- **~/.bash_profile**
- **~/.bash_login**
- **~/.profile** (mais comum)

Já o correspondente ao **/etc/bash.bashrc**, temos o (nova sessão, novo shell):
- **~/.bashrc**

Dentro do **/home** do usuário há ainda um arquivo chamado **.bash_logout** que é executado ao fazer logout do usuário.

Em **/etc/inputrc** temos a opção de editar as definições de input do terminal. Por exemplo, o que fará a combinação de Ctrl + alguma tecla específica, como limpar tela, e etc.

Em **/etc/skel** (de esqueleto): toda vez que criar um usuário, a base de arquivos básicos do usuário será pega daqui, arquivos esses que irão fazer parte do usuário. (será pego desse "esqueleto" de arquivos).<br>
Se criarmos um arquivo qualquer, como **touch teste-skel**, quando criarmos um novo usuário, esse arquivo ficará disponível no **/home** do novo usuário. Isso se torna interessante quando é necessário colocar alguma configuração específica para todo usuário durante sua criação.

Principais Variáveis de Ambiente
É importante conhecer a função de algumas variáveis de ambiente existentes no sistema, as principais são:

- **DISPLAY**: Indica às aplicações gráficas onde as janelas deverão ser exibidas. Será estudado no Tópico 106
- **HISTFILE**: Arquivo do histórico de comandos
- **HISTFILESIZE**: Quantidade de linhas/comandos armazenados no arquivo de histórico
- **HOME**: Indica o diretório do usuário atual
- **LANG**: Definição do idioma
- **LOGNAME** e **USER**: Nome do usuário atual
- **PATH**: Diretórios em que o Linux irá procurar por arquivos executáveis
- **PS1**: Aparência do prompt do shell.
- **PWD**: Diretório atual
- **OLDPWD**: Diretório anterior

Outro comando interessante e que alguns alunos já reportaram ter caído nos exames é o **chsh**.

O **chsh** serve simplesmente para alterar o shell utilizado pelo usuário. Por exemplo:
```
1.  $ chsh
2.  Senha:
3.  Mudando o shell de login para ricardo
4.  Informe o novo valor ou pressione ENTER para aceitar o padrão
5.    Shell de Login  [/bin/sh]:  /bin/bash
```



<!--stackedit_data:
eyJoaXN0b3J5IjpbNjc0NTIxMDc2XX0=
-->