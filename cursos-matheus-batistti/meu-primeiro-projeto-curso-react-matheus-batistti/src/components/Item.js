import PropTypes from 'prop-types'

export function Item({marca, ano_lancamento}) {
  return (
    <>
      <li>{marca} - {ano_lancamento}</li>
    </>
  )
}

// validação dos tipos
Item.propTypes = {
  marca: PropTypes.string.isRequired,
  ano_lancamento: PropTypes.number,
}

// colocando um valor padrão para as propriedades, caso não seja definido
Item.defaultProps = {
  marca: 'Faltou marca',
  ano_lancamento: 0,
}
