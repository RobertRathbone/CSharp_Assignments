import './App.css';
import React, { useState } from 'react';
// import List from './components/List';
// import Person from './components/Person';
// import Name from './components/Name';
// import Inversion from "./components/Inversion"
// import UserForm from './components/UserForm';
// import DisplayForm from './components/DisplayForm';
// import NewUser from './components/NewUser'
// import BoxGenerator from './components/BoxGenerator';
// import BoxBox from './components/BoxBox'
// import MessageForm from './components/MessageForm';
// import MessageDisplay from './components/MessageDisplay'
// import Boxes from './components/Boxes';
// import New from './components/New'
import TabDisplay from './components/TabDisplay'


function App() {
  // const [currentMsg, setCurrentMsg] = useState("There are no messages");
  // const youveGotMail = ( newMessage ) => {
  //   setCurrentMsg( newMessage );
  // const [currentColor, setCurrentColor]= useState ([
  //   {color: "red"},
  //   {color: "blue"}
  // ]);
  // const colorChoice = (newColor) => {
  //   setCurrentColor( [...currentColor,newColor ])
  // }
  const [tabs, setTabs] = useState([
    {tab1: "red"},
    {tab2: "blue"},
    {tab3: [45, 67, 96, "iems in a cribbage game"]}
  ]);

  // const createBox = (box) => {
  //   setBoxes([...boxes,box])
  // };

  

  return (
    <div className="App">
        <>
          {/* <BoxGenerator onNewBox={colorChoice}/>
          {currentColor.map((each,i) => {
            return(
              <BoxBox colorPicked ={each} key = {i} />    
            )
          })} */}
          {/* <New createBox = {createBox} />
          <Boxes boxes ={boxes} /> */}

          <TabDisplay tabs = {tabs} />
          
          {/* <MessageForm onNewMessage={ youveGotMail } />
          <MessageDisplay message={ currentMsg } /> */}
        </>
      {/* < UserForm /> */}
      {/* < NewUser /> */}
      {/* <BoxGenerator /> */}
    </div>
  );
}

export default App;
