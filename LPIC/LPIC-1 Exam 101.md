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
- `env` ou `printenv`: imprime uma lista de todas as variáveis de ambiente

- `printenv` ainda pode ser usado de forma semelhante ao comando `echo`:<br>

    $ echo $PWD
    /home/user2
    $ printenv PWD
    /home/user2
Note, entretanto, que com `printenv` o nome da variável não é precedido por `$`.

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

>[!NOTE]
>
>`unset` deve ser seguido somente pelo nome da variável (não precedido pelo símbolo `$`)

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

Para que uma variável local do shell se torne uma variável de ambiente, usamos o comando  `export`:

    $ export reptile

Com  `export reptile`, transformamos nossa variável local em uma variável de ambiente para que os shells filhos possam reconhecê-la e usá-la:

    $ bash
    $ echo $reptile
    tortoise

Da mesma maneira,  `export`  pode ser usado para definir e exportar uma variável de uma só vez:

    $ export amphibian=frog

Agora podemos abrir uma nova instância do Bash e referenciar com sucesso a nova variável:

    $ bash
    $ echo $amphibian
    frog

>[!NOTE]
>
>Com `export -n <VARIABLE-NAME>`, a variável será novamente transformada em variável local do shell.

- `export` ou `export -p`: lista todas as variáveis de ambiente existentes. (`declare -x` é equivalente a `export`)

Comando `alias`: cria alias para os comandos (sinônimos de comandos)

`alias dt="date +%H:%M"`: cria um alias chamado **dt** que vai executar o comando date mostrando apenas hora e minuto (se deslogar, perde a configuração, é apenas temporário)

Se digitar somente `alias`, é mostrado os alias cadastrados no bash atual.

- `unalias alias_criado`: remove o alias criado

Podemos escapar um alias com  `\`:

    $ alias where?='echo $PWD'
    $ where?
    /home/user2
    $ \where?
    -bash: where?: command not found

O escape de um alias é útil quando um alias tem o mesmo nome de um comando regular. Nesse caso, o alias tem precedência sobre o comando original, que, no entanto, ainda pode ser acessado escapando-se o alias.

Da mesma forma, podemos colocar um alias dentro de outro alias:

    $ where?
    /home/user2
    $ alias my_home=where?
    $ my_home
    /home/user2

Além disso, também é possível colocar uma função dentro de um alias, como será demonstrado mais adiante.

#### Expansão e avaliação de aspas em aliases

Ao se usar aspas com variáveis de ambiente, as aspas simples tornam a **expansão dinâmica**:

    $ alias where?='echo $PWD'
    $ where?
    /home/user2
    $ cd Music
    $ where?
    /home/user2/Music

No entanto, com aspas duplas, a expansão é feita **estaticamente**:

    $ alias where?="echo $PWD"
    $ where?
    /home/user2
    $ cd Music
    $ where?
    /home/user2

#### Persistência de aliases: scripts de inicialização

Como no caso das variáveis, para que nossos aliases se tornem persistentes devemos colocá-los em scripts de inicialização que são originados quando o sistema é iniciado. Como já sabemos, um bom arquivo para os usuários colocarem seus aliases pessoais é  `~/.bashrc`. Já deve haver alguns aliases por lá (a maioria deles comentados e prontos para uso, bastando remover o  `#`  inicial):

    $ grep alias .bashrc
    # enable color support of ls and also add handy aliases
        alias ls='ls --color=auto'
        #alias dir='dir --color=
        #alias vdir='vdir --color=
        #alias grep='grep --color=
        #alias fgrep='fgrep --color'
        #alias egrep='egrep --color=
    # some more ls aliases
    #ll='ls -al'
    #alias la='ls -A'
    #alias l='ls -CF'
    # ~/.bash_aliases, instead of adding them here directly.
    if [ -f ~/.bash_aliases ]; then
       . ~/.bash_aliases

Como podemos ver nas últimas três linhas, é possível ter nosso próprio arquivo dedicado aos aliases — `~/.bash_aliases` — para o  `.bashrc`  abrir e executar a cada inicialização do sistema. Ao escolher essa opção, criamos e preenchemos esse arquivo:

    ###########
    # .bash_aliases:
    # a file to be populated by the user's personal aliases (and sourced by ~/.bashrc).
    ###########
    alias git_info='which git;git --version'
    alias greet='echo Hello world!'
    alias ll='ls -al'
    alias where?='echo $PWD

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

Como no caso das variáveis e aliases, se quisermos que as funções sejam persistentes durante as reinicializações do sistema, temos de colocá-las em scripts de inicialização do shell, como  `/etc/bash.bashrc`  (global) ou  `~/.bashrc`  (local).

>[!WARNING]
>
>Depois de adicionar aliases ou funções para qualquer arquivo de script de inicialização, é preciso executar  `.`  ou  `source`  nesses arquivos para que as alterações tenham efeito (caso você não queira fazer logout e login novamente ou reinicializar o sistema).

#### Variáveis integradas especiais do Bash

O  _Bourne Again Shell_  vem com um conjunto de variáveis especiais que são particularmente úteis para funções e scripts. Elas são especiais porque só podem ser referenciadas — e não atribuídas. Eis uma lista das mais relevantes:

- `$?`: a referência desta variável se expande para o resultado do último comando executado. Um valor de  `0`  significa sucesso:

    $ ps aux | grep bash
    user2      420  0.0  0.4  21156  5012 pts/0    Ss   17:10   0:00 -bash
    user2      640  0.0  0.0  12784   936 pts/0    S+   18:04   0:00 grep bash
    $ echo $?
    0

Um valor diferente de  `0`  significa erro:

    user1@debian:~$ ps aux |rep bash
    -bash: rep: command not found
    user1@debian:~$ echo $?
    127

- `$$`: expande-se para o PID do shell (ID do processo):

    $ ps aux | grep bash
    user2      420  0.0  0.4  21156  5012 pts/0    Ss   17:10   0:00 -bash
    user2      640  0.0  0.0  12784   936 pts/0    S+   18:04   0:00 grep bash
    $ echo $$
    420

- `$!`: expande-se para o PID do último trabalho em segundo plano:

    $ ps aux | grep bash &
    [1] 663
    $ user2      420  0.0  0.4  21156  5012 pts/0    Ss+  17:10   0:00 -bash
    user2      663  0.0  0.0  12784   972 pts/0    S    18:08   0:00 grep bash
    ^C
    [1]+  Done                   ps aux | grep bash
    $ echo $!
    663

>[NOTE]
>
>Lembre-se, o e comercial (`&`) é usado para iniciar processos em segundo plano.

#### Parâmetros posicionais  `$0`  a  `$9`

Expandem-se para os parâmetros ou argumentos que estão sendo passados para a função (alias ou script) — `$0`  se expande para o nome do script ou shell.

Um _parâmetro posicional_ é um parâmetro denotado por um ou mais dígitos diferentes do dígito único `0`. Por exemplo, a variável `$1` corresponde ao primeiro argumento dado ao script (parâmetro posicional um), `$2` corresponde ao segundo argumento e assim por diante. Se a posição de um parâmetro for maior que nove, ele deve ser referenciado com chaves, como em `${10}`, `${11}` etc.

Vamos criar uma função para demonstrar os parâmetros posicionais — note  `PS2`  (`>`) indicando novas linhas após as quebras de linha:

    $ special_vars() {
    > echo $0
    > echo $1
    > echo $2
    > echo $3
    }

Agora, vamos invocar a função (`special_vars`) passando três parâmetros para ela (`debian`,  `ubuntu`,  `zorin`):

    $ special_vars debian ubuntu zorin
    -bash
    debian
    ubuntu
    zorin

Tudo funcionou como esperado.

>[!WARNING]
>
>Embora seja tecnicamente possível passar parâmetros posicionais para aliases, não é lá muito prático, já que — com aliases — os parâmetros posicionais são sempre passados no final:

    $ alias great_editor='echo $1 is a great text editor'
    $ great_editor emacs
    is a great text editor emacs

#### Outras variáveis integradas especiais do Bash incluem:

- `$#`: expande-se para o número de argumentos passados para o comando.
- `$@`,  `$*`: expandem-se para os argumentos passados para o comando.
-`$_`:  expande-se para o último parâmetro ou o nome do script (dentre outras coisas; consulte  `man bash`  para saber mais!):

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

- `env -i bash`: inicia um bash removendo a maioria das variáveis (além de funções e aliases)

    $ env -i bash

Agora, a maioria das nossas variáveis de ambiente se foi:

    $ echo $USER
    $

Restam apenas algumas:

    $ env
    LS_COLORS=
    PWD=/home/user2
    SHLVL=1
    _=/usr/bin/printenv

`PS1`

Essa variável armazena o valor do prompt do Bash. No trecho de código a seguir (igualmente de  `/etc/profile`), a declaração  `if`  testa a identidade do usuário e lhe atribui um prompt bastante personalizado (  `#`  para  `root`  ou  `$`  para usuários regulares):

    if [ "`id -u`" -eq 0 ]; then
      PS1='# '
    else
      PS1='$ '
    fi

>[!NOTE]
>
>O  `id`  de  `root`  é  `0`. Entre como  `root`  e teste você mesmo com  `id -u`.

Eis algumas outras variáveis de prompt:
- `PS2`: normalmente definido como  `>`  e usado como prompt de continuação para comandos longos de muitas linhas.
- `PS3`: usado como prompt para o comando  `select`.
- `PS4`: normalmente definido como  `+`  e usado para depuração.
- `SHELL`: esta variável armazena o caminho absoluto do shell atual:

    $ echo $SHELL
    /bin/bash

- `USER`: armazena o nome do usuário atual:

    $ echo $USER
    carol

- `echo $DISPLAY` retorna `reptilium:0:2`, que quer dizer: a máquina **reptilium** tem um servidor X rodando e estamos usando a **segunda** tela do sistema

### 105.2 Editar e escrever scripts simples

Ao criar um `script.sh` (script = sequencia de comandos) comum, como por exemplo:

    clear
    echo "===== Primeiro Script======"
    echo ""
    uptime
    free -m
    echo ""
    echo "Fim Script"

Podemos executá-lo usando apenas o comando source ou ponto (.)<br>
Quando um script é executado, os comandos nele contidos não são executados diretamente na sessão atual, mas sim por um novo processo do Bash, chamado _sub-shell_. Isso evita que o script sobrescreva as variáveis de ambiente da sessão atual e faça modificações indesejadas nela. Se o objetivo é executar o conteúdo do script na sessão atual do shell, ele deve ser executado com `source script.sh` ou `. script.sh` (note que há um espaço entre o ponto e o nome do script).

    $ source script.sh
    $ . script.sh

Isso inclusive faz com que seja executado no bash atual, sem mudar para um processo filho.

Se tentar executar usando `./script.sh` ou o caminho absoluto dele, vai dar erro de permissão, pois o arquivo não tem permissão de execução. Precisaria executar `chmod u+x script`, para dar permissão de execução para o arquivo (nesse caso, permissão para o usuário dono do arquivo).

- `exec script.sh`: executa o script e fecha a sessão atual (nem dá pra ver o resultado, pois já fecha em seguida)
- `bash script.sh`: também executa o script sem o bit de execução.

>[!WARNING]
>
>Um script que executa ações restritas pode ter sua permissão SUID ativada e, portanto, os usuários comuns também podem executar o script com privilégios de root. Nesse caso, é muito importante garantir que nenhum usuário além do root tenha permissão para escrever no arquivo. Caso contrário, um usuário comum poderá modificar o arquivo para realizar operações arbitrárias e potencialmente prejudiciais.

Caso queira direcionar algum outro interpretador, usa-se o she-bang, que no caso é indicar no começo do arquivo: `#!/bin/bash`

>[!TIP]
>
>O Bash chama qualquer comando indicado após o `#!` como interpretador do arquivo de script. Pode ser útil, por exemplo, empregar o shebang para outras linguagens de script, como _Python_ (`#!/usr/bin/python`), _Perl_ (`#!/usr/bin/perl`) ou _awk_ (`#!/usr/bin/awk`).

#### Parâmetro em shell script

- `$*`: imprime todos os argumentos passados para o script.
- `$@`: todos os argumentos passados para o script. Se usado com aspas duplas, como em  `"$@"`, todos os argumentos serão colocados entre aspas duplas.
- `$0`: imprime o nome do arquivo que está sendo executado
- `$#`: imprime a quantidade de parâmetros utilizados
- `$1`, `$2`, etc: imprime os parâmetros sequencialmente conforme entrada
- `$!`: PID do último programa executado.
- `$$`: PID do shell atual.
- `$?`: código de status de saída numérico do último comando concluído. Para processos POSIX padrão, um valor numérico de  `0`  indica que o último comando foi executado com sucesso, o que também se aplica a scripts do shell.
```
echo "O meu script se chama $0"
echo ""
echo "Esse script recebeu $# parâmetros, que são, $1 e $2"
```

#### Receber uma variável com read

É possível, ao usuário digitar, receber um valor de variável durante a execução de um script, usando o read:

```
echo -n "Digite um valor: "
read VAR1
echo "O valor digitado foi $VAR1"
```
- `echo -n`: não quebra linha

echo "Do you want to continue (y/n)?"
read ANSWER

O valor retornado será armazenado na variável  `ANSWER`. Se o nome da variável não for fornecido, o nome da variável  `REPLY`  será usado por padrão. Também é possível usar o comando  `read`  para ler mais de uma variável simultaneamente:

    echo "Type your first name and last name:"
    read NAME SURNAME

Neste caso, cada termo separado por espaços será atribuído às variáveis  `NAME`  e  `SURNAME`  respectivamente. Se o número de termos dados for maior que o número de variáveis, os termos excedentes serão armazenados na última variável. O próprio  `read`  pode exibir a mensagem para o usuário com a opção  `-p`, tornando o comando  `echo`  redundante nesse caso:

    read -p "Type your first name and last name:" NAME SURNAME

#### Declarando variáveis

A declaração de variáveis **NÃO** usa cifrão (`$`), já no uso da variável, **TEM QUE USAR**. Exemplo:<br>

    echo ""
    VAR1=`cat /etc/passwd|wc -l`
    VAR2=$(date +%H)
    echo ""
    echo "O arquivo /etc/passwd possui $VAR1 linhas. A hora atual é $VAR2."

*Podemos colocar execução de comandos utilizando crase (\`) ou com a sitaxe `$()`
**A notação de crase é conhecida como **backtick**

##### Comprimento de uma variável
O comprimento de uma variável, ou seja, a quantidade de caracteres que ela contém, é retornado acrescentando-se um hash  `#`  antes do nome da variável. Esse recurso, no entanto, requer o uso da sintaxe das chaves para indicar a variável:

    $ OS=$(uname -o)
    $ echo $OS
    GNU/Linux
    $ echo ${#OS}
    9

##### Arrays (matriz unidimensionais)

O Bash também apresenta variáveis de matriz (array) unidimensionais, de forma que um conjunto de elementos relacionados pode ser armazenado com um único nome de variável. Cada elemento de uma matriz possui um índice numérico, que deve ser usado para escrever e ler valores no elemento correspondente. Ao contrário das variáveis comuns, as matrizes devem ser declaradas com o comando interno do Bash  `declare`. Por exemplo, para declarar uma variável chamada  `SIZES`  como uma matriz:

    $ declare -a SIZES

As matrizes também podem ser declaradas implicitamente quando preenchidas a partir de uma lista predefinida de itens, usando a notação de parênteses:

    $ SIZES=( 1048576 1073741824 )
    $ set | grep SIZES
    SIZES=([0]="1048576"   [1]="1073741824")

No exemplo, os dois grandes valores inteiros foram armazenados na matriz  `SIZES`. Os elementos da matriz devem ser referenciados usando chaves e colchetes, caso contrário o Bash não alterará nem exibirá o elemento corretamente. Como os índices da matriz começam em 0, o conteúdo do primeiro elemento está em  `${SIZES[0]}`, o segundo em  `${SIZES[1]}`  e assim por diante:

    $ echo ${SIZES[0]}
    1048576
    $ echo ${SIZES[1]}
    1073741824

Diferente da leitura, a alteração do conteúdo de um elemento da matriz é realizada sem as chaves (por exemplo,  `SIZES[0]=1048576`). Como no caso das variáveis comuns, o comprimento de um elemento em uma matriz é retornado com o caractere hash (por exemplo,  `${#SIZES[0]}`  para o comprimento do primeiro elemento da matriz  `SIZES`). O número total de elementos em uma matriz é retornado se  `@`  ou  `*`  forem usados como o índice:

    $ echo ${#SIZES[@]}
    2
    $ echo ${#SIZES[*]}
    2

As matrizes também podem ser declaradas usando-se, como elementos iniciais, a saída de um comando, por meio da substituição de comando. O exemplo a seguir mostra como criar uma matriz do Bash cujos elementos são os sistemas de arquivos suportados pelo sistema atual:

    $ FS=( $(cut -f 2 < /proc/filesystems) )

O comando  `cut -f 2 < /proc/filesystems`  exibe todos os sistemas de arquivos atualmente suportados pelo kernel em execução (listados na segunda coluna do arquivo  `/proc/filesystems`), de forma que a matriz  `FS`  agora contém um elemento para cada sistema de arquivos suportado. Qualquer conteúdo de texto pode ser usado para inicializar uma matriz, já que, por padrão, quaisquer termos delimitados por caracteres de  _espaço_,  _tabulação_  ou  _nova linha_  **se tornarão um elemento de matriz**.

>[!TIP]
>
>O Bash trata cada caractere do  `$IFS`  (_Input Field Separator_  ou separador de campos) de uma variável de ambiente como um delimitador. Para alterar o delimitador de campo apenas para caracteres de nova linha, por exemplo, a variável IFS deve ser redefinida com o comando  `IFS=$'\n'`.

#### Instruções condicionais (if e case)

Vejamos um exemplo:

    if [ -f /etc/bash.bashrc ]; then
	    .	/etc/bash.bashrc
	fi
ou

    if test -f /etc/bash.bashrc ; then
	    .	/etc/bash.bashrc
	fi
Ambas as instruções produzem o mesmo efeito. Para que a instrução aninhada no `if` execute, ela deve ser **verdadeira**, no caso o que estiver entre os colchetes, ou após a instrução `test`. <br>Nesse exemplo é testado se o arquivo `/etc/bash.bashrc` existe e é um arquivo regular.

- `-f`: testa se arquivo existe e é um arquivo regular;
- `-e`: testa apenas se arquivo existe;
- `-s`: verifica se o tamanho do arquivo é maior que 0 (zero)
- `-z`: verifica se o tamanho da string é zero (usado com variáveis)
- `-n`: verifica se o tamanho da string é diferente de zero (nonzero)

Vejamos outro exemplo

    if [ "`id -u`" -eq 0]; then
	    PS1='# '
	else
		PS1='$ '
	fi

Nesse caso verificar, por meio do comando `id -u`, se o retorno é igual a 0 (zero), caso seja, retorna `#`, senão `$`. No caso testa se o usuário é root. Lembrando que id 0 é do root. Lembrando também que podemos executar comandos com a crase e por meio do cifrão abrindo e fechando parênteses `$()`

O comando  `test`  avalia as expressões usando duas sintaxes diferentes: as expressões de teste podem ser dadas como um argumento para o comando  `test`  ou podem ser postas entre colchetes, caso em que o comando  `test`  é dado implicitamente. Assim, o teste para avaliar se  `/etc`  é um diretório válido pode ser escrito como  `test -d /etc`  ou como  `[ -d /etc]`:

    $ test -d /etc
    $ echo $?
    0
    $ [ -d /etc ]
    $ echo $?
    0

O comando `test` pode ser executado no terminal diretamente. Vejamos alguns exemplos:

- `test LPI1 = LPI1`: precisamo ver no **return code**. Basta executar `echo $?` (**0** é igual sucesso, **1** ou outro número informa que não é verdadeiro)
- `test LPI1 = LPI2`: return code será **1**
- `test -f /etc/profile`: testa se o arquivo `/etc/profile` existe. (**0** = existe, **1** = não existe)
- `test 10 -gt 20`: se 10 é maior que (**greater than**) 20 (retorna **1**, pois 10 não é maior que 20)

Supõe que a variável $VAR guarde um caminho para um arquivo ou diretório. Podemos ter as seguintes opções:

- `-a "$VAR"`: avalia se o caminho em  `VAR`  existe no sistema de arquivos e é um arquivo.
- `-b "$VAR"`: avalia se o caminho em  `VAR`  é um arquivo de bloco especial.
- `-c "$VAR"`: avalia se o caminho em  `VAR`  é um arquivo de caractere especial.
- `-d "$VAR"`: avalia se o caminho em  `VAR`  é um diretório.
- `-e "$VAR"`: avalia se o caminho em  `VAR`  existe no sistema de arquivos.
- `-f "$VAR"`: avalia se o caminho em  `VAR`  existe e é um arquivo regular.
- `-g "$VAR"`: avalia se o caminho em  `VAR`  tem permissão SGID.
- `-h "$VAR"`: avalia se o caminho em  `VAR`  é um link simbólico.
- `-L "$VAR"`: avalia se o caminho em  `VAR`  é um link simbólico (como  `-h`).
- `-k "$VAR"`: avalia se o caminho em  `VAR`  tem a permissão  _sticky bit_.
- `-p "$VAR"`: avalia se o caminho em  `VAR`  é um arquivo  _pipe_.
- `-r "$VAR"`: avalia se o caminho em  `VAR`  é legível pelo usuário atual.
- `-s "$VAR"`: avalia se o caminho em  `VAR`  existe e não está vazio.
- `-S "$VAR"`: avalia se o caminho em  `VAR`  é um arquivo de socket.
- `-t "$VAR"`: avalia se o caminho em  `VAR`  está aberto em um terminal.
- `-u "$VAR"`: avalia se o caminho em  `VAR`  tem permissão SUID.
- `-w "$VAR"`: avalia se o caminho em  `VAR`  é gravável pelo usuário atual.
- `-x "$VAR"`: avalia se o caminho em  `VAR`  é executável pelo usuário atual.
- `-O "$VAR"`: avalia se o caminho em  `VAR`  é de propriedade do usuário atual.
- `-G "$VAR"`: avalia se o caminho em  `VAR`  pertence ao grupo efetivo do usuário atual.
- `-N "$VAR"`: avalia se o caminho em  `VAR`  foi modificado desde o último acesso.
- `"$VAR1" -nt "$VAR2"`: avalia se o caminho em  `VAR1`  é mais recente que o caminho em  `VAR2`, de acordo com as datas de modificação respectivas.
- `"$VAR1" -ot "$VAR2"`: avalia se o caminho em  `VAR1`  é mais antigo que  `VAR2`.
- `"$VAR1" -ef "$VAR2"`: esta expressão avalia como True (Verdadeiro) se o caminho em  `VAR1`  é um link físico para  `VAR2`.

##### Também existem testes para variáveis de texto arbitrárias, descritos a seguir:

- `-z "$TXT"`: avalia se a variável  `TXT`  está vazia (tamanho zero).
- `-n "$TXT"`  ou  `test "$TXT"`: avalia se a variável  `TXT`  não está vazia.
- `"$TXT1" = "$TXT2"`  ou  `"$TXT1" == "$TXT2"`: avalia se  `TXT1`  e  `TXT2`  são iguais.
- `"$TXT1" != "$TXT2"`: avalia se  `TXT1`  e  `TXT2`  não são iguais.
- `"$TXT1" < "$TXT2"`: avalia se  `TXT1`  vem antes de  `TXT2`, em ordem alfabética.
- `"$TXT1" > "$TXT2"`: avalia se  `TXT1`  vem depois de  `TXT2`, em ordem alfabética.

- `$NUM1 -lt $NUM2`: avalia se  `NUM1`  é menor que  `NUM2`.
- `$NUM1 -gt $NUM2`: avalia se  `NUM1`  é maior que  `NUM2`.
- `$NUM1 -le $NUM2`: avalia se  `NUM1`  é menor ou igual a  `NUM2`.
- `$NUM1 -ge $NUM2`: avalia se  `NUM1`  é maior ou igual a  `NUM2`.
- `$NUM1 -eq $NUM2`: avalia se  `NUM1`  é igual a  `NUM2`.
- `$NUM1 -ne $NUM2`: avalia se  `NUM1`  não é igual a  `NUM2`.

Todos os testes podem receber os seguintes modificadores:

`! EXPR`

Avalia se a expressão  `EXPR`  é falsa.

`EXPR1 -a EXPR2`

Avalia se tanto  `EXPR1`  quanto  `EXPR2`  são verdadeiras.

`EXPR1 -o EXPR2`

Avalia se ao menos uma das duas expressões é verdadeira.



Outro exemplo interessante:<br>

    if [ "$BASH" ] && [ "$BASH" != "/bin/sh" ]; then
	    #executa rotina
	fi
No caso acima, verifica se a variável `$BASH` está declarada **e** se a variável `$BASH` é diferente de `/bin/sh`, então vai para a rotina.

Vejamos um exemplo que verifica se o diretório `/etc/profile.d` existe:<br>

    if [ -d /etc/profile.d ]; then
	    # executa rotina
	fi

Abaixo, outro exemplo um pouco mais complexo:

    if [ -z "${debian_chroot:-}"] && [ -r /etc/debian_chroot ]; then
	    debian_chroot=$(cat /etc/debian_chroot)
	fi
O `-z` significa se o tamanho (length) da string é zero e o `-r` quer dizer se o arquivo existe e se está com permissão de somente leitura.

##### Case
Exemplo1:<br>

    read VAR1
    case $VAR1 in
	    0)
		    echo "O valor digitado foi 0"
	    ;;
	    1|2|3|4|5)
		    echo "O valor digitado foi entre 1 e 5"
		    sleep 3
	    ;;
	    *)
		    echo "O valor digitado foi maior que 5"
	esac

Exemplo 2:<br>

    #!/bin/bash
    
    DISTRO=$1
    
    echo -n "Distribution $DISTRO uses "
    case "$DISTRO" in
    	debian | ubuntu | mint)
        echo -n "the DEB"
      ;;
    	centos | fedora | opensuse )
        echo -n "the RPM"
      ;;
    	*)
        echo -n "an unknown"
      ;;
    esac
    echo " package format."

Cada lista de padrões e comandos associados deve terminar com  `;;`,  `;&`, ou  `;;&`. O último padrão, um asterisco, será usado se não for encontrada uma correspondência para nenhum outro padrão anterior. A instrução  `esac`  (_case_  de trás pra frente) conclui a construção  `case`. Supondo que o script de amostra anterior se chame  `script.sh`  e seja executado com  `opensuse`  como primeiro argumento, a seguinte saída será gerada:

    $ ./script.sh opensuse
    Distribution opensuse uses the RPM package format.
>[!TIP]
>
>
> O Bash tem uma opção chamada `nocasematch` que ativa a correspondência de padrões sem distinção entre maiúsculas e minúsculas para a construção `case` e outros comandos condicionais. O comando interno `shopt` alterna os valores das configurações que controlam comportamentos opcionais do shell: `shopt -s` habilita (_set_) a opção fornecida e `shopt -u` desabilita (_unset_) a opção fornecida. Portanto, colocar `shopt -s nocasematch` antes da construção case permite encontrar padrões sem diferenciar maiúsculas de minúsculas. As opções modificadas por `shopt` afetarão apenas a sessão atual, de forma que as opções modificadas dentro de scripts em execução em um sub-shell — o que é a maneira padrão de executar um script — não afetarão as opções da sessão pai.

#### Loops

- `seq`: faz uma sequencia de acordo com o número que coloque.<br>
Ex.<br>
    $ seq 5
    1
    2
    3
    4
    5

- `expr`: faz conta aritmética<br>
Ex.<br>

    $ expr 1 + 2
    3

    $ expr 1 - 2
    -1

##### for

	read VAR1
    for i in 1 2 3 4 5
    do
	    j=`expr $i + $VAR1`
		echo "$i + $VAR1 = $j
	done

>[!TIP]
>
>O comando `expr` pode ser substituído por `$(())`, de forma que o exemplo anterior no laço `for` poderia ser reescrito como `j=$(( $i + $VAR1 ))`

>[!TIP]
>
>É possível escrever potenciação com o operador duplo asterisco (`**`). Ex.<br>

    $ echo $( (5**2 ) )
    25

Exemplo1:<br>

Usando o `seq`:

    read VAR1
    for i in `seq $1` # $1 = parâmetro 1
    do
	    j=`expr $i + $VAR1`
		echo "$i + $VAR1 = $j
		sleep 1
	done

Exemplo2:<br>

    #!/bin/bash
    
    SEQ=( 1 1 2 3 5 8 13 )
    
    for (( IDX = 0; IDX < ${#SEQ[*]}; IDX++ ))
    do
    	echo -n "${SEQ[$IDX]} is "
    	if [ $(( ${SEQ[$IDX]} % 2 )) -ne 0 ]
    	then
    		echo "odd."
    	else
    		echo "even."
      fi
    done

Este script gera exatamente a mesma saída do exemplo anterior. No entanto, em vez de usar a variável  `NUM`  para armazenar um item por vez, a variável  `IDX`  é empregada para rastrear o índice da matriz atual em ordem crescente, começando de 0 e continuando a adicionar enquanto esse número permanecer abaixo do número de itens na matriz  `SEQ`. O item em si é recuperado de sua posição na matriz com  `${SEQ[$IDX]}`.



##### while

    while [ $VAR1 -le $1 ] #enquanto o teste = Verdadeiro, faça...
    do
	    echo "O valor atual do \$VAR1 é: $VAR1"
	    VAR1=`expr $VAR1 + 1`
	    sleep 1
	done

##### until

    until [ $VAR1 = 0 ] #até que isso seja verdadeiro, faça...chegando no 0, não executa o looping
    do
	    echo "O valor atual do \$VAR1 é: $VAR1"
	    VAR1=`expr $VAR1 - 1`
	    sleep 1
	done

#### Execução sequencial de comandos

Ex. `cat teste | wc -l`: executa o primeiro comando e a saída do mesmo serve de entrada para o próximo comando, no caso `wc`.

Ex. `date ; ls -l ; teste2 ; echo Exemplo`: executa todos, mesmo que dê erro

Ex. `ls -ls teste && echo Exemplo`: só executa o segundo comando, se o primeiro der sucesso (caso exista o arquivo teste, senão seria erro, caso ele não exista - ou seja, o status de saída igual a `0`)

Ex. `ls -ls teste || echo Exemplo`: só executa o segundo comando, se o primeiro falhar. Executando o primeiro já encerra a execução.

Ex. `ls -ls teste || echo Exemplo || date`: é sequencial, na hora que chegar em um que executou com sucesso, ele para.

Exemplo de uso de `||`
Imagine uma checagem de um arquivo muito importante, caso ele desaparece, tenha qualquer problema, mudança de permissão, etc, avise o usuário administrador ou root sobre isso:

    ls -l arquivo_importante || mail -s "arquivo não existe mais" root < .
Se executar o comando acima e der sucesso, não faz nada. Mas se der erro, executa o segundo comando, no caso, enviar um email avisando que o arquivo não existe mais.

###$$ Saída do script (echo e printf)

Mesmo quando a finalidade de um script envolve apenas operações orientadas a arquivos, é importante exibir mensagens relacionadas ao progresso na saída padrão, para que o usuário seja informado sobre quaisquer problemas e possa, eventualmente, usar essas mensagens para gerar logs de operação.

O comando interno do Bash  `echo`  é comumente usado para exibir strings de texto simples, mas ele também oferece alguns recursos estendidos. Com a opção  `-e`, o comando  `echo`  é capaz de exibir caracteres especiais usando sequências de escape (uma sequência de barra invertida designando um caractere especial). Por exemplo:

    #!/bin/bash
    
    # Get the operating system's generic name
    OS=$(uname -o)
    
    # Get the amount of free memory in bytes
    FREE=$(( 1000 * `sed -nre '2s/[^[:digit:]]//gp' < /proc/meminfo` ))
    
    echo -e "Operating system:\t$OS"
    echo -e "Unallocated RAM:\t$(( $FREE / 1024**2 )) MB"

Embora o uso de aspas seja opcional ao se usar  `echo`  sem opções, é necessário adicioná-las ao incluir a opção  `-e`; caso contrário, os caracteres especiais podem não ser lidos corretamente. No script anterior, ambos os comandos  `echo`  usam o caractere de tabulação  `\t`  para alinhar o texto, resultando na seguinte saída:

    Operating system:       GNU/Linux
    Unallocated RAM:        1491 MB

O caractere de nova linha  `\n`  pode ser usado para separar as linhas da saída, de forma que exatamente a mesma saída é obtida combinando-se os dois comandos  `echo`  em um só:

    echo -e "Operating system:\t$OS\nUnallocated RAM:\t$(( $FREE / 1024**2 )) MB"

Embora adequado para exibir a maioria das mensagens de texto, o comando  `echo`  pode não ser o melhor para padrões de texto mais específicos. O comando interno do Bash  `printf`  oferece mais controle sobre a exibição das variáveis. O comando  `printf`  usa o primeiro argumento como formato da saída, onde os marcadores serão substituídos pelos argumentos seguintes na ordem em que aparecem na linha de comando. Assim, a mensagem do exemplo anterior poderia ser gerada com o seguinte comando  `printf`:

    printf "Operating system:\t%s\nUnallocated RAM:\t%d MB\n" $OS $(( $FREE / 1024**2 ))

O espaço reservado  `%s`  destina-se ao conteúdo de texto (será substituído pela variável  `$OS`) e o espaço reservado  `%d`  destina-se a números inteiros (será substituído pelo número resultante de megabytes livres na RAM). O  `printf`  não acrescenta um caractere de nova linha no final do texto, então o caractere de nova linha  `\n`  deve ser posto ao fim do padrão, se necessário. Todo o padrão deve ser interpretado como um único argumento e, portanto, deve ser posto entre aspas.

>[!TIP]
>
> O formato de substituição do espaço reservado realizada por  `printf`  pode ser personalizado com o mesmo formato usado pela função  `printf`  da linguagem de programação C. A referência completa para a função  `printf`  pode ser encontrada em sua página de manual, acessada com o comando  `man 3 printf`.

Com  `printf`, as variáveis são postas fora do padrão de texto, o que torna possível armazenar o padrão de texto em uma variável separada:

    MSG='Operating system:\t%s\nUnallocated RAM:\t%d MB\n'
    printf "$MSG" $OS $(( $FREE / 1024**2 ))

Este método é particularmente útil para exibir formatos de saída distintos, dependendo dos requisitos do usuário. Fica mais fácil, por exemplo, produzir um script que use um padrão de texto distinto se o usuário precisar de uma lista CSV (valores separados por vírgula) em vez de uma mensagem de saída padrão.
cho $TESTE`: lê o valor da variável TESTE
- `TESTE=valor1`: define um valor para a variável TESTE
- `env | grep TESTE`: não encontra nada da variável TESTE pois ela não foi exportada, está localmente apenas
- `set | grep TESTE`: mostra tanto as locais de ambiente, como as exportadas de usuário.

Se tivermos um script que lê essa variável TESTE, ao executá-lo ele não irá ler, pois quando é executado um script ele cria abre em uma nova sessão do shell/bash (processo filho do shell atual), logo essa variável teria que estar exportada.

É possível contornar isso com o comando **source**. Ele faz com que seja executado no mesmo shell da sessão atual, dessa forma retornando o valor de TESTE.

Outra forma é utilizar o ponto (.): `. script.sh` (faz rodar/executar localmente o script, sem chamar outra sessão de bash) (**NÃO** confundir com `./script.sh`)

Comando alias: cria alias para os comandos (sinônimos de comandos)

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

### 106.1 Instalar e configurar o X11

Em cima do X ou X11, que roda os gerenciadores de janela, como gnome, kde, xfce, etc.
Display Manager: aquela tela de login inicial.<br>
O servidor X funciona como um modelo cliente-servidor. Os apps que são executados são clientes do servidor X, que provê as disponibilidades gráficas. Assim como a calculadora, que também é um cliente do servidor X.

Normalmente as configurações ficam em `/etc/X11/xorg.conf` (por padrão já nem vem mais com as distros, tendo em vista que tudo já vem muito bem configurado.). No entanto, para explorar, teria que criar o arquivo `xorg.conf` manualmente.

Trecho LPI: [Tradicionalmente, o principal arquivo de configuração usado para configurar um servidor X é o arquivo `/etc/X11/xorg.conf`. Nas distribuições Linux modernas, o servidor X configura a si mesmo em tempo de execução quando é iniciado e, portanto, nenhum arquivo `xorg.conf` pode existir.]

O arquivo  `xorg.conf`  é dividido em estrofes separadas chamadas  _seções_. Cada seção começa com o termo  `Section`  e, após este termo está o  _nome da seção_, que se refere à configuração de um componente. Cada  `Section`  é encerrada por uma  `EndSection`  correspondente. Um arquivo  `xorg.conf`  típico contém as seguintes seções:

- `InputDevice`: usada para configurar um modelo específico de teclado ou mouse.
- `InputClass`: InputClass Nas distribuições Linux modernas, esta seção é tipicamente encontrada em um arquivo de configuração à parte, localizado em  `/etc/X11/xorg.conf.d/`.  `InputClass`  é usada para configurar uma classe de dispositivos de hardware como teclados e mouses, e não um componente específico de hardware. Veja abaixo um exemplo de arquivo  `/etc/X11/xorg.conf.d/00- keyboard.conf`:

    Section "InputClass"
            Identifier "system-keyboard"
            MatchIsKeyboard "on"
            Option "XkbLayout" "us"
            Option "XkbModel" "pc105"
    EndSection

A opção de  `XkbLayout`  determina a disposição das teclas de um teclado, como Dvorak, canhoto ou destro, QWERTY e idioma. A opção de  `XkbModel`  é usada para definir o tipo de teclado utilizado. Há uma tabela de modelos, layouts e suas descrições em  `xkeyboard-config(7)`. Os arquivos associados aos layouts de teclado podem ser encontrados em  `/usr/share/X11/xkb`. Um layout de teclado grego politônico em um computador Chromebook apareceria desta maneira:

    Section "InputClass"
            Identifier "system-keyboard"
            MatchIsKeyboard "on"
            Option "XkbLayout" "gr(polytonic)"
            Option "XkbModel" "chromebook"
    EndSection

Alternativamente, o layout de um teclado pode ser modificado durante uma sessão X em execução com o comando  `setxkbmap`. Eis um exemplo desse comando para configurar o layout grego politônico em um computador Chromebook:

    $ setxkbmap -model chromebook -layout "gr(polytonic)"

Essa configuração só permanecerá ativa enquanto a sessão X estiver em uso. Para que essas alterações se tornem permanentes, modifique o arquivo  `/etc/X11/xorg.conf.d/00-keyboard.conf`  de forma a incluir as configurações necessárias.

>[!NOTE]
>
>O comando  `setxkbmap`  utiliza a X Keyboard Extension (XKB). Este é um exemplo da funcionalidade aditiva do X Window System por meio do uso de extensões.

As distribuições Linux modernas fornecem o comando  `localectl`  através do  `systemd`, que também pode ser usado para modificar um layout de teclado e cria automaticamente o arquivo de configuração  `/etc/X11/xorg.conf.d/00-keyboard.conf`. Novamente, eis um exemplo de configuração de um teclado grego politônico em um Chromebook, desta vez com o comando  `localectl`:

    $ localectl --no-convert set-x11-keymap "gr(polytonic)" chromebook

A opção  `--no-convert`  é usada aqui para impedir que o  `localectl`  modifique o mapa do teclado no console do hospedeiro.

- `Monitor`: a seção  `Monitor`  descreve o monitor físico utilizado e onde está conectado. Eis um exemplo de configuração que mostra um monitor de hardware conectado à segunda porta de vídeo e usado como monitor principal.

    Section "Monitor"
            Identifier  "DP2"
            Option      "Primary" "true"
    EndSection

- `Device`: a seção  `Device`  descreve a placa de vídeo física utilizada. A seção também contém o módulo do kernel usado como driver para a placa de vídeo, junto com sua localização física na placa-mãe.

    Section "Device"
            Identifier  "Device0"
            Driver      "i915"
            BusID       "PCI:0:2:0"
    EndSection

- `Screen`: a seção  `Screen`  reúne as seções  `Monitor`  e  `Device`. Um exemplo de seção  `Screen`  seria semelhante ao seguinte:

    Section "Screen"
            Identifier "Screen0"
            Device     "Device0"
            Monitor    "DP2"
    EndSection

-`ServerLayout`: a seção  `ServerLayout`  agrupa todas as seções como mouse, teclado e telas em uma única interface do X Window System.

    Section "ServerLayout"
    	Identifier   "Layout-1"
    	Screen       "Screen0" 0 0
    	InputDevice  "mouse1"  "CorePointer"
    	InputDevice  "system-keyboard"  "CoreKeyboard"
    EndSection

>[!NOTE]
>
>Nem todas as seções estão presentes em um arquivo de configuração. Nos casos em que uma seção está ausente, os valores padrão são fornecidos pela instância do servidor X em execução.

Os arquivos de configuração específicos ao usuário também residem em `/etc/X11/xorg.conf.d/`. Os arquivos de configuração fornecidos pela distribuição localizam-se em `/usr/share/X11/xorg.conf.d/`. Os arquivos de configuração localizados em `/etc/X11/xorg.conf.d/` são analisados antes do arquivo `/etc/X11/xorg.conf` se ele existir no sistema.

O comando  `xdpyinfo`  é usado em um computador para exibir informações sobre uma instância do servidor X em execução. Veja abaixo um exemplo de saída do comando (determinar quais extensões Xorg estão disponíveis em um sistema):

    $ xdpyinfo
    name of display:    :0
    version number:    11.0
    vendor string:    The X.Org Foundation
    vendor release number:    12004000
    X.Org version: 1.20.4
    maximum request size:  16777212 bytes
    motion buffer size:  256
    bitmap unit, bit order, padding:    32, LSBFirst, 32
    image byte order:    LSBFirst
    number of supported pixmap formats:    7
    supported pixmap formats:
        depth 1, bits_per_pixel 1, scanline_pad 32
        depth 4, bits_per_pixel 8, scanline_pad 32
        depth 8, bits_per_pixel 8, scanline_pad 32
        depth 15, bits_per_pixel 16, scanline_pad 32
        depth 16, bits_per_pixel 16, scanline_pad 32
        depth 24, bits_per_pixel 32, scanline_pad 32
        depth 32, bits_per_pixel 32, scanline_pad 32
    keycode range:    minimum 8, maximum 255
    focus:  None
    **number of extensions:    25
        BIG-REQUESTS
        Composite
        DAMAGE
        DOUBLE-BUFFER
        DRI3
        GLX
        Generic Event Extension
        MIT-SCREEN-SAVER
        MIT-SHM
        Present
        RANDR
        RECORD
        RENDER
        SECURITY
        SHAPE
        SYNC
        X-Resource
        XC-MISC
        XFIXES
        XFree86-VidModeExtension
        XINERAMA
        XInputExtension
        XKEYBOARD
        XTEST
        XVideo
    default screen number:    0
    number of screens:    1
    
    screen #0:
      dimensions:    3840x1080 pixels (1016x286 millimeters)
      resolution:    96x96 dots per inch
      depths (7):    24, 1, 4, 8, 15, 16, 32**
      root window id:    0x39e
      depth of root window:    24 planes
      number of colormaps:    minimum 1, maximum 1
      default colormap:    0x25
      default number of colormap cells:    256
      preallocated pixels:    black 0, white 16777215
      options:    backing-store WHEN MAPPED, save-unders NO
      largest cursor:    3840x1080
      current input event mask:    0xda0033
        KeyPressMask             KeyReleaseMask           EnterWindowMask
        LeaveWindowMask          StructureNotifyMask      SubstructureNotifyMask
        SubstructureRedirectMask PropertyChangeMask       ColormapChangeMask
      number of visuals:    270
    ...

As partes mais relevantes da saída estão em negrito, como o nome da tela (que é idêntico ao conteúdo da variável de ambiente  `DISPLAY`), as informações de versão do servidor X em uso, o número e a listagem das extensões do Xorg em uso e mais informações sobre a tela em si.

Com `ps axu | grep X`, podemos ver o processo `/usr/lib/xorg/Xorg`, que roda no terminal **tty7**. E para gerar o `xorg.conf`, é necessário parar esse processo, logo terá que mudar para o **tty1** (`Ctrl+Alt+F1`), acessar como root e parar o processo **Xorg**.

É necessário mudar para o modo de multiusuários, mas sem interface gráfica, por meio do comando: `# systemctl isolate multi-user.target`. Confirme com `ps axu | grep X`, e verá que o servidor X não está mais rodando.

Agora resta executar o comando `Xorg -configure` (ubuntu 16.04) que irá gerar um arquivo `xorg.conf` em `/root`. Copie esse arquivo para `/etc/x11/xorg.conf`. Rode `startx` para subir o servidor X (mas sem a tela de login), para sair basta fazer um logout. Volte para a interface gráfica padrão executando o comando `systemctl default`. O `xorg.conf` é separado por seções, com identificadores e opções, para mouse, teclado, telas, fontes (que podem ser remotas inclusive), etc.

Vejamos algumas:

- **Module**: carregamento dinâmico de módulos.

Exemplo:  

    _Section "Module"  
    Load "glx"  
    Load "dbe"  
    Load "extmod"  
    EndSection  

_ **Files**: caminhos para alguns arquivos e diretórios utilizados pelo servidor X, como módulos mas principalmente as  **fontes**.

Exemplos:

    _Section "Files"  
    ModulePath "/usr/lib/xorg/modules"  
    FontPath "/usr/share/fonts/X11/misc"  
    FontPath "/usr/share/fonts/X11/Type1"  
    FontPath "/usr/share/fonts/X11/100dpi"  
    FontPath "/usr/share/fonts/X11/75dpi"  
    FontPath "built-ins"  
    FontPath "unix:/7100"  
    FontPath "tcp/fonts.server.com:7100"  
    EndSection_
  
- **InputDevice**: contêm configurações referentes aos dispositivos de entrada, principalmente  **mouse** e  **teclado**.  _Identifier_ e  _Driver_  são parâmetros obrigatórios utilizados para especificar o dispositivo. Além disso parâmetros  _Option_ podem ser adicionados para implementar configurações específicas

Exemplo:

    _Section "InputDevice"  
    Identifier "Keyboard0"  
    Driver "kbd"  
    Option "XkbModel" "pc105"  
    Option "XkbLayout" "us"  
    Option "AutoRepeat" "500 200"  
    EndSection_

      _Section "InputDevice"  
    Identifier "Mouse0"  
    Driver "mouse"  
    Option "Protocol" "auto"  
    Option "Device" "/dev/input/mice"  
    Option "Emulate3Buttons" "no"  
    Option "ZAxisMapping" "4 5"  
    EndSection_

  
- **Device**: seção utilizada principalmente para configuração da  **placa de vídeo**. Semelhante ao InputDevice, tem os parâmetros  _Identifier_ e  _Driver_ como obrigatórios.

Exemplo:

    _Section "Device"  
    Identifier "VideoCard0"  
    Driver "nv"  
    VendorName "nVidia"  
    BoardName "GeForce 6100"  
    VideoRam 131072  
    EndSection_

  
- **Monitor**: configurações específicas do monitor utilizado, como  _HorizSync_ e  _VertRefresh_.

Exemplo:

    _Section "Monitor"  
    Identifier "Monitor0"  
    VendorName "Monitor Vendor"  
    ModelName "Monitor Model"  
    HorizSync 30.0 - 83.0  
    VertRefresh 55.0 - 75.0  
    EndSection_

  
- **Screen**: a seção screen é uma combinação entre o monitor e a placa de vídeo, dizendo ao X quais os modos que ele pode trabalhar. Na sub-seção  **Display**, são informados por exemplo as  **resoluções** suportadas,  **color depth** (bits por pixel), e etc.

    _Section "Screen"  
    Identifier "Screen0"  
    Device "Card0"  
    Monitor "Monitor0"  
    SubSection "Display"  
    Viewport 0 0  
    Depth 1  
    EndSubSection  
    SubSection "Display"  
    Viewport 0 0  
    Depth 4  
    EndSubSection  
    SubSection "Display"  
    Depth 24  
    Modes "1920x1080" "1280x1024" "1024x768"  
    EndSubSection  
    SubSection "Display"  
    Depth 8  
    Modes "1024x768" "800x600" "640x480"  
    EndSubSection  
    EndSection_

  
- **ServerLayout**: esta seção agrega as outras definições da configuração do X, associando principalmente as informações do Screen e InputDevices.

Exemplo:

    _Section "ServerLayout"  
    Identifier "X.org Configured"  
    Screen 0 "Screen0" 0 0  
    InputDevice "Mouse0" "CorePointer"  
    InputDevice "Keyboard0" "CoreKeyboard"  
    EndSection_

#### Uso da variável $DISPLAY

$Display mostra, normalmente, a seguinte informação:<br>

    :0.0

    hostname:displaynumber.screennumber

O nome de exibição também informa a um aplicativo gráfico onde ele deve ser renderizado e em qual hospedeiro (no caso de uma conexão X remota).

O  `hostname`  refere-se ao nome do sistema que exibirá o aplicativo. Se o nome de exibição não contiver o nome do hospedeiro, o host local será pressuposto.

O  `displaynumber`  faz referência à coleção de “telas” que estão em uso, seja uma única tela de laptop ou diversas telas em uma estação de trabalho. Cada sessão do servidor X em execução recebe um número de exibição começando em  `0`.

O  `screennumber`  padrão é  `0`. Esse pode ser o caso se apenas uma tela física ou diversas telas físicas estiverem configuradas para funcionar como uma só tela. Quando todas as telas de uma configuração de múltiplos monitores são combinadas em uma única tela lógica, as janelas do aplicativo podem ser movidas livremente entre as telas. Em situações em que cada tela é configurada para funcionar independentemente uma da outra, cada tela abrigará as janelas dos aplicativos que forem abertos dentro delas e as janelas não podem ser movidas de uma tela para outra. A cada tela independente será atribuído seu próprio número. Se houver apenas uma tela lógica em uso, o ponto e o número da tela serão omitidos.

Para iniciar um aplicativo em uma tela específica, atribua o número da tela à variável de ambiente  `DISPLAY`  antes de iniciar o aplicativo: $

    $ DISPLAY=:0.1 firefox &

Esse comando iniciaria o navegador Firefox na tela à direita do diagrama acima. Alguns kits de ferramentas também oferecem opções de linha de comando para instruir um aplicativo a ser executado em uma tela especificada. Procure por  `--screen`  end  `--display`  na página do manual de  `gtk-options(7)`  para ver um exemplo..

O nome de exibição de uma sessão X em execução é armazenado na variável de ambiente  `DISPLAY`:

    $ echo $DISPLAY
    :0

A saída detalha o seguinte:

1.  O servidor X em uso está no sistema local, portanto não há nada impresso à esquerda dos dois pontos.
    
2.  A sessão atual do servidor X é a primeira indicada por  `0`  imediatamente após os dois pontos.
    
3.  Há apenas uma tela lógica em uso, portanto um número de tela não é visível.

Quando não há nada antes dos dois pontos (`:`), considera-se que o valor é `localhost`. Para executar algum app em outro computador, precisa-se mudar o valor da variável de ambiente DISPLAY:<br>

    export DISPLAY="192.168.0.100:0.0"

No host que irá receber a abertura do programa escolhido, é preciso liberá-lo para essa conexão. Por meio do comando `xhost` é possível verificar quem que está autorizado a permitir essa conexão. Para liberar o acesso, digite o comando:<br>

    xhost +192.168.0.99

Digite `xhost` novamente para confirmar que realmente foi liberado. Para liberar completamente o controle de acesso, digite `xhost +` (comando para restringir novamente é: `xhost -`)

Ainda assim não será possível realizar a tarefa, pois o **Display Manager** está bloqueando. No caso, seria o **lightdm**. Se olhar no processo Xorg (`ps axu | grep X`), é possível notar que **lightdm** está rodando com a opção `-nolisten tcp`, e é necessário mudar essa opção (isso para distros baseadas no Debian/Ubuntu).

Vá até `/usr/share/lightdm/lightdm.conf.d`, e edite o arquivo `50-xserver-command.conf`e adicione `xserver-allow-tcp=true` no final do arquivo. Reinicie o `lightdm: systemctl restart lightdm`. Se olhar no processo Xorg **NÃO** terá mais o `-nolisten tcp`

Agora do computador que irá executar o comando para abertura de algum app, execute **xcalc** ou qualquer outro aplicativo para abrir no outro computador.<br>
*O processamento todo está sendo feito pelo PC que abriu a aplicação, no caso o PC de origem. O de destino apenas está gerando a parte gráfica.

#### X11 e o Desktop Environment

Desktop Environment: é um **conjunto de aplicações**, com a finalidade de prover uma interface gráfica amigável ao usuário.<br>
Exemplos: KDE, GNOME, MATE, Cinnamon, LXDE, Xfce

##### Gerenciador de Janelas (Window Manager)

- Controla a criação e fechamento das janelas no ambiente, além do posicionamento e aparência
- São clientes X
- Podem ser parte integrada de um DE ou independentes
- Exemplos: mutter (GNOME), KWin (DKE), Muffin (Cinnamon), Xfwm (XFCE), enligntenment, window maker, Openbox, etc
*É requisito ter um **Window Manager** dentro do **Desktop Environment**

- `update-alternatives --display x-window-manager`: verifica qual é o gerenciador de janelas, ou com o comando:
- `ls -l /etc/alternatives/ | grep window`

##### Interface Gráfica (GUI)

Quando falamos qual interface gráfica você usa? E respondemos KDE, GNOME, etc. Na realidade tecnicamente estamos falando de **Desktop Environment (DE)**.

Já a Interface Gráfica em que o usuário interage, que é o que é usado pelo usuário, é chamado de, por exemplo, do KDE é o KDE Plasma. Do GNOME, é o GNOME Shell, etc

Portanto, a interface gráfica é um dos muitos componentes dentro do DE.

Se estiver usando o GNOME, faça o teste:

- `ps axu | grep gnome`: lista diversas aplicações, como gnome-settings, gnome-software, etc. Essas são aplicações que fazem parte do DE. Uma delas vai ser o `gnome-shell`, que no caso é a interface gráfica.
 
##### Display Managers (DM)

- Interface para realização do login
- Exemplos: GDM (GNOME), LightDM (XFCE), XDM, LXDM
*Geralmente termina em DM (Display Manager)

##### Wayland

É uma alternativa ao X11, criado pelo xorg.

- Protocolo que especifica a comunicação entre o servidor gráfico (Compositor Wayland) e os clientes
- Criado com a intenção de substituir o X, com uma arquitetura diferente visado melhor performance geral
- Atualmente utilizando por padrão no Fedora com GNOME, mas é uma opção em todos os DE
- Projeto em Desenvolvimento

#### Comandos e Arquivos

Em `/usr/share/X11/xorg.conf.d` ficam alguns arquivos de configuração de aplicativos ligados ao hardware, como GPUs, com padrão Section e EndSection

Todos os usuário terão em seu diretório padrão o arquivo `.xsession-errors`, que é um arquivo de log para guardar os erros que ocorrem relacionados à interface gráfica

O acesso remoto que foi feito usando **xhost**, pode ser feito usando o `xauth list`, pegando o magic cookie do computador que quer executar os aplicativos, e adicionando esse magic coockie no computador que irá realizar o acesso, usando o comando:

- `xauth add 192.168.0.100 MIT-MAGIC-COOKIE-1 hash_gerada`

### 106.2 Desktops gráficos

Desktops Environments: GNOME, KDE, MATE, Cinnamon, XFCE, LXDE

- **GNOME**: forte associação ao Projeto GNU, usa biblioteca gráfica **GTK**, Window Manager: mutter.
- **KDE**: pode ser usado em Linux, FreeBSD, Solaris, Windows e Mac OS X, usa biblioteca gráfica **Qt**, Window Manager: KWin
- **MATE**: derivado do GNOME 2, utiliza biblioteca GTK, Window Manager: marco
- **Xfce**: pretende ser rápido e leve, utiliza biblioteca GTK, Window Manager: xfwm

*O **LXDE** é um ambiente de desktop adaptado para baixo consumo de recursos, sendo assim uma boa escolha para instalação em equipamentos mais antigos ou computadores de placa única. Embora não ofereça todas as capabilidades dos ambientes de trabalho mais pesados, o LXDE inclui todos os recursos básicos esperados de uma interface gráfica de usuário moderna

#### Protocolos para acesso remoto a Desktops

- **XDMCP** (X Display Manager Control Protocol)
	- Protocolo nativo do X
	- Implementado pelo Display Manager
	- Não implementa segurança/criptografia
	- Não faz compressão (ocupa bastante banda  )

Para conexão remota utilizando XDMCP, configure o arquivo `/etc/lightdm/lightdm.conf` de maneira que habilite o XDMCPServer:<br>

    [XDMCPServer]
    enable=true

Em outro terminal, execute: `Xorg -terminate -query 192.168.0.100 :1`

- **VNC** (Virtual Networking Computing)
	- utiliza o protocolo RFB (Remote Framebuffer Protocol)
	- Não é totalmente seguro, mas as senhas são criptografadas
	- É possível executar muitos servidores VNC na mesma máquina, mas cada servidor VNC precisa de uma porta TCP exclusiva na interface de rede que aceite solicitações de sessão de entrada. Por convenção, o primeiro servidor VNC deve usar a porta TCP 5900, o segundo deve usar 5901 e assim por diante

- **SPICE** (Simple Protocol for Independent Computing Environment)
	- solução open source completa para acesso remoto
	- solução mais segura

- **RDP** (Remote Desktop Protocol): é usado sobretudo para acessar remotamente a área de trabalho de um sistema operacional _Microsoft Windows_ por meio da porta de rede TCP 3389
	- utilizado para o Microsoft Terminal Service

### 106.3 Acessibilidade

Acessibilidade também é chamada de **AccessX**. Pode ser alterado com a linha de comando: `xkbset`

Locais para acesso: O módulo de configurações de acessibilidade é chamado de _Acesso universal_ na área de trabalho do Gnome, enquanto que no KDE ele está em _Configurações do sistema_, _Personalização_, _Acessibilidade_. Outros ambientes de desktop, como o _Xfce_, também o chamam de _Acessibilidade_ em seu gerenciador de configurações gráficas. Porém, de maneira geral eles oferecem um conjunto reduzido de funcionalidades em comparação com o Gnome e o KDE.

- `sticky keys` (teclas de aderência): é para quem não consegue apertar mais de uma tecla ao mesmo tempo (Ex. shift + A ou S, etc). Se habilitar, basta apertar a tecla do atalho e a outra sequencial (sem necessidade de segurar) - (O KDE também oferece a opção de _Teclas de bloqueio_: se habilitada, as teclas Alt, Ctrl e Shift permanecerão “apertadas” se o usuário as pressionar duas vezes, semelhante ao comportamento da tecla Caps lock) - (o recurso de teclas de aderência será ativado pressionando-se a tecla Shift cinco vezes consecutivas. Para ativar o recurso de teclas lentas, a tecla Shift deve ser mantida pressionada por oito segundos consecutivos)
- `slow keys`(teclas lentas): usado por quem tem problema motor, as teclas lentas exigem que o usuário mantenha a tecla pressionada por um período de tempo especificado antes de ela ser aceita
- `bouce keys`(teclas de repercussão): serve para inibir pressionamentos de tecla não intencionais adicionando um tempo de latência entre eles (se precisar digitar a mesma tecla, precisa aguardar o delay)
- `mouse emulation`: para usar o mouse com o teclado numérico da direita. (Ex. 8=cima, 2=baixo, 6=direita e 4=esquerda, 5=pressionar - clique esquerdo)
- `Preferências do mouse  na janela de configuração do sistema`: se o usuário não conseguir pressionar um ou mais botões do mouse, os cliques podem ser simulados usando diferentes técnicas. Na seção  _Assistência de clique_  do  _Acesso Universal_  do Gnome, a opção  _Clique secundário simulado_  simula um clique com o botão direito se o usuário pressionar e segurar o botão esquerdo do mouse. Com a opção  _Clique flutuante_  habilitada, um evento de clique será disparado quando o usuário segurar o mouse sem movê-lo. No KDE, o aplicativo  _KMouseTool_  fornece esses mesmos recursos para facilitar as ações com o mouse.
- `GOK (Gnome On-board Keyboard)`: teclado virtual On-board
- `KMag`: screen magnifier (ampliador de tela - LUPA)
- `Orca e emacspeak`: leitor de tela, sendo o Orca o principal
- `BRLTTY`: app do Linux para entender e usar o [Braille Display](https://www.google.com/search?q=braille+display&oq=brai&gs_lcrp=EgZjaHJvbWUqBggAEEUYOzIGCAAQRRg7MgYIARBFGDkyBggCEEUYPDIGCAMQRRg8MgYIBBBFGEEyBggFEEUYQTIGCAYQRRhBMgYIBxAuGEDSAQgzODQ5ajBqMagCALACAA&sourceid=chrome&ie=UTF-8)]

#### Reconhecimento de voz

Software capaz de executar comandos através do reconhecimento de voz. Diversos projetos Open Source em desenvolvimento (além dos principais para dispositivos móveis, como Siri, Cortana, etc.
- CMUSphinx
- Simon (KDE)
- Julius

Pergunta interessante na [lpi.org](https://learning.lpi.org/pt/learning-materials/102-500/106/106.3/106.3_01/) citando o Orca<br>
De que forma o aplicativo  _Orca_  pode ajudar usuários com deficiência visual a interagir com o ambiente de trabalho?
O Orca é um leitor de tela que gera uma voz sintetizada que descreve os eventos na tela e lê o texto sob o cursor do mouse. Ele também funciona com dispositivos chamados de  _visores braille atualizáveis_, permitindo ao usuário identificar o texto com padrões táteis.

### 107.1 Administrar contas de usuário, grupos e arquivos de sistema relacionados

Cada usuário tem, obrigatoriamente, seu grupo padrão. E pode estar em vários grupos, ou seja, o mesmo usuário pode estar em vários grupos, facilitando para o administrador de sistemas.

- `/etc/passwd`: principal arquivo que contém todos os usuários do sistema
	- `id` e `gid`: id e id do grupo padrão do usuário (**root SEMPRE será id igual a 0 zero, assim como seu gid**)

Se observar há diversos usuários de sistema, como por exemplo o **daemon**. No Linux, para poder criar um processo ou gerar um arquivo, é preciso ter um usuário, no mínimo. Por isso a necessidade de aplicações terem seu usuário

Normalmente (varia de distro para distro), ids a partir de 1000 são os de usuário, abaixo de 1000 são os de sistema. Lembrando que isso é mais comum, mas pode variar, pois é totalmente configurável.

Por convenção, em `/etc/passwd`, é recomendado sempre utilizar caracteres minúsculos, pois haveria distinção de um usuário chamado rodrigo e Rodrigo, pois é case sensitive. Portanto, é recomendado seguir o padrão.

#### Entendendo cada campo do /etc/passwd
Ordem dos campos:<br>
1. nome do usuário
2. senha do usuário: que armazena em `/etc/shadow`. Antigamente armazenava aqui mesmo
3. id do usuário
4. grupo padrão do usuário: que por padrão usa o mesmo nome do usuário, mas também é configurável e possível alterar
5. descrição: normalmente o nome do usuário, mas isso não é regra.
6. home do usuário: local onde usuário poderá gravar sem restrições, e onde também o usuário cai ao fazer login
7. shell padrão do usuário, que é aberto após login. Quando tem `/bin/false`, significa que **NÃO** é um usuário que irá se logar. Assim como quando tiver `/usr/sbin/nologin`.<br>
*Internamente o Linux sempre trabalha com o `id`.<br>
**Pode-se ter dois usuários com o mesmo id, no entanto o Linux tratará os dois como se fosse um só.<br>
***Pode-se alterar o arquivo `/etc/passwd` diretamente, mas não é recomendado, pois há comandos para isso (e pense que o `/etc/passwd` é ligado com o `/etc/shadow`, e mudar uma coisa em um, geraria inconsistência no outro). Ex. Poderia alterar o id de qualquer usuário para o `id = 0`, e esse usuário se comportaria como root. No entanto não é recomendado.

#### Entendendo cada campo do /etc/shadow

Contém as senhas dos usuário<br>
Ordem dos campos:<br>
1. usuário propriamente dito
2. hash da senha (**quando não** tem senha definida, fica um ponto de exclamação - `!`)

#### Entendendo cada campo do /etc/group

Contém os grupos dos usuário<br>
Ordem dos campos:<br>
1. nome do grupo
2. senha (sim, grupo também pode ter senha, que fica em `/etc/gshadow`)
3. ids do grupo
4. usuários que fazem parte do grupo

#### Entendendo cada campo do /etc/login.defs

Contém as definições de login de cada usuário. No caso, aqui também tem as definições de ids dos usuários (fica próximo da linha 172). Por exemplo:<br>

    # Min/max values for automatic uid selection in useradd
    171 #
    172 UID_MIN                  1000
    173 UID_MAX                 60000

Lembrando que é uma convenção, o Linux, por exemplo, pode ter mais que 60k usuários.

É nesse arquivo também que podemos alterar a variável `DEFAULT_HOME` para `yes` para criar o `/home` dos usuários.

Mais algumas diretivas importantes:<br>

- `UID_MIN`  e  `UID_MAX`: O intervalo de IDs de usuário que podem ser atribuídos a novos usuários comuns.
- `GID_MIN`  e  `GID_MAX`: O intervalo de IDs de grupo que podem ser atribuídos a novos grupos comuns.

- `CREATE_HOME`: Especifica se um diretório pessoal deve ser criado por padrão para novos usuários.
- `USERGROUPS_ENAB`: Especifica se o sistema deve, por padrão, criar um novo grupo para cada nova conta de usuário com o mesmo nome do usuário, e se, ao deletar a conta do usuário, o grupo primário do usuário também deve ser removido, caso não contenha mais membros.

- `MAIL_DIR`: O diretório de spool de email.
- `PASS_MAX_DAYS`: O número máximo de dias que uma senha pode ser usada.
- `PASS_MIN_DAYS`: O número mínimo de dias permitido entre mudanças de senha.
- `PASS_MIN_LEN`: O comprimento mínimo aceitável da senha.
- `PASS_WARN_AGE`: O número de dias de aviso antes que uma senha expire.

>[!TIP]
>
>Ao gerenciar usuários e grupos, sempre verifique este arquivo para visualizar e, eventualmente, alterar o comportamento padrão do sistema, se necessário.

#### 107.1 Gerenciamento de Usuários e Grupos - useradd, userdel, usermod, passwd

Basicamente são:<br>
- `useradd`: adiciona usuário
- `userdel`: exclui usuário
- `usermod`: altera configurações usuário

##### useradd
- `useradd usuário1`: cria o **usuário1**, e em `/etc/passwd`, cria uma linha referente a esse usuário. Algumas distros antigas tinham um bug que não informava o shell a ser utilizado, ficando em branco, e tendo que alterar manualmente (é possível mudar com o comando `chsh`)

Exemplo com várias opções:<br>

    useradd -c "Nome do Usuario" -s /bin/bash -g 1001 -G 1010

- `-c`(`--comment`):  string de texto com o comentário do usuário (geralmente nome completo do usuário
- `-s`(--shell): shell padrão desejado pelo novo usuário
- `-g`(`--gid`): grupo primário (padrão) do novo usuário
- `-G` (`--group`): grupo(s) secundários do novo usuário (outros grupos que o usuário vai pertencer)
- `-e` (`--expiredate`) (YYYY-MM-DD): define um tempo que a conta será desabilitada
- `-f`: cria uma nova conta de usuário definindo o número de dias após a expiração de uma senha durante os quais o usuário deve atualizar a senha (caso contrário, a conta será desabilitada).
- `-k`: cria uma nova conta de usuário copiando os arquivos de esqueleto de um diretório personalizado específico (esta opção só é válida se a opção  `-m`  ou  `--create-home`  for especificada).

Outras opções importantes:
- `-d` (`--home-dir`): definir um `/home` para usuário (não precisa ser o padrão)
- `-m` (`--create-home`): caso no `/etc/user.defs` esteja para não criar `/home` por padrão, pode-se usar essa opção para "forçar" criar o `/home` do usuário (conforme definido no skeleton directory) 
- `-M` (`--no-create-home`): não cria o `/home`
- `-p` (`--password`): define a senha. Mas precisa ser em hash, que pode ser criado por outro comando.
- `-u` (`--uid`): id deseja que o usuário tenha

##### userdel

Supõe que **usuario1** tenha entrada referente a ele em `/etc/passwd`, `/etc/shadow`, `/etc/group` e ele possua um diretório em `/home`, sendo `/home/usuario1`. Se executar:<br>

    userdel usuario1

Será apagado as entradas nos três arquivos, mas **não apagará** a pasta `/home/usuario1`. Para apagar, tem que usar a opção `-r`:
- `-r` (`--remove`): remove o /home do usuário

##### usermod

Basicamente muda o que poderia ser definido com o `useradd`. Supõe que usuario1 não tem comentário, podemos adicionar um comentário com o comando:<br>

    usermod -c "Nome usuario 1" usuario1

Mudar o shell do usuário1:<br>

    usermod -s /sbin/ksh usuario1

`usermod -g suporte usuario1`: muda para **suporte** o grupo padrão do **usuario1** (originalmente, quando criado, o grupo padrão (`-g`) do **usuario1** era **usuario1**. Confirme com um `cat /etc/passwd` para ver o id do grupo do **usuario1**.

Repare na similaridade dos dois comandos a seguir:<br>


`usermod -G devops usuario1`: define o grupo **devops** como grupo secundário do **usuario1**. Repare que em `/etc/passwd`, ainda continuar sendo o grupo padrão o grupo `suporte` (considerando a mudança no comando do parágrafo anterior. No entanto o **usuario1** também pertence agora ao grupo **devosp**.

`usermod -G qateams usuario1`: define o grupo **qateams** como grupo secundário do **usuario1**. Repare que em `/etc/passwd`, ainda continuar sendo o grupo padrão o grupo `suporte` (considerando a mudança no comando do parágrafo anterior. No entanto o **usuario1** também pertence agora ao grupo **qateams**. NO ENTANTO, o usuario1 não faz mais parte do grupo suporte (imagine que o limite de grupos secundários seja 1 grupo por vez), mas podemos podemos manter o grupo secundário com o argumento `-a`, que no caso vai adicionando os grupos. Ficando o comando:<br>

    usermod -G -a qateams usuario1

ou

    usermod -aG qateams usuario1
- `-a` ou `--append`

- `-l` ou `--login`: altera o nome de login da conta de usuário especificada.
- `-L`ou `--lock`:  bloqueia a conta de usuário especificada. Um ponto de exclamação é posto na frente da senha criptografada dentro do arquivo  `/etc/shadow`, desabilitando assim o acesso com senha para esse usuário. (Note: if you wish to lock the account (not only access with a password), you should also set the EXPIRE_DATE to 1).
- `-U`ou `--unlock`: desbloqueia a conta de usuário especificada. Remove o ponto de exclamação na frente da senha criptografada no arquivo  `/etc/shadow`.

>[!TIP]
>
>Lembre que, ao alterar o nome de login de uma conta de usuário, você provavelmente deve renomear o diretório pessoal desse usuário e outros itens relacionados a ele, como arquivos de spool de email. Lembre também que, ao alterar o UID de uma conta de usuário, provavelmente será preciso corrigir a propriedade dos arquivos e diretórios que estejam fora do diretório inicial do usuário (o ID do usuário é alterado automaticamente na caixa de email do usuário e em todos os arquivos pertencentes ao usuário e localizados no diretório inicial do usuário).

##### passwd

Usado para alterar/criar senha para usuários (precisa ser root)

    passwd usuario1 #troca a senha do usuario1
    passwd #troca a senha do usuario root

Lembre-se que ao criar um usuário, no campo referente à senha lá no arquivo `/etc/shadow`, vai ficar com um ponto de exclamação (`!`), indicando que não foi definido uma senha. Após criar uma senha com o comando `passwd`, uma hash entrará no lugar do ponto de exclamação.

    su usuario1 #troca para o usuario1

Mais algumas opções para o uso de `passwd`:<br>

- `-d`: apaga a senha de uma conta de usuário (desabilitando o usuário).
- `-e`:  força a conta de usuário a alterar a senha.
- `-i`:  define o número de dias de inatividade após a expiração de uma senha, durante os quais o usuário deve atualizar a senha (caso contrário, a conta será desabilitada).
- `-l`: bloqueia a conta de usuário (a senha criptografada é prefixada com um ponto de exclamação no arquivo  `/etc/shadow`).
- `-n`: define o tempo de vida mínimo da senha.
- `-S`: exibe informações sobre o status da senha de uma conta de usuário específica.
- `-u`: desbloqueia a conta do usuário (o ponto de exclamação é removido do campo de senha no arquivo  `/etc/shadow`).
- `-x`: define o tempo de vida máximo da senha.
- `-w`: define o número de dias de aviso antes que a senha expire, durante os quais o usuário é advertido de que a senha deve ser alterada.

>[!NOTE]
>
>Os grupos também podem ter uma senha, que pode ser definida usando o comando  `gpasswd`. Os usuários que não são membros de um grupo mas conhecem a senha podem ingressar nele temporariamente usando o comando  `newgrp`.  `gpasswd`  também é usado para adicionar e remover usuários de um grupo e para definir a lista de administradores e membros comuns do grupo.

> [!IMPORTANT] 
>
>Há o comando `adduser`, que na realidade é um script que vai interagindo para criação do usuário. A nível de LPI, focar no `useradd`.

#### 107.1 Gerenciamento de Usuários e Grupos - groupadd, groupdel, groupmod

- `groupadd`: adiciona grupo
- `groupdel`: exclui grupo
- `groupmod`: altera configurações do grupo

##### groupadd

- `groupadd suporte`: cria o grupo **suporte**
- `groupadd -g 2000 dev`: cria o grupo **dev** com id 2000

##### groupdel

- `groupdel suporte`: exclui o grupo suporte

##### groupmod

É possível mudar o id e o nome do grupo. No exemplo abaixo está mudando o nome do grupo de **suporte** para **devops**:

- `groupmod -n devops suporte`(`-n` ou `--new-name`): altera o nome do grupo de suporte para devops
- `groupmod -g 2001 suporte`(`-g` ou `--gid`): altera o id do grupo para 2001

*Não é possível excluir um grupo caso se trate do grupo principal de uma conta de usuário. Portanto, é preciso remover o usuário antes de remover o grupo. Quanto aos usuários, se você excluir um grupo, os arquivos pertencentes a esse grupo permanecerão em seu sistema de arquivos e não serão excluídos ou atribuídos a outro grupo.

##### newgrp

Comando faz com que o usuário atual assuma determinado grupo durante a sessão de login.

- `newgrp suporte`: supõe que está logado com **usuario1**, e que esse usuário também faça parte do grupo **suporte**, além de seu próprio (grupo **usuario1**). Ao executar o comando no começo da frase, ele assume o grupo **suporte** como padrão, e arquivos criados serão do grupo **suporte**.

##### senhas nos grupos

Seria uma situação que o usuário não faz parte de um grupo, se tentar, por exemplo, com o comando: `newgrp devops`, e o usuário não fizer parte do grupo, vai pedir uma senha, e somente será admitido ao grupo se souber a senha.

Primeiramente é necessário criar uma senha para o grupo, com o comando:<br>

    gpasswd nome_grupo

Lembrando que se pode observar os grupos que têm senha olhando o arquivo `/etc/gshadow`

Para acessar o grupo, basta usar o comando `newgrp nome_grupo`

> [!CAUTION]
> 
>Utilizar senha em grupo não é recomendado, e é pouquíssimo ou quase não utilizado.

#### 107.1 Gerenciamento de Usuários e Grupos - id, groups, getent e chage

- `id`: imprime o id do usuário e o id dos grupos pertencentes a esse usuário (usuário atual). Ou pode usar colocando o usuário como parâmetro: `id usuario1`

Sequencialmente seria: 
- **id do usuário**; 
- **id do grupo padrão do usuário**;
- **grupos secundários que esse usuário pertence**.

- `groups`: mesmo conceito que no comando `id`, que irá listar os grupos do usuário logado. Podendo também especificar um usuário. Ex. `groups usuario1`<br>
Ex.<br>

```
    $ groups rodrigo
    rodrigo adm cdrom sudo dip plugdev users lpadmin sambashare
```
`getent`: pega informações de grupos/usuários (esse comando exibe entradas de bancos de dados suportados pelas bibliotecas _Name Service Switch_ (NSS) e requer o nome do banco de dados e uma chave de pesquisa)

- `getent passwd rodrigo`: pega informação do usuário rodrigo em `/etc/passwd` (como se fosse um `cat /etc/passwd | grep rodrigo`)
- `getent group suporte`: pega informações do usuário suporte no `/etc/group`

>[!NOTE]
>
>Lembre-se de que o `getent` só pode acessar os bancos de dados configurados no arquivo `/etc/nsswitch.conf`.

##### chage (change age - alterar idade)

`chage`: mostra as propriedades do usuário: quando a senha vai expirar ou ficar ativa/inativo, número máximo entre dias para alterar senha, última vez que usuário mudou a senha, etc
- `chage -l rodrigo`: mostra propriedades do usuário rodrigo (apenas **root** pode ver as propriedades de todos os usuários, os demais somente suas próprias informações)
- `chage -M 60 rodrigo`: força que o usuário troque de senha a cada 60 dias. Execute `chage -l rodrigo` para saber se aplicou mesmo.
- `chage -d "2024-11-13" rodrigo`: (ou `--lastday`): muda a data que da última vez que o usuário mudou a senha. (isso implica se, por exemplo, foi configurado para trocar a senha a cada x dias. Se for 10 dias, e voltar mais de 10 dias da última troca, vai ter que cadastrar uma nova senha, pois a senha é considerada expirada).
- - `chage -E "2024-12-12" rodrigo`: (ou `--expiredate`): define uma data para a conta expirar. Se expirar, ao logar, vai informar que a conta foi expirada, que é preciso falar com o administrador do sistema
- `chage -E -1 rodrigo`: volta a conta ao normal, em referências ao comando anterior.
- `chage -d0 rodrigo`: força o usuário a trocar senha no próximo login (**root enforced**)
- `-I`: define o número de dias de inatividade após a expiração de uma senha, durante os quais o usuário deve atualizar a senha (caso contrário, a conta será desabilitada).
- `-W`: define o número de dias de aviso antes que a senha expire, durante os quais o usuário é advertido de que a senha deve ser alterada.

>[!NOTE]
>
>A sequencias mostrada no comando `chage`, é a mesma dentro do arquivo `/etc/shadow`. Inclusive a última coluna, é o número de dias, desde 1970. (Nos sistemas mais novos não foi percebido essa informação).

##### Exercícios interessantes no site lpi.org

Comandos aleatórios:<br>
|   |   |
|---|---|
| `usermod -L` | Bloquear a conta de usuário |
| `passwd -u` | Desbloquear a conta de usuário |
| `chage -E` | Definir a data de expiração da conta de usuário |
| `groupdel` | Excluir o grupo  |
| `useradd -s` |Criar uma nova conta de usuário com um shell de login específico |
| `groupadd -g` | Criar um novo grupo com um GID específico |
| `userdel -r` | Remover a conta de usuário e todos os arquivos em seu diretório inicial, o próprio diretório inicial e o spool de email do usuário |
| `usermod -l` | Alterar o nome de login da conta de usuário |
| `groupmod -n` | Alterar o nome do grupo |
| `useradd -m` | Criar uma nova conta de usuário e seu diretório inicial  |

Comandos correspondentes:<br>

|   |   |
|---|---|
| `passwd -n` | `chage -m` |
| `passwd -x` | `chage -M` |
| `passwd -w` | `chage -W` |
| `passwd -i` | `chage -I` |
| `passwd -S` | `chage -l` |

No Linux, podemos usar o comando `passwd -n` (ou `chage -m`) para definir o número mínimo de dias entre as mudanças de senha, o comando `passwd -x` (ou `chage -M`) para definir o número máximo de dias durante os quais uma senha é válida, o comando `passwd -w` (ou `chage -W`) para definir o número de dias de aviso antes que a senha expire, o comando `passwd -i` (ou `chage -I`) para definir o número de dias de inatividade durante os quais o usuário deve alterar a senha e o comando `passwd -S` (ou `chage -l`) para exibir informações breves sobre a senha da conta de usuário.

Comando para bloquear e desbloquear usuários<br>
`usermod --lock` e `passwd -l`. 
Já para desbloqueá-la, os comandos seriam `usermod -U`, `usermod --unlock` e `passwd -u`.

***O comando `usermod` não inclui a opção de remover apenas um grupo; portanto, você precisa especificar todos os grupos secundários aos quais o usuário pertence.

    # usermod -G administrators,web-designers kevin

Questão 6:<br>
Usando o comando  `chage`, primeiro verifique a data de expiração da conta de usuário  `kevin`  e depois altere-a para 31 de dezembro de 2022. Que outro comando pode ser usado para alterar a data de expiração de uma conta de usuário?

    # chage -l kevin | grep "Account expires"
    Account expires		: never
    # chage -E 2022-12-31 kevin
    # chage -l kevin | grep "Account expires"
    Account expires		: dec 31, 2022

O comando  `usermod`  com a opção  `-e`  equivale a  `chage -E`.

Questão 7:<br>
Adicione uma nova conta de usuário chamada  `emma`  com UID 1050 e defina  `administrators`  como seu grupo principal e  `developers`  e  `web-designers`  como seus grupos secundários.

    # useradd -u 1050 -g administrators -G developers,web-designers emma
    # id emma
    uid=1050(emma) gid=1028(administrators) groups=1028(administrators),1029(developers),1031(web-designers)

### 107.2 Automatizar e agendar tarefas administrativas de sistema

#### Cron

É um daemon, um processo que fica rodando, de agendamento do sistema (normalmente do sistema). Verificar se está rodando com `systemctl status cron`

O arquivo `/etc/crontab` é onde ficam as tarefas de agendamento, que é executado pelo usuário **root** (a tabela de tarefas `cron` é comumente chamado de `contrabs`, que contêm os chamdos `cron jobs` - trabalho cron)

>[!NOTE]
>
>No Linux, também existe o recurso `anacron`, adequado para sistemas que podem ser desligados (como computadores de mesa ou laptops). Ele só pode ser usado pelo root. Se a máquina estiver desligada quando os trabalhos de `anacron` tiverem de ser executados, isso ocorrerá na próxima vez em que se ligar a máquina. O `anacron` está fora do escopo da certificação LPIC-1.

Os 5 primeiros campos são delimitados questões do tempo, sendo, sequencialmente: <br>
**minuto da hora (0-59)** | **hora do dia (0-23)** | **dia do mês - dom (1-31)** | **mês do ano -mon (1-12)** | **dia da semana - dow (0-7 com Domingo=0 ou Domingo=7)**

*Para o mês do ano e o dia da semana, podemos usar as três primeiras letras do nome em vez do número correspondente.

- supõe que esteja com o valor 17 no campo **m** (**minute**), significa que vai executar todo minuto 17.
- `*` = qualquer valor. qualquer minuto, qualquer hora, qualquer dia do mês, etc. Outra forma que facilitar a leitura do asterisco é interpretá-lo como "**seja o dia/hora/mes que for** ou **qualquer valor**"
- **dow (day of week):**
	-  `7` e `0` = domingo, logo: 
	- `1`= segunda-feira, 
	- `2`= terça-feira, etc

- `,`: especifica uma lista de valores possíveis
- `-`(hífen): especifica um intervalor de valores possiveis
- `/`: especifica valores escalonados

A primeira entrada no arquivo `/etc/crontab`:

    17 *	* * *	root	cd / && run-parts --report /etc/cron.hourly

Nesse caso, executa em todo minuto 17, em todas as horas, em todos os dias do mês, todo mês, todos os dias da semana, o que estiver dentro de `/etc/cron.hourly`. A execução de todos os arquivos se deve ao programa `run-parts`.

A sintaxe dos crontabs de sistema é semelhante à dos crontabs de usuário, porém ela requer um campo adicional obrigatório que especifica **qual usuário executará o cron job**. Portanto, cada linha em um crontab de sistema contém sete campos separados por um espaço:

-   O minuto da hora (0-59).
-   A hora do dia (0-23).
-   O dia do mês (1-31).
-   O mês do ano (1-12).
-   O dia da semana (0-7 com Domingo=0 ou Domingo=7).
-   O nome da conta de usuário a ser usada ao executar o comando.
-   O comando a executar.
    
Quanto aos crontabs do usuário, podemos especificar mais de um valor nos campos de tempo usando os operadores  `*`,  `,`  ,  `-`  e  `/`. Também é possível indicar o mês do ano e o dia da semana com as três primeiras letras do nome em vez do número correspondente.

- **crontab de sistema**: `/etc/crontab` e `/etc/cron.d`
	- `/etc/cron.hourly` (de hora em hora)
	- `/etc/cron.daily` (diariamente)
	- `/etc/cron.weekly` (semanalmente)
	- `/etc/cron.monthly` (mensalmente)
- **crontab de usuário**: `/var/spool/cron`

>[!WARNING]
>
>Algumas distribuições usam `/etc/cron.d/hourly`, `/etc/cron.d/daily`, `/etc/cron.d/weekly` e `/etc/cron.d/monthly`. Lembre-se de sempre conferir os diretórios corretos nos quais colocar os scripts que o cron deve executar.

##### Especificações de tempo particulares

Ao editar os arquivos crontab, podemos usar atalhos especiais nas primeiras cinco colunas em vez das especificações de tempo:

- `@reboot`: roda a tarefa especificada uma vez após a reinicialização.
- `@hourly`: roda a tarefa especificada uma vez por hora no início da hora.
- `@daily`  (ou  `@midnight`): roda a tarefa especificada uma vez por dia à meia-noite.
- `@weekly`: roda a tarefa especificada uma vez por semana, à meia-noite de domingo.
- `@monthly`: roda a tarefa especificada uma vez por mês, à meia-noite do primeiro dia do mês.
- `@yearly`  (ou  `@annually`): roda a tarefa especificada uma vez por ano, à meia-noite de 1º de janeiro.

**Exemplos**

-   **`@reboot`**
    -   Executa a tarefa apenas uma vez, sempre que o sistema é inicializado.
    -   Exemplo:
        ```
        bash
        
        Copiar código
        
        `@reboot /path/to/script.sh` 
        ```
        
        Uso comum: Iniciar serviços personalizados ou scripts após reiniciar o servidor.
        
-   **`@hourly`**
    -   Executa a tarefa uma vez por hora, no minuto 0.
    -   Equivalente a:
        ```
        `0 * * * *` 
        ```
    -   Exemplo:
        ```
        bash
        
        `@hourly /path/to/script.sh` 
        ```
        
-   **`@daily` (ou `@midnight`)**
    
    -   Executa a tarefa uma vez por dia, à meia-noite.
    -   Equivalente a:
        ```
        `0 0 * * *` 
        ```
    -   Exemplo:
        ```
        bash
        
        `@daily /path/to/script.sh` 
        ```
        
-   **`@weekly`**
    
    -   Executa a tarefa uma vez por semana, no primeiro minuto do domingo.
    -   Equivalente a:
        ```
        `0 0 * * 0` 
        ```
    -   Exemplo:
        ```
        bash
        
        `@weekly /path/to/script.sh` 
        ```
        
-   **`@monthly`**
    
    -   Executa a tarefa uma vez por mês, no primeiro minuto do primeiro dia.
    -   Equivalente a:
        ```
        `0 0 1 * *` 
        ```
    -   Exemplo:
        ```
        bash
        
        `@monthly /path/to/script.sh` 
        ```
        
-   **`@yearly` (ou `@annually`)**
    
    -   Executa a tarefa uma vez por ano, no primeiro minuto do primeiro dia do ano.
    -   Equivalente a:
        ```
        `0 0 1 1 *` 
        ```
    -   Exemplo:
	   ```
        bash
        
        `@yearly /path/to/script.sh`
	```

#### Variáveis no crontab

Dentro de um arquivo crontab, pode haver atribuições de variáveis definidas antes que as tarefas agendadas sejam declaradas. As variáveis de ambiente comumente definidas são:

- `HOME`: o diretório no qual o  `cron`  invoca os comandos (por padrão, o diretório inicial do usuário).
- `MAILTO`: o nome do usuário ou o endereço para o qual a saída e o erro padrão são enviados (por padrão, o proprietário do crontab). Diversos valores separados por vírgulas também são permitidos, e um valor vazio indica que nenhum email deve ser enviado.
- `PATH`: o caminho no qual os comandos podem ser encontrados.
- `SHELL`: o shell a ser usado (por padrão  `/bin/sh`).

No `crontab`, variáveis como `HOME`, `MAILTO`, `PATH`, e `SHELL` são usadas para definir o ambiente no qual os comandos serão executados. Aqui está uma explicação detalhada e exemplos para cada uma:

---

### 1. **`HOME`**
   - Define o diretório de trabalho padrão para os comandos executados pelo `crontab`.
   - Se não for especificado, o valor padrão geralmente é o diretório home do usuário que criou o crontab.
   - Exemplo:
     ```bash
     HOME=/home/usuario
     @daily /path/to/script.sh
     ```
     Aqui, o script será executado com `/home/usuario` como diretório de trabalho.

---

### 2. **`MAILTO`**
   - Define o endereço de email para o qual serão enviados os logs de saída e erros dos comandos executados.
   - Se definido como vazio (`MAILTO=""`), o email não será enviado.
   - Exemplo:
     ```bash
     MAILTO=admin@exemplo.com
     @hourly /path/to/script.sh
     ```
     Aqui, os logs da execução do script serão enviados para `admin@exemplo.com`.

---

### 3. **`PATH`**
   - Define os diretórios onde o `crontab` buscará os comandos executados.
   - Por padrão, o `PATH` do cron é mais restrito que o de um shell interativo, frequentemente algo como `/usr/bin:/bin`.
   - Exemplo:
     ```bash
     PATH=/usr/local/bin:/usr/bin:/bin
     @daily script.sh
     ```
     Aqui, o cron poderá localizar `script.sh` se ele estiver em qualquer um dos diretórios especificados.

---

### 4. **`SHELL`**
   - Define qual shell será usado para executar os comandos.
   - O valor padrão geralmente é `/bin/sh`.
   - Exemplo:
     ```bash
     SHELL=/bin/bash
     @reboot /path/to/script.sh
     ```
     Aqui, o script será executado usando o Bash em vez do shell padrão.

---

#### Exemplo Completo
```bash
SHELL=/bin/bash
HOME=/home/usuario
MAILTO=admin@exemplo.com
PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin

@hourly /home/usuario/scripts/backup.sh
@daily /home/usuario/scripts/limpeza.sh
```

#### O que acontece aqui:
1. O `backup.sh` será executado a cada hora.
2. O `limpeza.sh` será executado diariamente.
3. Os logs dessas tarefas serão enviados para o email `admin@exemplo.com`.
4. Os comandos serão executados no diretório `/home/usuario` usando o Bash.

---

#### Dicas Adicionais
- Se seus scripts dependem de variáveis de ambiente específicas (como `JAVA_HOME`), você pode defini-las no início do arquivo `crontab`.
- Teste seus comandos ou scripts manualmente para evitar problemas de execução devido a configurações de ambiente no cron. 

#### uso interessante para o dia a dia

    @daily /path/to/script.sh >> /var/log/script.log 2>&1
    
Executa diariamente o script script.sh e joga a saída de sucesso ou de erro no arquivo script.log (2>&1 indica para jogar as duas saídas no arquivo script.log)

Outro exemplo interessante, executar o script  `barfoo.sh`  localizado no diretório  `/root`  todos os dias à 01:30, abra  `/etc/crontab`  com seu editor preferido e adicione a seguinte linha:

    30 01 * * * root /root/barfoo.sh >>/root/output.log 2>>/root/error.log

No exemplo acima, a saída do job é anexada a  `/root/output.log`, enquanto os erros são anexados a  `/root/error.log` (`2>` é a saída padrão para erro, usando `2>>` vai concatenar os valores).

>[!WARNING]
>
>Exceto nos casos em que a saída é redirecionada para um arquivo, como no exemplo acima (ou se a variável `MAILTO` estiver definida como um valor em branco), toda a saída de um trabalho cron será enviada ao usuário via email. Uma prática comum é redirecionar a saída padrão para `/dev/null` (ou para um arquivo, para revisão posterior, se necessário) e não redirecionar o erro padrão. Desta forma, o usuário é notificado imediatamente por email sobre eventuais erros.

#### Cron para usuários

Para listar os agendamentos do seu usuário: `contrab -l -u rodrigo`. Se tentar listar agendamentos de outro usuário, se deparará com a mensagem que a opção `-u` deve ser com usuário privilegiado (root ou usuário com privilégio de root).

    must be privileged to use -u

- `crontab -e`: entra no modo de edição do crontab. (primeiro acesso lhe pergunta qual editor que usa). O formato é parecedo com o crontab que foi visto anteriormente, apenas com a exceção que não tem o campo de usuário.

Vejamos um exemplo:<br>

    #m	h	dia	mes	diasemana	comando/script
    15	13	1,10,20,30	* 1-5	/home/rodrigo/Scripts/script.sh >> /tmp/teste-cron.txt

Acima temos:<br>
nos dias 1,10,20,30 às 13h15m, em todo/qualquer mês (`*`), de segunda à sexta-feira (1=segunda, 5=sexta-feira), execute o script em `/home/rodrigo/Scripts/script.sh` e grava a saída do `script.sh` no arquivo `/tmp/teste-cron.txt`<br>
**Observação**: se os dias 1, 10, 20, ou 30 caírem em um sábado ou domingo, o script.sh não vai rodar.

Outro exemplo:

    #m		h	dia		mes	diasemana	comando/script
    */10	*	*		* 	*			/ cd /tmp ; rm -f teste

Nesse caso, a cada 10 minutos o comando será executado. (A `/` significa **a cada**). Se usar `*/5` no dia, seria a cada 5 dias, etc

Mais um exemplo para ilustrar:

    #m		h	dia		mes	diasemana	comando/script
	0		0	1		1	* /usr/bin/fogo-artificio.sh

No dia 1 de Janeiro, de todo ano, às meia noite (hora = 0 e minuto = 0), executa o script `fogos-artificio.sh`

Exemplo com range:<br>

    #m		h		dia		mes	diasemana	comando/script
    20		9-18	*		*	1-5 		/home/usuario/relatorio.sh

Em todo minuto 20, das horas entre 9h e 18h (horário comercial), qualquer dia, qualquer mês, de segunda à sexta-feira, executa o script `relatorio.sh`<br>
Observação: Lembrando que isso é referente ao usuário atual, e os scripts precisam ter permissão de execução.

- `crontab -r`: remove toda configuração do crontab.
- `# crontab -r -u lpi2`: (só root pode) remover crontab de outro usuário
- `crontab -u`: especifica o nome do usuário cujo crontab precisa ser modificado. Esta opção requer privilégios de root e permite que o usuário root edite os arquivos crontab do usuário.

Há possibilidade de criar um arquivo separado contendo as regras da crontab, e importar com o comando: `crontab arquivo_com_contrab`. Confirme com `contrab -l`

Essas modificações, tanto em sistema (`/etc/cron`) como a de usuário, são permanentes, e o(s) arquivo(s) do(s) usuário(s) fica(m) em `/var/spool/cron/crontabs` (precisa ser acesso como **root**). Em algumas distros pode ser que o diretório `crontabs` seja apenas `cron`.

Usuário root pode determinar quem pode e quem não pode utilizar crontab. Isso se baseia nos arquivos:
- /etc/cron.allow ou
- /etc/cron.deny

*Pode ser que esses arquivos não existam, sendo necessário criá-los. A lista de usuários é comum, linha a linha. O `cron.allow` tem prioridade em cima do `cron.deny` (caso tenha os mesmos usuários nas duas listas.

>[!NOTE]
>
>Há uma diferença no comportamento dos arquivos  **/etc/cron.allow**  e  **/etc/cron.deny**  entre sistemas baseados em Debian e RedHat.<br>
No Debian, e distribuições baseadas como o Ubuntu que usamos nesse curso, se ambos arquivos não existem, o uso dos recursos da cron é liberado para todos os usuários.<br>
No entanto, em sistemas baseados em RedHat, como o CentOS, na inexistência dos arquivos, o uso é bloqueado a todos os usuários, exceto para o usuário root.

#### Exercício interessante no lpi.org

Crie um job agendado simples que execute o comando  `date`  todas as sextas-feiras às 13h. Onde você poderia ver o resultado deste trabalho?

    00 13 * * 5 date

A saída é enviada ao usuário; para visualizá-la, use o comando  `mail`(no caso, é possível ver a saída com esse comando, mas precisa ter o pacote **mailutils** instalado (`apt install mailutisl`)

#### Exercício com MAILTO

Como é possível enviar a saída e os erros do seu trabalho agendado para a conta de usuário  `emma`  via email? E como evitar o envio da saída padrão e erros por email?

Para enviar a saída padrão e o erro para  `emma`, definimos a variável de ambiente  `MAILTO`  em nosso arquivo  `crontab`  desta forma:

    MAILTO="emma"

Para dizer ao  `cron`  que nenhum email deve ser enviado, atribuímos um valor vazio à variável de ambiente  `MAILTO`.

    MAILTO=""

### at (agendamento de uma execução)

Tipo de agendamento em determinada hora/momento (executa apenas uma vez)

    at now +2hour
    at> echo "Hora cafe"

Executa o echo `"Hora do café"` daqui duas horas, e poderia ir adicionando comandos. No final use `Ctrl+D` para adicionar essa tarefa (job).<br>
**(Como no caso do `cron`, a saída padrão e o erro são enviados por email)<br>
***Observe que o daemon `atd` precisará estar rodando no sistema para ser possível usar o agendamento de tarefas `at`

>[!NOTE]
>
>No Linux, o comando `batch` é semelhante a `at`, porém os jobs `batch` são executados apenas quando a carga do sistema está baixa o suficiente para permiti-lo.

- `atq` ou `at -l`: mostra os jobs que estão aguardando (vem de `queue`).

>[!NOTE]
>
>Se você executar `atq` como root, ele exibirá os trabalhos na fila para todos os usuários.

```
at 13:20
at> echo "Hora do almoço
```

Executa o echo às `13:20`. Caso já tenha passado esse horário, o at agenda para o dia seguinte. (para especificar quando um job `at` determinado deve ser executado, use o formato `HH:MM`, seguido opcionalmente por **AM** ou **PM** no caso do formato de 12 horas)

Formatos aceitos para passar uma data após a hora:
- `MMDDYY`
- `MM/DD/YY`
- `DD.MM.YY` e
- `YYYY-MM-DD`

- `at midnight`: agenda para meia noite
- `at teatime`: agenda para às 16h ou 4PM (hora do chá)
- `at noon`: agenda para hora do almoço - meio-dia
- `now`: para agora

- `at 02:20pm 05202024`: agenda para dia 20/05/2024 às 14:20

Para **remover** algum job feito pelo at: `atrm 10` ou `at -d 10`: remove o job 10.

>[!NOTE]
>
>O usuário que rodar `atrm` como root pode excluir os jobs de todos os outros usuários.

- `/etc/at.allow`: permite quais usuário usar o `at`
- `/etc/at.deny`: nega quais usuário a usar o `at` (esse já vem criado, preenchido com usuários do sistema, permitindo somente usuários comuns utilizar)

**As opções mais importantes do comando**  `at`  são:

- `-c`: imprime os comandos de um ID de trabalho específico na saída padrão.
- `-d`: exclui trabalhos com base em seu ID de trabalho. É um alias para  `atrm`.
- `-f`: lê o job em um arquivo em vez da entrada padrão.
- `-l`: lista as tarefas pendentes do usuário. Se o usuário for root, todos os trabalhos de todos os usuários serão listados. É um alias para  `atq`.
- `-m`: envia um email para o usuário no final do trabalho, mesmo se não houver saída.
- `-q`: especifica uma fila na forma de uma única letra de  `a`  a  `z`  e de  `A`  a  `Z`  (por padrão,  `a`  para  `at`  e  `b`  para  `batch`). Os jobs nas filas com as letras mais altas são executados com um valor nice maior. Os jobs enviados a uma fila com uma letra maiúscula são tratados como trabalhos em lote (`batch`).
- `-v`: mostra a hora em que o trabalho será executado antes de ler o trabalho.

Leia o arquivo `timespec` na árvore `/usr/share` para saber mais sobre a definição exata das especificações de data e hora.

#### Agendamento de Tarefas - systemd timer

- `systemct list-timers`: lista os agendamentos que já estão no sistema, no caso `UNITs`, com sufixo  (terminam com)`.timer`, ligadas às `ACTIVATES`, com sufixo (terminam com) `.service` (Por padrão, um `timer` ativa um serviço com o mesmo nome, exceto pelo sufixo).
- `systemct list-timers --all`: mostra agendamentos que possam estar inativas
- `systemctl list-units --type=timer`: lista as unidades do tipo `timer`

Para observar uma unit, é possível pode meio do comando:<br>
- `systemct list-timers`

```
$ systemctl status apt-daily-upgrade.timer 
● apt-daily-upgrade.timer - Daily apt upgrade and clean activities
     Loaded: loaded (/usr/lib/systemd/system/apt-daily-upgrade.timer; enabled; preset: enabled)
     Active: active (waiting) since Fri 2024-11-08 16:41:52 -03; 5 days ago
    Trigger: Fri 2024-11-15 06:13:26 -03; 16h left
   Triggers: ● apt-daily-upgrade.service

Nov 08 16:41:52 OptiPlex-3080 systemd[1]: Started apt-daily-upgrade.timer - Daily apt upgrade and clean activities.
```

Na terceira linha, é mostrado onde está configurado o `.timer`, no caso: `/usr/lib/systemd/system/apt-daily-upgrade.timer`(esse arquivo que seria alterado para definir novo agendamento). Sendo em um formato normal de unit, com sua descrição, e demais itens.

O que é interessante é a parte de `[Timer]`, onde tem a opção `onCalendar`, com a seguinte sintaxe:
- `OnCalendar= *-*-* 6:00`: sendo qualquer ano (*), qualquer mês (*), qualquer dia (*) às 6 horas.
- `OnCalendar= *-*-* 6..18:00,30 `: sendo qualquer ano (*), qualquer mês (*), qualquer dia (*) das 6 até às 18h, no minuto 00 e minuto 30. Se preferir, pode adicionar dois pontos (`:`) e informar os segundos, ou `*` para qualquer segundo.
- `OnCalendar=Mon..Fri *-*-* 6:00`: caso queira colocar de segunda à sexta-feira (entre dois valores para indicar um intervalo contíguo)
- `OnCalendar=Sat,Sun *-*-* 6:00`: caso queira colocar sábado e domingo (padrão para **Debian** e derivados - **RedHat** seria `Mon-Fri`)

- `RandomizedDelaySec=60m`: nessa faixa de 60 minutos, a execução pode fica aleatória dentro desse tempo, para que não tenha várias coisas executando ao mesmo tempo, achando um gap melhor para essa execução.
- `Persistend=true`: grava caso o `.service` seja executado manualmente. E considera essa informação para a próxima execução.

Na Unit do `.service`, é basicamente um `.service` comum, que é descrito o que será executado no `ExecStart`.<br>
Mais detalhes em `man systemd.timer`. Mais detalhes de como especificar a data e hora no calendar, usar: `man systemd.time`

Vejamos o exemplo de `systemd-tmpfiles-clean.timer`:<br:
- executa `systemctl status systemd-tmpfiles-clean.timer` para saber onde está carregado esse arquivo, no caso é em `/usr/lib/systemd/system/systemd-tmpfiles-clean.timer`, tendo como conteúdo:
```
[Unit]
Description=Daily Cleanup of Temporary Directories
Documentation=man:tmpfiles.d(5) man:systemd-tmpfiles(8)
ConditionPathExists=!/etc/initrd-release

[Timer]
OnBootSec=15min
OnUnitActiveSec=1d
```
Em `OnUnitActiveSec`, é informado que se a unidade estiver ativa, para executar o serviço a cada 1 dia. Em `OnBootSec` é nítido que será executado após 15 minutos. Isso é para casos que o sistema não é desligado.

**Outro exemplo interessante listado nas lições da lpi.org**

Por exemplo, para rodar o serviço  `/etc/systemd/system/foobar.service`  às 05:30 da primeira segunda-feira do mês, adicionamos as seguintes linhas no arquivo de unidade  `/etc/systemd/system/foobar.timer`  correspondente:

    [Unit]
    Description=Run the foobar service
    
    [Timer]
    OnCalendar=Mon *-*-1..7 05:30:00
    Persistent=true
    
    [Install]
    WantedBy=timers.target

Depois de criar o novo temporizador, você pode ativá-lo e iniciá-lo executando os seguintes comandos como root:

    # **systemctl enable foobar.timer**
    # **systemctl start foobar.timer**

Podemos alterar a frequência do trabalho agendado modificando o valor  `OnCalendar`  e, em seguida, digitando o comando  `systemctl daemon-reload`.

Finalmente, se você quiser ver a lista de temporizadores ativos ordenados pelo momento em que terminam, use o comando  `systemctl list-timers`. A opção  `--all`  exibe também as unidades de temporizador inativas.

>[!NOTE]
>
>Lembre-se de que os temporizadores são registrados no diário (journal) do systemd e você pode rever os registros das diferentes unidades usando o comando  `journalctl`. Além disso, se estiver trabalhando como um usuário comum, será preciso usar a opção  `--user`  dos comandos  `systemctl`  e  `journalctl`.

##### Criando um agendamento

1. criar um **serviço**, dentro de `/etc/systemd/system`:
	1.1 usar `vim exemplo.servce`
```
[Unit]
Description=Exemplo systemd-timer

[Service]
Type=oneshot
ExecStart=/bin/sh -c '/bin/date >> /tmp/exemplo-system.txt'
```

2. criar um **timer**, dentro de `/etc/systemd/system`:
	2.1 usar `vim exemplo.timer` (usar o mesmo nome, mudando só o final)
```
[Unit]
Description=Teste de systemd

[Timer]
OnCalendar=*-*-* *:*:10 # toda hora, todo min, no segundo 10

AccuracySec=1us # 1 micro segundo ou 1ms, que é a margem para executar com precisão. Se for muito alto, lembrar que tem prioridade.

Unit=teste.service
```

No `OnCalendar`, pode informar só a hora: `OnCalendar=*:*:10`<br>
No `AccuracySec` lembrar da prioridade, se colocar tempo muito alto e se estiver tendo muita execução, vai utilizar um tempo random dentro do intervalo colocado

Se rodar `systemctl list-timer`, o timer feito não aparece. Portanto é preciso iniciar o timer criado: `systemctl start exemplo.timer`. Executando novamente `systemctl list-timer`, já vai aparecer na lista. É interessante também recarregar as informações do daemon do systemctl, com o comando `systemctl daemon-reload`

Em vez da forma normalizada mais longa mencionada acima, é possível usar algumas expressões especiais que descrevem frequências específicas para a execução de um job:

- `hourly`: roda a tarefa especificada uma vez por hora, no início da hora.
- `daily`: roda a tarefa especificada uma vez por dia à meia-noite.
- `weekly`: roda a tarefa especificada uma vez por semana, na meia-noite de segunda-feira.
- `monthly`: roda a tarefa especificada uma vez por mês, na meia-noite do primeiro dia do mês.
- `yearly`: roda a tarefa especificada uma vez por ano, na meia-noite de 1º de janeiro.

Consulte as páginas de manual para ver a lista completa de especificações de hora e data em  `systemd.timer(5)`.

No **`systemd`**, os **timers** substituem ou complementam o uso do `cron` para agendamento de tarefas, proporcionando maior flexibilidade e integração com o sistema. Aqui estão os equivalentes aos atalhos do `crontab` usando **`systemd.timer`**:

---

#### Estrutura de um Timer no `systemd`
Um timer no `systemd` geralmente é composto por dois arquivos:

1. **Arquivo de Serviço (`.service`)**
   - Define o que será executado.
2. **Arquivo de Timer (`.timer`)**
   - Define quando será executado.

---

### Equivalentes no `systemd.timer`
Os timers no `systemd` usam o campo `[Timer]` para configurar os intervalos de execução. Aqui estão os exemplos:

1. **`@reboot` (ao iniciar o sistema)**
   - Use o parâmetro **`OnBootSec`** no arquivo `.timer`.
   - Exemplo:
     ```ini
     [Timer]
     OnBootSec=1min
     ```
     Este timer executará a tarefa 1 minuto após o sistema inicializar.

2. **`@hourly` (uma vez por hora)**
   - Use **`OnCalendar`** com o valor `hourly`.
   - Exemplo:
     ```ini
     [Timer]
     OnCalendar=hourly
     ```
     Este timer será acionado no início de cada hora.

3. **`@daily` (uma vez por dia, à meia-noite)**
   - Use **`OnCalendar`** com o valor `daily`.
   - Exemplo:
     ```ini
     [Timer]
     OnCalendar=daily
     ```
     O timer será acionado diariamente à meia-noite.

4. **`@weekly` (uma vez por semana, no domingo)**
   - Use **`OnCalendar`** com o valor `weekly`.
   - Exemplo:
     ```ini
     [Timer]
     OnCalendar=weekly
     ```
     O timer será acionado no primeiro minuto de cada domingo.

5. **`@monthly` (uma vez por mês, no primeiro dia)**
   - Use **`OnCalendar`** com o valor `monthly`.
   - Exemplo:
     ```ini
     [Timer]
     OnCalendar=monthly
     ```
     O timer será acionado no primeiro minuto do dia 1 de cada mês.

6. **`@yearly` (ou `@annually`)**
   - Use **`OnCalendar`** com o valor `yearly`.
   - Exemplo:
     ```ini
     [Timer]
     OnCalendar=yearly
     ```
     O timer será acionado no primeiro minuto do primeiro dia de cada ano.

---

### Exemplo Completo
Aqui está um exemplo completo para criar um serviço e um timer que executam um script diariamente.

#### Arquivo de Serviço (`exemplo.service`)
```ini
[Unit]
Description=Executa o script de exemplo

[Service]
ExecStart=/path/to/script.sh
```

#### Arquivo de Timer (`exemplo.timer`)
```ini
[Unit]
Description=Executa o script de exemplo diariamente

[Timer]
OnCalendar=daily

[Install]
WantedBy=timers.target
```

#### Comandos para Ativar
1. Coloque os arquivos em `/etc/systemd/system/`.
2. Habilite o timer:
   ```bash
   sudo systemctl enable exemplo.timer
   ```
3. Inicie o timer:
   ```bash
   sudo systemctl start exemplo.timer
   ```

---

### Vantagens do `systemd.timer`
- Integração nativa com o `systemd` (logs em `journalctl`).
- Opções adicionais, como **`AccuracySec`** (ajustar precisão) e **`Persistent`** (executar tarefas perdidas durante períodos de inatividade).

Isso torna o `systemd` uma alternativa poderosa ao `cron` para tarefas agendadas! 

##### systemd-run (equivalente ao at)

Normalmente ele é usado para criar uma unidade transiente de temporizador para que um comando seja executado em um momento específico sem a necessidade de se criar um arquivo de serviço

Por exemplo, atuando como root, você pode executar o comando  `date`  às 11h30 em 06/10/2019 usando o seguinte:

    # **systemd-run --on-calendar='2019-10-06 11:30' date**

Se quiser executar o script  `foo.sh`, localizado em seu diretório atual, depois de dois minutos, use:

    # **systemd-run --on-active="2m" ./foo.sh**

- `system-run --on-active=60s /bin/touch /tmp/exemplo-run.txt`: roda após **60 segundos** o comando `touch` que criar o arquivo `exemplo-run.txt` em `/tmp`
- `system-run --on-active=60s --time-property=AccuracySec=1ms /bin/touch /tmp/exemplo-run.txt`: faz o mesmo do comando anterior, mas em system-run também tem a questão de prioridade, e se quiser com precisão, precisar incluir o argumento `--time-property`

É possível observar que após rodar o `system-run` acima, será retornado um run, que no caso é algo parecido com isso: `run-sequecia-caracteres.timer`

Se pegar esse `.timer` e olhar no `journalctl`, com a opção `-u` `--unit`, mais o `run` (`journalctl -u run-sequecia-caracteres.timer`), é possível notar que já está rodando (mas o comando ainda não foi executado - lembre-se, foi configurado para 60 segundos).

Se olhar no `systemctl list-timers` o `run-sequecia-caracteres.timer` que foi criado, vai estar listado, mostrando quanto tempo falta para sua hora de executar.

Depois que passar os 60 segundos, ele irá executar, e **não** irá mais aparecer no `systemctl list-timers`. Mostrando que fez 1 execução apenas. É possível ter certeza verificando o .service dele, com o comando `journalctl -u run-sequecia-caracteres.service`

Consulte as páginas de manual para aprender todos os usos possíveis de  `systemd-run`  com  `systemd-run(1)`.

>[!NOTE]
>
>Os temporizadores são registrados no diário do systemd e você pode rever os registros das diferentes unidades usando o comando `journalctl`. Além disso, se estiver trabalhando como um usuário comum, precisará usar a opção `--user` dos comandos `systemctl` e `journalctl`.

### 107.3 Localização e internacionalização

Para o Linux, o horário padrão é o UTC (Tempo Universal Coordenado). O UTC substituiu o GMT (Greenwich Meridian Time) ou Z (Zulu), que se baseava na hora local de Greenwich. Depois disso ele pega e converte para o que foi configurado de acordo com a localização.

- `timedatectl`: é um date melhorado, mais completo, que descreve a **data/hora** em diferentes formatos

Tudo isso fica configurado em `/etc/localtime` (é um arquivo que não tem como ler, nele fica configurado a questão do **timezone**, horário de verão - **daylight saving time**). Lembrando que `/etc/localtime` aponta para `/usr/share/zoneinfo/America/Sao_Paulo` (a depender da sua localização).

    ls -l /etc/localtime 
    lrwxrwxrwx 1 root root 37 Nov  8 16:21 /etc/localtime -> /usr/share/zoneinfo/America/Sao_Paulo

Vendo em `/usr/share/zoneinfo/Brazil` é possível notar onde `/etc/localtime` está apontando:

    ls -l /usr/share/zoneinfo/Brazil/
    total 0
    lrwxrwxrwx 1 root root 21 Nov  8 16:20 Acre -> ../America/Rio_Branco
    lrwxrwxrwx 1 root root 18 Nov  8 16:20 DeNoronha -> ../America/Noronha
    lrwxrwxrwx 1 root root 20 Nov  8 16:20 East -> ../America/Sao_Paulo
    lrwxrwxrwx 1 root root 17 Nov  8 16:20 West -> ../America/Manaus

Em `/etc/timezone` está configurado o timezone atual (se der um `cat` nesse arquivo, retorna `America/Sao_Paulo`)

- `tzselect`: usado para ajudar a selecionar o **timezone** correto/desejado.

Para alterar o timezone apenas da sessão atual, pode-se alterar a variável `$TZ`:<br>
- export TZ=America/Chicago<br>

*Faça um date antes e depois de alterar o timezone para perceber as mudanças:<br>

    $ date
    Mon Nov 18 20:22:49 AM -03 2024
    $ export TZ=America/Los_Angeles
    $ date
    Mon Nov 18 15:23:05 AM PST 2024

*Lembrando que isso apenas está traduzindo o que está internamente no UTC. Para voltar a data no timezone que estava antes, basta executar: `unset TZ`.

Ainda alterando o `TZ`, se criar um arquivo, a data de criação atualiza caso o `TZ` seja alterado. Isso se torna interessante quando há usuário de diferentes timezones, para que possa ajustar de acordo com a localização (ajustar no `.bashrc`, por exemplo - ou até mesmo para todos os usuário, em `/etc/profile`).

A mudança por meio da variável prevalece a configuração em `/etc/localtime`. Podemos preencher da forma por extenso também:

    TZ=:/usr/share/zoneinfo/America/Sao_Paulo

Para alterar por meio do `/etc/localtime`, basta remover o link atual, e apontar para um novo (esse novo é possível consultar por meio das opções que há em `/usr/share/zoneinfo`)

    $ ls -l /etc/localtime
    /etc/localtime -> /usr/share/zoneinfo/America/Sao_Paulo
    
    rm -f /etc/localtime
    
    ln -s /usr/share/zoneinfo/Asia/Bangkok /etc/localtime

Confirme as mudanças com os comandos `date` e `timedatectl`. As mudanças são refletidas de imediato.

#### Localização e Internacionalização - Idioma, Linguagem e Codificação

- `locale`: lista informações específicas de acordo com locais, países no mundo, tais como: sistema monetário, se usa vírgula ou ponto, formato de datas, etc, assim como o tipo de codificação de caracteres.

Ex. **en_US.UTF-8** ou **pt_BR.UTF-8** (codificação usada na instalação)

O primeiro mais antigo é o ASCII, depois foi melhorado para ISO/IEC 8859-1, e UTF-8 é o mais recente/utilizado.

- `locale -a` (`--all-locales`): lista os modelos disponíveis para o uso.

<!--stackedit_data:
eyJoaXN0b3J5IjpbLTE0NzA0MDQzMzYsLTE4ODY3ODk5MjUsLT
EwNzE3NTQ4MzUsLTExMDIzNTMwNjgsMjA2ODc5NDkxMCwxODA4
MzM0OTAwLDcxMDI4ODAzLC0xNDA3NzgyMDM1LC01ODUyMTgwNj
gsLTEwNjUxMzg1OTAsLTE1MzY0OTUwMSw4MTgwNjQxNSwtMTk1
OTUyOTk2OSwtMzY2MjYyMTkyLC0xNTE3NDc3MTAxLC00NTAzOD
k0MzMsLTg4MTE1MTM2MywxMTI2MzM5ODE4LC0yNjAwNTYxNjIs
ODc3NTk3Njg1XX0=
-->