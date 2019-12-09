import React from 'react';
import './Product.Component.css';

const ProductComponent = (props) => {
    const { selectedProduct, product } = props;

    return (
        <div className="x-product-summary col-sm col-md col-lg card" onClick={() => selectedProduct(product)}>
        <div className="card-body">
            <h5 className="card-title">{product.firstName} {product.lastName}</h5>
            <h6 className="card-subtitle mb-2 text-muted">{product.email}</h6>
            <p className="card-text">{product.id}</p>
        </div>
    </div>
    );
}

export default ProductComponent;