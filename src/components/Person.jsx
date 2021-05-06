import React, { Component } from 'react';
// import { ProvidePlugin } from 'webpack';

class Person extends Component {
    constructor(props){
        super(props);
        this.state = {
            age: props.age
        }

    }
    counter = () => {
        this.setState({
            age: this.state.age + 1
        })
    }
    render(){
        return(
            <div>
                <h3>{ this.props.lastName }, { this.props.firstName }</h3>
                <p> Age: { this.state.age } </p>
                <p> Hair Color: { this.props.hairColor }</p>
                <p>
                    <button onClick = { this.counter } >Happy Birthday! </button>
                </p>
            </div>
        )
    }
}
export default Person;