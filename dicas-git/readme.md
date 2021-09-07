# Git
Repositório para guardar dicas e comandos git/github para uso e consulta.
#### Sumário
1. [Git e Github para iniciantes](#Git-e-Github-para-iniciantes)
2. [Git na prática](#Git-na-prática)

# Git e Github para iniciantes
## Anotações referente ao curso [Git e Github para iniciantes](https://www.udemy.com/course/git-e-github-para-iniciantes/).

- Controle de versão é um sistema com a finalidade de gerenciar diferentes versões de um arquivo/documento.

- GIT trabalha em controle de estado (snapshot), enquanto alguns concorrentes são baseados em diferenças nos arquivos.

### Comandos básicos inicial
- **configurar usuário e email**
    - `git config --global  user.name "Name"`
    - `git config --global  user.email email@email.com`

- **configurar editor preferido**
    - `git config --global core.editor emacs/vi/vim/etc`

- **confirmar usuário e email**
    - `git config user.name`
    - `git config user.email`

- **listar todas as configurações**
    - `git config --list`

### Iniciando um repositório
- **Crie uma pasta/diretório e navegue até ele. Use o comando a seguir para criar um repositório Git**
    - `git init`: um diretório `.git` será criado com os arquivos necessário para o Git funcionar.

# Git na prática
## Anotações referente ao curso [Git na prática](https://www.youtube.com/channel/UCXepHP9GmUtF73xtEIa9RWA) do canal Fábrica de Código

- Após instalar o Git, é preciso gerar as chaves para acesso ao Github.
https://docs.github.com/pt/github/authenticating-to-github/connecting-to-github-with-ssh/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent
Após, cole a chave pública lá no `Settings > SSD and GPG keys` e identificar o equipamento.

### Ignorar arquivos
- usar o arquivo `.gitignore`,  em .NET por exemplo, usar [https://github.com/github/gitignore/blob/master/VisualStudio.gitignore](https://github.com/github/gitignore/blob/master/VisualStudio.gitignore)
- repositório com vários exemplos de `.gitignore` [aqui](https://github.com/github/gitignore)

### Branches

- Para criar uma nova branch, use `git checkout -b nome-da-branch`
- para atualizar no servidor essa nova branch, use `git push -u origin nome-da-branch`
- `git reset` retira os arquivos do staged (pode especificar um arquivo)
- `git pull`: pega tudo de novo e faz o merge no branch local
- `git fetch`: pega tudo de novo, mas não faz o merge no seu branch local
- `git merge --no-ff nome-da-branch`: faz o merge das branches (fazer o checkout no branch que você quer enviar as modificações antes - fazer um `git pull` para pegar as últimas atualiazções da equipe)
- `git cherry-pick --no-ff numero-do-commit`: faz checkout no branch que você quer enviar as modificações
- `git status`: me mostra informações de forma geral

### Comandos relevantes
- depois de fazer o merge das branches no master (ou main), você pode excluir esses branches, com o comando `git branch -d nome-da-branch` (exclui localmente). 
- para excluir **remotamente**, use `git push origin -d nome-do-branch`.
- desfazer um commit ainda não enviado para o repositório, ou seja, reverter mantendo as modificações localmente, use `git reset --soft HEAD~1`. (**tomar cuidado, caso queira manter modificações, use o --soft, caso não queira, use o --hard**)
- para reverter **removendo** as modificações, use `git reset --hard HEAD~1`
- para desfazer um commit no repositório remoto, use `git revert numero-do-commit` (**o git não deixa desfazer commit de merge, precisa usar o commit que foi feito a alteração em si**). Algumas vezes vai ser preciso usar os comandos de salvar do vi (`ESC + wq`). Por fim use o `git push` para enviar as modificações para o  repositório.

### Git-flow

- é um modelo de criação de branches, criado pelo [Vicent Driessen](https://nvie.com/posts/a-successful-git-branching-model/), utilizado quando se trabalha com uma equipe muito grande de programadores.
- há duas branches principais, a **master (ou main)** e a **develop (ou staging)**. A **master** será a principal, onde irá somente todo código/modificações que se tenha certeza que está funcionando devidamente, que já foi testada pelo programador. Nela também que ocorre o **CI/CD** caso a empresa opte por isso. Na **develop**, é onde é feito o commit das modificações, também que já tenham sido testadas pelo programador, praticamente prontas para ir para a branch de produção, a **master** no caso.
- quando for criar uma nova funcionalidade (feature), tente usar o padrão `feature/nome-da-feature`, o git irá aceitar `/` normalmente. Terminando a nova funcionalidade, faça um merge no branch de develop para homologar essa nova funcionalidade.
- após a nova funcionalidade estar homologada e pronta, basta fazer o merge com a branch release, e enumerar, usando o padrão, por exemplo, `release-01`, `release-02` e etc (ou `release/1.0.0` e assim por diante).
