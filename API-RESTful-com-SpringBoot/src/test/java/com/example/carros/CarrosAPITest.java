package com.example.carros;


import com.example.carros.domain.Carro;
import com.example.carros.domain.dto.CarroDTO;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.HttpMethod;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.List;

@RunWith(SpringRunner.class)
@SpringBootTest(classes = CarrosApplication.class, webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class CarrosAPITest {

    @Autowired
    protected TestRestTemplate rest;

    private ResponseEntity<CarroDTO> getCarro(String url) {
        return rest.getForEntity(url, CarroDTO.class);
    }

    private ResponseEntity<List<CarroDTO>> getCarros(String url) {
        return rest.exchange(
                url,
                HttpMethod.GET,
                null,
                new ParameterizedTypeReference<List<CarroDTO>>() {
                });
    }

    @Test
    public void testSave() {
        Carro carro = new Carro();
        carro.setNome("Porshe");
        carro.setTipo("esportivos");

        //insert
        ResponseEntity response = rest.postForEntity("/api/v1/carros", carro, null);
        System.out.println(response);

        //verifica se criou
        Assert.assertEquals(HttpStatus.CREATED, response.getStatusCode());

        //buscar o obj
        String location = response.getHeaders().get("location").get(0);
        CarroDTO c = getCarro(location).getBody();

        Assert.assertNotNull(c);
        Assert.assertEquals("Porshe", c.getNome());
        Assert.assertEquals("esportivos", c.getTipo());

        //deletar o obj
        rest.delete(location);

        //verifica se deletou
        Assert.assertEquals(HttpStatus.NOT_FOUND, getCarro(location).getStatusCode());
    }

    @Test
    public void testaLista() {
        List<CarroDTO> carros = getCarros("/api/v1/carros").getBody();
        Assert.assertNotNull(carros);
        Assert.assertEquals(30, carros.size());
    }

    @Test
    public void testaListaPorTipo() {
        Assert.assertEquals(10, getCarros("/api/v1/carros/tipo/classicos").getBody().size());
        Assert.assertEquals(10, getCarros("/api/v1/carros/tipo/esportivos").getBody().size());
        Assert.assertEquals(10, getCarros("/api/v1/carros/tipo/luxo").getBody().size());

        Assert.assertEquals(HttpStatus.NO_CONTENT, getCarros("/api/v1/carros/tipo/xxx").getStatusCode());
    }

    @Test
    public void getTestOk() {
        ResponseEntity<CarroDTO> response = getCarro("/api/v1/carros/11");
        Assert.assertEquals(response.getStatusCode(), HttpStatus.OK);

        CarroDTO c = response.getBody();
        Assert.assertEquals("Ferrari FF", c.getNome());
    }

    @Test
    public void testGetNotFound() {
        ResponseEntity<CarroDTO> response = getCarro("/api/v1/carros/1100");
        Assert.assertEquals(response.getStatusCode(), HttpStatus.NOT_FOUND);
    }
}
