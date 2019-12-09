import React from 'react';
import Button from 'react-bootstrap/Button';

class ProductDetailComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            item: null
        };
    }

    goBack () {
        this.props.history.push('/');
    }

    getProductById(productId) {
        fetch("http://localhost:4000/users/" + productId)
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        item: result
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

    componentDidMount() {
        const productId = this.props.match.params.id;

        this.getProductById(productId);
    }

    render() {
        const { error, isLoaded, item } = this.state;

        const actionSection = <div><Button onClick={() => this.goBack()}>Go Back</Button></div>;
        if (error) {
            return <div>
                <div>Error!</div>
                {actionSection}
            </div>
        }
        if (!isLoaded) {
            return <div>
                <div>Loading...</div>
                {actionSection}
            </div>
        } else {
            return (<div>
                <div className="x-product-detail">
                    Name: {item.lastName}
                </div>
                {actionSection}
            </div>)
        }

    }
}

export default ProductDetailComponent;