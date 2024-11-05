# LPIC-3 Exam 305
## Virtualization and Containerization
#### Version: 3.0
A certificação LPIC-3 é o culminar do programa de certificação profissional em vários níveis do Linux Professional Institute (LPI). O LPIC-3 foi desenvolvido para o profissional Linux de nível empresarial e representa o nível mais alto de certificação Linux profissional e de distribuição neutra no setor. Três certificações especiais LPIC-3 separadas estão disponíveis. A aprovação em qualquer um dos três exames concederá a certificação LPIC-3 para essa especialidade.

A certificação LPIC-3 Enterprise Virtualization and Containerization cobre a administração de sistemas Linux em toda a empresa, com ênfase em virtualização e containers

#### Anotações para servir de resumo e consulta futuramente

## 351.1 - Conceitos e Teoria sobre Virtualização

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
>As VMs criadas terão arquivo de configuração dentro de **/etc/xen**
<!--stackedit_data:
eyJoaXN0b3J5IjpbNDg5NzY3ODM4LDEyOTExNzc3NDFdfQ==
-->