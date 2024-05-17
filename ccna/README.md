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