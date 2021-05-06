
import {useState} from 'react';
import Box from './Box';

const Boxes = ({boxes}) => {

    return (
       boxes.map((box, idx) =>
            <Box key={idx} box={box} />
       )
    )
}

export default Boxes;