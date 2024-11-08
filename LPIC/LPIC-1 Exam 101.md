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
<!--stackedit_data:
eyJoaXN0b3J5IjpbLTE1MTY5NTI4OTMsLTE3MDU0ODM4NjIsNj
QyMDA3MDc0LDMxNjM3MzUyOCwtMzczOTk4NTg2LC04MDA4MTg5
NzIsLTkzNTEwMTk4Nyw1MjAzNjQ5NjksLTE2NDE5NTcyNDgsLT
EzMTA4MDQzMjAsNzU2NDU1MzY1LDEzNDcwODY1MTMsMTY3NTgw
ODc0MywtODYzMDkyODkxLDkwNjQ2MjU2LC0xMDgwNjg1OTQ5LC
05NjY0NTM3LDEzMTQ5MDY0NDIsLTI2OTQ3MDQ1NywtNDI5MTE1
OTg5XX0=
-->