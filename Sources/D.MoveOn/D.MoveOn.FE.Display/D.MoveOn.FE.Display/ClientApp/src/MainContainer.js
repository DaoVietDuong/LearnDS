import React from 'react';
import './MainContainer.css';
import ProductGroupComponent from './components/Product.Group.Component';
import ProductDetailComponent from './components/Product.Detail.Component';
import { BrowserRouter as Router, Route, Switch} from 'react-router-dom';

function MainContainer() {
    return (

        <div className="x-main-container">
            <Router>
                <Switch>
                    <Route path='/product/:id' component={ProductDetailComponent} />
                    <Route path='' component={ProductGroupComponent} />
                </Switch>
            </Router>
        </div>

    )
}

export default MainContainer;
