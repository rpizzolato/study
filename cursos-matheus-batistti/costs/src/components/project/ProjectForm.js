import Input from '../form/Input'
import styles from './ProjectForm.module.css'

function ProjectForm() {
  return (
    <form className={styles.form}>
      <div>
        <Input />
      </div>
      <div>
        <Input />
      </div>
      <div>
        <select name="category_id">
          <option disabled selected>Selecione a categoria</option>
        </select>
      </div>
      <div>
        <input type="submit" value="Criar projeto" />
      </div>  
    </form>
  )
}

export default ProjectForm
