import React, { Component } from 'react';
import { View, Text } from 'react-native';
import api from '../services/api';

export default class Main extends Component {

  //brings a title for header
  static navigationOptions = {
    title: 'JSHunt'
  }

  state = {
    counter: 0
  };

  componentDidMount() {
    this.loadProducts();
  }

  loadProducts = async () => {
    const response = await api.get('/products');

    const { docs } = response.data;

    console.log(docs);

    this.setState({
      counter: response.total
    });
  };

  render() {
    return(
      <View>
        <Text>Total de Páginas: {this.state.counter}</Text>
      </View>
    );
  }
}