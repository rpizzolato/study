### Comutação
- movimentação, troca, substituição de um frame de uma porta para outra, por meio do endereço MAC e a tabela MAC, o que faz os switches serem rápidos e eficientes;
- a tomada de decisão da troca de um frame de uma porta para outra é baseada no cabeçalho da camada 2, e a comutação é realizada em hardware, e não software.

#### Tipos de Comutação
- **Store and Forward**: o frame é recebido e armazenado no buffer (é feito uma cópia). É feita uma checagem de erros e depois enviado, é padrão nas linhas mais recentes de switches Cisco (2960, 3560);
- **Cut-Through**: comum em linhas de switches de alta performance, como a linha Nexus (utilizada bastante em data center de alta performance). Examina apenas o MAC de destino e encaminha, muito rápido, mas não sabe se o frame está ou não corrompido. No entanto, as camadas superiores irão tratar caso haja erro.
- **Fragmentfree**: variação do cut-through. Assume que se houver uma eventual colisão, tem grandes chances de ser identificada nos 64 bytes iniciais do frame. Comum em switches antigos (1900 e 2900)

### Domínio de broadcast
- uma sala com 200 pessoas, e um professor dando aula a essas 200 pessoas, é um exemplo de domínio de broadcast. No entanto isso não é muito eficiente. No meio dessas 200 pessoas, pode haver um pequeno grupo conversando, colidindo com o que o professor está falando. O mais sensato é dividir essas 200 pessoas em 4 grupos de 50 pessoas, e colocar um professor para cada grupo.
- para que os grupos conversarem entre si, precisamos de um roteador, no entanto é importante frisar que roteador **não propaga broadcast**, propaga apenas **unicast**. No exemplo, seria como se um professor saísse da sala, e fosse até a outra sala conversar com o outro professor.

### Domínio de colisão
- em um HUB, o domínio de colisão ocorre em todas as portas, pois o HUB é um dispositivo que trabalha na camada 1 - L1. Já em um Switch, que trabalha na camada 2 - L2, cada porta é um único domínio de colisão. Um switch de 24 portas, temos 24 domínios de colisão, um HUB de 24 portas, temos um único domínio de colisão.
- o roteador segmenta/divide domínios de broadcast (lembre-se: **o roteador não propaga broadcast!**). O roteador é um dispositivo de camada 3 - L3. Já a questão de domínio de broadcast, funciona como um switch, cada porta é um domínio de broadcast.

### Loop de camada 2 (broadcast storm)
- supondo que uma empresa, no 1º andar, compre um switch e adicione 2 computadores e um link de internet. Depois de um tempo, a empresa expande para o 2º andar, e adiciona um novo switch com 2 computadores. Supõe ainda que a interligação entre esses 2 switches é feito com dois uplinks entre eles. Se um dos computadores do 2º andar, quiser se comunicar com algum computador do 1º andar, um frame é propagado, e copiado 3 vezes. Essas 3 vezes é devido ao número de portas ativas no switch do 2º andar (menos 1 porta que é do próprio dispositivo que propagou o frame). Portanto será propagado pelos 2 uplinks. Assim que chegar no switch do 1º andar, será feito uma cópia do frame e disparado um broadcast (anteriormente o frame já vinha como mensagem de broadcast), e novamente, propagado para as portas ativas (novamente, menos a porta que o frame chegou), e consequentemente o frame volta para o switch do 2 andar, propaga nas portas ativas, e como 2 dessas portas são uplinks, propaga novamente para o switch 1, e entramos em um looping, também conhecido como **broadcast storm**. No final das contas, o próprio computador que gerou o frame inicialmente, está recebendo o frame novamente, algo que não deveria acontecer jamais. Com isso o processamento do switch vai aumentar, a tabela MAC vai ficar confusa (também conhecido como trashing de tabela MAC)
- para inibir o loops

### STP (Spanning Tree Protocol - padrão IEEE 802.1D - Classic Spanning Three)

- server para evitar que loops em redes comutadas (em camada 2 - L2) ocorram;
- basicamente o STP fica monitorando a rede identificando eventuais conexões redundantes;
- se houver caminhos redundantes, o STP atuará elegendo um deles como primário e desativando os caminhos alternativos por meio do bloqueio de interfaces.
- os switches que tenham o protocolo STP ativo trocam informações de controle, chamado **BPDU** (Bridge Protocol Data Unit), que basicamente faz com que os switches fiquem conversando um com o outro, informando qual switch ele é, qual o custo, a prioridade, etc.
- é feito uma eleição entre os switches, o qual será eleito o switch raiz (root bridge), por meio das prioridades de cada switch. Os switches vem de fábrica com a prioridade **32768**, e como são iguais, a eleição leva em consideração um segundo critério, no caso, o MAC Address, sendo a **MENOR** prioridade eleito como switch root (lembrar: **MENOR** => **MELHOR** - caso de empate, o **MENOR** vai **GANHAR**! E consequentemente será o **ESCOLHIDO**)
- após a eleição, o switch que for designado como o **ROOT**, terá suas portas que fazem uplink designadas como **DESIGNATED PORT**, e **NUNCA** terão suas portas bloqueadas pelo STP.
- no ponto de vista dos demais switches, esses tem que ter definido o caminho menos custoso para chegar até o switch raiz, por meio do **path cost**.

Exemplo de um path cost:

| Path Cost  | Speed | Cost |
|-----------|----------|----|
| Path Cost  | 100Mb/s | 19 |
| Path Cost  | 1Gb/s | 4 |
| Path Cost  | 10Gb/s | 2 |

Se por exemplo, tivermos um 3 switches, A, B e C, sendo um deles o switch raiz (switch A), interligado nos outros dois switches (B e C), formando uma topologia em anel (ring). Os switches que estão diretamente conectados no switch raiz (A), via conexão gigabit (custo 4), terão um custo 4 para chegar até o switch raiz (A). Caso o 2º switch (B) passe pelo 3º switch (C), para assim chegar no switch raiz (A), temos que somar o custo, no caso, ficaria 4 + 4 = 8.
Portanto, as portas que **NÃO** são **DESIGNATED PORT** (do switch raiz), e que terão o **MENOR** custo para chegar no switch raiz, serão chamadas de **ROOT PORT**. IMPORTANTE mencionar que essas portas (**DESIGNATED PORT** e **ROOT PORT**) *<u>NÃO PODERÃO SER BLOQUEADAS</u>*

Sobram, portante, as portas entre os switches B e C, e analisando as prioridades, juntamente com o critério de desempate do MAC Address, será definido a porta a ser bloqueada. No caso, supondo que o MAC Address do switch B seja menor, ele ganha (lembra do **MENOR** => **MELHOR**), logo, a porta a ser bloqueada é a porta do switch C (**BLOCKED PORT** ou Portas Não Designadas), e a porta do switch B fica como **DESIGNATED PORT**. Os demais switches que não seja o **ROOT**, ou seja, B e C, serão **NON-ROOT BRIDGES**.

Portanto, resumidamente:
- Todas as portas do Switch raiz são designated ports. (forwarding state) - modo de encaminhamento;
- Outros Switches são denominados Non-Root Bridges;
- No caso dos Switches Non-Root, a interface com menor custo até o Raiz é denominada Root-Port e também estará em forwarding state;
- Em cenários com redundância de caminhos, uma ou mais portas estarão em modo block, as portas block são chamadas de non-designated ports;
- as portas em blocking mode não recebem nem enviam frames de dados, mas podem receber frames de controle STP (BPDUS¹), para saber se, em algum momento, elas precisarão ser reativadas.<br>
¹Quadros BPDUs são quadros especiais trocados pelos switches para formar a topologia em árvore que torna o Spanning Tree Protocol possível.

### STP Parte 2

Um breve resumo: supõe que temos 2 switches, o <u>00:00:00:AA:AA:AA</u> e o <u>00:00:00:CC:CC:CC.</u>

O switch AA:AA:AA está interligado com o switch CC:CC:CC, via porta F0/0 no AA:AA:AA que vai até a porta F0/1 do switch CC:CC:CC:, e via porta F0/1 no switch AA:AA:AA que vai até a porta F0/0 no switch CC:CC:CC.

Ambos os switches possuem prioridade 32768 padrão, logo a escolha do switch raiz será por meio do menor MAC Address, assim, ganhando o switch AA:AA:AA, e sendo nomeado como o Root (menor BID - Bridge ID é eleito Root - **IMPOSSÍVEL EMPATE!!**).

As portas que fazem o uplink, são conhecidas como designated port. E como são duas portas de uplink interligando os dois switches, eles, sem o STP, entrariam em looping. Para evitar esse looping, o switch precisa definir a Root Port, a qual é, primeiramente, definida pelo custo de chegar até o switch Root Bridge (Path Cost).

Como estão interligados diretamente, o custo será o mesmo. Com o custo empatado, o STP opta pelo caminho conectado com menor BID (lembrando que o padrão é 32768). Depois de tentar pelo custo e pelo BID,então o <u>STP irá escolher a porta com menor número do switch vizinho (partindo do switch CC:CC:CC, que é o switch que quer chegar no Root Bridge)</u>.

Logo o switch irá eleger a porta F0/1 do switch CC:CC:CC que interliga na porta F0/0 do switch AA:AA:AA, e irá bloquear a porta F0/0 do switch CC:CC:CC que interliga na porta F0/1 do switch AA:AA:AA (lembrando que bloqueia os frame de dados, mas os frames de verificação STP ficam executando para detectar mudanças).

>Importante
>
>As porta bloqueadas não ficam no switch Root!

#### Modos STP de Operação das Portas de um Switch
- **BLOCKING**: Não encaminhará frames. Pode receber e analisar BPDUS (para saber informações de outros switches, como Root Bridge, qual porta bloquear, etc). Todas as portas de um switch encontram-se em modo blocking quando ele é ligado;
- **LISTENING**: envia, recebe e analisa BPDUS para verificar a topologia da rede antes de começar o encaminhamento de frames. <u>Não encaminhará frames</u>;
- **LEARNING**: registra os endereços dos hardwares conectados às interfaces e forma a tabela MAC. <u>Não encaminha frames</u>;
- **FORWARDING**: envia e recebe frames de dados e BPDUS normalmente;
- **DISABLED**: a interface está em modo inativo e não encaminha ou recebe qualquer tipo de frames ou participa do processo STP.

#### Observações
- tipicamente, portas de um switch se encontram ou no modo blocking ou no modo forwarding;
- uma porta em modo forwarding será sempre uma Root Port ou Designated Port;
- se houver alteração na topologia, todas as portas STP do switch retornarão aos modos listening e learning. (Ex. Falha de Link ou adição de outro switch na rede);
- se o STP por algum motivo determinar que uma porta não designada (em modo Blocking) deve tornar-se uma porta designada, esta entrará em modo listening, analisando todas as BPDUS recebidas para certificar-se de que não criará um loop (o que torna o STP um protocolo lento).

#### Convergência STP
- Tempo gasto para a porta sair do modo blocking e entrar no modo forwarding;
- Não há transmissão de dados durante o processo, só troca de informações STP;
- Assegura que todos os switches estejam em perfeito sincronismo;
- Leva em média 40 segundos.

### STP Portfast e BPDU Guard
- Em vários casos determinadas portas não precisarão participar do processo STP (Ex. Servidores, Computadores, Impressoras, etc);
- O Recurso Portfast exclui a porta configurada com ele do processo STP, ficando permanentemente em modo forwarding. Não precisa esperar o tempo de convergência (40 segundos);
- Deve ser utilizado apenas em portas de acesso/untagged (conectadas a dispositivos finais);
- Para evitar problemas causados pela eventual conexão de switches às portas configuradas com esse recurso (como forçar a inserção de um switch root na rede), a Cisco criou o "**BPDU Guard**", que bloqueia uma porta configurada com portfast se uma BPDU for recebida por ela. (Apenas switches deveriam gerar BPDUS).

#### Spanning Tree UplinkFast (da Cisco)
- Identifica um caminho alternativo para o switch root (o segundo caminho com menor custo) e o adiciona a um grupo chamado "UPLINK GROUP";
- caso o link primário falhe, o link secundário será imediatamente ativado, sem passar pelos modos "listening" e "learning";
- convergência cai de 40 segundos para 1 segundo, no caso de falhas de uplinks;
- proprietário da Cisco;

#### Spanning Tree BackBoneFast (da Cisco)
- funciona de forma contrária ao UplinkFast e pode ser aplicado em todos os switches da rede;
- análise mais eficiente de falhas indiretas fazendo com que o switch precise de menos tempo para compreender o que houve com a rede e como ele deve agir;
- o uplink fast ajuda os switches que não são root a achar o caminho alternativo de forma mais rápida para o switch root, caso a porta root caia. O backbone fast é o contrário, sentido root para os não root. Só é utilizada quando tenho mais de um uplink para o mesmo equipamento;
- pode reduzir em até 20 segundos o tempo de convergência;
- proprietário da Cisco;

#### Rapid Spanning Tree 802.1W (protocolo aberto)
- Protocolo STP "tunado" do IEEE que incorpora todas as melhorias citadas anteriormente e ainda funciona em switches de qualquer fabricante;
    - Os modos de Operação das portas foram alterados:
        - **DISCARDING**
        - **LEARNING**
        - **FORWARDING**
    - Tipos de Portas:
        - **ALTERNATE**: porta que entra em atividade caso a root port falhe;
        - **BACKUP**: porta que entra em atividade caso a designated port falhe;
        - **EDGE** (access/untagged): normalmente a porta que conecta um host.
- comando para ativar o Rapid STP: `spanning-tree mode rapid-pvst`

### Vlan
Considerando que em uma rede comutada temos um único domínio de broadcast, e que cada element oda rede consegue enxergar todos os demais, e quanto mais uma rede cresce, maior o volume de frames broadcast, há uma solução que é a divisão da rede em redes menores (separadas logicamente), as Vlans.

<u>Domínios Lógicos definidos em switches</u>: é uma forma de conseguirmos segmentar um grande domínio de broadcast (uma LAN) sem a necessidade de utilizarmos um elemento de camada 3 (Roteador);

As máquinas associadas a outras Vlans não terão acesso a esses frames, mesmo que estejam fisicamente conectadas ao mesmo switch. Logo teremos algumas vantagens, como:
- segmentação de domínios de broadcast;
- agrupamento lógico de usuários e recursos;
- flexibilidade e escalabilidade;
- melhor desempenho e gerenciabilidade;
- melhor segurança e controle

#### Identificação de Vlans
- **PORTAS DE ACESSO (UNTAGGED/ACCESS)**: geralmente portas conectadas aos dispositivos finais (PCs, Impressoras, Servidores, etc). Pode ser associada a uma única vlan. Dispositivos conectados a portas de acesso não conseguem se comunicar na camada de enlace com dispositivos em outras vlans;
- **PORTAS DE TRANSPORTE (TRUNK PORT/TAGGED)**: normalmente utilizadas em uplinks entre switches, de forma simplista seria como associar uma única porta a várias vlans simultaneamente

#### Tipos de associações Vlan
- **ESTÁTICO**: Vlans são, tipicamente, criadas manualmente (modo estático). Esse seria o modo que temos que entrar na porta que queremos associar uma vlan (usado 99% das vezes)
- **DINÂMICO**: podemos associar uma vlan a uma determinada porta de forma dinâmica por meio de um servidor centralizado que mapeia determinadas informações a determinadas vlans.
    - Requer um servidor VMPS (Vlan Management Policy Server);
    - Útil em redes com alta mobilidade de dispositivos;
    - É possível a associação de vlans específicas a endereços MAC, protocolos, aplicações e até login de usuário

### Vlan TAG
- O processo de identificação de frames (frame tagging) insere no cabeçalho do frame Ethernet um campo que permite essa identificação (vlan ID ou vlan COLOR);
- Esse campo adicional é inserido **APENAS** quando um frame precisa ser encaminhado através de uma porta de transporte (porta tronco), e é removido assim que o frame atravessa o tronco (em portas ACCESS/UNTAGGED não é necessário, pois o switch já identifica);
- Dessa forma, as máquinas finais não recebem frames com este campo adicional, não possuindo nenhuma informação sobre qual vlan pertencem.

O campo de identificação adicionado ao frame original tem um "tamanho". Se interfaces de rede comuns recebem frames com esse campo adicional, não vão compreender a "modificação" e vão pensar que o frame não está íntegro, descartando-o. Esse tamanho pode fazer com que o frame ultrapasse o tamanho padrão de **1518 bytes** definidos pelo padrão Ethernet (geralmente em switches mais antigos)

O método de identificação de vlans é o DOT1Q (802.1Q - IEEE), que é uma padrão criado pela IEEE alterando o cabeçalho original inserindo a informação sobre a Vlan (para identificá-la)

#### Frame tagging
- Como já sabemos, o campo de identificação adicionado ao frame original tem um "tamanho";

<table border="1">
<thead><tr><th colspan="5">Frame Ethernet tem um tamanho de 18 bytes</th></tr></thead>
<tr>
<td>Destination Address</td>
<td>Source Address</td>
<td>Type/Len</td>
<td>Data</td>
<td>Frame Check</td>
</tr>
</table>

<table border="1">
<thead><tr><th colspan="9">Método de Identificação de Vlans DOT1Q (802.1Q) - Adiciona mais <u>4 bytes</u> (adicionado dentro do frame tradicional)<br>Total: 22 bytes</th></tr></thead>
<tr>
<td>Destination Address</td>
<td>Source Address</td>
<td style="background-color: #FA3004; font-weight: bold">Tag Protocol ID (16 bits)</td>
<td style="background-color: #FA3004; font-weight: bold">User Priority (3 bits para fazer QoS)</td>
<td style="background-color: #FA3004; font-weight: bold">Caconical format indicator (1 bit)</td>
<td style="background-color: #FA3004; font-weight: bold">Vlan ID (12 bits = 2¹² = 4096 Vlans)</td>
<td>Type/Len</td>
<td>Data</td>
<td>Frame Check</td>
</tr>
</table>

Reforçamos que se caso uma interface de rede comum receber esse frame modificado com o campo adicional, não irá compreender a "modificação" e irá pensar que o frame não está íntegro e irá descartá-lo.

#### Vlan nativa
É a vlan que não precisa ser identificada, ou seja, frames originados nessa vlan não precisam ser "tagueados" quando atravessam um link de transporte (porta trunk/tronco). Repare que é exatamente o contrário que acontece com uma vlan com tag.

Normalmente, a vlan nativa, por padrão é a vlan 1, no entanto ela pode ser alterada. Caso ela seja alterada em algum equipamento, essa alteração deve ser replicada para os demais equipamentos, senão teremos problemas.

Todas as portas inicialmente estão nessa vlan. Usado em dispositivos que não conseguem fazer tag de vlan ou associação de vlan, e temos que adicioná-los na vlan nativa. O mais importante que devemos saber é que a vlan nativa **NÃO** é "tagueada" quando atravessam um link de transporte (única vlan que isso ocorre - e precisa que todos equipamento estejam com o mesmo número, no caso, padrão 1)!

### Router on a stick (Roteamento entre Vlans)

- Dispositivos na mesma Vlan estão no mesmo domínio de broadcast, na mesma rede, e portanto podem ser comunicar.
- Dispositivos em Vlans diferentes **NÃO** se comunicam!
- Em uma situação comum, para interligarmos duas vlans distintas (L2), teríamos que utilizar um roteador (L3), ou seja, teríamos que ocupar mais duas portar no switch, para interligar no roteador, e esse por sua vez, realizar o roteamento entre as vlans (imagine 10 vlans, teríamos que ter mais 10 portas para o roteador, totalmente inviável!);
- para contornar o problema apontado no item anterior, normalmente usamos uma única porta para o roteador (por isso o nome router on a stick), a qual criaremos sub-interfaces na interface principal, a qual será utilizada como tronco para passagem das vlans necessárias. Um exemplo seria, utilizarmos a porta F0/0 do roteador, e criarmos 2 sub-interfaces, como F0/0.10 e F0/0.20, que irão interligar duas vlans, utilizando apenas uma porta no switch.

### VTP (Vlan Trunk Protocol)

- Criado pela Cisco para gerenciar e manter a consistência de todas as vlans configuradas na rede;
- Um domínio VTP nada mais é que um conjunto de switches que trocam informações VTP entre eles;
- Teremos um switch que fará o papel de VTP Server, que basicamente centraliza todo o processo de criação e alteração de vlans;
- Switches clientes recebem informações do server e não podem adicionar nem alterar vlans;
- Não é muito usado, pois por ser proprietário da Cisco, se colocarmos outro switch na rede, de outra marca, não irá funcionar.
- Outro motivo por não ser usado é devido ao perigo de, ao se adicionar outro switch (Cisco) na rede, ele entrar como Server e "zerar" os demais, com a configuração de fábrica.
- Ao configurar uma nova vlan no switch server, ele muda o **vtp revision number**, que inicialmente começa em 0, e após as alterações no server, esse número muda e os demais switches se atualizam conforme verificação a nesse número (para qualquer alteração sofrida por um switch em modo servidor VTP, é propagada para todo o domínio VTP com um número de revisão de atualização igual ao último recebido +1 - incremental).
- Basicamente para configurá-lo, escolhemos o switch que vai ser o server e executamos o comando:<br>
`sw-server# vtp domain CCNA`<br>
`sw-server# vtp mode server`<br>
- E nos switches que serão clientes, usamos o comando:<br>
`sw-1# vtp domain CCNA`<br>
`sw-1# vtp mode client`<br>

#### Modos de Operação VTP
- **Server**: 
    - modo padrão de todos os switches Cisco;
    - um switch em modo server é capaz de criar, excluir ou modificar vlans em um domínio VTP;
    - é necessário ao menos um servidor em um domínio VTP, mas vários servidores podem coexistir;
    - qualquer alteração sofrida por um switch em modo servidor VTP é propagada para todo o domínio VTP com um número de revisão de atualização igual ao último recebido +1;
- **Client**:
    - os switches recebem informações de servidores VTP, verificam os números de revisão de atualização e aceitam a que tiver o maior número;
    - switches clientes não podem efetuar mudanças. Atualizações recebidas são também propagadas para switches vizinhos que estejam no mesmo domínio VTP;
    - 
- **Transparent**:
    - o switch não modifica suas configurações com base nas atualizações VTP recebidas, mas ainda assim os encaminha para switches vizinhos;
    - podem adicionar, excluir ou modificar vlans livremente, mas não propagam suas próprias configurações para o domínio VTP.
- **OFF**:
    - semelhante ao modo transparente, porém não encaminha atualizações VTP recebidas para outros switches vizinhos.

#### VTP - Vlan Trunk Protocol Review - Top 7
1. um switch pode trocar frames vtp apenas com outros switches configurados no mesmo domínio, e são sempre encaminhados via portas de transporte (trunk/tagged);
2. frames VTP contêm várias informações de controle, como o domínio VTP. O número de revisão da configuração (configuration revision number). A senha do domínio e as vlans conhecidas;
3. senhas podem ser definidas para aumentar o controle do domínio VTP. Todos os switches pertencentes a um mesmo domínio devem ser configurados com a mesma senha;
4. Todos os switches cisco têm, por padrão, o protocolo VTP ativado e encontram-se pré configurados no modo server;
5. quando um switch em domínio VTP recebe uma atualização com um número de revisão mais alto do que a última recebida, ele sobrescreve seu banco de dados com as novas informações trazidas por ela;
6. não é muito utilizado na prática: **proprietário** e **perigoso**;
7. **vantagens**: administração centralizada, controle, consistência das informações.

#### VTP Pruning (PODA)

- quando o pruning está ativado, frames com destino a vlans são apenas encaminhados para switches que tenham portas de acesso associadas a essas vlans. Exemplo: temos um switch com as vlans 10, 20 e 30. No entanto tempos apenas a vlan 20 associada a alguma porta que vai a outro switch, nesse outro switch iremos receber apenas a vlan 20, as demais são "podadas" (pois se em uma porta associada há apenas a vlan 20, não há motivo para ter as outras vlans, 10 e 30). Resumidamente, um frame da vlan 20 não será encaminhado a um switch que não tenha pelo menos uma porta na vlan 20

> Importante
>
> Versão 1 e 2, precisamos habilitar o recurso no switch VTP server. Na versão 3, temos que habilitar em cada switch.

### PVST (Pen Vlan Spanning Tree)
- Quando temos uma rede com várias vlans, podemos criar uma instancia do STP para cada vlan existente.
- Imagine 3 switches interligados em topologia de anel, switch A, B e C. Em um STP comum, teríamos um link entre o switch A e B interrompido pelo STP, agora imagine esse link sendo um link de 10G/s, o desperdício que seja deixar esse link "interrompido".
- Pense agora o switch A como o **Root Vlan 100**, e o switch B como **Root Vlan 200**. O tráfego da Vlan 100 são encaminhados ao switch A, portanto temos a interligação de B para A e C para A. Se definirmos o switch B como Root da vlan 200, temos que o tráfego é mandado de A para B e C para B.
- Dessa forma podemos utilizar o Link de B para C ou vice-versa, criando duas topologias lógicas diferentes, uma para cada vlan, e agora todos os links são utilizados.

### Etherchannel (arquitetura de alta disponibilidade)
- é quando **agrupamos links** ethernet para criar um único canal virtual cuja largura de banda equivale à soma das larguras de banda dos links que o compõem. Ex. Se temos 2 links de 1 Gb/s, com o etherchannel, teremos um link virtual de 2 Gb/s. Lembrando que as duas interligações tem que ser nos mesmos switches, e essa técnica acaba "enganando" o spanning tree;
- apenas portas com características **idênticas** em ambas as pontas (velocidade, duplex, STP, Vlan, Vlan Nativa);
- caso a configuração do etherchannel não estiver consistente, podemos causar loops L2 na rede;

Dois Protocolos de negociação para a formação de links etherchannels foram criados:
1. **PAGP (Port Aggregation Protocol)**: proprietário da cisco (em vias de ser descontinuado)
2. **LACP (Link Aggregation Protocol)**: do IEEE (Padrão 802.3AD)

> Observação
>
> Podemos usar a opção de "forçar" o canal virtual manualmente (MODO ON), sem utilizar algum protocolo para negociação, no entanto não é recomendado pois é muito suscetível a loop.

É preciso DETERMINAR como as interfaces participantes se comportarão no que se refere ao processo de negociação. Em cada protocolo temos dois modos (e as portas devem estar com o mesmo protocolo - LACP e PAGP **NÃO** se conversam):
- <u>LACP</u>:
    - **Active**: inicia a comunicação
    - **Passive**: espera que alguém comece a comunicação.
- <u>PAGP</u>:
    - **Desirable** (é o mesmo que **Active**)
    - **Auto** (é o mesmo que **Passive**)

Podemos ter em ambos os lados as combinações:
- Active / Active
- Active / Passive
- e vice-versa<br>
Mas **NÃO PODEMOS** ter ambos os lados Passive / Passive (e nem Auto / Auto, no caso do PAGP), pois ambos ficam aguardando alguém iniciar a comunicação. Dessa forma apenas respondem as solicitações, não inicia a comunicação, por isso é impossível estabelecer um etherchannel com as duas pontas em modo PASSIVO (LACP) ou AUTO (PAGP).

Para configurarmos um etherchannel, basicamente usamos os seguintes comandos (considerando que temos 2 switches interligados nas portas range f0/1-2):

Switch A<br>
`SW1# configure terminal`<br>
`SW1 (config)# interface range f0/1-2`<br>
`SW1 (config-if-range)# no shut`<br>
`SW1 (config-if-range)# channel-group 1 mode active`<br>

Switch B<br>
`SW1# configure terminal`<br>
`SW1 (config)# interface range f0/1-2`<br>
`SW1 (config-if-range)# no shut`<br>
`SW1 (config-if-range)# channel-group 1 mode passive`<br>

Podemos consultar as configurações de etherchannel com o comando:<br>
`SW1# show etherchannel summary`<br>

> Observação
>
> A configuração apresentada foi utilizando o protocolo **LACP**. Poderíamos usar o **PAGP**, bastando apena mudar os comandos de <u>active</u> para <u>desirable</u> e <u>passive</u> para <u>auto</u>.

### TCP/IP
#### Camadas TCP/IP em relação ao modelo OSI

<table border="1">
<thead><tr>
<th colspan="2">MODELO OSI</th>
<th colspan="2">TCP/IP</th>
</tr></thead>
<tr>
<td>7</td>
<td>Aplicação</td>
<td rowspan="3">4</td>
<td rowspan="3">Aplicação</td>
</tr>
<tr>
<td>6</td>
<td>Apresentação</td>
</tr>
<tr>
<td>5</td>
<td>Sessão</td>
</tr>
<tr>
<td>4</td>
<td>Transporte</td>
<td>3</td>
<td>Host-To-host</td>
</tr>
<tr>
<td>3</td>
<td>Rede</td>
<td>2</td>
<td>Internet</td>
</tr>
<tr>
<td>2</td>
<td>Enlace</td>
<td rowspan="2">1</td>
<td rowspan="2">Network Interface</td>
</tr>
<tr>
<td>1</td>
<td>Fisica</td>
</tr>
</table>

#### Camada de Aplicação (4)
- **TELNET**: aplicação para acesso remoto. O nome vem de telephone network. Desenvolvido pelo DOD em 1969
- **FTP/TFTP**: File Transfer Protocol/Trivial FTP. O FTP pode ser utilizado por outras aplicações como base para a transferência de arquivos. No modo aplicação é utilizado no formato Cliente-Servidor para executar a transferência de arquivos. O TFTP é uma versão mais simples e leve, usado para transferir imagem em equipamentos de rede.
- **DHCP**: Dynamic Host Configuration Protocol, permite a configuração dinâmica de elementos conectados a uma rede, via concessão de endereços IP, Máscara, Default Gateway, DNS e algumas outras opções. O sucessor do antigo protocolo BOOTP.
- **HTTP**: Hypertext Transfer Protocol, é a base para comunicação de dados na Internet. Hipertexto é uma forma estruturada de texto que utiliza ligações lógicas chamadas hiperlinks para outras fontes de informações existentes na Web. Dessa forma, páginas em um site são associadas a outras. (Core da Internet)
- **SMTP**: Simple Mail Transfer Protocol - Protocolo padrão para envio de e-mails por meio da Internet.
- **POP3**: Post Office Protocol, é um protocolo utilizado no acesso remoto a uma caixa de correio eletrônico.
- **SNMP**: Simple Network Management Protocol, é o protocolo padrão para a gerência de redes TCP/IP.
- **SYSLOG**: aplicação servidora que recebe mensagens de Log de outros dispositivos de rede, armazenando-as e possibilitando o tratamento posterior.

#### Camada Host-To-host - Transporte TCP (3)

- **TCP (Transmission Control Protocol)**: recebe um fluxo de dados da camada de aplicação e os quebra em partes menores, chamadas segmentos;
- Os segmentos são numerados, permitindo a montagem do fluxo original na camada de aplicação da máquina de destino. Ou seja, o essas parte menores são numeradas e encaminhadas, e não precisam ir pelo mesmo caminho, sendo apenas no destino, montado as partes fragmentadas. Caso haja uma única parte faltando, significa que o pacote está corrompido.
- É orientado a conexão, ou seja, é confiável, no entanto é lento. <u>Confiável</u>> **NÃO** é ser<u>Seguro</u>! Para ser seguro, geralmente precisa-se de Criptografia!
- Utilizado em transferência de arquivos, por exemplo.
- usa 3-WAY HAND-SHAKE

#### 3-WAY HAND-SHAKE

- Método utilizado no TCP para criar uma nova conexão entre o Host/Cliente e o Servidor;
- É um método de 3 passos que requer que o cliente e o servidor troquem mensagens de **SYN** (SYNCHRONIZATION) e **ACK** (ACKNOWLEDGMENT) antes que o dado seja realmente transmitido.

<u>Exemplo de um cliente/servidor:</u>
1. Cliente manda um SYN
2. Servidor responde com um SYN/ACK
3. Cliente responde com ACK


Cabeçalho TCP (Transporte) = Tamanho Total: **20 bytes**
<table border="1">
  <tr>
    <th colspan="5">Porta Lógica de origem (16bits = 65536 portas)</th>
    <th colspan="6">Porta Lógica de destino (16bits = 65536 portas)</th>
  </tr>
  <tr>
    <td colspan="11">Sequence Number</td>
  </tr>
  <tr>
    <td colspan="11">Acknowledgment Number</td>
  </tr>
  <tr>
    <th>Data Offset</th>
    <th>Reserved</th>
    <th>URG</th>
    <th>ACK</th>
    <th>PSH</th>
    <th>PST</th>
    <th>SYN</th>
    <th>FIN</th>
    <th colspan="2">Flags</th>
    <th colspan="4">Window Size</th>
  </tr>
  <tr>
    <td colspan="5">Checksum</td>
    <td colspan="6">Urgent Pointer</td>
  </tr>
  <tr>
    <td colspan="6">Options</td>
    <td colspan="5">Padding</td>
  </tr>
   <tr>
    <td colspan="11">Dados (payload)</td>
  </tr>
</table>

Na camada de transporte, as portas são categorizadas em três faixas principais: portas bem conhecidas (well-known ports), portas registradas (registered ports) e portas dinâmicas/privadas (dynamic/private ports)

1. Portas Bem Conhecidas (Well-Known Ports)
  - Faixa: 0 a 1023
  - Uso: Reservadas para serviços e aplicações mais comuns e amplamente utilizados.
  - Exemplos:
    - HTTP: Porta 80
    - HTTPS: Porta 443
    - FTP: Portas 20 e 21
    - SSH: Porta 22
    - Telnet: Porta 23
    - SMTP: Porta 25
    - DNS: Porta 53

2. Portas Registradas (Registered Ports)
  - Faixa: 1024 a 49151
  - Uso: Atribuídas a serviços e aplicações menos comuns, mas que ainda precisam de um identificador único.
  - Exemplos:
     - MySQL: Porta 3306
     - PostgreSQL: Porta 5432
     - Microsoft SQL Server: Porta 1433
     - RDP (Remote Desktop Protocol): Porta 3389

3. Portas Dinâmicas/Privadas (Dynamic/Private Ports)
  - Faixa: 49152 a 65535
  - Uso: Usadas temporariamente por aplicações para comunicação cliente-servidor. Essas portas são frequentemente atribuídas dinamicamente pelo sistema operacional quando uma conexão de rede é iniciada.
  - Exemplos: Utilizadas por clientes ao estabelecer conexões de saída.

Resumo em Tabela
|Categoria |	Faixa |	Descrição |	Exemplos |
|--|--|--|--|
|Portas Bem Conhecidas |	0 - 1023 |	Reservadas para serviços padrão e protocolos mais conhecidos |	HTTP (80), HTTPS (443), FTP (20/21) |
|Portas Registradas |	1024 - 49151 |	Atribuídas a serviços e aplicações menos comuns, mas ainda reconhecidas |	MySQL (3306), PostgreSQL (5432) |
|Portas Dinâmicas/Privadas |	49152 - 65535 |	Utilizadas dinamicamente por aplicações para conexões temporárias e comunicação cliente-servidor |	- |

#### Camada Host-To-host - Transporte UDP (3)

- **UDP (User Datagram Protocol)**: o UDP surgiu após o TCP. Basicamente o UDP seria uma versão simplificada do TCP. O cabeçalho possui poucos campos de controle.
- é rápido, mas não confiável;
- não sequencia os segmentos, não aguarda uma confirmação de recebimento, simplesmente ele transmite;
- aplicações em tempo real (VoIP) e aplicações que não precisam de confiabilidade de entrega (é melhor uma transmissão em tempo real perder um pixel ou uma ligação "picotar" em um breve trecho do que termos delay na transmissão);

Cabeçalho UDP (Transporte) = Tamanho Total: **8 bytes**
<table border="1">
  <tr>
    <th>Porta Lógica de origem (16bits = 65536 portas)</th>
    <th>Porta Lógica de destino (16bits = 65536 portas)</th>
  </tr>
  <tr>
    <td>Length</td>
    <td>Checksum</td>
  </tr>
  <tr>
    <td colspan="2">Dados (Payload)</td>
  </tr>
</table>

#### TCP vs UDP
<table border="1">
  <tr>
    <th>TCP</th>
    <th>UDP</th>
  </tr>
  <tr>
    <td>Numera e sequencia os segmentos</td>
    <td>Não os numera ou sequencia</td>
  </tr>
  <tr>
    <td>Comunicação confiável, com a confirmação de "lotes" de segmentos recebidos pelo destino</td>
    <td>Comunicação não-confiável</td>
  </tr>
  <tr>
    <td>Comunicação orientada à conexão (o processo de 3-way handshake e estabelece um circuito virtual)</td>
    <td>Não orientado à conexão</td>
  </tr>
  <tr>
    <td>Latência de transmissão mais elevada devido ao maior tamanho de cabeçalho (20 bytes)</td>
    <td>Baixa latência (cabeçalho menor, 8 bytes)</td>
  </tr>
  <tr>
    <td>Analogia: Carta registrada ("mandou, chegou")</td>
    <td>Analogia: Carta comum (melhor esforço, ou seja, não há garantia de que a carta chegará)</td>
  </tr>
</table>

### Portas lógicas e Sockets
- TCP e UDP utilizam portas lógicas e sockets (encaixes) para o gerenciamento das conexões entre as diversas aplicações;
- 2¹6 (65536) portas de conexões disponíveis por host;
- porta de destino e origem;
- **socket** é a identificação de sessão gerada pela **camada de transporte** no host, definido pelo PAR: **PORTA DE DESTINO** + **IP DE DESTINO**;
- sendo de 0 a 1023 portas bem conhecidas, associadas a aplicações que operam em moo servidor;
- FTP 21, TELNET 23, SMTP 25, HTTP 80, POP3 110, DNS 53, TFTP 69, SNMP 161, SYSLOG 514

Exemplo: temos o computador A, que está com Firefox aberto, na camada de aplicação, com a porta 30521, aberta na camada de transporte, com o IP 172.16.10.3, na camada de rede. 

<table border="1">
  <tr>
    <th colspan="2" class="text-center">PC 1</th>
    <th>SOCKET</th>
    <th>Caminho pela rede</th>
    <th>SOCKET 1</th>
    <th>Server WEB HTTP</th>
  </tr>
  <tr>
    <td>Aplicação</td>
    <td>FIREFOX</td>
    <td rowspan="3">10.0.0.20:80</td>
    <td rowspan="3">Router</td>
    <td rowspan="3">172.16.10.3:30521</td>
    <td>Web Server HTTP</td>
  </tr>
    <tr>
    <td>Transporte</td>
    <td>30521</td>
    <td>80</td>
  </tr>
  <tr>
    <td>Rede</td>
    <td>172.16.10.3</td>
    <td>10.0.0.20</td>
  </tr>
</table>

Se tentarmos acessar o PC Servidor WEB, que está com o IP 10.0.0.20, na porta 80, iremos criar um socket entre o PC cliente e o pc servidor.

Da mesma forma, se tivermos um outro computador, com o Chrome aberto, na porta 40501, com o IP 192.168.0.6, querendo acessar nosso mesmo servidor web, que fica no 10.0.0.20, iremos criar um novo socket interligando:

<table border="1">
  <tr>
    <th colspan="2" class="text-center">PC 2</th>
    <th>SOCKET</th>
    <th>Caminho pela rede</th>
    <th>SOCKET 1</th>
    <th>Server WEB HTTP</th>
  </tr>
  <tr>
    <td>Aplicação</td>
    <td>CHROME</td>
    <td rowspan="3">10.0.0.20:80</td>
    <td rowspan="3">Router</td>
    <td rowspan="3">192.168.0.6:40501</td>
    <td>Web Server HTTP</td>
  </tr>
    <tr>
    <td>Transporte</td>
    <td>40501</td>
    <td>80</td>
  </tr>
  <tr>
    <td>Rede</td>
    <td>192.168.0.6</td>
    <td>10.0.0.20</td>
  </tr>
</table>

Nesse momento, nosso servidor web tem aberto 2 sockest, um para cada computador cliente

### IP (Internet Protocol)
- Pertence a camada 3, camada de REDE;
- identificação lógica de redes e elementos a elas conectados;
- na camada 2, temos a identificação física, por meio do MAC, na camada 3 teremos a identificação lógica de redes e elementos a ela conectados;
- é aqui que acontece o ROTEAMENTO dos pacotes de dados de uma domínio lógico para outro;
- nenhuma das camadas superiores ou inferiores interferem no processo de roteamento de pacotes;
- o roteamento é sempre baseado no identificador da rede, sendo o identificador (lógico), o IP. Portanto o ROTEAMENTO SEMPRE acontece de acordo com o IP;
- Método unificado de identificação, garantindo compatibilidade entre os diferentes tipos de protocolo/tecnologia da camada de enlace (Ethernet, Frame-Relay...)

#### A camada Internet (Rede)
- Protocolos importantes, existe para ajudar o IP:
  1. Internet Protocol (IPv4 ou IPv6);
  2. Internet Control Message Protocol (ICMP);
  3. Address Resolution Protocol (ARP);
  4. Reverse Address Resolution Protocol (RARP);

  Temos que o IP, é a base da comunicação, e os demais (ICMP, ARP, RARP) são suporte ao IP:

  <table border="1">
  <tr>
    <th rowspan="2">INTERNET (REDE)</th>
    <th>ICMP</th>
    <th>ARP</th>
    <th>RARP</th>
  </tr>
  <tr>
    <td colspan="3" align="center">IP</td>
  </tr>
</table>

Portanto o IP é o "chefão" da camada 3 (rede) e é:
- um identificador lógico;
- endereço identifica de onde um pacote de dados vem, e para onde ele deve ir;
- dividido em duas partes, **host** e **rede**;
- qual rede ele pertence, e dentro desta rede, qual a sua identificação de host;
- <u>IPv4 possui 20 bytes de extensão</u>;
- <u>Os outros protocolos da camada de rede existem apenas para suportá-lo</u>

#### IP (Internet Protocol)
- o protocolo IP recebe os segmentos da camada de transporte e os encapsula em pacotes;
- os pacotes recebem um cabeçalho IP com uma série de campos de controle, dentre eles, endereço IP de origem e destino;
- os roteadores fazer a análise do endereço IP de destino, identificam a porção de rede deste endereço e com base em sua tabela de roteamento, escolhem a melhor rota para alcançar a rede remota.

Cabeçalho IP = Tamanho Total: **20 bytes**
<table border="1">
  <tr>
    <td align="center">Version</td>
    <td align="center">Header Lenght</td>
    <td align="center">Priority and type of service</td>
    <td align="center">Total Length</td>
  </tr>
  <tr>
    <td colspan="2" align="center">Identification</td>
    <td align="center">Flags</td>
    <td align="center">Fragment Offset</td>
  </tr>
  <tr>
    <td align="center">Time to Live Number</td>
    <td align="center">Protocol</td>
    <td colspan="2" align="center">Header Checksum</td>
  </tr>
  <tr>
    <td colspan="4" align="center">Endereço IP de Origem</td>
  </tr>
  <tr>
    <td colspan="4" align="center">Endereço IP de Destino</td>
  </tr>
  <tr>
    <td colspan="4" align="center">Options</td>
  </tr>
  <tr>
    <td colspan="4" align="center">Dados (payload)</td>
  </tr>
</table>

### MTU(Maximum Transmission Unit), MSS (Maximum Segment Size) e ICMP (Internet Control Message Protocol)

MTU e MSS: MTU engloba as camadas 3 (rede) e 4 (transporte) (deixando de fora a camada 2), e vai somando o tamanho dos cabeçalhos IP e TCP com o Payload, não podendo ultrapassar 1500 bytes. O que sobra para o Payload, é chamado de MSS. O frame é o tamanho total do MTU + o cabeçalho da camada 2, conforme abaixo:
<table border="1">
  <tr>
    <td rowspan="4" style="background-color: black; color: white">Frame 1580</td>
    <td></td>
    <td>MAC (Cabeçalho)</td>
    <td>18 bytes (Ethernet)</td>
  </tr>
  <tr>
    <td rowspan="3" style="background-color: red; color: white">MTU 1500</td>
    <td>IP (Cabeçalho)</td>
    <td>20 bytes (IPv4)</td>
  </tr>
  <tr>
    <td>TCP (Cabeçalho)</td>
    <td>20 bytes</td>
  </tr>
  <tr>
    <td>Dados (Payload)</td>
    <td style="background-color: #63025a; color: white">1460 bytes (MSS)</td>
  </tr>
</table>

Caso quiséssemos utilizar IPv6 em vez de IPv4, teremos um cabeçalho IP de 40 bytes, em vez de 20 bytes, logo teremos a seguinte MTU/MSS:
<table border="1">
  <tr>
    <td rowspan="4" style="background-color: black; color: white">Frame 1580</td>
    <td></td>
    <td>MAC (Cabeçalho)</td>
    <td>18 bytes (Ethernet)</td>
  </tr>
  <tr>
    <td rowspan="3" style="background-color: red; color: white">MTU 1500</td>
    <td>IP (Cabeçalho)</td>
    <td>40 bytes (IPv6)</td>
  </tr>
  <tr>
    <td>TCP (Cabeçalho)</td>
    <td>20 bytes</td>
  </tr>
  <tr>
    <td>Dados (Payload)</td>
    <td style="background-color: #63025a; color: white">1440 bytes (MSS)</td>
  </tr>
</table>

Resumidamente: 40 bytes do cabeçalho IPv6 + 20 bytes do cabeçalho TCP = 60 bytes. Menos 1500 do tamanho do MTU, teremos um MSS de 1440 bytes, que serão utilizados para o Payload.

Uma analogia para lembrar do MAX SEGMENT SITE (MSS) é pensar como um caminhão de carga. Supõe que o caminhão tenha um peso máximo de 1500 MTU, mas só o caminhão pesa 60 (soma dos cabeçalhos), o que sobra 1440 de carga, que vai ser o Payload, totalizando os 1500 MTU. O peso da suspensão poderia ser os 18 bytes da camada 2, pensando em frame total.

#### ICMP - Internet Control Message Protocol
- utilizado pelo IP como mensageiro;
- mensagens ICMP são encapsuladas e transportadas em pacotes IP;
  - **TIPO 0**: <u>echo reply</u> (resposta a um ping);
  - **TIPO 3**: <u>destination unreachable</u> (destino inalcançável);
  - **TIPO 8**: <u>echo request</u> (solicitação de ping);
  - **TIPO 11**: <u>time exceed</u> (TTL - Time to Live Excedido);
  - **TIPO 30**: <u>traceroute</u>.

  ### ARP (Address Resolution Protocol)
  - Protocolo de apoio usado pelo IP para localizar o endereço de hardware de um dispositivo a partir de seu endereço IP;
  - Opera como um "Detetive" contratao pelo IP;
  - Interroga todas as máquinas da rede a qual pertence (broadcast), perguntando se aquele ip é dela, se for, ela pede o seu MAC;
  - Mapeamento de um endereço lógico (IP) para um endereço físico (MAC);
  - Encaminhadas em frames broadcast, jamais cruzarão um domínio de broadcast

  Vamos supor que temos o PC1 e o PC2 em uma mesma rede, com as seguintes configurações:

<table border="1">
<tr>
    <td colspan="2" align="center">PC1</td>
  </tr>
  <tr>
    <td>L3 - REDE</td>
    <td style="background-color: red">172.16.30.2</td>
  </tr>
  <tr>
    <td>L2 - ENLACE</td>
    <td style="background-color: black">AA:AA:AA:00:00:00</td>
  </tr>
</table>

<table border="1">
<tr>
    <td colspan="2" align="center">PC2</td>
  </tr>
  <tr>
    <td>L3 - REDE</td>
    <td style="background-color: red">172.16.30.3</td>
  </tr>
  <tr>
    <td>L2 - ENLACE</td>
    <td style="background-color: black">BB:BB:BB:00:00:00</td>
  </tr>
</table>

  Caso o PC1 faça um "ping" no PC2 (`ping 172.16.30.3`), irá ser montado o seguinte cabeçalho:

<table border="1">
  <tr>
    <td colspan="2" align="center">L3 - REDE</td>
  </tr>
  <tr>
    <td>IP ORIGEM</td>
    <td style="background-color: red">172.16.30.2</td>
  </tr>
  <tr>
    <td>IP DESTINO</td>
    <td style="background-color: blue">172.16.30.3</td>
  </tr>
  <tr>
    <td colspan="2" align="center">L2 - ENLACE</td>
  </tr>
  <tr>
    <td>MAC ORIGEM</td>
    <td style="background-color: red">AA:AA:AA:00:00:00</td>
  </tr>
  <tr>
    <td>MAC DESTINO</td>
    <td style="background-color: blue">?</td>
  </tr>
</table>

Como o MAC de destino não é conhecido, o IP vai pedir ajuda ao ARP para descobrir o MAC do IP 172.16.30.3 via ARP REQUEST. Essa request é por meio do broadcast usando MAC, que no caso é o **FF:FF:FF:FF:FF:FF** (tudo bit 1 - ligados) - (o switch vai mandar para todas as portas ativas dele):

<table border="1">
  <tr>
    <td colspan="2" align="center">L3 - REDE</td>
  </tr>
  <tr>
    <td>IP ORIGEM</td>
    <td style="background-color: red">172.16.30.2</td>
  </tr>
  <tr>
    <td>IP DESTINO</td>
    <td style="background-color: blue">172.16.30.3</td>
  </tr>
  <tr>
    <td colspan="2" align="center">L2 - ENLACE</td>
  </tr>
  <tr>
    <td>MAC ORIGEM</td>
    <td style="background-color: red">AA:AA:AA:00:00:00</td>
  </tr>
  <tr>
    <td>MAC DESTINO</td>
    <td style="background-color: blue">FF:FF:FF:FF:FF:FF</td>
  </tr>
</table>

No ARP REPLY, é informado de forma UNICAST, montando um cabeçalho com a informação do MAC requisitado, diretamente para quem o requisitou, no caso, o MAC AA:AA:AA:00:00:00, conforme o cabeçalho montado abaixo:

<table border="1">
  <tr>
    <td colspan="2" align="center">L3 - REDE</td>
  </tr>
  <tr>
    <td>IP ORIGEM</td>
    <td style="background-color: red">172.16.30.3</td>
  </tr>
  <tr>
    <td>IP DESTINO</td>
    <td style="background-color: blue">172.16.30.2</td>
  </tr>
  <tr>
    <td colspan="2" align="center">L2 - ENLACE</td>
  </tr>
  <tr>
    <td>MAC ORIGEM</td>
    <td style="background-color: red">BB:BB:BB:00:00:00</td>
  </tr>
  <tr>
    <td>MAC DESTINO</td>
    <td style="background-color: blue">AA:AA:AA:00:00:00</td>
  </tr>
</table>

No PC1, na tabela ARP (do PC mesmo, via cache, definido por tempo, de acordo com cada S.O.), é montado uma tabela ARP com as informações de MAC do IP 172.16.30.3, para que a próxima vez que precise falar com esse IP, não seja necessário realizar um broadcast novamente. E no caso seja necessário mandar mensagem novamente, o MAC de destino já estará preenchido.

<table border="1">
  <tr>
    <td colspan="2" align="center">L3 - REDE</td>
  </tr>
  <tr>
    <td>IP ORIGEM</td>
    <td style="background-color: red">172.16.30.2</td>
  </tr>
  <tr>
    <td>IP DESTINO</td>
    <td style="background-color: blue">172.16.30.3</td>
  </tr>
  <tr>
    <td colspan="2" align="center">L2 - ENLACE</td>
  </tr>
  <tr>
    <td>MAC ORIGEM</td>
    <td style="background-color: red">AA:AA:AA:00:00:00</td>
  </tr>
  <tr>
    <td>MAC DESTINO</td>
    <td style="background-color: blue">BB:BB:BB:00:00:00</td>
  </tr>
</table>

#### RARP (Reverse ARP)
- Determina um IP por meio do MAC;
- Precisa de um Servidor RARP;

Supõe o seguinte MAC:

<table border="1">
  <tr>
    <td style="background-color: white; color: black;">L3 - REDE</td>
    <td style="background-color: red; font-weight: bold;" align="center">?</td>
  </tr>
  <tr>
    <td style="background-color: black">L2 - ENLACE</td>
    <td style="background-color: red; font-weight: bold;">AA:AA:AA:00:00:00</td>
  </tr>
</table>

Não conseguimos determinar o IP dele, logo no RARP será consultado um servidor RARP, que disponibiliza uma tabela ARP:

```
RARP SERVER TABLE
AA:AA:AA:00:00:00 172.26.20.2
AA:AA:AA:00:00:00 172.26.20.3
AA:CC:AA:00:00:00 172.26.20.4
FF:AA:AA:00:00:00 172.26.20.5
AF:AC:FA:00:00:00 172.26.20.6
BA:BA:CA:00:00:00 172.26.20.7
```

Após consulta, é completado com a informação que faltava:

<table border="1">
  <tr>
    <td style="background-color: white; color: black;">L3 - REDE</td>
    <td style="background-color: red; font-weight: bold;" align="center">172.26.20.2</td>
  </tr>
  <tr>
    <td style="background-color: black">L2 - ENLACE</td>
    <td style="background-color: red; font-weight: bold;">AA:AA:AA:00:00:00</td>
  </tr>
</table>

Hoje me dia o DHCP na camada de aplicação consegue desempenhar o mesmo papel com muito mais opções, logo hoje em dia não é mais utilizado o RARP.

### Cálculo IP

#### Endereçamento IPv4

É e sempre foi um dos temas mais cobrados no exame CCNA (necessário realizar de cabeça)

#### Notação Binária
- base de toda comunicação digital
- **bit**: menor porção de informação
- **byte** ou **octeto**: uma sequencia de 8 bits. Ex. 10010101

No dia a dia utilizamos o sistema **decimal** de numeração - **BASE 10**

Ex. de base 10 (decimal): <u>435</u> - CENTENA (400), DEZENA (30) UNIDADE (5) = 400+30+5=435

<table border="1">
  <tr>
    <td align="center">4</td>
    <td align="center">3</td>
    <td align="center">5</td>
  </tr>
  <tr>
    <td>Centena</td>
    <td>Dezena</td>
    <td>Unidade</td>
  </tr>
  <tr>
    <td>Posição 2</td>
    <td>Posição 1</td>
    <td>Posição 0</td>
  </tr>
  <tr>
    <td>10^2=100</td>
    <td>10^1=10</td>
    <td>10^0=1</td>
  </tr>
  <tr>
    <td>100*4=400</td>
    <td>10*3=30</td>
    <td>1*5=5</td>
  </tr>
</table>

Ex. de base 2 (binário): 101101

<table border="1">
  <tr>
    <td align="center">1</td>
    <td align="center">0</td>
    <td align="center">1</td>
    <td align="center">1</td>
    <td align="center">0</td>
    <td align="center">1</td>
  </tr>
  <tr>
    <td>Posição 5</td>
    <td>Posição 4</td>
    <td>Posição 3</td>
    <td>Posição 2</td>
    <td>Posição 1</td>
    <td>Posição 0</td>
  </tr>
  <tr>
    <td align="center">2^5=32</td>
    <td align="center">2^4=16</td>
    <td align="center">235=8</td>
    <td align="center">2^2=4</td>
    <td align="center">2^1=2</td>
    <td align="center">2^0=1</td>
  </tr>
  <tr>
    <td align="center">32</td>
    <td align="center">0</td>
    <td align="center">8</td>
    <td align="center">4</td>
    <td align="center">0</td>
    <td align="center">1</td>
  </tr>
  <tr>
    <td colspan="6" align="center">45</td>
  </tr>
</table>

#### Endereçamento IPv4
- Formado por 32 bits divididos em 4 grupos, separados por ponto, chamados octetos ou bytes

<table border="1">
  <tr>
    <td colspan="8" align="center">192</td>
    <td colspan="8" align="center">188</td>
    <td colspan="8" align="center">180</td>
    <td colspan="8" align="center">181</td>
  </tr>
  <tr>
    <td>1</td>
    <td>1</td>
    <td>0</td>
    <td>0</td>
    <td>0</td>
    <td>0</td>
    <td>0</td>
    <td>0</td>
    <td>1</td>
    <td>0</td>
    <td>1</td>
    <td>1</td>
    <td>1</td>
    <td>1</td>
    <td>0</td>
    <td>0</td>
    <td>1</td>
    <td>0</td>
    <td>1</td>
    <td>1</td>
    <td>0</td>
    <td>1</td>
    <td>0</td>
    <td>0</td>
    <td>1</td>
    <td>0</td>
    <td>1</td>
    <td>1</td>
    <td>0</td>
    <td>1</td>
    <td>0</td>
    <td>1</td>
  </tr>
  <tr>
    <td>Posição 7</td>
    <td>Posição 6</td>
    <td>Posição 5</td>
    <td>Posição 4</td>
    <td>Posição 3</td>
    <td>Posição 2</td>
    <td>Posição 1</td>
    <td>Posição 0</td>
    <td>Posição 7</td>
    <td>Posição 6</td>
    <td>Posição 5</td>
    <td>Posição 4</td>
    <td>Posição 3</td>
    <td>Posição 2</td>
    <td>Posição 1</td>
    <td>Posição 0</td>
    <td>Posição 7</td>
    <td>Posição 6</td>
    <td>Posição 5</td>
    <td>Posição 4</td>
    <td>Posição 3</td>
    <td>Posição 2</td>
    <td>Posição 1</td>
    <td>Posição 0</td>
    <td>Posição 7</td>
    <td>Posição 6</td>
    <td>Posição 5</td>
    <td>Posição 4</td>
    <td>Posição 3</td>
    <td>Posição 2</td>
    <td>Posição 1</td>
    <td>Posição 0</td>
  </tr>
    <tr>
    <td>2^7=128</td>
    <td>2^6=64</td>
    <td>2^5=32</td>
    <td>2^4=16</td>
    <td>2^3=8</td>
    <td>2^2=4</td>
    <td>2^1=2</td>
    <td>2^0=1</td>
    <td>2^7=128</td>
    <td>2^6=64</td>
    <td>2^5=32</td>
    <td>2^4=16</td>
    <td>2^3=8</td>
    <td>2^2=4</td>
    <td>2^1=2</td>
    <td>2^0=1</td>
    <td>2^7=128</td>
    <td>2^6=64</td>
    <td>2^5=32</td>
    <td>2^4=16</td>
    <td>2^3=8</td>
    <td>2^2=4</td>
    <td>2^1=2</td>
    <td>2^0=1</td>
    <td>2^7=128</td>
    <td>2^6=64</td>
    <td>2^5=32</td>
    <td>2^4=16</td>
    <td>2^3=8</td>
    <td>2^2=4</td>
    <td>2^1=2</td>
    <td>2^0=1</td>
  </tr>
</table>

Cada octeto em um endereço IPV4 pode conter um número decimal compreendido entre o intervalo de 0 a 255, que resulta em 256 valores possíveis (o ZERO CONTA!)

Ex. 11111111 em binário é = a 255 em decimal (2^8 bits = 256), e temos que:

```
0 -> número 1
1 -> número 2
2 -> número 3
3 -> número 4
4 -> número 5
5 -> número 6
...
253 -> número 254
254 -> número 255
255 -> número 256
```

#### Hierarquia

Assim como em sistemas telefônicos, que temos os códigos dos países, do estado, da área e a identificação do assinante, o IP funciona de forma similar a essa hierarquia.

Se estiver nos EUA, e quer ligar para o Brasil, temos que digitar 55 19 4321 1234, sendo respectivamente o que já vimos, código do país, do estado/região, a identificação da região/cidade e os 4 últimos dígitos que identificam o cliente.

No exemplo que você quer ligar para o Brasil estando nos EUA, ao digitar o telefone, a ligação é redirecionada para o Brasil, devido ao código 55. Não faz sentido nos EUA terem todos os número de telefones possíveis no Brasil, basta apenas redirecionar de acordo com o código. Quando a ligação chegar ao Brasil, as operadoras que irão tratar o restante do número.

Da mesma forma com IPs, considerando que temos um plano de 2^32 = 4,3 bilhões de endereços possíveis, não faz sentido cada país conhecer todas as rotas possíveis, seria inviável, e teria um custo muito alto (igualmente no exemplo com telefones) e teria muita lentidão.

A escolha do modelo hierárquico em vez do modelo plano, se dá pelas razões apresentadas, e nele é possível criar um esquema de endereçamento compreendido em **dois níveis principais**: **<u>REDE</u>** e **<u>HOST</u>**, e podemos ter ainda um nível adicional, chamado **<u>SUB-REDE</u>**.

- O endereço de rede identifica cada rede DISTINTAMENTE
- Toda e qualquer máquina em uma mesma rede possui o mesmo identificador de rede como parte do seu endereço

#### Classes de endereços IPv4
- Foram criadas classes de endereços
- cada uma com suporte a um número de redes e hosts diferentes

<table border="1">
  <tr>
    <td></td>
    <td style="background-color: black; color: white; font-weight: bold;">8 bits</td>
    <td style="background-color: black; color: white; font-weight: bold;">8 bits</td>
    <td style="background-color: black; color: white; font-weight: bold;">8 bits</td>
    <td style="background-color: black; color: white; font-weight: bold;">8 bits</td>
    <td></td>
  </tr>
  <tr>
    <td style="background-color: red; color: white; font-weight: bold;">CLASSE A</td>
    <td style="background-color: DimGray; color: white; font-weight: bold;">REDE</td>
    <td style="background-color: DarkGray; color: white; font-weight: bold;">HOST</td>
    <td style="background-color: DarkGray; color: white; font-weight: bold;">HOST</td>
    <td style="background-color: DarkGray; color: white; font-weight: bold;">HOST</td>
    <td style="background-color: red; color: white; font-weight: bold;">REDES = 2^8 = 256<br>HOSTS = 2^24 = 16777216</td>
  </tr>
  <tr>
    <td style="background-color: DarkCyan; color: white; font-weight: bold;">CLASSE B</td>
    <td style="background-color: DimGray; color: white; font-weight: bold;">REDE</td>
    <td style="background-color: DimGray; color: white; font-weight: bold;">REDE</td>
    <td style="background-color: DarkGray; color: white; font-weight: bold;">HOST</td>
    <td style="background-color: DarkGray; color: white; font-weight: bold;">HOST</td>
    <td style="background-color: DarkCyan; color: white; font-weight: bold;">REDES = 2^16 = 65536<br>HOSTS = 2^16 = 65536</td>
  </tr>
   <tr>
    <td style="background-color: blue; color: white; font-weight: bold;">CLASSE C</td>
    <td style="background-color: DimGray; color: white; font-weight: bold;">REDE</td>
    <td style="background-color: DimGray; color: white; font-weight: bold;">REDE</td>
    <td style="background-color: DimGray; color: white; font-weight: bold;">REDE</td>
    <td style="background-color: DarkGray; color: white; font-weight: bold;">HOST</td>
    <td style="background-color: blue; color: white; font-weight: bold;">REDES = 2^24 = 16777216<br>HOSTS = 2^8 = 256</td>
  </tr>
  <tr>
    <td colspan="5" style="background-color: DarkCyan; color: black; font-weight: bold;">CLASSE D: Classe reservada para endereços MULTICAST</td>
  </tr>
  <tr>
    <td colspan="5" style="background-color: DarkCyan; color: black; font-weight: bold;">CLASSE E: Classe reservada para PESQUISA</td>
  </tr>
</table>

Lembrando que classe com número de hosts acima de 256 hosts não é recomendado pela Cisco, pois devido ao alto domínio de broadcast, a rede irá se tornar lenta (exemplo das salas de aula).

Agora como saber se um endereço é da classe A, B ou C? A resposta é que existe um padrão único de bits no **PRIMEIRO OCTETO**, conforme abaixo:

<table border="1">
  <tr>
    <td></td>
    <td style="background-color: gray; color: black; font-weight: bold">128</td>
    <td style="background-color: gray; color: black; font-weight: bold">64</td>
    <td style="background-color: gray; color: black; font-weight: bold">32</td>
    <td style="background-color: gray; color: black; font-weight: bold">16</td>
    <td style="background-color: gray; color: black; font-weight: bold">8</td>
    <td style="background-color: gray; color: black; font-weight: bold">4</td>
    <td style="background-color: gray; color: black; font-weight: bold">2</td>
    <td style="background-color: gray; color: black; font-weight: bold">1</td>
    <td style="background-color: #149FDB; color: black; font-weight: bold">DE</td>
    <td></td>
    <td></td>
    <td style="background-color: gray; color: black; font-weight: bold">128</td>
    <td style="background-color: gray; color: black; font-weight: bold">64</td>
    <td style="background-color: gray; color: black; font-weight: bold">32</td>
    <td style="background-color: gray; color: black; font-weight: bold">16</td>
    <td style="background-color: gray; color: black; font-weight: bold">8</td>
    <td style="background-color: gray; color: black; font-weight: bold">4</td>
    <td style="background-color: gray; color: black; font-weight: bold">2</td>
    <td style="background-color: gray; color: black; font-weight: bold">1</td>
    <td style="background-color: DarkCyan; color: black; font-weight: bold">ATÉ</td>
  </tr>
  <tr>
    <td style="background-color: #FB4B4B; color: white; font-weight: bold">A</td>
    <td style="background-color: #FB4B4B; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #149FDB; color: black; font-weight: bold">0</td>
    <td></td>
    <td style="background-color: #FB4B4B; color: white; font-weight: bold">A</td>
    <td style="background-color: #FB4B4B; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: DarkCyan; color: black; font-weight: bold">127</td>
  </tr>
     <tr>
    <td style="background-color: #F83030; color: white; font-weight: bold">B</td>
    <td style="background-color: #F83030; color: white; font-weight: bold">1</td>
    <td style="background-color: #F83030; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #149FDB; color: black; font-weight: bold">128</td>
    <td></td>
    <td style="background-color: #F83030; color: white; font-weight: bold">B</td>
    <td style="background-color: #F83030; color: white; font-weight: bold">1</td>
    <td style="background-color: #F83030; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: DarkCyan; color: black; font-weight: bold">191</td>
  </tr>
    <tr>
    <td style="background-color: #F51C1C; color: white; font-weight: bold">C</td>
    <td style="background-color: #F51C1C; color: white; font-weight: bold">1</td>
    <td style="background-color: #F51C1C; color: white; font-weight: bold">1</td>
    <td style="background-color: #F51C1C; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #149FDB; color: black; font-weight: bold">192</td>
    <td></td>
    <td style="background-color: #F51C1C; color: white; font-weight: bold">C</td>
    <td style="background-color: #F51C1C; color: white; font-weight: bold">1</td>
    <td style="background-color: #F51C1C; color: white; font-weight: bold">1</td>
    <td style="background-color: #F51C1C; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: DarkCyan; color: black; font-weight: bold">223</td>
  </tr>
    <tr>
    <td style="background-color: #C80303; color: white; font-weight: bold">D</td>
    <td style="background-color: #C80303; color: white; font-weight: bold">1</td>
    <td style="background-color: #C80303; color: white; font-weight: bold">1</td>
    <td style="background-color: #C80303; color: white; font-weight: bold">1</td>
    <td style="background-color: #C80303; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #149FDB; color: black; font-weight: bold">224</td>
    <td></td>
    <td style="background-color: #C80303; color: white; font-weight: bold">D</td>
    <td style="background-color: #C80303; color: white; font-weight: bold">1</td>
    <td style="background-color: #C80303; color: white; font-weight: bold">1</td>
    <td style="background-color: #C80303; color: white; font-weight: bold">1</td>
    <td style="background-color: #C80303; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: DarkCyan; color: black; font-weight: bold">239</td>
  </tr>
    <tr>
    <td style="background-color: #0B0000; color: white; font-weight: bold">E</td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">1</td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">1</td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">1</td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">1</td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">0</td>
    <td style="background-color: #149FDB; color: black; font-weight: bold">240</td>
    <td></td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">E</td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">1</td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">1</td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">1</td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">1</td>
    <td style="background-color: #0B0000; color: white; font-weight: bold">0</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: #A5ACAF; color: white; font-weight: bold">1</td>
    <td style="background-color: DarkCyan; color: black; font-weight: bold">247</td>
  </tr>
</table>

Ex.1. qual classe pertence o IP **10.30.20.5**? Basta pegar o primeiro octeto e passar para binário, ou seja, será: <span style="color: red">0</span>0001010, sendo o primeiro bit = 0, portanto pertence à classe A (Se o <span style="color: red">primeiro</span> bit iniciar por zero, automaticamente podemos dizer que é de classe A)

Ex.2. qual classe pertence o IP **200.67.5.2**? Mesmo processo, passa o primeir octeto para binário, ficando <span style="color: red">110</span>01000. Logo é um IP de classe C (o <span style="color: red">terceiro</span> bit é = ZERO).

Ex.3. **129.7.123.54** = <span style="color: red">10</span>000001, logo é de classe B (<span style="color: red">segundo</span> bit = ZERO)

Ex.4. **235.6.7.8** = <span style="color: red">1110</span>1011 = <span style="color: red">quarto</span> bit é igual a ZERO, logo é classe D

Podemos fazer dessa forma, observando o primeiro bit ZERO que aparecer, ou por meio da tabela que divide as faixas:

<table border="1">
  <tr>
    <td></td>
    <td style="background-color: #149FDB; color: black; font-weight: bold">DE</td>
    <td style="background-color: DarkCyan; color: black; font-weight: bold">ATÉ</td>
  </tr>
  <tr>
    <td style="background-color: #FB4B4B; color: white; font-weight: bold">A</td>
    <td style="background-color: #149FDB; color: white; font-weight: bold">0</td>
    <td style="background-color: DarkCyan; color: white; font-weight: bold">127</td>
  </tr>
  <tr>
    <td style="background-color: #F83030; color: white; font-weight: bold">B</td>
    <td style="background-color: #149FDB; color: white; font-weight: bold">128</td>
    <td style="background-color: DarkCyan; color: white; font-weight: bold">191</td>
  </tr>
  <tr>
    <td style="background-color: #F51C1C; color: white; font-weight: bold">C</td>
    <td style="background-color: #149FDB; color: white; font-weight: bold">192</td>
    <td style="background-color: DarkCyan; color: white; font-weight: bold">223</td>
  </tr>
  <tr>
    <td style="background-color: #C80303; color: white; font-weight: bold">D</td>
    <td style="background-color: #149FDB; color: white; font-weight: bold">224</td>
    <td style="background-color: DarkCyan; color: white; font-weight: bold">239</td>
  </tr>
  <tr>
    <td style="background-color: #0B0000; color: white; font-weight: bold">E</td>
    <td style="background-color: #149FDB; color: white; font-weight: bold">240</td>
    <td style="background-color: DarkCyan; color: white; font-weight: bold">247</td>
  </tr>
</table>

<table border="1">
  <tr>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td></td>
  </tr>
</table>