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