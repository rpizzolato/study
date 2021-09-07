# Dicas Windows
Repositório para reunir diversas dicas em relação ao Sistema Operacional Windows no seu uso díario.

Dicas que achei importantes ao assisitr o canal no Baboo no [Youtube](https://www.youtube.com/channel/UC5KejskaVsfsIyXqbgqPRvg).

### 1. Corrigindo arquivos do Windows
`chkdsk /r` e reinicia o windows
`sfc /scannow`*

*às vezes pode ocorrer de não conseguir acesso ao arquivo a ser recuperado e ser necessário ser feito com o windows offline, se for o caso vá em: 
`Configurações > Atualizações e segurança > Recuperação > Reiniciar agora`
Na tela de `inicialização > Solução de problemas > Opções avançadas > Prompt de comando`, informe seu login e senha.
Digite `BCDEDIT` para descobrir a partição do windows (normalmente vem precedido de `\Windows`), logo digite o comando:
`sfc /scannow /offbootdir=X:\ /offwindir=X:\windows`

Para finalizar, use o comando `dism /online /cleanup-image /restorehealth`<br>
*o comando dism não funciona no windows 7

### 2. Bloqueador de propagandas e sites maliciosos
- usar a extensão do Kaspersky protection, para evitar abertura de URLs maliciosas (lembrar de permitir que ele funcione em modo anônimo):
    - [versão para chrome](https://chrome.google.com/webstore/detail/kaspersky-protection/ahkjpbeeocnddjkakilopmfdlnjdpcdm)
    - [versão para firefox](https://addons.mozilla.org/pt-BR/firefox/addon/kaspersky-protection-2020/)
- para verificação de paginas seguras, use o TrafficLight da BitDefender:
    - [versão para chrome](https://chrome.google.com/webstore/detail/trafficlight/cfnpidifppmenkapgihekkeednfoenal?hl=pt)
    - [versão para firefox](https://addons.mozilla.org/pt-BR/firefox/addon/trafficlight/)
- há ainda mais dois bloqueadores que são bons, sendo eles:
    - [Windows Defender Browser Protection](https://chrome.google.com/webstore/detail/microsoft-defender-browse/bkbeeeffjjeopflfhgeknacdieedcoml)
    - [Malwarebytes Browser Guard](https://chrome.google.com/webstore/detail/malwarebytes-browser-guar/ihcjicgdanjaechkgeegckofjjedodee)

### 3. Desfragmentação de arquivos
- sugerido baixar o [Defraggler](https://www.ccleaner.com/defraggler/download) da Piriform e usar as seguintes opções depois de instalar:
    - `Settings > Drive map > Custom view` e escolha 12 por 12 em `Minumun block width` e `Minimum block height`. Em `Style` escolha `plan` e `Mode` deixe `bars`.
    - `Settings > Defrag`, marque `Move large files to end of drive during whole drive defrag`. Em `minimum  file size` coloque o valor `1000`, clique em Ok.
    - `Settings > Boot Time Defrag > Run once` para executar uma desfragmentação antes de iniciar o Windows (ideal para não mexer em arquivos de antivírus, por exemplo)

### 4. Windows Update
- No Windows 10, ir em `Configurações > Atualização e Segurança > Solução de Problemas > Windows Update > Executar a solução de problemas`
- Para Windows 7 e 8, use [https://support.microsoft.com/pt-br/sbs/windows/corrigir-erros-do-windows-update-18b693b5-7818-5825-8a7e-2a4a37d6d787](https://support.microsoft.com/pt-br/sbs/windows/corrigir-erros-do-windows-update-18b693b5-7818-5825-8a7e-2a4a37d6d787) ou [baixe](https://windowsrapidoeseguro.com.br/16/)