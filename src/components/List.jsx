import React, { Component } from "react";
import Name from './Name';

class List extends Component {
    constructor(props){
        super(props);

        this.state = {
            count: 0
        }
    }

    countHandler = () => {
        this.setState({
            count: this.state.count +1
        })
    }

    render() {
        return (
            <>
                <p> This button has been clicked { this.state.count } times.</p>
                <p>
                    <button onClick = { this.countHandler } >Click me! </button>
                </p>
                <Name name = {"Yugo"} handler = { this.countHandler }/>
                <Name name = {"Yugi"} handler = { this.countHandler }/>
                <Name name = {"Yogo"} handler = { this.countHandler }/>
            </>
        )
    }
}
export default List;