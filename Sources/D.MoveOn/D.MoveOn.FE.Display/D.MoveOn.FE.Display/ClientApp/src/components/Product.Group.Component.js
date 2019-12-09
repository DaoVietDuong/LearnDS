import React from 'react';
import ProductComponent from './Product.Component';

class ProductGroupComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
          error: null,
          isLoaded: false,
          items: []
        };
      }

    onClick = (item) => {
        const path = '/product/' + item.id;
        this.props.history.push(path);
    }

    componentDidMount() {
        fetch("http://localhost:4000/users")
          .then(res => res.json())
          .then(
            (result) => {
              this.setState({
                isLoaded: true,
                items: result
              });
            },
            // Note: it's important to handle errors here
            // instead of a catch() block so that we don't swallow
            // exceptions from actual bugs in components.
            (error) => {
              this.setState({
                isLoaded: true,
                error
              });
            }
          )
      }
     
      render() {
        const { error, isLoaded, items } = this.state;

        if (error) {
          return <div>Error: {error.message}</div>;
        } else if (!isLoaded) {
          return <div>Loading...</div>;
        } else {
          return (
            items.map(item => (
                <div key={item.id} className="row">
                 <ProductComponent product={item} selectedProduct={this.onClick}/>
                </div>
              ))
          );
        }
      } 
}

export default ProductGroupComponent;