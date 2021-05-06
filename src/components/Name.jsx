import React, { Component } from 'react';

class Name extends Component {
    constructor(props){
        super(props);
    }
    render(){
        return (
            <>
            <h3>I am the {this.props.name} component!</h3>
            <p>
                <button onClick = { this.props.handler } >I am another on click! </button>
            </p>
            </>
        )
    }


}

export default Name;