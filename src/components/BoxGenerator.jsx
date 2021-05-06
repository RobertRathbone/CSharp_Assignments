import { ifError } from 'assert';
import React, { useState } from  'react';



const BoxGenerator = (props) => {
    const [boxColor, setBoxColor] = useState("");
    // const [count =0, setCount] = useState("");




    let handleSubmit = (e) => {
        e.preventDefault();
        // setCount(count + 1);
        // console.log(count);
        props.onNewBox( boxColor );
    };

    return (
    <>
        <form onSubmit={ handleSubmit }>
            <h3>You, you like colored boxes?</h3>
            <h5>Pick a color, any color! (red)</h5>
            <textarea 
                rows="1"
                cols="10"
                placeholder="red"
                onChange={ (e) => setBoxColor(e.target.value) }
                value={ boxColor }
            ></textarea>
            <input type="submit" value="Pick color" />
        </form>
    </>

    );
}

export default BoxGenerator;