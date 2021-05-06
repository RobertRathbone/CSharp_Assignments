import {useState} from 'react';

const Box = ({box}) => {

    return (
           <span style={{'backgroundColor': box.color, height: '200px', width: '200px', display: "inline-block"}}>This is the {box.color} box</span>
    )
}

export default Box;