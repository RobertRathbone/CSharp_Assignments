import {useState} from 'react';

const New = ({createBox}) => {

    const [newColor, setNewColor] = useState("");
    const [error, setError] = useState("");

    const submitColor = (event) => {
        event.preventDefault();

        // validations
        let errs = false;

        if(newColor.length < 3){
            setError("Must add a longer color!");
            errs = true;
        }

        // success!
        if(!errs){
            setError(null);
            console.log(newColor);
            createBox({color: newColor});
        }
    }

    return (
        <>
            What the user is typing: {newColor}
            <br/>
            {error}
            <br/>
            <form onSubmit={submitColor}>
                <br/>
                <input type="color" value={newColor} onChange={e => setNewColor(e.target.value)}/>
                <br/>
                <input type="submit" value="Create a color!!"/>
                <br/>
            </form>
        </>
    )
}
export default New;