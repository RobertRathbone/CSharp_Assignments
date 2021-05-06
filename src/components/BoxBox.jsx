import React, { useState } from  'react';

const BoxBox = (props) => {

    const boxStyle = {
        margin: '20px',
        height: '70px',
        width: '70px',
        backgroundColor: props.colorPicked,
        display: 'inline-block'
    }
    // setAllColors([...allColors,props.colorPicked]);
    return (
        // allColors.map((color,idx)
            <div style = { boxStyle } >
            </div>
    )
    // )
}

export default BoxBox;



