# Projeto Outdoor Adventure
## Projeto HTML e CSS para reforçar a compreensão

- criar uma estrutura `HTML` e `CSS`, com uma pasta `assets` contando o `styles.css` e as imagens usadas.

### Criando variáveis no CSS
- para criar variáveis no CSS, usamos o seletor `:root`:
```css
:root {
    --color-primary: #fb2056;
    --color-primary-hover: #da1c4b;
    --color-light-gray: #f5f5f5;
    --color-light-black: #191919;
    --color-white: #ffffff;

    --nome-da-variavel: 'valor';
}
```
>**Observação**
>
>Os nomes das variáveis começam com `--`. Uma boa **vantagem** de usar esse modelo é que quando precisamos mudar as cores, por exemplo, mudaremos apenas em um lugar, e afetará todo o resto.