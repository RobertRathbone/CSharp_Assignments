import react, { useState } from 'react';
    
    
const TabDisplay = ({tabs}) => {
    const [arrayValue, setArrayValue] = useState("");
    // const arrayDisplay = () => tabs.filter( item => item.includes({value}))

    console.log(tabs)
    return (
        <>
            {tabs.map((each,value) => 
            <>
                <button >Tab: {value}</button>
                <p> 
                    
                </p>
            
            </>)}

        </>
    );
};
    
export default TabDisplay;