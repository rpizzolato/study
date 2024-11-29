# LPIC-3 Exam 305
## Virtualization and Containerization
#### Version: 3.0
A certificação LPIC-3 é o culminar do programa de certificação profissional em vários níveis do Linux Professional Institute (LPI). O LPIC-3 foi desenvolvido para o profissional Linux de nível empresarial e representa o nível mais alto de certificação Linux profissional e de distribuição neutra no setor. Três certificações especiais LPIC-3 separadas estão disponíveis. A aprovação em qualquer um dos três exames concederá a certificação LPIC-3 para essa especialidade.

A certificação LPIC-3 Enterprise Virtualization and Containerization cobre a administração de sistemas Linux em toda a empresa, com ênfase em virtualização e containers

#### Anotações para servir de resumo e consulta futuramente

# 351.1 - Conceitos e Teoria sobre Virtualização

**Simulador**
- Comporta-se de forma similar;
- busca reproduzir a experiência;
- desenvolvido com linguagem de alto nível;

**Emulador**
- réplica do sistema original;
- imita o hardware e software;
- desenvolvido em linguagem de máquina. (Ex. emulador do N64, Android, etc)

## 351.2 - Xen

### Conceitos e Arquitetura

### Instalação do Xen

Instalar o Debian normalmente, criar um disco com 100 GB em LVM. Depois de tudo instalado, executar o comando: 

```
# apt install xen-system-amd64 -y
# apt install xen-tools -y
```

>[!NOTE]<br>
>Observar que em determinado ponto da instalação do Xen, é sobrescrito a configuração do Grub, criando uma entrada para o iniciar o Xen, o que o torna um hypervisor de nível 1 (bare metal). Veja um exemplo da saída abaixo:
>
>Including Xen overrides from /etc/default/grub.d/xen.cfg<br>
>Warning: GRUB_DEFAULT changed to boot into Xen by default! Edit /etc/default/grub.d/xen.cfg to avoid this warning.<br>
>Generating grub configuration file ...

Terminado a instalação, basta reiniciar o sistema (note que a entrada no grub já mudou)

>[!TIP]<br>
>O Debian não vem com `/sbin` mapeado no PATH, sendo assim, para reiniciar o sistema, teria que digitar `# /sbin/reboot`. É possível contornar isso executando o comando `# echo PATH=$PATH:/sbin >> /etc/profile` ou incluindo o seguinte no final do arquivo `/etc/profile`:<br>
>`PATH=$PATH:/sbin`

- `xl info`: se tudo estiver funcionado, é possível testar a instalação do Xen esse comando;
- `xl dmesg | less`: usado para depurar a inicialização do Xen ou simplesmente ver sua versão;
- `xl list`: lista as VMs que temos (também conhecido como domínios). No caso a **Domain-0** (VM de Administração, sempre estará iniciada e com **ID = 0**)

O primeiro passo com a **Domain-0**, é limitar seu uso de CPU e memória. Acesse o arquivo do grub em /etc/default/grub e adicione a linha após o bloco: 

```
GRUB_DEFAULT=0
GRUB_TIMEOUT=5
GRUB_DISTRIBUTOR=`lsb_release -i -s 2> /dev/null || echo Debian`
GRUB_CMDLINE_LINUX_DEFAULT="quiet"
GRUB_CMDLINE_LINUX=""

GRUB_CMDLINE_XEN_DEFAULT="dom0_max_vcpus=1 dom0_mem=1024M,max:1024M
```
Terminando a edição, execute `update-grub` para que as alterações surtam efeito e reinicie o servidor.

Finalizando, execute o comando `xl list` para verificar se realmente aplicou as configurações de cpu e memória da **Domain-0**. Se quiser confirmar que foi executado que foi passado ao grub, só executar o comando `xl dmesg | grep dom0`, ou na linha **xen_commandline** no retorno do comando `xl info`

### Configurações de Rede e Armazenamento

#### Rede

Caso esteja utilizando o ubuntu server, o ideal é verificar se está usando o NetworkManager e pará-lo. Verifique com o comando `# systemctl status NetworkManager` se está rondando ele. Se estiver pare com o comando `# systemctl stop/disable NetworkManager`.

Próximo passo é instalar os programas para utilizar o recurso de bridge, com o comando `apt install bridge-utils`

Depois vamos acessar com o vi ou vim o arquivo `/etc/network/interfaces`. Desabilite o **DHCP** e o **DHCP** do **IPv6**, comentando a linha correspondente. Crie uma bridge chamada **xenbr0** (é importante criar um **bridge_hw** indicando a placa de rede física para que possa ser utilizado o mesmo MAC Address):
>[!NOTE]<br>
>
>A bridge precisa se chamar xenbr0. Caso coloque outro nome, é necessário mudar o arquivo `/etc/xen/xl.conf`, na linha `vif.defaul.bridge="xenbr0"`

```
auto xenbr0
   iface xenbr0 inet static
      address 192.168.1.150
      netmask 255.255.255.0
      gateway 192.168.1.1
      bridge_ports enp0s3
      bridge_hw enp0s3
```

Podemos confirmar com o comando `brctl show` que a interface **enp0s3** está vinculada à bridge **xenbr0**

#### Storage

Identificamos o disco que reservamos para LVM, no caso, de 100G. Com o comando `fdisk -l`.

Posteriormente execute `pvcreate /dev/sdb` para cria um **Physical Volume (PV)**

Crie um **Volume Group (VG)** com o comando `vgcreate xen-vg /dev/sdb` (permitindo a criação de **Logical Volumes (LV)** que podem ser redimensionados dinamicamente.)

- `vgs`: para consultar o Volume criado

### Arquivos de Configuração

Os principais arquivos de configuração ficam em **/etc/xen**, sendo o principal, `xl.conf`.<br>
- `xl.conf`: (arquivo de configuração principal do Xen) onde ficam uma série de configurações e parâmetros, como do **vif** (virtual interface) e vários outros parâmetros padrão, como também default bridge, que foi configurado há poucos no tópico anterior ao storage, que foi dado o nome de **xenbr0**;
- `oxenstored.conf`: referente ao pid do oxenstore que fica rodando, podendo mudar tamanho de recursos, ligações com logs;

>["NOTE]
>
>As VMs criadas terão arquivo de configuração dentro de **/etc/xen** (arquivos como `/etc/xen/xlexample.pvlinux` e `/etc/xen/xlexample.hvm`)
- `xl.cfg`: configuração geral das VMs
- `/etc/default/xen` e `xendomains`: configurações gerais e configurações gerais do domínios em nível de serviço

### Configuração de uma VM PV

Será criado por meio do Xen Tools, que é um conjunto de scripts para criação de VMs, no caso com instâncias PV (Paravirtualization)

- `/etc/xen-tools`: dispõe com as principais configurações do xen-tools
- `/usr/share/xen-tools`: diretórios que estão os arquivos para realizar as instalações

- `xen-create-image`: comando para criar (para ver os parâmetros usar `man xen-create-image`)
- `# xen-create-image --hostname=lpic3-pv-guest --memory=512mb --vcpus=2 --lvm=xen-vg --dhcp --pygrub --dist=bookworm`: cria uma VM, lembrando que pygrub é um topo de grub recomendado pelo projeto Xen.
>[!IMPORTANT]
>
>Anote a senha de root fornecida após a criação da VM

Depois de criado, é adicionado um arquivo `lpic3-pv-guest.cfg` em `/etc/xen/` para configuração da VM (se notar, é o nome do host, o nome do arquivo)

`xl create -c /etc/xen/lpic3-pv-guest.cfg`: executa a VM que foi criada. o `-c` já coloca em um console. Basta autenticar como root com a senha disponibilizada anteriormente.

Para sair da VM, digite `Ctrl + ]`. Veja que a VM ainda está executando, com o comando `xl list`. No estado vai estar com `b` de **blocked**, que alterna para **running** (`r`) conforme o uso da VM

Para acessar novamente a VM, use: `xl console nome_instancia` ou `numero_id`

Fora da VM, ao executar o comando `brctl show` é possível ver que foi criado uma vif (virtual interface) chamada **vif1.0**, que se conecta à bridge `xenbr0`. Esse 1.0 da **vif** faz referência ao ID da VM quando é listado com o comando `xl list`, ou seja, a **vif1.0** pertence à VM de **id 1**.

- `xl shutdown nome_instancia ou numero_id`: desliga/baixa a VM, depois pode confirmar com `xl list`. Lembrando que para subir a VM novamente, usa `xl create -c /etc/xen/lpic3-pv-guest.cfg`
- `xen-delete-image --lvm=xen-vg`: remove o disco LVM criado, seria como removesse a imagem. Terá que reinstalar novamente.

### Configuração de uma VM HVM

Primeiramente é preciso habilitar o suporte à virtualização no processador, no caso no virtualbox, usar o comando: `VBoxManage modifyvm "Nome_da_VM" --nested-hw-virt on`

Criar uma LVM de 20G para ser o disco da VM, com o comando: `lvcreate -n lpic3-hvm-guest-disk -L 20G xen-vg`

Fazer download de alguma distro linux, no caso será a Rocky Linux, com o wget: `wget https://download.rockylinux.org/pub/rocky/9/isos/x86_64/Rocky-9.4-x86_64-minimal.iso`

Agora em `/etc/xen`, trabalhe com o arquivo `xlexample.hvm`, realizando uma cópia dele: `cp xlexample.hvm lpic3-hvm-guest.cfg`

Agora no arquivo de configuração `lpic3-hvm-guest.cfg`:
- `type = hvm` (podemos usar `builder=hvm`, tem o mesmo efeito)
- `name = "lpic3-hvm-guest"`:
- `memory = 2048`: quantidade de memória, nesse caso 2G
- `vcpus = 2`: quantidade de cpus, nesse caso 2 vcpus
- `vif = [ 'bridge=xenbr0' ]`: aqui aceita mac address também, mas o recomendado é informar a bridge

Agora a parte de disco:<br>

    disk = [
	    'phy:/dev/xen-vg/lpic3-hvm-guest-disk,hda,w',
	    'file:/root/Rocky-9.4-x86_64-minimal.iso,hdc:cdrom,r'
    ]
E para forçar boot pelo cdrom, usamos abaixo da configuração do disco:<br>

    boot="d"
    #para hard drive seria "c"
    #pode colocar por ordem
    #boot="dc" #primeiro o disco cdrom e depois o hard drive

Para o acesso à interface de instalação, pode escolher entre **sdl** e **vnc**, basta retirar comentário da opção desejada. Aqui será escolhido a opção de vnc:<br>

    # Guest VGA console configuration, either SDL or VNC
    #sdl = 1
    vnc = 1


Para efetivamente começar a criar a VM, use o comando: `xl create lpic3-hvm-guest.cfg`

Depois de dar um parsing da construção da VM, pode-se conferir sua efetividade usando `xl list` para listar se realmente já aparece na lista de VMs

Use o comando `ss -nalp | grep 5900` para conferir se a porta do vnc está aberta e em estado de listening.

    # ss -nalp | grep 5900
    tcp   LISTEN 0      1                                       127.0.0.1:5900             0.0.0.0:*    users:(("xen-qemu-system",pid=907,fd=38))

Do host local, precisamos abrir uma porta até o servidor Xen (criará um túnel), por meio do comando:  `ssh -l rodrigo -L 5900:localhost:5900 192.168.1.200`

Depois instale o remmina ou qualquer outro cliente vnc viewer e entre como `localhost`(lembre-se de usar protocolo vnc)

Com as duas VMs criadas, se rodar `xl list`, é possível ver seus status. Rodando o comando `ip address show`, é possível notar que há duas interfaces de rede virtuais, vif2.0 e vif3.0, uma para cada VM:<br>

    5: vif1.0: <BROADCAST,MULTICAST,UP,LOWER_UP> mtu 1500 qdisc fq_codel master xenbr0 state UP group default qlen 1000
        link/ether fe:ff:ff:ff:ff:ff brd ff:ff:ff:ff:ff:ff
        inet6 fe80::fcff:ffff:feff:ffff/64 scope link
           valid_lft forever preferred_lft forever
    6: vif2.0: <BROADCAST,MULTICAST,UP,LOWER_UP> mtu 1500 qdisc fq_codel master xenbr0 state UP group default qlen 1000
        link/ether fe:ff:ff:ff:ff:ff brd ff:ff:ff:ff:ff:ff
        inet6 fe80::fcff:ffff:feff:ffff/64 scope link
           valid_lft forever preferred_lft forever

É possível confirmar também com o comando `brctl show`

Para monitorar o processamento das VMs, usamos os comandos: `xl top`, `xen top`, `xentop` (há diversas variações)

Resumidamente:
- **baixar VM**: `shutdown` ou `destroy`
- **subir VM**: `create`

### Comandos para gerenciamento das VMs

Estados que aparecem no comando `xl list`:
- r - running
- b - blocked
- p - paused
- s - shudown
- c -crashed
- d - dying
É recomendado ver no `man xl`

- `xl list -l nome_da_vm`: mostra informações da VM específica, como nome, id, cpu, discos, etc. (sem o nome da VM traz info de todas as VMs)
- `xl uptime`: mostra o tempo que as VMs estão no ar
- `xl pause nome_da_vm`: pausa a VM (interessante olhar o estado dela, vai ficar com um `p`)
- `xl unpause nome_da_vm`: retira o pause da VM (volta para running)
- `xl -v save nome_da_vm ~root/image-pv-guest.save`: salva uma instancia/estado da VM (quando salva, sai da lista do `xl list`)
- `xl -v restore ~root/image-pv-guest.save`: restaura a instância/VM (interessante ao salvar, deixar um `top` rodando, para mostrar que a VM é salva em um estado que estava com um programa em execução, e após o restore, ela volta no mesmo local que estava executando)
- `xen-list-images`: lista as imagens criadas

Para fazer com que determinada VM suba assim que o hypervisor seja iniciado (de um reinício inesperado, por exemplo), criamos um diretório chamado `auto` em `/etc/xen` (isso é predefinido em `/etc/default/xendomains`).<br>
Dentro de `auto` crie um link para o arquivo `.cfg` da VM: `ln -s ../lpic3-pv-guest.cfg .` (considerando que já esteja em `/etc/xen/auto`)

### Comandos para gerenciamento dos dispositivos de armazenamento

- `xl block-list lpic3-pv-guest`: lista os discos (devices) na VM lpic3-pv-guest.
- `xl block-detach lpic3-pv-guest sdb`: desanexa um disco da VM lpic3-pv-guest
- `xl block-attach lpic3-pv-guest 'phy:/dev/xen-vg/lpic3-pv-novo,sdc,w'` (isso considerando que tenha criado uma LV de 5G, com o comando: `lvcreate -n lpic3-pv-guest-novo -L 5G xen-vg`)

#### Remover e inserir disco em CDROM

- `xl cd-eject lpic3-hvm-guest hdc`: remove o disco que há no cdrom (não remove o dispositivo, apenas remove o disco inserido internamente nele)
- `xl cd-insert lpic3-hvm-guest hdc /root/Rocky-9.3-x86_64-minimal.iso`: insere a iso do Rocky linux no cdrom

## 351.3 - QEMU (e KVM)

### KVM: Kernel-based Virtual Machine

- Tecnologia de virtualização open source integrada ao Kernel Linux
- Implementa um Hypervisor do tipo **1**
- Multiplataforma (x86, x86_64, ARM, ...)
- Utiliza as extensões de virtualização dos processos (HVM): Intel VT-x (vmx) e AMD-V (svm)
- Usado em conjunto com o QEMU, Libvirt, Virt-manager, etc
- Usado em soluções como oVirt, Proxmox, OpenStack

### QEMU - Quick Emulador

- É um emulador e virtualizador Open Source
- Pode emular uma máquina completa ou a execução de um software em outra arquitetura de hardware
- É um conjunto com outro Hypervisor (KVM/Xen), atingindo performance próxima a uma máquina nativa
- Hypervisor do tipo **2**

### QEMU + KVM

- **QEMU**: faz a emulação dos dispositivos (rede, disco, placas, etc)
- **KVM**: disponibiliza o recurso de HVM (CPU Hardware Virtualization)

Antes da instalação, precisa testar se KVM está disponível:
- `egrep -o '(vmx|svm)' /proc/cpuinfo`
- `lscpu |grep Virtualization`
- `lsmod|grep kvm`
- `ls -l /dev/kvm`

#### instalação
- QEMU (**Debian**): `apt install qemu-system-x86 -y`
- QEMU (**Red Hat**): `yum install qemu-kvm qemu-img`

### Execução de VMs com QEMU/KVM

Comando que será bem utilizado: `qemu-system-x86_64` (recomendado explorar `man qemu-system-x86_64`)

Primeiro passo é criar um disk image utility, usando o qemu-img, sendo o comando o seguinte: `qemu-img create -f qcow2 vm-disco1.qcow2 20G`.
O `-f` é de first image format. `qcow2` é um formato de imagem comprimido, nesse caso será de 20GB com nome `vm-disco1.qcow2`

Para gerar a VM:
- `qemu-system-x86_64 -name LPIC3-Debian -enable-kvm -hda vm-disco1.qcow2 -cdrom /home/rodrigo/Downloads/debian.iso -boot d -m 2048 -smp cpus=2`: iniciar uma VM usando o disco `vm-disco1.qcow2` com boot no cdrom (`-boot d`, vide `man qemu-system-x86_64`, basicamente é `a`, `b` para floppy, `c` para hard disk e `d` para CD-ROM, `n-p` para Etherboot), com 2G de RAM e 2 CPUs.

Usando interface gráfica, vai subir uma janela para controlar a máquina. Caso esteja utilizando apenas console, é preciso abrir um túnel (`ssh -l rodrigo -L 5900:localhost:5900 192.168.1.200`) com a VM e acessar por VNC, o qual também precisa indicar, colocando mais um argumento ao comando anterior: `-vnc :0`

A partir daqui, poderia segui a instalação do linux normalmente, no entanto é possível pegar imagem pronta, como as que há no site [https://osboxes.org](https://osboxes.org/). As imagens são no formato do VirtualBox (`.vdi`) e no QEMU não ofereceriam a possibilidade de snapshot. Para isso é necessário converter de `.vdi` para `.qcow2`.

Para conversão precisa rodar o comando: `qemu-img convert -f vdi -O qcow2 CentOS9.vdi CentOS9.qcow2 -p -m10`. O `-m10` é o número de coroutines (corrotinas) paralelas no processo de conversão. O `-p` é para mostrar uma barra de progresso da conversão.

Com o comando `hostnamectl` é possível notar que o **Virtualization** é igual a **kvm**, e **Hardware Vendor** é igual a **QEMU**. O comando `systemd-detect-virt` ajuda a detectar também. (se não usar `-enable-kvm` na hora de criar a VM, **Virtualization** vai ser igual a **QEMU**)

Dentro da VM, rode `lshw` para ver as configurações de hardware do computador.

### Kernel e Boot

É possível fazer a VM subir com o kernel da máquina Host, assim como a imagem de initrd.img: `qemu-system-x86_64 -name LPIC3-Ubuntu -enable-kvm -hda Ubuntu-Server.qcow2 -m 2048 -smp cpus=2 -kernel /boot/vmlinuz -initrd /boot/initrd.img -append 'root=/dev/ubuntu-vg/ubuntu-lv fastboot quiet'`

Para confirmar, dentro da VM, veja a versão do Kernel e em `/proc/cmdline`, os argumentos que foram passados no boot (`'root=/dev/ubuntu-vg/ubuntu-lv fastboot quiet'`)

### Storage

Por enquanto foi feito apenas VM com um disco, para adicionar mais um disco (reaproveitar aquele criado no começo das aulas de QEMU (`vm-dsico1.qcow2`): `sudo qemu-system-x86_64 -name LPIC3-CentOS9 -enable-kvm -hda ./Downloads/64bit/CentOS9.qcow2 -hdb ./Downloads/64bit/vm-disco1.qcow2 -m 1024 -smp cpus=2`

Para conferir, use `fdisk -l` ou `lsblk`

#### Outras formas de indicação de disco:
- `-drive file=file,index=0,media=disk`: seria equivalente ao `-hda` (`index=1`, seria `hdb`, etc)
- `-drive file=iscsi://user%password@fileserver/iqn.2001-04.com.example/1`
- `-drive file=nbd:192.0.2.1:30000`: usando NBD (Network Block Devices)
- `-drive file=ssh://user@host/tmp/file.img`
- `-drive file=http://user:password@fileserver/pub/linux.iso,media=cdrom,readonly`

Exemplo de uso (com `-hda` e `-drive`): 
- `sudo qemu-system-x86_64 -name LPIC3-CentOS9 -enable-kvm -hda ./Downloads/64bit/CentOS9.qcow2 -drive file=./Downloads/64bit/vm-disco1.qcow2,index=1,media=disk,if=ide -m 1024 -smp cpus=2` (`-drive` tem mais opções)


### Networking

#### User Mode

Se subir uma VM no padrão, como já subimos as anteriores, irá subir no modo [User Networking](https://www.linux-kvm.org/page/Networking). Esse modo atribui um endereço IP somente para a VM, deixando-a isolada do Host. No entanto a VM acessará à Internet, com o rota padrão que é criado (para ver a rota, use: `ip route show`).

Inclusive, se tentar usar ssh para conectar na rota padrão (exemplo: `ssh rodrigo@10.0.2.2`), conseguirá conectar no computador host. Entretanto o inverso não é possível (ICMP não funciona por padrão no Guest). Inclusive, se instalar utilitário ping (`apt install iputils-ping`), poderá "pingar" em algum site.

Uso especificando uma interface de rede:
- `sudo qemu-system-x86_64 -name LPIC3-UbuntuServer -enable-kvm -hda ./Downloads/64bit/UbuntuServer24-04.qcow2 -m 1024 -smp cpus=2 -netdev user,id=mynet0,net=192.168.80.0/24,dhcpstart=192.168.80.88 -device e1000,netdev=mynet0`

Cria uma netdev (`-netdev`) e identifica pelo `id`. Cria um device (`-device`) e associa com o `mynet0` (que é um `netdev`). Assim podemos controlar os IPs, no entanto ainda não teríamos acesso do Host ao Guest.

Para contornar isso, pode-se criar uma entrada `hostfwd=tcp::2222:22`, a qual da porta 2222 (no Host), disponibiliza acesso à porta 22 do Guest. Com o seguinte comando:
- `sudo qemu-system-x86_64 -name LPIC3-UbuntuServer -enable-kvm -hda ./Downloads/64bit/UbuntuServer24-04.qcow2 -m 1024 -smp cpus=2 -netdev user,id=mynet0,net=192.168.80.0/24,dhcpstart=192.168.80.88,hostfwd=tcp::2222-:22 -device e1000,netdev=mynet0`

A partir do Host, acesse o Guest por ssh com o comando:  `ssh -l osboxes localhost -p 2222`

#### Private Virtual Bridge

Também conhecido como [TAP](https://wiki.qemu.org/Documentation/Networking#Tap). Esse cenário é para quando deseja conectar duas VMs entre si internamente, sem acesso à rede local do Host. Para isso é necessário criar uma bridge para conexão das duas ou mais VMs. Como root:
- `ip link add br0 type bridge` ou `brctl addr br0` (este segundo precisa instalar antes - `apt install bridge-utils`)
- após criar a bridge, ela vai ficar "down". Para deixá-la "up", use: `ip link set br0 up`
- confirme que está up com `ip address show`:
```
    10: br0: <NO-CARRIER,BROADCAST,MULTICAST,UP> mtu 1500
```

É necessário apontar para a nova bridge no arquivo /etc/qemu-ifup, alterando da seguinte forma:

    #switch=$(ip route ls | \
    #    awk '/^default / {
    #          for(i=0;i<NF;i++) { if ($i == "dev") { print $(i+1); next; } }
    #         }'
    #        )
    switch=br0 #onde aponta para a nova bridge, e comenta o resto.

Confirme a bridge também com o comando: `brctl show`. 

Para subir a VM, use (aqui sobe um **Ubuntu Server**):
- `sudo qemu-system-x86_64 -name LPIC3-UbuntuServer -enable-kvm -hda ./Downloads/64bit/UbuntuServer24-04.qcow2 -m 1024 -smp cpus=2 -device e1000,netdev=br0,mac=DE:AD:BE:EF:1A:24 -netdev tap,id=br0 &`

Agora configure outra VM, usando outra imagem (Aqui sobe um **CentOS**):
- `sudo qemu-system-x86_64 -name LPIC3-CentOS9 -enable-kvm -hda ./Downloads/64bit/CentOS9.qcow2 -m 1024 -smp cpus=2 -device e1000,netdev=br0,mac=DE:AD:BE:EF:1A:25 -netdev tap,id=br0 &`

**Use o comando a seguir para gerar um endereço `MAC: printf 'DE:AD:BE:EF:%02X:%02X\n' $((RANDOM%256)) $((RANDOM%256))`

Agora dentro das VMs configure um IP e máscara, para cada uma. (Ex. `192.168.0.10/24` para uma e `192.168.0.20/24` para outra), usando o comando:
- `ip address add 192.168.0.10/24 dev ens3` (veja o nome da interface antes, nesse caso é `ens3`)

Terminado as configurações de IP, como a bridge interliga as VMs de forma direta, um já vai "pingar" no outro.

Examine a saída do comando `brclt show`, na coluna de interfaces, vai mostrar o **tap0** e **tap1**, ambos conectando na bridge `br0`

### QEMU Monitor

Interface de monitoramento do QEMU. O mais comum para entrar no modo de "monitor", seria apertando as teclas `Ctrl + Alt + 2`, ou simplesmente indo no menu > `View` > `Show tabs`, para que facilite alterar entre a VM e o monitoramento.

No console de monitoramento (fica com `(qemu)`):
- `info cpu`: mostra informações dos cpus
- `info network`: mostra informações da rede, se está em modo user, etc.
- `info block`: mostra informações do armazenamento
- `info version`: versão 
- `stop`: pausa a VM
- `cont`: sai do estado de pause
- `system_powerdown`: desliga a VM
- `system_reset`: reinicia a VM

#### snapshot

Lembrando que esse recurso é quando usa VM `qcow2`, caso use `.vdi`, não será possível realizar snapshot
- `info snapshots`: para verificar se há snapshots disponíveis
- `savevm NOME_DO_SNAP`: cria um snapshot da VM
- `loadvm NOME_DO_SNAP`: volta um snapshot da VM
- `delvm NOME_DO_SNAP`: exclui um snapshot

#### adicionar um CD no CD-ROM

Com o comando info block, pode-se notar que há um cd-rom sem CD. Para adicionar um CD/ISO nele, use o comando: `change ide-cd0 /home/rodrigo/Downloads/Rocky-9.5-x86_64-minimal.iso`

Posteriormente digite `info block` para confirmar que o a ISO foi associada ao CD-ROM. Logo poderá montar a ISO: `mount /dev/sr0 /media/cdrom`

Para retirar o CD, use: `eject ide1-cd0`

Vale muito a pena chamar o `help` do **qemu monitor** para explorar outras opções.

## VirtIO e QEMU Guest Agent

[VirtIO](https://en.wikibooks.org/wiki/QEMU/Devices/Virtio) é uma plataforma para virtualização de IO (input/output), comum para vários hypervisors (e QEMU). Resumidamente, em vez de usar os drivers padrão do QEMU, são utilizados drivers VirtIO, especialmente quando são drivers específicos para manusear alguns dispositivos. (são drivers virtuais)

Vejamos um exemplo usando driver para Hard Disk usando VirtIO e uma rede também nos mesmos moldes (rede já com modo **user**): `sudo qemu-system-x86_64 -name LPIC3-CentOS9 -enable-kvm -drive file=./Downloads/64bit/CentOS9.qcow2,if=virtio -m 1024 -smp cpus=2 -net nic,model=virtio -net user`

Geralmente por melhorar performance e resposta, ambiente de produção usa VirtIO

O [Guest Agent](https://wiki.qemu.org/Features/GuestAgent) busca implementar uma melhor interação entre o Guest e o Host, como habilitar cópia de arquivos entre eles, enviar comandos, como se fosse uma comunicação via API. Funciona por meio de um socket, que precisa previamente ser aberto. Usado com o comando `qemu-ga`

Para subir uma VM utilizando esse recurso e já abrindo um socket, use: `sudo qemu-system-x86_64 -name LPIC3-Ubuntu -enable-kvm -hda ./Downloads/64bit/UbuntuServer24-04.qcow2 -m 2024 -smp cpus=2 -chardev socket,path=/tmp/qga.sock,server=on,wait=off,id=qga0 -device virtio-serial -device virtserialport,chardev=qga0,name=org.qemu.guest_agent.0`

Na VM precisa instala o **qemu-guest-agent** (`apt install qemu-guest-agent`) e habilitá-lo (`systemctl start qemu-guest-agent`)

## 351.4 - Libvirt

### Conceitos

- **Libvirt Virtual Machine Management**: um conjunto de software open-source utilizado para gerenciar diversos hypervisors e máquinas virtuais, incluindo os recursos de rede e armazenamento.
- suporta KVM, QEMU, Xen, VMWare ESX, VirtualBox, Microsoft Hyper-V, LXC, entre outros
- é composto por:
	- API
	- Daemon: libvirtd
	- Interface de Comandos: `virsh` (CLI)

Pode usar o **virt-manager** também, que é um interface gráfica.

### Instalação e Configuração

- `apt install libvirt-daemon-system libvirt-clients -y`: instalar o daemon e algumas ferramentas cliente

Caso esteja em uma máquina sem interface gráfica (só CLI): use: `apt install --no-install-recommends libvirt-daemon-system libvirt-clients -y`: evita de instalar bibliotecas gráficas.

Instale também o gerenciador de máquinas virtuais (a grosso modo seria aquela janela gráfica para criar as VMs, mudar as configurações, etc - como no VirtualBox, por exemplo): `apt install virt-manager -y`


<!--stackedit_data:
eyJoaXN0b3J5IjpbLTU0MzY3MDM3MywtNzEzNDE2MDE0LDIyNT
g4NTc0OCwxNzA3MTcyMTEwLDEwNTI5MDE1NTMsLTM4Njk1NzQ0
NiwtMTQ4MTY4MzUyLDE1Nzk0Mjc3NjcsLTI2MjAxMTgzMSwzMD
k4ODgzNTQsLTIxNzAyODQzMCwyODE2MzkwODYsMTM4OTIzNTUw
LC0yMTM4NzM2MzQ5LC0xNjI4MDMwNzUxLDE2ODE5NjMzNDYsLT
EzNDY3MzAxMzYsLTE5NjczODYwNDEsMTI5MDA3MTk0MywtMjQz
ODc2NTddfQ==
-->