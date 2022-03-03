import { Button } from "./evento/Button"

function Evento({ numero }) {

  function meuEvento() {
    console.log(`Primeiro evento`)
  }
  
  function segundoEvento() {
    console.log(`Segundo evento`)
  }

  return (
    <>
      <p>Clique para disparar um evento:</p>
      <button onClick={meuEvento}>Ativar!</button>
      <Button text="Primeiro evento" event={meuEvento} />
      <Button text="Segundo evento" event={segundoEvento} />
    </>
  )
}

export default Evento
