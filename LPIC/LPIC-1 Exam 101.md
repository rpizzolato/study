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

- `/proc`: informações dos processos ativos e recursos de hardware
- `/sys`: informações sobre dispositivos de hardware (sysfs) - similar ao `/proc`
- `/dev`: referências aos dispositivos do sistema, inclusive de armazenamento (udev)
- `udev`: Device Manager
- `dbus` ou `hald`: Comunicação entre processos. Informa os processos a situação dos dispositivos de hardware.



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
- `echo $TESTE`: lê o valor da variável TESTE
- `TESTE=valor1`: define um valor para a variável TESTE
- `env | grep TESTE`: não encontra nada da variável TESTE pois ela não foi exportada, está localmente apenas
- `set | grep TESTE`: mostra tanto as locais de ambiente, como as exportadas de usuário.

>[!NOTE]
>
>Para remover variáveis definidas (locais ou globais), usamos o comando  `unset`:

$ echo $reptile
tortoise
$ unset reptile
$ echo $reptile
$

Se tivermos um script que lê essa variável TESTE, ao executá-lo ele não irá ler, pois quando é executado um script ele cria abre em uma nova sessão do shell/bash (processo filho do shell atual), logo essa variável teria que estar exportada.

É possível contornar isso com o comando **source**. Ele faz com que seja executado no mesmo shell da sessão atual, dessa forma retornando o valor de TESTE.

Outra forma é utilizar o ponto (.): `. script.sh` (faz rodar/executar localmente o script, sem chamar outra sessão de bash) (**NÃO** confundir com `./script.sh`)

Para criarmos variáveis imutáveis, basta a deixarmos como **readonly** (somente leitura).<br>
Ex. `readonly reptile=tortoise`<br>
Ou transformá-las depois de criá-las:<br>

    reptile=tortoise
    readonly reptile

Agora, se tentarmos alterar o valor de  `reptile`, o Bash se recusará:

    $ reptile=lizard
    -bash: distro: readonly variable

>[!NOTE]
>
>Para listar todas as variáveis somente leitura da sessão atual, digite `readonly` ou `readonly -p` no terminal

Comando **alias**: cria alias para os comandos (sinônimos de comandos)

`alias dt="date +%H:%M"`: cria um alias chamado **dt** que vai executar o comando date mostrando apenas hora e minuto (se deslogar, perde a configuração, é apenas temporário)

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
Para visualizar a função é só digitar `set`

>[!NOTE]
>
>É possível começar uma função digitando tudo em uma linha ou omitindo a palavra `function`. Ex. `funcao3 () {date; uptime; }`

Para configuração/customização do ambiente shell, usamos os seguintes arquivos:
- `/etc/profile`: usado para quando um usuário faz o procedimento de login, seja por interface gráfica ou por terminal.
- `/etc/bash.bashrc`: aplicado quando se abre uma nova sessão de bash/shell (sempre que ver `algo.bashrc`, está ligado a um novo shell/bash e `algo.profile` é um novo login)

Por exemplo, caso adicionar uma variável no final do arquivo **/etc/profile**, chamada E**TCPROFILE=Valor1** e tentarmos com o **echo** ver o valor dessa variável, não será retornado nada. No entanto se for mudado para qualquer outro terminal (com CTRL+ALT+F1) e efetuar o login, e exibir a variável com o comando **echo**, a variável aparecerá. 
Isso deixa claro que **profile** está ligado com login realmente, no caso quando qualquer usuário fizer login.

Ainda no **/etc/profile** que é possível incluir um alias, uma função, para que seja comum a todos que se logarem no sistema.

É possível notar que ao se fazer o mesmo teste, agora com **/etc/bash.bashrc**, é possível perceber que a variável apenas irá aparecer quando for aberto um novo shell (e **NÃO** no shell atual) ou um novo login, que automaticamente irá abrir um novo shell.

Dentro de **/etc/profile.d/** há scripts que são carregados ao fazer login. Lembrando que o que foi visto é de definição geral, para todos os usuários.

Para configurações individuais de login (correspondente ao **/etc/profile**), geralmente em **/home** de cada usuário, temos os arquivos, nessa ordem, e logo que um é encontrado e executado, os outros são ignorados:
- `~/.bash_profile`
- `~/.bash_login` (caso não exista `~/.bash_profile`)
- `~/.profile` (mais comum)

Já o correspondente ao **/etc/bash.bashrc**, temos o (nova sessão, novo shell):
- **~/.bashrc**

Dentro do **/home** do usuário há ainda um arquivo chamado **.bash_logout** que é executado ao fazer logout do usuário.

- `~/.bash_logout`: se existir, este arquivo específico do Bash faz algumas operações de limpeza ao sair do shell. Isso pode ser conveniente em certos casos, como as sessões remotas.

Em **/etc/inputrc** temos a opção de editar as definições de input do terminal. Por exemplo, o que fará a combinação de Ctrl + alguma tecla específica, como limpar tela, e etc.

>[!WARNING]
>
>Lembre-se, devido à ordem em que os arquivos são executados, os arquivos **locais** têm precedência sobre os **globais**.

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

Ainda sobre terminais, podemos dizer se estiver usando uma interface gráfica, muito provavelmente estará em uso de algum emulador de terminal na GUI, ou um shell **pts**, como gnome-terminal ou konsole (são mais ricos em recursos e fáceis de usar).<br>
Agora se estiver lidando com console do sistema, ou tty, trata-se de um terminal baseado em texto.<br>
Pode-se alternar, como já vimos, com as teclas Ctrl+Alt+F1-F6, sendo o F7 normalmente a sssão que leva à interface gráfica.

>[!NOTE]
>
>**tty** significa teletypewritter (teletipo); **pts** é a abreviação de pseudo terminal slave (pseudo terminal escravo). Para saber mais: **man tty** e **man pts**

#### Lançando shells com o bash
Após fazer o login, digite **bash** em um terminal para abrir um novo shell. Tecnicamente, este shell é um processo filho do shell atual.

Ao iniciar o processo filho bash, podemos especificar diversas opções para definir que tipo de shell queremos iniciar. Eis algumas opções importantes de invocação no bash:

- **bash -l** ou **bash --login**: invoca um shell de login.
- **bash -i**: invoca um shell interativo.
- **bash --noprofile**: com shells de login, ignora o arquivo de inicialização do sistema /etc/profile e os arquivos de inicialização em nível de usuário ~/.bash_profile, ~/.bash_login e ~/.profile.
- **bash --norc**: com shells interativos, ignora tanto o arquivo de inicialização do sistema /etc/bash.bashrc quanto o arquivo de inicialização em nível de usuário ~/.bashrc.
- **bash --rcfile** <file>: com shells interativos, considera <file> como arquivo de inicialização, ignorando os arquivos de inicialização do sistema /etc/bash.bashrc e em nível de usuário ~/.bashrc

#### #### Iniciando shells com  `su`  e  `sudo`

- `su`: muda o ID de user ou o torna superusuário
- `su - user2`, `su -l user2` ou `su --login user2`: iniciam um shell de login interativo com **user2**
- `su user2`: inicia um shell sem login interativo como **user2**
- `su - root` ou `su -`: inicia um shell de login interativo como **root**
- `su root` ou `su` inicia um shell interativo sem login como `root`

`sudo`: executa comandos como outro usuário (incluindo o superusuário). Como este comando é usado principalmente para obter privilégios de root temporariamente, o usuário que o emprega deve estar no arquivo  `sudoers`. Para adicionar usuários a  `sudoers`, precisamos nos tornar  `root`  e então executar:

    # usermod -aG sudo user2

Assim como o  `su`, o  `sudo`  permite invocar shells de login e sem login:
-   `sudo su - user2`,  `sudo su -l user2`  ou  `sudo su --login user2`  iniciam um shell de login interativo como  `user2`.   
-   `sudo su user2`  inicia um shell sem login interativo como  `user2`.
-   `sudo -u user2 -s`  inicia um shell sem login interativo como  `user2`.
-   `sudo su - root`  ou  `sudo su -`  inicia um shell de login interativo como  `root`.
-   `sudo -i`  inicia um shell de login interativo como  `root`.
-   `sudo -i <algum_comando>`  inicia um shell de login interativo como  `root`, executa o comando e retorna ao usuário original.
-   `sudo su root`  ou  `sudo su`  inicia um shell sem login interativo como  `root`.
-   `sudo -s`  ou  `sudo -u root -s`  iniciam um shell sem login como  `root`.

Ao usar `su` ou `sudo`, é importante considerar o contexto particular antes de iniciar um novo shell: Precisamos ou não do ambiente do usuário de destino? Se a resposta for sim, usaríamos as opções que invocam shells de login; se não, as que invocam shells sem login.

- `echo $0`: mostra qual tipo de shell está sendo usado no momento

#### Ver a quantidade de shells

`ps aux | grep bash`: supondo a seguinte saída<br>

    user2@debian:~$ **ps aux | grep bash**
    user2       5270  0.1  0.1  25532  5664 pts/0    Ss   23:03   0:00 bash
    user2       5411  0.3  0.1  25608  5268 tty1     S+   23:03   0:00 -bash
    user2       5452  0.0  0.0  16760   940 pts/0    S+   23:04   0:00 grep --color=auto bash
    
A usuária `user2` em `debian` se logou em uma sessão GUI (ou X Window System) e abriu _gnome-terminal_, depois pressionou Ctrl+Alt+F1 para entrar em uma sessão de terminal `tty`. Finalmente, ela retornou à sessão GUI pressionando Ctrl+Alt+F7 e digitou o comando `ps aux | grep bash`. Assim, a saída mostra um shell sem login interativo por meio do emulador de terminal (`pts/0`) e um shell de login interativo por meio do terminal baseado em texto (`tty1`). Note também como o último campo de cada linha (o comando) é `bash` para o primeiro e `-bash` para o último.

- `-bash`  ou  `-su`: Interativo de login
- `bash`  or  `/bin/bash`: Interativo sem login
- `<nome_do_script>`: Não-interativo sem login (scripts)

#### Variáveis

Pode conter letras (`a-z, A-Z`), números (`0-9`) e sublinhados (`_`). E não deve começar com um número para não confundir o Bash. Não deve conter espaços (nem mesmo entre aspas), por convenção, os sublinhados são usados no lugar dos espaços.

No que diz respeito à referência ou valor das variáveis, também é importante considerar uma série de regras. As variáveis podem conter quaisquer caracteres alfanuméricos (`a-z`,`A-Z`,`0-9`), além da maioria dos outros caracteres (`?`,`!`,`*`,`.`,`/`, etc.). Os valores das variáveis devem ser postos entre aspas se contiverem espaços simples.

Os valores das variáveis também devem ser postos entre aspas se contiverem caracteres como os usados para redirecionamento (`<`,`>`) ou o símbolo de barra vertical (`|`). A única coisa que o comando a seguir faz é criar um arquivo vazio chamado  `zorin`:

    $ distro=>zorin
    $ echo $distro
    
    $ ls zorin
    zorin

Mas quando usamos as aspas, a coisa funciona:

    $ distro=">zorin"
    $ echo $distro
    >zorin

No entanto, aspas simples e duplas nem sempre são intercambiáveis. Dependendo do que estamos fazendo com uma variável (atribuindo ou referenciando), o uso de uma ou de outra tem implicações e produzirá resultados diferentes. No contexto da atribuição de variáveis, as **aspas simples** consideram  _literalmente_  todos os caracteres do valor da variável, enquanto as **aspas duplas** permitem a substituição de variáveis:

    $ lizard=uromastyx
    $ animal='My $lizard'
    $ echo $animal
    My $lizard
    $ animal="My $lizard"
    $ echo $animal
    My uromastyx

Por outro lado, ao referenciar uma variável cujo valor inclui alguns espaços iniciais (ou extras) — às vezes combinados com asteriscos — é obrigatório usar aspas duplas após o comando  `echo`  para evitar  _divisão de campos_  e  _expansão de nome de caminho_:

    $ lizard="   genus   |   uromastyx"
    $ echo $lizard
    genus | uromastyx
    $ echo "$lizard"
       genus   |   uromastyx

Se a referência da variável contiver um ponto de exclamação no final, este deve ser o último caractere da string (caso contrário, o Bash pensará que estamos nos referindo a um evento de  `history`):

    $ distro=zorin.?/!os
    -bash: !os: event not found
    $ distro=zorin.?/!
    $ echo $distro
    zorin.?/!

Todas as barras invertidas devem ser escapadas com outra barra invertida. Aliás, se uma barra invertida for o último caractere na string e não o escaparmos, o Bash interpretará que queremos uma quebra de linha e criará uma nova linha:

    $ distro=zorinos\
    >
    $ distro=zorinos\\
    $ echo $distro
    zorinos\


<!--stackedit_data:
eyJoaXN0b3J5IjpbMjgzMjU3NDAyLDg1MjI0NTc4NSwtMTc0OD
cwOTE3NSwtMzEzNzI0OTI1LDQwMzM5ODgwNywtMzc4NzM3MjE2
LC0xNDY1NjY0MDY2LDE2MjU3NzI4NCwtMTk2OTEwNzY1NCwyND
g4NzMzNDgsNjYyNzc5NDMyLC02NDYzMDk3NzIsLTc2MDQxOTE0
MCwtNTU5ODQxODkyLDIwMzI3MDYzODgsNjc0NTIxMDc2XX0=
-->