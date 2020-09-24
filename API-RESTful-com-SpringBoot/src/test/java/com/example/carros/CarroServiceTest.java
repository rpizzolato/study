package com.example.carros;

import com.example.carros.api.exception.ObjectNotFoundException;
import com.example.carros.domain.Carro;
import com.example.carros.domain.CarroService;
import com.example.carros.domain.dto.CarroDTO;
import org.junit.Assert;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.List;
import java.util.Optional;

@SpringBootTest
class CarroServiceTest {

	@Autowired
	private CarroService service;

	@Test
	void testeSave() {
		Carro carro = new Carro();
		carro.setNome("Ferrari");
		carro.setTipo("esportivos");

		CarroDTO c = service.insert(carro);

		Assertions.assertNotNull(c);

		Long id = c.getId();
		Assert.assertNotNull(id);

		//buscar o objeto
		c = service.getCarroById(id);
		Assertions.assertNotNull(c);

		Assert.assertEquals("Ferrari", c.getNome());
		Assert.assertEquals("esportivos", c.getTipo());

		//Deletar o objeto
		service.delete(id);

		//Verificar se deletou
		try {
			Assert.assertNull(service.getCarroById(id));
			Assert.fail("O carro não foi excluído");
		} catch (ObjectNotFoundException e) {
			//OK
		}

	}

	@Test
	void testeLista() {
		List<CarroDTO> carros = service.getCarros();
		Assert.assertEquals(30, carros.size());
	}

	@Test
	void testeGet() {
		CarroDTO c = service.getCarroById(11L);
		Assert.assertNotNull(c);

		Assert.assertEquals("Ferrari FF", c.getNome());
	}

	@Test
	void testeGetPorTipo() {

		Assert.assertEquals(10, service.getCarroByTipo("esportivos").size());
		Assert.assertEquals(10, service.getCarroByTipo("classicos").size());
		Assert.assertEquals(10, service.getCarroByTipo("luxo").size());

		Assert.assertEquals(0, service.getCarroByTipo("x").size());
	}

}
