# Curso de Redes de Computadores Básico Mão na Massa

Anotações que fiz durante o Curso do Gustavo Kalau, disponível no [YouTube](https://www.youtube.com/playlist?list=PL6BTdBqzl1oY9EQ4151rGNEbATMNgX8vK).

## Tabela Mac (Aula 02)
Para comunicar dois computadores conectados por meio de um switch (comutador), é criado uma tabela Mac no switch, pois ele trabalha basicamente na camada 2 (Layer 2). Se conectarmos dois pcs em um SW, e dermos um ping um no outro, dentro do sw, por meio do comando `show mac-address-table`, podemos ver a tabela mac que o sw montou para localizar esses dispositivos na rede.
> Lembrando que essa tabela montada fica apenas por alguns minutos se não houver comunicação, depois ela é desfeita. Isso ocorre pois podemos mudar os dispositivos conectados a ela, e assim o sw recria um novo endereçamento se for preciso.

## Configurações em dispositivos Cisco, criação de Vlans (Aula 3)

`en` ou `enable` e depois `conf t` ou `conf terminal`

### Configuração/criação de Vlan em uma porta específica do SW

### Criando a vlan 100 (FINANANCEIRO)

Primeiro criamos a vlan com o comando `vlan 100` (que efetivamente cria a vlan 100). No terminal notamos que ficará `switch(config-vlan)`, indicando que estamos dentro da configuração da vlan 100. Damos um nome a ela, com o comando `name FINANCEIRO`. Logo podemos ver que foi criado a vlan 100 de nome FINANCEIRO por meio do comando `show vlan brief`.

Devemos criar a vlan 100 por todo o caminho que ela irá percorrer. Se criarmos no sw de acesso, teremos que criar também no sw core, ou algum outro equipamento que seja necessário passar durante a transmissão na rede, até o seu destino.

Apenas criar a vlan não é ainda suficiente, precisamos adicioná-la a alguma porta do sw. Podemos fazer isso da seguinte forma:<br>

`interface fastEthernet 0/1` (selecionamos a porta 1 do sw a 100Mbps - note que no terminal ficará dessa forma: `switch(config-if)#` - indicando que selecionamos a interface/porta desejada)<br>
`switchport mode access` (modo de acesso - onde passará apenas 1 vlan, também conhecida como **untagged**)<br>
`switchport access vlan 100` (indica em qual vlan a porta `0/1` vai passar)<br>

>Para selecionar um range de portas, como por exemplo, porta 1 e 2, usamos:
`interface range fastEthernet 0/1-2`

### Excluir uma vlan

Para remover uma VLAN, precisamos ir no modo de configuração (`conf t`) e colocar o comando `no` antes, ficando, por exemplo, a exclusão da vlan 900, da seguinte forma: `no vlan 900`.

### Visualizar as vlans criadas/existentes

`show vlan brief` (mostra as portas/vlans)


>**lembrando que as vlans precisam estar em todos os switches, assim como no Core que interliga os switches. No Core, as portas que interligam os switches são especiais, chamadas de portas de transporte, pois muito provavelmente teremos que ter mais de uma vlan passando por elas (ou simplesmente todas as vlans). Essas portas são também conhecidas como porta tronco (trunk). Os pacotes serão equiquetados (tagged)**


### Configuração no SW Core (deixar as portas trunk) 

No SW Core, como muito corriqueiramente, irá passar mais de uma vlan por porta (ou até mesmo todas as vlans em uma única porta), teremos que configurá-la como modo tronco (trunk), também conhecido como tagged. A criação da vlan é similar ao do sw de acesso, no entanto, após selecionarmos a porta/interface que desejamos utilizar, usaremos os comandos:

`interface gigabitEthernet 1/0/3` (selecionamos a porta 3 a 1000Mbps. Lembrando que 1000Mbps é igual a 1Gbps)<br>
`switchport mode trunk` (faz o encapsulation dot1q. Esse trunk mode evita que apenas uma vlan funcione por acesso, como é feito no modo access)

`int gi1/0/6` (maneira abreviada do mesmo comando anterior, apenas utilizando a porta 6)

>Ao chegar um frame do switch para o Core, esse frame chega com uma etiqueta (tagged) informando de qual vlan ele está vindo. E o Core verificando que tem a vlan desejada, ele encaminha para a vlan de destino.

`show interfaces trunk` (exibe quais portas estão em tronco (trunk), e as vlans que estão configuradas nas portas)

### DTP (Dynamic Trunking Protocol) da Cisco

Se executarmos o comando `sh interfaces trunk` no switch de acesso, percebemos que foi automaticamente criado um tronco na porta gigabit que interconecta o SW Core, isso graças ao [DTP (Dynamic Trunking Protocol)](https://ccna.network/protocolo-trunking-dinamico/), um protocolo proprietário da Cisco. Em outros fabricantes, muito provavelmente teríamos que configurar manualmente o tronco no sw de acesso.

## Gateway entre as vlans

No Core, como ele normalmente trabalha na camada 3 (L3), há nele um Router virtual embutido, que podemos interligar as vlans, por meio de um Gateway.
Para interligar a vlan 100 e 200, por exemplo:

`interface vlan 100` (é como um SW Virtual Interface, que colocaremos uma interface nela)<br>
`ip address 172.16.0.1 255.255.255.0` (indicamos o endereço do Gateway)<br>
`no shutdown` (para ligar a porta - normalmente já vem ligada por padrão, diferentemente de Routers)<br>
`show ip interface brief` (nos mostra o gateway da vlan que acabamos de definir - é o portão da casa)<br>

Depois, caso não esteja utilizando DHCP, colocar o Gateway manualmente no computador conectado no SW.

Por fim, temos que concluir o roteamento, com o comando:<br>
`ip routing`

## Servidor DHCP
O ideal é termos uma nova vlan para o servidor DHCP, um switch (o 2960, por exemplo), e interligar esse switch ao switch core, por meio das interfaces gigabit.

`vlan 400`<br>
`name DC` (DC = Data Center)<br>
`interface fastEthernet 0/1`<br>
`switchport mode access`<br>
`switchport access vlan 400`<br>

O próximo passo é criarmos um tronco (trunk) nessa porta. Mas iremos fazer por meio do SW CORE:
`interface gigabitEthernet 1/0/7`<br>
`switchport mode trunk`

Assim dessa forma, utilizando o DTP, é criado o trunk na porta lá do switch DC.
Precisamos criar a vlan 400 no Core, e um Gateway
`vlan 400`<br>
`name DC`<br>
`interface vlan 400`<br>
`ip address 10.0.0.1 255.255.255.0`

>**Importante!**
>
>Ainda assim, se adicionarmos um PC na rede financeiro, que pertence a vlan 100, o broadcast do PC adicionado não alcançará o servidor DHCP, pois nosso switch CORE, que trabalha em L3 para interligar os Gateways, por padrão, não propaga **broadcast**, é apenas propagado **unicast**.

Para que o servidor DHCP seja alcançado, temos que acessar o CORE e colocar uma instrução na interface de vlan 100, que vai para o servidor DHCP:
`interface vlan 100`<br>
`ip helper 10.0.0.20` (10.0.20 é o ip do servidor DHCP)

>Lembrando que se adicionar um novo PC na rede, precisamos atribuir a porta que esse novo PC está usando no switch, a uma vlan que ele irá pertencer.

### Adicionando um ISP
Adicionar o Router-PT e instalar uma placa de rede gigabit, conectar as interfaces no switch CORE.
Mudar o hostname, e adicionar um ip a ele, na porta GigabitEthernet6/0
`interface g6/0`<br>
`ip address 10.99.99.1 255.255.255.0`<br>
`no shudown` (comando para ligar as portas, ao contrário do SW que já vem ligado, no Router precisamos ligar)

No SW CORE, precisamos criar a vlan 500 e adicionar essa vlan na interface gigabit 1/0/9:
`interface vlan 500`<br>
`ip address 10.99.99.10 255.255.255.0`<br>
`no shutdown`<br>
`int g1/0/9`<br>
`switchport mode access`<br>
`switchport mode vlan 500`<br>
`show ip route` (o core já vai reconhecer a rede 10.99.99.0. Podemos dar um ping em 10.99.99.1 para testar)

### Direcionar o tráfego para o ISP Batata
Agora devemos pegar todo o tráfego que chegar até o Core, que for direcionado para Internet, e apontar para o ISP Batata. Dentro do Core devemos criar uma rota default:
`ip route 0.0.0.0 0.0.0.0 10.99.99.1`<br>
`show ip route` (vai nos mostrar a rota default criada. Se, por exemplo, chega um pacote para encaminhar para a rede 172.16.0.0, vai ser encaminhado para a vlan 100, a qual ela faz parte dessa rota. Caso algum pacote chegue e não tenha a rota especificado, o pacote será encaminhado para a rota padrão, no caso, essa que acabamos de criar **0.0.0.0**. Ela é uma rota genérica, caso não exista uma rota específica, será utilizado essa rota default. Na tabela de roteamento, as demais rotas sempre terão preferência, apenas quando não houver opção, a rota **default** é utilizada)

### Aula 7 - configurando a Internet
Iremos adicionar outro Router-PT. Nele teremos que adicionar mais 2 placas gigabitEthernet, assim como adicionar uma nova placa de rede no Router-ISP BATATA. Iremos interligar ambos Router-PT com um cabeamento na interface 7/0 (no packet só aceita cabo cross, pois são equipamentos iguais, na vida real não tem problema).
No Router batata colocamos:
#int g7/0
#ip address 1.1.1.1 255.255.255.252
#no shutdown
#end
#wr

Já no novo Router, iremos acessá-lo e defenir um hostname (que será **INTERNET**), e atribuir um ip a porta g7/0, e ligar a porta:
`hostname INTERNET`<br>
`int g7/0`<br>
`ip address 1.1.1.2 255.255.255.252`<br>
`no shutdown`<br>
`end`<br>

Podemos testar se fucionou executando um `ping 1.1.1.1`, para ter certeza que está chegando na outra ponta, no caso, o Router ISP Batata

Criaremos uma interface vitual no Router INTERNET, para simular como se fosse a Internet. Lembrando que essa interface não existe, ela é apenas vitual:<br>
`interface loopback 0`<br>
`ip address 8.8.8.8 255.255.255.0`<br>
`end`<br>
`wr`<br>

Agora no ISP Batata, usamos o comando `show ip route`, e veremos as rotas criadas. Percebemos que o ISP Batata apenas conhece as redes que vai até o CORE e a que vai para o Router INTERNET.

Precisamos criar uma caminho até os computadores, que estão nos SW de Acesso. Vamos adicionar as rotas até as redes dos computadores:<br>
`ip route 172.16.0.0 255.255.255.0 10.99.99.10` (temos que adicionar o próximo salto, que no caso é o IP do CORE que está configurado na interface vlan 500, logo após informarmos a rede que o ISP deverá mandar no caso a rede do FINANCEIRO)

>O próximo salto sempre tem que ser uma rede que seja possível alcançar.

Agora faremos para as outras redes:<br>
`ip route 172.16.10.0 255.255.255.0 10.99.99.10`<br>
`ip route 172.16.20.0 255.255.255.0 10.99.99.10`<br>
E também para o Data Center (DC):<br>
`ip route 10.0.0.0 255.255.255.0 10.99.99.10`<br>
`end`<br>
`show ip route`(para confirmarmos que foram criadas as rotas)<br>

Agora podemos realizar um ping, a partir do ISP Batata, até algum PC na rede, e é para termos um resultado positivo, inclusive no Data Center

Agora, para sairmos na Internet, iremos utilizar uma rota que é conhecida como rota Default. A rota Default é a última rota a ser escolhida, é o caminho que será escolhido em último caso. Como na Internet há um número gigantesco de redes, não faz sentido adicionar todas ao Router, pois não iria haver espaço para isso, além de ocupar processamento e tudo mais. Logo, essa rota Default é um mecanismo que se algum pacote chega, e o destino é uma rede diferente das que estão configuradas, uma rede que nao conheço, mandamos o tráfego para o provedor de serviços, ou seja, para Internet. Também conhecida como rota de último recurso.
Para definirmos essa rota Defaul, usamos no ISP Batata:<br>
`ip route 0.0.0.0 0.0.0.0 1.1.1.2` (definimos o próximo salto para a Internet, no caso, para o Router INTERNET, que seria o IP do provedor que está na outra ponta)<br>

Para confirmar rodamos o comando `show ip route`. Vemos que foi criada a rota **0.0.0.0**, mandando para **1.1.1.2**. Essa rota é uma rota estática (basta percebermos o S, indicando Static), é é uma rota estática defaul (basta percebermos o asterisco na frente do S - S*)

Podemos ir em um computador, abrir o prompt de comando e testar o ping em 8.8.8.8:<br>
`ping 8.8.8.8`<br>
Perceba que NÃO irá pingar! É normal, pois conseguimos chegar na Internet, mas a Internet não chega no computador, há uma rota para ir, mas não tem rota para voltar. Podemos enxergar melhor isso utilizando o comando `tracert 8.8.8.8` e percebemos que está parando logo após chegar no IPS Batata, no caso, no ip 10.99.99.1.
Isso deve-se ao fato que, quando criado a Internet, era para todos terem um IP válido, é um IP que você é identificado na Internet. Mas com a escassez dos IPs IPv4, foi criado grupos de IPs, os privados e os públicos, para ser utilizado em NAT (Netword Address Translation). A solução atual seria o uso de IPv6, mas no caso do exemplo, teremos que utilizar o NAT.

No ISP-Batata, na porta g6/0, que pertence a rede interna, usaremos:<br>
`interface g6/0`<br>
`ip nat inside`<br>

E na interface g7/0, que vai para a operadora (externo):<br>
`int g7/0`<br>
`ip nat outside`<br>

Agora precisamos informar ao NAT quais endereços que ele tem que "natear", no caso, tudo que for para a rota default:<br>
`access list permit any`<br>
`ip nat inside source list 1 interface g7/0` (tudo que bater na **inside source**, vai para a **interface g7/0**)<br>
`wr`<br>

Agora, vamos rodar o comando `show ip nat translation` no ISP-Batata. Repare que nada é mostrado ainda.

Vamos voltar lá no PC e executar novamente o **ping** em **8.8.8.8**. Repare que agora o ping retorna que está respondendo.

Rode `show ip nat translation` novamente e verá a tabela de NAT realizando a tradução! Abra outro PC e repita o teste. Será montado outra tabela, agora para o novo PC!

### Aula 8 - Resiliência (quem tem um, não tem nenhum!)

Como a rede está atrelada ao CORE1, iremos criar uma dedundância, iremos adicionar um SW **3650-24PS** ao lado do CORE 1. É necessário adicionar uma fonte para que ele funcione. Após isso, acesse e mude o nome dele para CORE 2 (`hostname CORE2`). Interligue o CORE1 e o CORE2 por meio das portas **g1/0/1**, tanto no CORE1 como no CORE2.

No CORE2, precisamos informar que a porta g1/0/1 será tronco (trunk):<br>
`int g1/0/1`<br>
`switchport mode trunk`<br>

Como o SW CORE2 será um backup do CORE1, ele precisa conhecer todas as vlans do CORE1, vamos adicioná-las. Podemos ver as vlans no CORE1 com o comando `show vlan brief`:<br>
`vlan 100`<br>
`name FINANCEIRO`<br>
`vlan 200`<br>
`name RH`<br>
`vlan 300`<br>
`name DIRETORIA`<br>
`vlan 400`<br>
`name DC`<br>
`end`<br>
`wr`<br>

Podemos confirmar com `show vlan brief`. Podemos confirmar o tronco, com `show interfaces trunk`, apenas para confirmar se a interface `g/0/1` está como tronco mesmo.

Agora criaremos um port channel entre o CORE1 e o CORE2, que nada mais é que a interligação de mais um cabo entre eles. Dessa forma eles terão redundância com 2 cabos, e além disso, o SW CORE entenderá que há apenas uma conexão entre eles, e caso as portas sejam de 1Gbps, o SW entenderá que essa porta agregada será de 2Gbps.

Pegaremos um cabo e ligaremos nas portas g1/0/2 em cada SW

Vamos deixar a porta **g1/0/2** do CORE2 como trunk:<br>
`interface gigabitEthernet 1/0/2`<br>
`switchport mode trunk`<br>

Lembrando que o estado normal, é que uma dos cabos fique off, pois isso evita um possível looping, de o CORE1 mandar broadcast, e esse broadcast voltar para o CORE2, e subsequentemente, o CORE2 também mandar broadcast, o que afetaria o CORE1, e por sua vez, voltaria ao CORE2

Para evitar isso, faremos o port channel, para que os cabos se agregem e formem um só, somando um link de 2Gbps

No CORE1, execute:<br>
`interface range gigabitEthernet 1/0/1-2` (range da porta 1 e 2 selecionados)<br>
`channel-group 1 mode active`(cria a interface virtual que agrupa duas interfaces físicas, chamada de 1)<br>
`end`<br>
`wr`<br>

Isso cria uma interface lógica com as duas interfaces físicas dentro! Agora temos que fazer o mesmo no CORE2. O id **1** pode ser igual ao anterior, não tem problema!

>Temos o `mode active` e `mode passive`. Podemos configurar **active** de um lado e **passive** do outro, ou ambos em **active**. O que NÃO podemos é colocar **passive** em ambos os lados. O **passive** em ambos os lados, os SWs vão aguardar um o outro, e não haverá a formação de port channel.

Para visualizarmos o que foi feito, usamos o comando: `show etherchannel summary`

Repare nesse summary que tem uma legenda **SU**. O **S** é de camada 2 (Layer 2) e o **U** indica que está em uso.

Podemos ver também mais informações no `show interfaces trunk`. Repare que caso queira fazer qualquer configuração nas portas 1/0/1 e 1/0/2, deverá acessar o port-channel, como por exemplo: `interface port-channel 1`

Se executarmos o comando `show interfaces port-channel 1`, é exibido o link de 2000Mbps.

### Aula 9 - STP Spanning tree Protocol

Agora vamos ligar o CORE2 nos SWs de acesso. Começando pelo quarto SW de acesso. Vamos ligar a porta **g1/0/6** do SW CORE2 na porta **g0/2** do quarto SW de acesso.

Temos que colocar a porta **g1/0/6** em modo trunk:<br>
`interface g1/0/6`<br>
`switchport mode trunk`<br>
`end`<br>
`wr`<br>

Se perceber na topologia, temos um looping acontecendo, entre os SW CORE2, O SW de acesso AC4 e o CORE1. Podemos verificar se o spanning tree está funcionando, indo no AC4, e digitando:
`show spanning-tree summary`<br>

Isso vai nos retornar algumas informações, como por exemplo a linha `Switch is in pvst mode`, que nos informa que o SW está rodando uma spanning-tree em modo pvst ( Per VLAN Spanning Tree Protocol). É como se houvesse um software/instrução em cada vlan. No AC4, temos duas vlans, logo, o pvst está ativo em ambas.

Para verificarmos o spanning-tree na vlan, usamos o comando (tomando o exemplo na vlan 100):<br>
`show spanning-tree vlan 100`<br>

Repare que o **State** (abreviado em Sts), está em modo **FWD** (modo de encaminhamento - Foward) em todas as portas, logo não temos nenhuma porta bloqueada. Temos que olhar em todos os equipamentos que estão gerando looping. Em algum deles devemos ter um Status **BLK** (Blocked)

Essa definição é basicamente feita pela priority, que quanto menos tem mais prioridade, e quando a priority for igual, é definido pelo menor númeore mac. Podemos pegar os retornos do comando `show spanning-tree vlan 100` e montar uma tabela:
|SW|Priory e MAC|É Root STP?|
|-----|---------|-----|
|AC4: |  BID = 32868-0004.9A85.7EB8 |Sim, é o Root STP |
|CORE2: | BID = 32868-0005.5E78.4320 |Não, **NÃO** é o Root STP |
>Repare na tabela que a prioridade (antes do traço) são iguais. E o começo do MAC Address pertencente ao AC4 é menor (0004) que o CORE2 (0005), logo o AC4 que está fazendo o papel de Root, para evitar o looping.

Essa não é uma situação ideal, pois não é legal atribuir essa função para um SW de acesso. O ideal é defenirmos o Root STP no SW CORE. Portanto definiremos manualmente que o Root STP será o CORE1, e o CORE2 será um backup.

Para definir isso, vamos até o CORE1, e executar:<br>
`spanning-tree vlan 100 priority 0` (0 é a menor prioridade possível!)<br>
Precisamos definir na vlan 200 também:
`spanning-tree vlan 200 priority 0`<br>
`end`<br>
`wr`<br>

No SW CORE2, vamos definir:
`spanning-tree vlan 100 priority 4096` (4096 é o menor valor depois do 0)<br>
`spanning-tree vlan 200 priority 4096`<br>
`end`<br>
`wr`<br>

Podemos confirmar, no CORE1, com o comando `show spanning-tree vlan 100` e procurar pela linha com o seguinte conteúdo: `This bridge is the root`.

No CORE2, podemos verificar também com o comando `show spanning-tree vlan 100` e vermos que o priority está com o valor 100. Mas colocamos o valor de 0 para ser o Root, por que aparece 100? Esse é um comportamento padrão, pois ele soma o 0 mais o valor da vlan, que no caso é 100, resultando em 100.

Rorando o comando `show spanning-tree vlan 100` no AC4, ele nos informa, por meio de uma tabela, que a porta G0/1 está ligada diretamente no Root, no caso o CORE1.

Tarefa: ligar as demais vlans com backup, mas usando o Core1 como Root.
